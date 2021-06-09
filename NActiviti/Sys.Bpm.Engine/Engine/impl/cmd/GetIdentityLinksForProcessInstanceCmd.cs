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
    using Sys.Workflow.Engine.Tasks;

    /// 
    [Serializable]
    public class GetIdentityLinksForProcessInstanceCmd : ICommand<IList<IIdentityLink>>
    {

        private const long serialVersionUID = 1L;

        protected internal string processInstanceId;

        public GetIdentityLinksForProcessInstanceCmd(string processInstanceId)
        {
            this.processInstanceId = processInstanceId;
        }

        public virtual IList<IIdentityLink> Execute(ICommandContext commandContext)
        {
            IExecutionEntity processInstance = commandContext.ExecutionEntityManager.FindById<IExecutionEntity>(processInstanceId);

            if (processInstance is null)
            {
                throw new ActivitiObjectNotFoundException("Cannot find process definition with id " + processInstanceId, typeof(IExecutionEntity));
            }

            return (IList<IIdentityLink>)processInstance.IdentityLinks;
        }

    }

}