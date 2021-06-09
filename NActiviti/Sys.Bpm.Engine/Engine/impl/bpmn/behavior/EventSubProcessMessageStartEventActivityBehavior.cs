﻿using System;
using System.Collections.Generic;

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

namespace Sys.Workflow.Engine.Impl.Bpmn.Behavior
{

    using Sys.Workflow.Bpmn.Models;
    using Sys.Workflow.Engine.Impl.Contexts;
    using Sys.Workflow.Engine.Impl.Interceptor;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;

    /// <summary>
    /// Implementation of the BPMN 2.0 event subprocess message start event.
    /// 
    /// 
    /// </summary>
    [Serializable]
    public class EventSubProcessMessageStartEventActivityBehavior : AbstractBpmnActivityBehavior
    {

        private const long serialVersionUID = 1L;

        protected internal MessageEventDefinition messageEventDefinition;

        public EventSubProcessMessageStartEventActivityBehavior(MessageEventDefinition messageEventDefinition)
        {
            this.messageEventDefinition = messageEventDefinition;
        }

        public override void Execute(IExecutionEntity execution)
        {
            StartEvent startEvent = (StartEvent)execution.CurrentFlowElement;
            EventSubProcess eventSubProcess = (EventSubProcess)startEvent.SubProcess;

            execution.IsScope = true;

            // initialize the template-defined data objects as variables
            IDictionary<string, object> dataObjectVars = ProcessDataObjects(eventSubProcess.DataObjects);
            if (dataObjectVars is object)
            {
                execution.VariablesLocal = dataObjectVars;
            }
        }

        public override void Trigger(IExecutionEntity execution, string triggerName, object triggerData, bool throwError = true)
        {
            ICommandContext commandContext = Context.CommandContext;
            IExecutionEntityManager executionEntityManager = commandContext.ExecutionEntityManager;

            StartEvent startEvent = (StartEvent)execution.CurrentFlowElement;
            if (startEvent.Interrupting)
            {
                IList<IExecutionEntity> childExecutions = executionEntityManager.FindChildExecutionsByParentExecutionId(execution.ParentId);
                foreach (IExecutionEntity childExecution in childExecutions)
                {
                    if (!childExecution.Id.Equals(execution.Id))
                    {
                        executionEntityManager.DeleteExecutionAndRelatedData(childExecution, History.DeleteReasonFields.EVENT_SUBPROCESS_INTERRUPTING + "(" + startEvent.Id + ")", false);
                    }
                }
            }

            IEventSubscriptionEntityManager eventSubscriptionEntityManager = Context.CommandContext.EventSubscriptionEntityManager;
            IList<IEventSubscriptionEntity> eventSubscriptions = execution.EventSubscriptions;
            foreach (IEventSubscriptionEntity eventSubscription in eventSubscriptions)
            {
                if (eventSubscription.EventType == MessageEventSubscriptionEntityFields.EVENT_TYPE && eventSubscription.EventName.Equals(messageEventDefinition.MessageRef))
                {

                    eventSubscriptionEntityManager.Delete(eventSubscription);
                }
            }

            execution.CurrentFlowElement = (SubProcess)execution.CurrentFlowElement.ParentContainer;
            execution.IsScope = true;

            IExecutionEntity outgoingFlowExecution = executionEntityManager.CreateChildExecution(execution);
            outgoingFlowExecution.CurrentFlowElement = startEvent;

            Leave(outgoingFlowExecution);
        }

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