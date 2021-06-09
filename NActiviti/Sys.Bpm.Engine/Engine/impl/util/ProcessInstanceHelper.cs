﻿using System.Collections.Generic;

/* Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace Sys.Workflow.Engine.Impl.Util
{
    using Spring.Expressions;
    using Spring.Objects.Factory.Config;
    using Sys.Workflow.Bpmn.Models;
    using Sys.Workflow.Engine;
    using Sys.Workflow.Engine.Delegate.Events;
    using Sys.Workflow.Engine.Delegate.Events.Impl;
    using Sys.Workflow.Engine.Impl.Contexts;
    using Sys.Workflow.Engine.Impl.Identities;
    using Sys.Workflow.Engine.Impl.Interceptor;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;
    using Sys.Workflow.Engine.Repository;
    using Sys.Workflow.Engine.Runtime;
    using Sys.Workflow.Services.Api.Commands;
    using System;
    using System.Linq;
    using System.Security.Principal;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 
    /// </summary>
    public class ProcessInstanceHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual IProcessInstance CreateProcessInstance(IProcessDefinitionEntity processDefinition, string businessKey, string processInstanceName, IDictionary<string, object> variables, IDictionary<string, object> transientVariables)
        {

            return CreateAndStartProcessInstance(processDefinition, businessKey, processInstanceName, variables, transientVariables, false);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual IProcessInstance CreateAndStartProcessInstance(IProcessDefinition processDefinition, string businessKey, string processInstanceName, IDictionary<string, object> variables, IDictionary<string, object> transientVariables, string initialFlowElementId = null)
        {

            return CreateAndStartProcessInstance(processDefinition, businessKey, processInstanceName, variables, transientVariables, true, initialFlowElementId);
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual IProcessInstance CreateAndStartProcessInstance(IProcessDefinition processDefinition, string businessKey, string processInstanceName, IDictionary<string, object> variables, IDictionary<string, object> transientVariables, bool startProcessInstance, string initialFlowElementId = null)
        {
            // Todo: ideally, context should be passed here
            _ = Context.CommandContext;

            // Do not start process a process instance if the process definition is suspended
            if (ProcessDefinitionUtil.IsProcessDefinitionSuspended(processDefinition.Id))
            {
                throw new ActivitiException("Cannot start process instance. Process definition " + processDefinition.Name + " (id = " + processDefinition.Id + ") is suspended");
            }

            // Get model from cache
            Process process = ProcessDefinitionUtil.GetProcess(processDefinition.Id);
            if (process is null)
            {
                throw new ActivitiException("Cannot start process instance. Process model " + processDefinition.Name + " (id = " + processDefinition.Id + ") could not be found");
            }

            FlowElement initialFlowElement = string.IsNullOrWhiteSpace(initialFlowElementId) ? process.InitialFlowElement : process.FindFlowElement(initialFlowElementId);
            if (initialFlowElement is null)
            {
                throw new ActivitiException("No start element found for process definition " + processDefinition.Id);
            }

            return CreateAndStartProcessInstanceWithInitialFlowElement(processDefinition, businessKey, processInstanceName, initialFlowElement, process, variables, transientVariables, startProcessInstance);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual IProcessInstance CreateAndStartProcessInstanceByMessage(IProcessDefinition processDefinition, string messageName, IDictionary<string, object> variables, IDictionary<string, object> transientVariables)
        {
            _ = Context.CommandContext;
            // Do not start process a process instance if the process definition is suspended
            if (ProcessDefinitionUtil.IsProcessDefinitionSuspended(processDefinition.Id))
            {
                throw new ActivitiException("Cannot start process instance. Process definition " + processDefinition.Name + " (id = " + processDefinition.Id + ") is suspended");
            }

            // Get model from cache
            Process process = ProcessDefinitionUtil.GetProcess(processDefinition.Id);
            if (process is null)
            {
                throw new ActivitiException("Cannot start process instance. Process model " + processDefinition.Name + " (id = " + processDefinition.Id + ") could not be found");
            }

            FlowElement initialFlowElement = null;
            foreach (FlowElement flowElement in process.FlowElements)
            {
                if (flowElement is StartEvent startEvent)
                {
                    if (CollectionUtil.IsNotEmpty(startEvent.EventDefinitions) && startEvent.EventDefinitions[0] is MessageEventDefinition definition)
                    {
                        MessageEventDefinition messageEventDefinition = definition;
                        if (messageEventDefinition.MessageRef.Equals(messageName))
                        {
                            initialFlowElement = flowElement;
                            break;
                        }
                    }
                }
            }
            if (initialFlowElement is null)
            {
                throw new ActivitiException("No message start event found for process definition " + processDefinition.Id + " and message name " + messageName);
            }

            return CreateAndStartProcessInstanceWithInitialFlowElement(processDefinition, null, null, initialFlowElement, process, variables, transientVariables, true);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual IProcessInstance CreateAndStartProcessInstanceWithInitialFlowElement(IProcessDefinition processDefinition, string businessKey, string processInstanceName, FlowElement initialFlowElement, Process process, IDictionary<string, object> variables, IDictionary<string, object> transientVariables, bool startProcessInstance)
        {

            ICommandContext commandContext = Context.CommandContext;

            // Create the process instance
            string initiatorVariableName = null;
            if (initialFlowElement is StartEvent @event)
            {
                initiatorVariableName = @event.Initiator;
            }

            IExecutionEntity processInstance = commandContext.ExecutionEntityManager.CreateProcessInstanceExecution(processDefinition, businessKey, processDefinition.TenantId, initiatorVariableName);

            commandContext.HistoryManager.RecordProcessInstanceStart(processInstance, initialFlowElement);

            processInstance.Variables = ProcessDataObjects(process.DataObjects);

            if (string.IsNullOrWhiteSpace(businessKey) == false)
            {
                processInstance.SetVariable(WorkflowVariable.GLOBAL_PROCESSINSTANCE_BUSINESSKEY_VARNAME, businessKey);
            }

            if (!string.IsNullOrWhiteSpace(Authentication.AuthenticatedUser?.Id))
            {
                processInstance.SetVariable(WorkflowVariable.PROCESS_START_USERID, Authentication.AuthenticatedUser.Id);
            }

            var title = process.GetExtensionElementAttributeValue(WorkflowVariable.GLOBAL_PROCESSINSTANCE_TITLE);
            if (string.IsNullOrWhiteSpace(title) == false)
            {
                var reg = new Regex(@"\${(.*?)}", RegexOptions.Multiline);
                title = reg.Replace(title, (m) =>
                {
                    try
                    {
                        return ExpressionEvaluator.GetValue(variables, m.Groups[1].Value)?.ToString();
                    }
                    catch (Exception ex)
                    {
                        return $"流程标题计算错误:{process.Name}--{ex.Message}";
                    }
                });
                processInstance.SetVariable(WorkflowVariable.GLOBAL_PROCESSINSTANCE_TITLE, title);
            }
            else
            {
                processInstance.SetVariable(WorkflowVariable.GLOBAL_PROCESSINSTANCE_TITLE, process.Name);
            }

            // Set the variables passed into the start command
            if (variables is object)
            {
                foreach (string varName in variables.Keys)
                {
                    processInstance.SetVariable(varName, variables[varName]);
                }
            }

            if (transientVariables is object)
            {
                foreach (string varName in transientVariables.Keys)
                {
                    processInstance.SetTransientVariable(varName, transientVariables[varName]);
                }
            }

            // Set processInstance name
            if (processInstanceName is object)
            {
                processInstance.Name = processInstanceName;
            }
            else if (title is object)
            {
                processInstanceName = title;
                processInstance.Name = title;
            }
            else
            {
                processInstanceName = process.Name;
                processInstance.Name = process.Name;
            }
            commandContext.HistoryManager.RecordProcessInstanceNameChange(processInstance.Id, processInstanceName);

            // Fire events
            if (Context.ProcessEngineConfiguration.EventDispatcher.Enabled)
            {
                Context.ProcessEngineConfiguration.EventDispatcher.DispatchEvent(ActivitiEventBuilder.CreateEntityWithVariablesEvent(ActivitiEventType.ENTITY_INITIALIZED, processInstance, variables, false));
            }

            // Create the first execution that will visit all the process definition elements
            IExecutionEntity execution = commandContext.ExecutionEntityManager.CreateChildExecution(processInstance);
            execution.CurrentFlowElement = initialFlowElement;

            if (startProcessInstance)
            {
                this.StartProcessInstance(processInstance, commandContext, variables);
            }

            return processInstance;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void StartProcessInstance(IExecutionEntity processInstance, ICommandContext commandContext, IDictionary<string, object> variables)
        {

            Process process = ProcessDefinitionUtil.GetProcess(processInstance.ProcessDefinitionId);


            // Event sub process handling
            IList<IMessageEventSubscriptionEntity> messageEventSubscriptions = new List<IMessageEventSubscriptionEntity>();
            foreach (FlowElement flowElement in process.FlowElements)
            {
                if (flowElement is EventSubProcess eventSubProcess)
                {
                    foreach (FlowElement subElement in eventSubProcess.FlowElements)
                    {
                        if (subElement is StartEvent startEvent)
                        {
                            if (CollectionUtil.IsNotEmpty(startEvent.EventDefinitions))
                            {
                                EventDefinition eventDefinition = startEvent.EventDefinitions[0];
                                if (eventDefinition is MessageEventDefinition messageEventDefinition)
                                {
                                    BpmnModel bpmnModel = ProcessDefinitionUtil.GetBpmnModel(processInstance.ProcessDefinitionId);
                                    if (bpmnModel.ContainsMessageId(messageEventDefinition.MessageRef))
                                    {
                                        messageEventDefinition.MessageRef = bpmnModel.GetMessage(messageEventDefinition.MessageRef).Name;
                                    }
                                    IExecutionEntity messageExecution = commandContext.ExecutionEntityManager.CreateChildExecution(processInstance);
                                    messageExecution.CurrentFlowElement = startEvent;
                                    messageExecution.IsEventScope = true;
                                    messageEventSubscriptions.Add(commandContext.EventSubscriptionEntityManager.InsertMessageEvent(messageEventDefinition.MessageRef, messageExecution));
                                }
                            }
                        }
                    }
                }
            }

            IExecutionEntity execution = processInstance.Executions[0]; // There will always be one child execution created
            commandContext.Agenda.PlanContinueProcessOperation(execution);

            if (Context.ProcessEngineConfiguration.EventDispatcher.Enabled)
            {
                IActivitiEventDispatcher eventDispatcher = Context.ProcessEngineConfiguration.EventDispatcher;
                eventDispatcher.DispatchEvent(ActivitiEventBuilder.CreateProcessStartedEvent(execution, variables, false));

                foreach (IMessageEventSubscriptionEntity messageEventSubscription in messageEventSubscriptions)
                {
                    commandContext.ProcessEngineConfiguration.EventDispatcher.DispatchEvent(ActivitiEventBuilder.CreateMessageEvent(ActivitiEventType.ACTIVITY_MESSAGE_WAITING, messageEventSubscription.ActivityId, messageEventSubscription.EventName, null, messageEventSubscription.Execution.Id, messageEventSubscription.ProcessInstanceId, messageEventSubscription.ProcessDefinitionId));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual IDictionary<string, object> ProcessDataObjects(ICollection<ValuedDataObject> dataObjects)
        {
            IDictionary<string, object> variablesMap = new Dictionary<string, object>();
            // convert data objects to process variables
            if (dataObjects is object)
            {
                foreach (ValuedDataObject dataObject in dataObjects)
                {
                    variablesMap[dataObject.Name] = dataObject.Value;
                }
            }
            return variablesMap;
        }
    }

}