///////////////////////////////////////////////////////////
//  GetDirectReporterBookmarkRuleCmd.cs
//  Implementation of the Class GetDirectReporterBookmarkRuleCmd
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
using System.Linq;

namespace Sys.Workflow.Engine.Bpmn.Rules
{
    /// <summary>
    /// 根据人员id查找直接汇报对象
    /// </summary>
    [GetBookmarkDescriptor(RequestUserCategory.GETUSER_DIRECTREPOT)]
    public class GetDirectReporterBookmarkRuleCmd : BaseGetBookmarkRule
    {
        private readonly ExternalConnectorProvider externalConnector;

        /// <inheritdoc />
        public GetDirectReporterBookmarkRuleCmd()
        {
            externalConnector = ProcessEngineServiceProvider.Resolve<ExternalConnectorProvider>();
        }
        /// <inheritdoc />
        public override IList<IUserInfo> Execute(ICommandContext commandContext)
        {
            IUserServiceProxy proxy = ProcessEngineServiceProvider.Resolve<IUserServiceProxy>();

            return proxy.GetUsers(externalConnector.GetUserByDirectReporter, new RequestUserParameter
            {
                IdList = Condition.QueryCondition.Select(x => x.Id).ToArray(),
                Category = RequestUserCategory.GETUSER_DIRECTREPOT
            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}