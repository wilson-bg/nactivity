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
namespace Sys.Workflow.Engine.Impl.Scripting
{
    using Sys.Workflow.Engine.Delegate;
    using Sys.Workflow.Engine.Impl.Contexts;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ScriptCondition : ICondition
    {
        private readonly string expression;
        private readonly string language;

        public ScriptCondition(string expression, string language)
        {
            this.expression = expression;
            this.language = language;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequenceFlowId"></param>
        /// <param name="execution"></param>
        /// <returns></returns>
        public virtual bool Evaluate(string sequenceFlowId, IExecutionEntity execution)
        {
            IScriptingEnginesProvider scriptingEnginesProvider = ProcessEngineServiceProvider.Resolve<IScriptingEnginesProvider>();
            IScriptingEngines scriptingEngines = scriptingEnginesProvider.Create(language);

            object result = scriptingEngines.Evaluate(expression, execution);
            if (result is null)
            {
                throw new ActivitiException("condition script returns null: " + expression);
            }
            if (result is not bool)
            {
                throw new ActivitiException("condition script returns non-Boolean: " + result + " (" + result.GetType().FullName + ")");
            }
            return Convert.ToBoolean(result);
        }
    }
}