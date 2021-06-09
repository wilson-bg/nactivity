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
namespace Sys.Workflow.Engine.Impl.Bpmn.Behavior
{
    using Sys.Workflow.Engine.Delegate;
    using Sys.Workflow.Engine.Impl.Contexts;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;
    using System.Collections.Generic;

    /// <summary>
    /// super class for all gateway activity implementations.
    /// 
    /// 
    /// </summary>
    [Serializable]
    public abstract class GatewayActivityBehavior : FlowNodeActivityBehavior
    {

        private const long serialVersionUID = 1L;

        protected internal virtual void LockFirstParentScope(IExecutionEntity execution)
        {

            IExecutionEntityManager executionEntityManager = Context.CommandContext.ExecutionEntityManager;

            bool found = false;
            IExecutionEntity parentScopeExecution = null;
            while (!found && execution is object && execution.ParentId is object)
            {
                parentScopeExecution = executionEntityManager.FindById<IExecutionEntity>(execution.ParentId);
                if (parentScopeExecution is object && parentScopeExecution.IsScope)
                {
                    found = true;
                }
                execution = parentScopeExecution;
            }

            parentScopeExecution.ForceUpdate();
        }

    }

}