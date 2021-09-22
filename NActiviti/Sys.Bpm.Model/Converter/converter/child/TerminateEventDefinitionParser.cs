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
namespace Sys.Workflow.Bpmn.Converters.Childs
{
    using Sys.Workflow.Bpmn.Constants;
    using Sys.Workflow.Bpmn.Converters.Utils;
    using Sys.Workflow.Bpmn.Models;
    using System;

    /// 
    /// 
    public class TerminateEventDefinitionParser : BaseChildElementParser
    {

        public override string ElementName
        {
            get
            {
                return BpmnXMLConstants.ELEMENT_EVENT_TERMINATEDEFINITION;
            }
        }
        public override void ParseChildElement(XMLStreamReader xtr, BaseElement parentElement, BpmnModel model)
        {
            if (!(parentElement is EndEvent))
            {
                return;
            }

            TerminateEventDefinition eventDefinition = new TerminateEventDefinition();

            ParseTerminateAllAttribute(xtr, eventDefinition);
            ParseTerminateMultiInstanceAttribute(xtr, eventDefinition);

            BpmnXMLUtil.AddXMLLocation(eventDefinition, xtr);
            BpmnXMLUtil.ParseChildElements(BpmnXMLConstants.ELEMENT_EVENT_TERMINATEDEFINITION, eventDefinition, xtr, model);

            ((Event)parentElement).EventDefinitions.Add(eventDefinition);
        }

        protected internal virtual void ParseTerminateAllAttribute(XMLStreamReader xtr, TerminateEventDefinition eventDefinition)
        {
            string terminateAllValue = xtr.GetAttributeValue(BpmnXMLConstants.ACTIVITI_EXTENSIONS_NAMESPACE, BpmnXMLConstants.ATTRIBUTE_TERMINATE_ALL);
            if (terminateAllValue is not null && "true".Equals(terminateAllValue))
            {
                eventDefinition.TerminateAll = true;
            }
            else
            {
                eventDefinition.TerminateAll = false;
            }
        }

        protected internal virtual void ParseTerminateMultiInstanceAttribute(XMLStreamReader xtr, TerminateEventDefinition eventDefinition)
        {
            string terminateMiValue = xtr.GetAttributeValue(BpmnXMLConstants.ACTIVITI_EXTENSIONS_NAMESPACE, BpmnXMLConstants.ATTRIBUTE_TERMINATE_MULTI_INSTANCE);
            if (terminateMiValue is not null && "true".Equals(terminateMiValue, StringComparison.OrdinalIgnoreCase))
            {
                eventDefinition.TerminateMultiInstance = true;
            }
            else
            {
                eventDefinition.TerminateMultiInstance = false;
            }
        }
    }

}