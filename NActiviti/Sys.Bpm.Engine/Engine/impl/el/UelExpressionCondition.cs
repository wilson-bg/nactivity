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

namespace Sys.Workflow.Engine.Impl.EL
{
    using Sys.Workflow.Engine.Delegate;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;

    /// <summary>
    /// <seealso cref="ICondition"/> that resolves an UEL expression at runtime.
    /// 
    /// 
    /// 
    /// </summary>
    [Serializable]
    public class UelExpressionCondition : ICondition
    {
        /// <summary>
        /// 
        /// </summary>
        protected internal IExpression expression;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public UelExpressionCondition(IExpression expression)
        {
            this.expression = expression;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequenceFlowId"></param>
        /// <param name="execution"></param>
        /// <returns></returns>
        public virtual bool Evaluate(string sequenceFlowId, IExecutionEntity execution)
        {
            object result = expression.GetValue(execution);

            if (result is null)
            {
                throw new ActivitiException("condition expression returns null (sequenceFlowId: " + sequenceFlowId + " execution: " + execution + ")");
            }
            if (result is not bool)
            {
                throw new ActivitiException("condition expression returns non-Boolean (sequenceFlowId: " + sequenceFlowId + " execution: " + execution + "): " + result + " (" + result.GetType().FullName + ")");
            }
            return Convert.ToBoolean(result);
        }
    }
}