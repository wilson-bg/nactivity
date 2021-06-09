﻿using Sys.Workflow.Concurrent;
using Microsoft.Extensions.Logging;
using Sys.Workflow;
using System.Collections.Concurrent;
using System.Threading;

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
namespace Sys.Workflow.Engine.Impl.Asyncexecutor
{
    /// <summary>
    /// Simple JSR-236 async job executor to allocate threads through <seealso cref="ManagedThreadFactory"/>. Falls back to <seealso cref="IAsyncExecutor"/> when a thread factory was not referenced in configuration.
    /// 
    /// In Java EE 7, all application servers should provide access to a <seealso cref="ManagedThreadFactory"/>.
    /// 
    /// 
    /// </summary>
    public class ManagedAsyncJobExecutor : DefaultAsyncJobExecutor
    {
        private static readonly ILogger log = ProcessEngineServiceProvider.LoggerService<ManagedAsyncJobExecutor>();

        /// <summary>
        /// 
        /// </summary>
        protected internal ManagedThreadFactory threadFactory;

        /// <summary>
        /// 
        /// </summary>
        public virtual ManagedThreadFactory ThreadFactory
        {
            get
            {
                return threadFactory;
            }
            set
            {
                this.threadFactory = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal override void InitAsyncJobExecutionThreadPool()
        {
            if (threadFactory is null)
            {
                log.LogWarning("A managed thread factory was not found, falling back to self-managed threads");
                base.InitAsyncJobExecutionThreadPool();
            }
            else
            {
                if (threadPoolQueue is null)
                {
                    log.LogInformation($"Creating thread pool queue of size {queueSize}");
                    threadPoolQueue = new ConcurrentQueue<ThreadStart>();
                }

                if (executorService is null)
                {
                    log.LogInformation($"Creating executor service with corePoolSize {corePoolSize}, maxPoolSize {maxPoolSize} and keepAliveTime {keepAliveTime}");

                    ThreadPoolExecutor threadPoolExecutor = new ThreadPoolExecutor(corePoolSize, maxPoolSize, keepAliveTime, threadPoolQueue);
                    //threadPoolExecutor.RejectedExecutionHandler = new ThreadPoolExecutor.CallerRunsPolicy();
                    executorService = threadPoolExecutor;

                }

                StartJobAcquisitionThread();
            }
        }
    }
}