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
    public class OperationValidator : ValidatorImpl
    {

        public override void Validate(BpmnModel bpmnModel, IList<ValidationError> errors)
        {
            if (bpmnModel.Interfaces is object)
            {
                foreach (Interface bpmnInterface in bpmnModel.Interfaces)
                {
                    if (bpmnInterface.Operations is object)
                    {
                        foreach (Operation operation in bpmnInterface.Operations)
                        {
                            if (bpmnModel.GetMessage(operation.InMessageRef) is null)
                            {
                                AddError(errors, ProblemsConstants.OPERATION_INVALID_IN_MESSAGE_REFERENCE, null, operation, ProcessValidatorResource.OPERATION_INVALID_IN_MESSAGE_REFERENCE);
                            }
                        }
                    }
                }
            }
        }

    }

}