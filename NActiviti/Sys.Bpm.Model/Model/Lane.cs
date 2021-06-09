﻿using Newtonsoft.Json;
using System.Collections.Generic;

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
namespace Sys.Workflow.Bpmn.Models
{
    public class Lane : BaseElement
    {

        protected internal string name;

        [JsonIgnore]
        protected internal Process parentProcess;

        protected internal IList<string> flowReferences = new List<string>();

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

        [JsonIgnore]
        public virtual Process ParentProcess
        {
            get
            {
                return parentProcess;
            }
            set
            {
                this.parentProcess = value;
            }
        }


        public virtual IList<string> FlowReferences
        {
            get
            {
                return flowReferences;
            }
            set
            {
                this.flowReferences = value;
            }
        }


        public override BaseElement Clone()
        {
            Lane clone = new Lane
            {
                Values = this
            };
            return clone;
        }

        public override BaseElement Values
        {
            set
            {
                base.Values = value;
                var val = value as Lane;
                Name = val.Name;
                ParentProcess = val.ParentProcess;

                flowReferences = new List<string>();
                if (val.FlowReferences is object && val.FlowReferences.Count > 0)
                {
                    ((List<string>)flowReferences).AddRange(val.FlowReferences);
                }
            }
        }
    }

}