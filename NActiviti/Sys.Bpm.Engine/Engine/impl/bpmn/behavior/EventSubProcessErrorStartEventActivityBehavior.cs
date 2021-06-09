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
    using Sys.Workflow.Engine.Delegate;
    using Sys.Workflow.Engine.Impl.Contexts;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;


    /// <summary>
    /// Implementation of the BPMN 2.0 event subprocess start event.
    /// 
    /// 
    /// </summary>
    [Serializable]
    public class EventSubProcessErrorStartEventActivityBehavior : AbstractBpmnActivityBehavior
    {

        private const long serialVersionUID = 1L;

        public override void Execute(IExecutionEntity execution)
        {
            StartEvent startEvent = (StartEvent)execution.CurrentFlowElement;
            EventSubProcess eventSubProcess = (EventSubProcess)startEvent.SubProcess;
            execution.CurrentFlowElement = eventSubProcess;
            execution.IsScope = true;

            // initialize the template-defined data objects as variables
            IDictionary<string, object> dataObjectVars = ProcessDataObjects(eventSubProcess.DataObjects);
            if (dataObjectVars is object)
            {
                execution.VariablesLocal = dataObjectVars;
            }

            IExecutionEntity startSubProcessExecution = Context.CommandContext.ExecutionEntityManager.CreateChildExecution(execution);
            startSubProcessExecution.CurrentFlowElement = startEvent;
            Context.Agenda.PlanTakeOutgoingSequenceFlowsOperation(startSubProcessExecution, true);
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