﻿using System.Collections.Generic;

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
namespace Sys.Workflow.Engine.Impl.Bpmn.Deployers
{

    using Sys.Workflow.Bpmn.Models;
    using Sys.Workflow.Engine.Impl.Asyncexecutor;
    using Sys.Workflow.Engine.Impl.Cmd;
    using Sys.Workflow.Engine.Impl.Contexts;
    using Sys.Workflow.Engine.Impl.JobExecutors;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;
    using Sys.Workflow.Engine.Impl.Util;

    /// <summary>
    /// Manages timers for newly-deployed process definitions and their previous versions.
    /// </summary>
    public class TimerManager
    {
        protected internal virtual void RemoveObsoleteTimers(IProcessDefinitionEntity processDefinition)
        {
            IList<ITimerJobEntity> jobsToDelete;
            if (processDefinition.TenantId is object && !ProcessEngineConfiguration.NO_TENANT_ID.Equals(processDefinition.TenantId))
            {
                jobsToDelete = Context.CommandContext.TimerJobEntityManager.FindJobsByTypeAndProcessDefinitionKeyAndTenantId(TimerStartEventJobHandler.TYPE, processDefinition.Key, processDefinition.TenantId);
            }
            else
            {
                jobsToDelete = Context.CommandContext.TimerJobEntityManager.FindJobsByTypeAndProcessDefinitionKeyNoTenantId(TimerStartEventJobHandler.TYPE, processDefinition.Key);
            }

            if (jobsToDelete is object)
            {
                foreach (ITimerJobEntity job in jobsToDelete)
                {
                    (new CancelJobsCmd(job.Id)).Execute(Context.CommandContext);
                }
            }
        }

        protected internal virtual void ScheduleTimers(IProcessDefinitionEntity processDefinition, Process process)
        {
            IJobManager jobManager = Context.CommandContext.JobManager;
            IList<ITimerJobEntity> timers = GetTimerDeclarations(processDefinition, process);
            foreach (ITimerJobEntity timer in timers)
            {
                jobManager.ScheduleTimerJob(timer);
            }
        }

        protected internal virtual IList<ITimerJobEntity> GetTimerDeclarations(IProcessDefinitionEntity processDefinition, Process process)
        {
            IJobManager jobManager = Context.CommandContext.JobManager;
            IList<ITimerJobEntity> timers = new List<ITimerJobEntity>();
            if (CollectionUtil.IsNotEmpty(process.FlowElements))
            {
                foreach (FlowElement element in process.FlowElements)
                {
                    if (element is StartEvent startEvent)
                    {
                        if (CollectionUtil.IsNotEmpty(startEvent.EventDefinitions))
                        {
                            EventDefinition eventDefinition = startEvent.EventDefinitions[0];
                            if (eventDefinition is TimerEventDefinition timerEventDefinition)
                            {
                                ITimerJobEntity timerJob = jobManager.CreateTimerJob(timerEventDefinition, false, null, TimerStartEventJobHandler.TYPE, TimerEventHandler.CreateConfiguration(startEvent.Id, timerEventDefinition.EndDate, timerEventDefinition.CalendarName));

                                if (timerJob is object)
                                {
                                    timerJob.ProcessDefinitionId = processDefinition.Id;

                                    if (processDefinition.TenantId is object)
                                    {
                                        timerJob.TenantId = processDefinition.TenantId;
                                    }
                                    timers.Add(timerJob);
                                }

                            }
                        }
                    }
                }
            }

            return timers;
        }
    }
}