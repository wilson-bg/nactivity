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

    /// 
    /// 
    [Serializable]
    public class GetProcessInstanceScopeExecutionCmd : ICommand<IExecutionEntity>
    {
        private const long serialVersionUID = 1L;
        protected internal IExecutionEntity currentlyExaminedExecution;

        public GetProcessInstanceScopeExecutionCmd(IExecutionEntity currentlyExaminedExecution)
        {
            this.currentlyExaminedExecution = currentlyExaminedExecution;
        }

        public virtual IExecutionEntity Execute(ICommandContext commandContext)
        {
            IExecutionEntityManager executionEntityManager = commandContext.ExecutionEntityManager;
            IExecutionEntity scopeExecution = null;
            IExecutionEntity currentExecution = currentlyExaminedExecution;
            while (currentExecution is object && scopeExecution is null)
            {
                if (currentExecution.IsScope)
                {
                    scopeExecution = currentExecution;
                    break;
                }
                else
                {
                    currentExecution = currentExecution.Parent;
                }
            }
            if (scopeExecution is null)
            {
                while (currentlyExaminedExecution is object && scopeExecution is null)
                {
                    if (currentlyExaminedExecution.IsScope)
                    {
                        scopeExecution = currentlyExaminedExecution;
                    }
                    else
                    {
                        currentlyExaminedExecution = executionEntityManager.FindById<IExecutionEntity>(currentlyExaminedExecution.ParentId);
                    }
                }
            }

            return scopeExecution;
        }
    }
}