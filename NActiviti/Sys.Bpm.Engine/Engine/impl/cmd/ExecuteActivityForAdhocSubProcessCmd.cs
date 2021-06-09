﻿using System;

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

namespace Sys.Workflow.Engine.Impl.Cmd
{
    using Sys.Workflow.Bpmn.Models;
    using Sys.Workflow.Engine.Impl.Contexts;
    using Sys.Workflow.Engine.Impl.Interceptor;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;
    using Sys.Workflow.Engine.Runtime;

    /// 
    [Serializable]
    public class ExecuteActivityForAdhocSubProcessCmd : ICommand<IExecution>
    {

        private const long serialVersionUID = 1L;
        protected internal string executionId;
        protected internal string activityId;

        public ExecuteActivityForAdhocSubProcessCmd(string executionId, string activityId)
        {
            this.executionId = executionId;
            this.activityId = activityId;
        }

        public virtual IExecution Execute(ICommandContext commandContext)
        {
            IExecutionEntity execution = commandContext.ExecutionEntityManager.FindById<IExecutionEntity>(executionId);
            if (execution is null)
            {
                throw new ActivitiObjectNotFoundException("No execution found for id '" + executionId + "'", typeof(IExecutionEntity));
            }

            if (!(execution.CurrentFlowElement is AdhocSubProcess))
            {
                throw new ActivitiException("The current flow element of the requested execution is not an ad-hoc sub process");
            }

            FlowNode foundNode = null;
            AdhocSubProcess adhocSubProcess = (AdhocSubProcess)execution.CurrentFlowElement;

            // if sequential ordering, only one child execution can be active
            if (adhocSubProcess.HasSequentialOrdering())
            {
                if (execution.Executions.Count > 0)
                {
                    throw new ActivitiException("Sequential ad-hoc sub process already has an active execution");
                }
            }

            foreach (FlowElement flowElement in adhocSubProcess.FlowElements)
            {
                if (activityId.Equals(flowElement.Id) && flowElement is FlowNode)
                {
                    FlowNode flowNode = (FlowNode)flowElement;
                    if (flowNode.IncomingFlows.Count == 0)
                    {
                        foundNode = flowNode;
                    }
                }
            }

            IExecutionEntity activityExecution = Context.CommandContext.ExecutionEntityManager.CreateChildExecution(execution);
            activityExecution.CurrentFlowElement = foundNode ?? throw new ActivitiException("The requested activity with id " + activityId + " can not be enabled");
            Context.Agenda.PlanContinueProcessOperation(activityExecution);

            return activityExecution;
        }

    }

}