﻿using Sys.Workflow.Engine.Impl.Contexts;
using Sys.Workflow.Engine.Impl.Interceptor;
using System;
using System.Collections.Generic;

/*
 * Copyright 2018 Alfresco, Inc. and/or its affiliates.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Sys.Workflow.Cloud.Services.Events.Listeners
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="L"></typeparam>
    public abstract class BaseCommandContextEventsAggregator<E, L> where L : ICommandContextCloseListener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>

        public virtual void Add(E element)
        {
            ICommandContext currentCommandContext = CurrentCommandContext;
            IList<E> attributes = currentCommandContext.GetGenericAttribute<IList<E>>(AttributeKey);
            if (attributes is null)
            {
                attributes = new List<E>();
                currentCommandContext.AddAttribute(AttributeKey, attributes);
            }
            attributes.Add(element);

            if (!currentCommandContext.HasCloseListener(CloseListenerClass))
            {
                currentCommandContext.AddCloseListener(CloseListener);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        protected internal abstract Type CloseListenerClass { get; }

        /// <summary>
        /// 
        /// </summary>
        protected internal abstract L CloseListener { get; }

        /// <summary>
        /// 
        /// </summary>
        protected internal abstract string AttributeKey { get; }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual ICommandContext CurrentCommandContext
        {
            get
            {
                return Context.CommandContext;
            }
        }
    }
}