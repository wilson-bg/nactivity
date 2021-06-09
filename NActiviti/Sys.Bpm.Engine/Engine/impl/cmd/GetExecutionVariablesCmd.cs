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
    using Sys.Workflow.Engine.Runtime;
    using System.Linq;

    /// 
    /// 
    [Serializable]
    public class GetExecutionVariablesCmd : ICommand<IDictionary<string, object>>
    {

        private const long serialVersionUID = 1L;
        protected internal string executionId;
        protected internal IEnumerable<string> variableNames;
        protected internal bool isLocal;

        public GetExecutionVariablesCmd(string executionId, IEnumerable<string> variableNames, bool isLocal)
        {
            this.executionId = executionId;
            this.variableNames = variableNames;
            this.isLocal = isLocal;
        }

        public virtual IDictionary<string, object> Execute(ICommandContext commandContext)
        {

            // Verify existance of execution
            if (executionId is null)
            {
                throw new ActivitiIllegalArgumentException("executionId is null");
            }

            IExecutionEntity execution = commandContext.ExecutionEntityManager.FindById<IExecutionEntity>(executionId);

            if (execution is null)
            {
                throw new ActivitiObjectNotFoundException("execution " + executionId + " doesn't exist", typeof(IExecution));
            }

            if ((variableNames?.Count()).GetValueOrDefault(0) == 0)
            {
                // Fetch all
                if (isLocal)
                {
                    return execution.VariablesLocal;
                }
                else
                {
                    return execution.Variables;
                }

            }
            else
            {
                // Fetch specific collection of variables
                if (isLocal)
                {
                    return execution.GetVariablesLocal(variableNames, false);
                }
                else
                {
                    return execution.GetVariables(variableNames, false);
                }

            }

        }
    }

}