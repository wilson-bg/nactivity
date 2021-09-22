﻿using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Sys.Workflow.Bpmn.Models
{

    public class ExtensionElement : BaseElement
    {
        protected internal string name;
        protected internal string namespacePrefix;
        protected internal string @namespace;
        protected internal string elementText;
        protected internal IDictionary<string, IList<ExtensionElement>> childElements = new ConcurrentDictionary<string, IList<ExtensionElement>>();

        public virtual string ElementText
        {
            get
            {
                return elementText;
            }
            set
            {
                this.elementText = value;
            }
        }


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


        public virtual string NamespacePrefix
        {
            get
            {
                return namespacePrefix;
            }
            set
            {
                this.namespacePrefix = value;
            }
        }


        public virtual string Namespace
        {
            get
            {
                return @namespace;
            }
            set
            {
                this.@namespace = value;
            }
        }


        public virtual IDictionary<string, IList<ExtensionElement>> ChildElements
        {
            get
            {
                if (childElements is null)
                {
                    childElements = new ConcurrentDictionary<string, IList<ExtensionElement>>();
                }

                return childElements;
            }
            set
            {
                this.childElements = value;
            }
        }

        public virtual void AddChildElement(ExtensionElement childElement)
        {
            if (childElement is not null && !string.IsNullOrWhiteSpace(childElement.Name))
            {
                if (!this.childElements.ContainsKey(childElement.Name))
                {
                    IList<ExtensionElement> elementList = new List<ExtensionElement>();
                    this.childElements[childElement.Name] = elementList;
                }
                this.childElements[childElement.Name].Add(childElement);
            }
        }


        public override BaseElement Clone()
        {
            ExtensionElement clone = new ExtensionElement
            {
                Values = this
            };
            return clone;
        }

        public override BaseElement Values
        {
            set
            {
                var val = value as ExtensionElement;

                Name = val.Name;
                NamespacePrefix = val.NamespacePrefix;
                Namespace = val.Namespace;
                ElementText = val.ElementText;
                Attributes = val.Attributes;

                childElements = new Dictionary<string, IList<ExtensionElement>>();
                if (val.ChildElements is object && val.ChildElements.Count > 0)
                {
                    foreach (string key in val.ChildElements.Keys)
                    {
                        IList<ExtensionElement> otherElementList = val.ChildElements[key];
                        if (otherElementList is object && otherElementList.Count > 0)
                        {
                            IList<ExtensionElement> elementList = new List<ExtensionElement>();
                            foreach (ExtensionElement extensionElement in otherElementList)
                            {
                                elementList.Add(extensionElement.Clone() as ExtensionElement);
                            }
                            childElements[key] = elementList;
                        }
                    }
                }
            }
        }
    }
}