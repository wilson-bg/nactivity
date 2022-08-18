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
    using Microsoft.Extensions.Logging;
    using Sys.Workflow.Engine.Impl.Interceptor;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;
    using Sys.Workflow.Engine.Runtime;
    using Sys.Workflow;

    /// 
    /// 
    [Serializable]
    public class UnlockExclusiveJobCmd : ICommand<object>
    {
        private static readonly ILogger<UnlockExclusiveJobCmd> log = ProcessEngineServiceProvider.LoggerService<UnlockExclusiveJobCmd>();

        private const long serialVersionUID = 1L;

        protected internal IJob job;

        public UnlockExclusiveJobCmd(IJob job)
        {
            this.job = job;
        }

        public virtual object Execute(ICommandContext commandContext)
        {

            if (job is null)
            {
                throw new ActivitiIllegalArgumentException("job is null");
            }

            if (log.IsEnabled(LogLevel.Debug))
            {
                log.LogDebug($"Unlocking exclusive job {job.Id}");
            }

            if (job.Exclusive)
            {
                if (job.ProcessInstanceId is not null)
                {
                    IExecutionEntity execution = commandContext.ExecutionEntityManager.FindById<IExecutionEntity>(job.ProcessInstanceId);
                    if (execution is object)
                    {
                        commandContext.ExecutionEntityManager.ClearProcessInstanceLockTime(execution.Id);
                    }
                }
            }

            return null;
        }
    }
}