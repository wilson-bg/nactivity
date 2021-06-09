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
    using Sys.Workflow;
    using System.Collections.Generic;

    /// 
    [Serializable]
    public class MoveDeadLetterJobToExecutableJobCmd : ICommand<IJobEntity>
    {
        private static readonly ILogger<MoveDeadLetterJobToExecutableJobCmd> log = ProcessEngineServiceProvider.LoggerService<MoveDeadLetterJobToExecutableJobCmd>();

        private const long serialVersionUID = 1L;

        protected internal string jobId;
        protected internal int retries;

        public MoveDeadLetterJobToExecutableJobCmd(string jobId, int retries)
        {
            this.jobId = jobId;
            this.retries = retries;
        }

        public  virtual IJobEntity  Execute(ICommandContext  commandContext)
        {

            if (jobId is null)
            {
                throw new ActivitiIllegalArgumentException("jobId and job is null");
            }

            IDeadLetterJobEntity job = commandContext.DeadLetterJobEntityManager.FindById<IDeadLetterJobEntity>(new KeyValuePair<string, object>("id", jobId));
            if (job is null)
            {
                throw new JobNotFoundException(jobId);
            }

            if (log.IsEnabled(LogLevel.Debug))
            {
                log.LogDebug($"Moving deadletter job to executable job table {job.Id}");
            }

            return commandContext.JobManager.MoveDeadLetterJobToExecutableJob(job, retries);
        }

        public virtual string JobId
        {
            get
            {
                return jobId;
            }
        }

    }

}