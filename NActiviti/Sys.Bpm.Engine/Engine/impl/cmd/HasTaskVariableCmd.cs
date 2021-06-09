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
    using Sys.Workflow.Engine.Tasks;
    using System.Collections.Generic;

    /// 
    [Serializable]
    public class HasTaskVariableCmd : ICommand<bool>
    {

        private const long serialVersionUID = 1L;
        protected internal string taskId;
        protected internal string variableName;
        protected internal bool isLocal;

        public HasTaskVariableCmd(string taskId, string variableName, bool isLocal)
        {
            this.taskId = taskId;
            this.variableName = variableName;
            this.isLocal = isLocal;
        }

        public  virtual bool  Execute(ICommandContext  commandContext)
        {
            if (taskId is null)
            {
                throw new ActivitiIllegalArgumentException("taskId is null");
            }
            if (variableName is null)
            {
                throw new ActivitiIllegalArgumentException("variableName is null");
            }

            ITaskEntity task = commandContext.TaskEntityManager.FindById<ITaskEntity>(new KeyValuePair<string, object>("id", taskId));

            if (task is null)
            {
                throw new ActivitiObjectNotFoundException("task " + taskId + " doesn't exist", typeof(ITask));
            }

            bool hasVariable;
            if (isLocal)
            {
                hasVariable = task.HasVariableLocal(variableName);
            }
            else
            {
                hasVariable = task.HasVariable(variableName);
            }

            return hasVariable;
        }
    }

}