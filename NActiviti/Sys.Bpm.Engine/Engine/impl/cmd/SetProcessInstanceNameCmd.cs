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

    using Sys.Workflow.Engine.Impl.Interceptor;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;
    using Sys.Workflow.Engine.Runtime;
    using System.Collections.Generic;

    [Serializable]
    public class SetProcessInstanceNameCmd : ICommand<object>
    {

        private const long serialVersionUID = 1L;

        protected internal string processInstanceId;
        protected internal string name;

        public SetProcessInstanceNameCmd(string processInstanceId, string name)
        {
            this.processInstanceId = processInstanceId;
            this.name = name;
        }

        public virtual object Execute(ICommandContext commandContext)
        {
            if (processInstanceId is null)
            {
                throw new ActivitiIllegalArgumentException("processInstanceId is null");
            }

            IExecutionEntity execution = commandContext.ExecutionEntityManager.FindById<IExecutionEntity>(processInstanceId);

            if (execution is null)
            {
                throw new ActivitiObjectNotFoundException("process instance " + processInstanceId + " doesn't exist", typeof(IProcessInstance));
            }

            if (!execution.ProcessInstanceType)
            {
                throw new ActivitiObjectNotFoundException("process instance " + processInstanceId + " doesn't exist, the given ID references an execution, though", typeof(IProcessInstance));
            }

            if (execution.Suspended)
            {
                throw new ActivitiException("process instance " + processInstanceId + " is suspended, cannot set name");
            }

            // Actually set the name
            execution.Name = name;

            // Record the change in history
            commandContext.HistoryManager.RecordProcessInstanceNameChange(processInstanceId, name);

            return null;
        }

    }

}