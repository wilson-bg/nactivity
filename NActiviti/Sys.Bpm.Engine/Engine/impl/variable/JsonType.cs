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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;

namespace Sys.Workflow.Engine.Impl.Variable
{
    /// 
    public class JsonType : AbstractVariableType
    {
        private static readonly ILogger<JsonType> log = ProcessEngineServiceProvider.LoggerService<JsonType>();

        protected internal readonly int maxLength;
        protected internal ObjectMapper objectMapper;

        public JsonType(int maxLength, ObjectMapper objectMapper)
        {
            this.maxLength = maxLength;
            this.objectMapper = objectMapper;
        }

        public override string TypeName
        {
            get
            {
                return "json";
            }
        }

        public override bool Cachable
        {
            get
            {
                return true;
            }
        }

        public override object GetValue(IValueFields valueFields)
        {
            JToken jsonValue = null;
            if (valueFields.TextValue is not null && valueFields.TextValue.Length > 0)
            {
                try
                {
                    jsonValue = objectMapper.ReadTree(valueFields.TextValue);
                }
                catch (Exception e)
                {
                    log.LogError(e, $"Error reading json variable {valueFields.Name}");
                }
            }
            return jsonValue;
        }

        public override void SetValue(object value, IValueFields valueFields)
        {
            valueFields.TextValue = value?.ToString();
        }

        public override bool IsAbleToStore(object value)
        {
            if (value is null)
            {
                return true;
            }
            if (value.GetType().IsAssignableFrom(typeof(JToken)))
            {
                JToken jsonValue = (JToken)value;
                return jsonValue.ToString().Length <= maxLength;
            }
            return false;
        }
    }

}