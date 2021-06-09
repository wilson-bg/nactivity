﻿using System.Collections.Generic;

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
namespace Sys.Workflow.Validation.Validators.Impl
{
    using Sys.Workflow.Bpmn.Models;

    /// 
    public class ExecutionListenerValidator : ProcessLevelValidator
    {
        protected internal override void ExecuteValidation(BpmnModel bpmnModel, Process process, IList<ValidationError> errors)
        {

            ValidateListeners(process, process, process.ExecutionListeners, errors);

            foreach (FlowElement flowElement in process.FlowElements)
            {
                ValidateListeners(process, flowElement, flowElement.ExecutionListeners, errors);
            }
        }

        protected internal virtual void ValidateListeners(Process process, BaseElement baseElement, IList<ActivitiListener> listeners, IList<ValidationError> errors)
        {
            if (listeners is object)
            {
                foreach (ActivitiListener listener in listeners)
                {
                    if (string.IsNullOrWhiteSpace(listener.Implementation) || string.IsNullOrWhiteSpace(listener.ImplementationType))
                    {
                        AddError(errors, ProblemsConstants.EXECUTION_LISTENER_IMPLEMENTATION_MISSING, process, baseElement, ProcessValidatorResource.EXECUTION_LISTENER_IMPLEMENTATION_MISSING);
                    }
                    if (!string.IsNullOrWhiteSpace(listener.OnTransaction) && ImplementationType.IMPLEMENTATION_TYPE_EXPRESSION.Equals(listener.ImplementationType))
                    {
                        AddError(errors, ProblemsConstants.EXECUTION_LISTENER_INVALID_IMPLEMENTATION_TYPE, process, baseElement, ProcessValidatorResource.EXECUTION_LISTENER_INVALID_IMPLEMENTATION_TYPE);
                    }
                }
            }
        }
    }
}