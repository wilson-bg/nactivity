﻿using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
namespace Sys.Workflow.Engine.Impl.Persistence.Entity.Data.Impl.Cachematcher
{


    /// 
    public class ExecutionsByParentExecutionIdAndActivityIdEntityMatcher : CachedEntityMatcherAdapter<IExecutionEntity>
    {

        public override bool IsRetained(IExecutionEntity executionEntity, object parameter)
        {
            if (parameter is null)
            {
                return false;
            }

            JToken @params = JToken.FromObject(parameter);
            string parentExecutionId = @params[nameof(parentExecutionId)]?.ToString();
            JArray list = @params["activityIds"] as JArray;
            string[] activityIds = list.Select(x => x.ToString())?.ToArray() ?? new string[0];

            return executionEntity.ParentId is object &&
                string.Compare(executionEntity.ParentId, parentExecutionId, true) == 0 &&
                executionEntity.ActivityId is object &&
                activityIds.Any(x => string.Compare(x, executionEntity.ActivityId, true) == 0);
        }
    }
}