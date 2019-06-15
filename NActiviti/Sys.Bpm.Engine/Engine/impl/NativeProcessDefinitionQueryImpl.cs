﻿using System;
using System.Collections.Generic;

namespace org.activiti.engine.impl
{
    using org.activiti.engine.impl.interceptor;
    using org.activiti.engine.repository;


    [Serializable]
    public class NativeProcessDefinitionQueryImpl : AbstractNativeQuery<INativeProcessDefinitionQuery, IProcessDefinition>, INativeProcessDefinitionQuery
    {

        private const long serialVersionUID = 1L;

        public NativeProcessDefinitionQueryImpl(ICommandContext commandContext) : base(commandContext)
        {
        }

        public NativeProcessDefinitionQueryImpl(ICommandExecutor commandExecutor) : base(commandExecutor)
        {
        }

        // results ////////////////////////////////////////////////////////////////

        public override IList<IProcessDefinition> ExecuteList(ICommandContext commandContext, IDictionary<string, object> parameterMap, int firstResult, int maxResults)
        {
            return commandContext.ProcessDefinitionEntityManager.FindProcessDefinitionsByNativeQuery(parameterMap, firstResult, maxResults);
        }

        public override long ExecuteCount(ICommandContext commandContext, IDictionary<string, object> parameterMap)
        {
            return commandContext.ProcessDefinitionEntityManager.FindProcessDefinitionCountByNativeQuery(parameterMap);
        }

    }

}