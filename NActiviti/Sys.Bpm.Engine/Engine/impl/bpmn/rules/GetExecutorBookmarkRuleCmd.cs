///////////////////////////////////////////////////////////
//  GetExecutorBookmarkRuleCmd.cs
//  Implementation of the Class GetExecutorBookmarkRuleCmd
//  Generated by Enterprise Architect
//  Created on:      30-1月-2019 8:32:00
//  Original author: 张楠
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys.Workflow.Engine.Impl.Interceptor;
using Microsoft.Extensions.Options;
using Sys.Workflow.Engine.Impl.Persistence.Entity;
using Sys.Workflow.Engine.Repository;
using Sys.Workflow.Bpmn.Models;
using System.Linq;
using Sys.Workflow.Engine.Tasks;
using Sys.Workflow.Engine.History;
using Sys.Net.Http;

namespace Sys.Workflow.Engine.Bpmn.Rules
{
    /// <summary>
    /// 获取当前执行人的用户信息
    /// </summary>
    [GetBookmarkDescriptor(RequestUserCategory.GETUSER_EXECUTOR)]
    public class GetExecutorBookmarkRuleCmd : BaseGetBookmarkRule
    {
        private readonly ExternalConnectorProvider externalConnector;
        /// <inheritdoc />
        public GetExecutorBookmarkRuleCmd()
        {
            externalConnector = ProcessEngineServiceProvider.Resolve<ExternalConnectorProvider>();
        }
        /// <inheritdoc />
        public override IList<IUserInfo> Execute(ICommandContext commandContext)
        {
            IList<IHistoricTaskInstance> hisTasks = commandContext.ProcessEngineConfiguration.HistoryService
                .CreateHistoricTaskInstanceQuery()
                .SetExecutionId(this.Execution.Id)
                .List();

            IList<IUserInfo> users = hisTasks.Where(x => Condition.QueryCondition.Any(y => y.Id == x.TaskDefinitionKey))
               .Select(x => new UserInfo
               {
                   Id = x.Assignee
               })
               .ToList<IUserInfo>();

            if (Condition.QueryCondition.Any(x => string.Equals("startuserid", x.Id, StringComparison.OrdinalIgnoreCase)))
            {
                var hisInst = commandContext.ProcessEngineConfiguration.HistoryService
                    .CreateHistoricProcessInstanceQuery()
                    .SetProcessInstanceId(this.Execution.ProcessInstanceId)
                    .SingleResult();

                string uid = hisInst == null ? this.Execution.Parent.StartUserId : hisInst.StartUserId;
                users.Add(new UserInfo
                {
                    Id = uid
                });
            }

            IUserServiceProxy proxy = ProcessEngineServiceProvider.Resolve<IUserServiceProxy>();

            return proxy.GetUsers(externalConnector.GetUser, new RequestUserParameter
            {
                IdList = users.Select(x => x.Id).ToArray(),
                Category = RequestUserCategory.GETUSER_EXECUTOR
            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}