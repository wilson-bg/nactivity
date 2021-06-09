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
namespace Sys.Workflow.Bpmn.Models
{
    public class CustomProperty : BaseElement
    {

        protected internal string name;
        protected internal string simpleValue;
        protected internal IComplexDataType complexValue;

        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }


        public virtual string SimpleValue
        {
            get
            {
                return simpleValue;
            }
            set
            {
                this.simpleValue = value;
            }
        }


        public virtual IComplexDataType ComplexValue
        {
            get
            {
                return complexValue;
            }
            set
            {
                this.complexValue = value;
            }
        }


        public override BaseElement Clone()
        {
            CustomProperty clone = new CustomProperty
            {
                Values = this
            };
            return clone;
        }

        public override BaseElement Values
        {
            set
            {
                var val = value as CustomProperty;

                Name = val.Name;
                SimpleValue = val.SimpleValue;

                if (val.ComplexValue is object && val.ComplexValue is DataGrid)
                {
                    ComplexValue = ((DataGrid)val.ComplexValue).Clone();
                }
            }
        }
    }

}