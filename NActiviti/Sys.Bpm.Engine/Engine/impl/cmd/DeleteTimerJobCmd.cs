﻿using System;

namespace org.activiti.engine.impl.cmd
{

    using org.activiti.engine.@delegate.@event;
    using org.activiti.engine.@delegate.@event.impl;
    using org.activiti.engine.impl.context;
    using org.activiti.engine.impl.interceptor;
    using org.activiti.engine.impl.persistence.entity;
    using org.activiti.engine.runtime;
    using System.Collections.Generic;

    /// 

    [Serializable]
    public class DeleteTimerJobCmd : ICommand<object>
    {
        private const long serialVersionUID = 1L;

        protected internal string timerJobId;

        public DeleteTimerJobCmd(string timerJobId)
        {
            this.timerJobId = timerJobId;
        }

        public virtual object Execute(ICommandContext commandContext)
        {
            ITimerJobEntity jobToDelete = GetJobToDelete(commandContext);

            SendCancelEvent(jobToDelete);

            commandContext.TimerJobEntityManager.Delete(jobToDelete);
            return null;
        }

        protected internal virtual void SendCancelEvent(ITimerJobEntity jobToDelete)
        {
            if (Context.ProcessEngineConfiguration.EventDispatcher.Enabled)
            {
                Context.ProcessEngineConfiguration.EventDispatcher.DispatchEvent(ActivitiEventBuilder.CreateEntityEvent(ActivitiEventType.JOB_CANCELED, jobToDelete));
            }
        }

        protected internal virtual ITimerJobEntity GetJobToDelete(ICommandContext commandContext)
        {
            if (timerJobId is null)
            {
                throw new ActivitiIllegalArgumentException("jobId is null");
            }
            //if (log.DebugEnabled)
            //{
            //  log.debug("Deleting job {}", timerJobId);
            //}

            ITimerJobEntity job = commandContext.TimerJobEntityManager.FindById<ITimerJobEntity>(new KeyValuePair<string, object>("id", timerJobId));
            if (job == null)
            {
                throw new ActivitiObjectNotFoundException("No timer job found with id '" + timerJobId + "'", typeof(IJob));
            }

            // We need to check if the job was locked, ie acquired by the job acquisition thread
            // This happens if the the job was already acquired, but not yet executed.
            // In that case, we can't allow to delete the job.
            if (!(job.LockOwner is null))
            {
                throw new ActivitiException("Cannot delete timer job when the job is being executed. Try again later.");
            }
            return job;
        }

    }

}