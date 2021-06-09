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
namespace Sys.Workflow.Engine.Impl.Bpmn.Parser.Handlers
{
    using Microsoft.Extensions.Logging;
    using Sys.Workflow.Bpmn.Models;

    /// 
    /// 
    public class IntermediateCatchEventParseHandler : AbstractFlowNodeBpmnParseHandler<IntermediateCatchEvent>
    {
        protected internal override Type HandledType
        {
            get
            {
                return typeof(IntermediateCatchEvent);
            }
        }

        protected internal override void ExecuteParse(BpmnParse bpmnParse, IntermediateCatchEvent @event)
        {
            EventDefinition eventDefinition = null;
            if (@event.EventDefinitions.Count > 0)
            {
                eventDefinition = @event.EventDefinitions[0];
            }

            if (eventDefinition is null)
            {
                @event.Behavior = bpmnParse.ActivityBehaviorFactory.CreateIntermediateCatchEventActivityBehavior(@event);

            }
            else
            {
                if (eventDefinition is TimerEventDefinition || eventDefinition is SignalEventDefinition || eventDefinition is MessageEventDefinition)
                {

                    bpmnParse.BpmnParserHandlers.ParseElement(bpmnParse, eventDefinition);

                }
                else
                {
                    logger.LogWarning("Unsupported intermediate catch event type for event " + @event.Id);
                }
            }
        }

    }

}