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

namespace Sys.Workflow.Engine.Impl.Cmd
{

    using Sys.Workflow.Engine.Impl.Interceptor;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;

    /// 
    [Serializable]
    public class GetTasksLocalVariablesCmd : ICommand<IList<IVariableInstance>>
    {


        private const long serialVersionUID = 1L;
        protected internal string[] taskIds;

        public GetTasksLocalVariablesCmd(string[] taskIds)
        {
            this.taskIds = taskIds;
        }

        public virtual IList<IVariableInstance> Execute(ICommandContext commandContext)
        {
            if (taskIds is null)
            {
                throw new ActivitiIllegalArgumentException("taskIds is null");
            }
            if ((taskIds?.Length).GetValueOrDefault(0) == 0)
            {
                throw new ActivitiIllegalArgumentException("Set of taskIds is empty");
            }

            IList<IVariableInstance> instances = new List<IVariableInstance>();
            IList<IVariableInstanceEntity> entities = commandContext.VariableInstanceEntityManager.FindVariableInstancesByTaskIds(taskIds);
            foreach (IVariableInstanceEntity entity in entities)
            {
                instances.Add(entity);
            }

            return instances;
        }

    }
}