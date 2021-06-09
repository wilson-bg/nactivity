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
    using Newtonsoft.Json.Linq;
    using Sys.Workflow.Engine.Impl.Interceptor;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;
    using Sys.Workflow;


    /// 
    [Serializable]
    public class SaveProcessDefinitionInfoCmd : ICommand<object>
    {

        private const long serialVersionUID = 1L;

        protected internal string processDefinitionId;
        protected internal JToken infoNode;

        public SaveProcessDefinitionInfoCmd(string processDefinitionId, JToken infoNode)
        {
            this.processDefinitionId = processDefinitionId;
            this.infoNode = infoNode;
        }

        public  virtual object  Execute(ICommandContext commandContext)
        {
            if (string.IsNullOrWhiteSpace(processDefinitionId))
            {
                throw new ActivitiIllegalArgumentException("process definition id is null");
            }

            if (infoNode is null)
            {
                throw new ActivitiIllegalArgumentException("process definition info node is null");
            }

            IProcessDefinitionInfoEntityManager definitionInfoEntityManager = commandContext.ProcessDefinitionInfoEntityManager;
            IProcessDefinitionInfoEntity definitionInfoEntity = definitionInfoEntityManager.FindProcessDefinitionInfoByProcessDefinitionId(processDefinitionId);
            if (definitionInfoEntity is null)
            {
                definitionInfoEntity = definitionInfoEntityManager.Create();
                definitionInfoEntity.ProcessDefinitionId = processDefinitionId;
                commandContext.ProcessDefinitionInfoEntityManager.InsertProcessDefinitionInfo(definitionInfoEntity);
            }

            if (infoNode is object)
            {
                try
                {
                    ObjectMapper writer = commandContext.ProcessEngineConfiguration.ObjectMapper;
                    commandContext.ProcessDefinitionInfoEntityManager.UpdateInfoJson(definitionInfoEntity.Id, writer.WriteValueAsBytes(infoNode));
                }
                catch (Exception)
                {
                    throw new ActivitiException("Unable to serialize info node " + infoNode);
                }
            }

            return null;
        }

    }
}