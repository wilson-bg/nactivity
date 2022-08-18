﻿/* Licensed under the Apache License, Version 2.0 (the "License");
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
namespace Sys.Workflow.Engine.Impl.Json
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;
    using Sys.Workflow.Engine.Repository;
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
    public class JsonProcessDefinitionConverter : JsonObjectConverter<IProcessDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="processDefinition"></param>
        /// <returns></returns>
        public override JToken ToJsonObject(IProcessDefinition processDefinition)
        {
            IProcessDefinitionEntity processDefinitionEntity = (IProcessDefinitionEntity)processDefinition;
            JToken jsonObject = new JObject
            {
                ["id"] = processDefinitionEntity.Id
            };
            if (processDefinitionEntity.Key is not null)
            {
                jsonObject["key"] = processDefinitionEntity.Key;
            }
            if (processDefinitionEntity.DeploymentId is not null)
            {
                jsonObject["deploymentId"] = processDefinitionEntity.DeploymentId;
            }
            return jsonObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public override IProcessDefinition ToObject(StreamReader reader)
        {
            var str = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<ProcessDefinitionEntityImpl>(str);
        }
    }

}