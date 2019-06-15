﻿using Sys.Bpm;
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
namespace org.activiti.bpmn.model
{
    public class SubProcess : Activity, IFlowElementsContainer
    {

        protected internal IDictionary<string, FlowElement> flowElementMap = new Dictionary<string, FlowElement>();
        protected internal IList<FlowElement> flowElementList = new List<FlowElement>();
        protected internal IList<Artifact> artifactList = new List<Artifact>();
        protected internal IList<ValuedDataObject> dataObjects = new List<ValuedDataObject>();

        public virtual FlowElement FindFlowElement(string id)
        {
            FlowElement foundElement = null;
            if (!string.IsNullOrWhiteSpace(id))
            {
                foundElement = flowElementMap[id];
            }
            return foundElement;
        }

        public virtual IList<FlowElement> FlowElements
        {
            get
            {
                return flowElementList;
            }
        }

        public virtual void AddFlowElement(FlowElement element)
        {
            flowElementList.Add(element);
            element.ParentContainer = this;
            if (element is IFlowElementsContainer)
            {
                flowElementMap.PutAll(((IFlowElementsContainer)element).FlowElementMap);
            }
            if (!string.IsNullOrWhiteSpace(element.Id))
            {
                flowElementMap[element.Id] = element;
                if (ParentContainer != null)
                {
                    ParentContainer.AddFlowElementToMap(element);
                }
            }
        }

        public virtual void AddFlowElementToMap(FlowElement element)
        {
            if (element != null && !string.IsNullOrWhiteSpace(element.Id))
            {
                flowElementMap[element.Id] = element;
                if (ParentContainer != null)
                {
                    ParentContainer.AddFlowElementToMap(element);
                }
            }
        }

        public virtual void RemoveFlowElement(string elementId)
        {
            FlowElement element = FindFlowElement(elementId);
            if (element != null)
            {
                flowElementList.Remove(element);
                flowElementMap.Remove(elementId);
                if (element.ParentContainer != null)
                {
                    element.ParentContainer.RemoveFlowElementFromMap(elementId);
                }
            }
        }

        public virtual void RemoveFlowElementFromMap(string elementId)
        {
            if (!string.IsNullOrWhiteSpace(elementId))
            {
                flowElementMap.Remove(elementId);
            }
        }

        public virtual IDictionary<string, FlowElement> FlowElementMap
        {
            get
            {
                return flowElementMap;
            }
            set
            {
                this.flowElementMap = value;
            }
        }


        public virtual bool ContainsFlowElementId(string id)
        {
            return flowElementMap.ContainsKey(id);
        }

        public virtual Artifact GetArtifact(string id)
        {
            Artifact foundArtifact = null;
            foreach (Artifact artifact in artifactList)
            {
                if (id.Equals(artifact.Id))
                {
                    foundArtifact = artifact;
                    break;
                }
            }
            return foundArtifact;
        }

        public virtual IList<Artifact> Artifacts
        {
            get
            {
                return artifactList;
            }
        }

        public virtual void AddArtifact(Artifact artifact)
        {
            artifactList.Add(artifact);
        }

        public virtual void RemoveArtifact(string artifactId)
        {
            Artifact artifact = GetArtifact(artifactId);
            if (artifact != null)
            {
                artifactList.Remove(artifact);
            }
        }

        public override BaseElement Clone()
        {
            SubProcess clone = new SubProcess
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

                var val = value as SubProcess;

                /*
                 * This is required because data objects in Designer have no DI info and are added as properties, not flow elements
                 * 
                 * Determine the differences between the 2 elements' data object
                 */
                foreach (ValuedDataObject thisObject in DataObjects)
                {
                    bool exists = false;
                    foreach (ValuedDataObject otherObject in val.DataObjects)
                    {
                        if (thisObject.Id.Equals(otherObject.Id))
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        // missing object
                        RemoveFlowElement(thisObject.Id);
                    }
                }

                dataObjects = new List<ValuedDataObject>();
                if (val.DataObjects != null && val.DataObjects.Count > 0)
                {
                    foreach (ValuedDataObject dataObject in val.DataObjects)
                    {
                        ValuedDataObject clone = dataObject.Clone() as ValuedDataObject;
                        dataObjects.Add(clone);
                        // add it to the list of FlowElements
                        // if it is already there, remove it first so order is same as
                        // data object list
                        RemoveFlowElement(clone.Id);
                        AddFlowElement(clone);
                    }
                }

                flowElementList.Clear();
                foreach (FlowElement flowElement in val.FlowElements)
                {
                    AddFlowElement(flowElement);
                }

                artifactList.Clear();
                foreach (Artifact artifact in val.Artifacts)
                {
                    AddArtifact(artifact);
                }
            }
        }

        public virtual IList<ValuedDataObject> DataObjects
        {
            get
            {
                return dataObjects;
            }
            set
            {
                this.dataObjects = value;
            }
        }

    }

}