﻿using System.Collections.Generic;

namespace org.activiti.engine.impl.cmd
{

    using org.activiti.engine.@event;
    using org.activiti.engine.impl.interceptor;

    /// 
    public class GetEventLogEntriesCmd : ICommand<IList<IEventLogEntry>>
    {

        protected internal string processInstanceId;
        protected internal long? startLogNr;
        protected internal long? pageSize;

        public GetEventLogEntriesCmd()
        {

        }

        public GetEventLogEntriesCmd(string processInstanceId)
        {
            this.processInstanceId = processInstanceId;
        }

        public GetEventLogEntriesCmd(long? startLogNr, long? pageSize)
        {
            this.startLogNr = startLogNr;
            this.pageSize = pageSize;
        }

        public virtual IList<IEventLogEntry> Execute(ICommandContext commandContext)
        {
            if (!ReferenceEquals(processInstanceId, null))
            {
                return commandContext.EventLogEntryEntityManager.FindEventLogEntriesByProcessInstanceId(processInstanceId);

            }
            else if (startLogNr != null)
            {
                return commandContext.EventLogEntryEntityManager.FindEventLogEntries(startLogNr.Value, pageSize != null ? pageSize.Value : -1);

            }
            else
            {
                return commandContext.EventLogEntryEntityManager.FindAllEventLogEntries();
            }
        }

    }

}