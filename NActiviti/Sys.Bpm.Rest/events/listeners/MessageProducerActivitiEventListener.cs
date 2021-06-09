﻿/*
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
    using Sys.Workflow.Cloud.Services.Api.Events;
    using Sys.Workflow.Cloud.Services.Events.Converters;
    using Sys.Workflow.Engine.Delegate.Events;

    /// <summary>
    /// 
    /// </summary>
    public class MessageProducerActivitiEventListener : IActivitiEventListener
    {

        private readonly EventConverterContext converterContext;

        private readonly ProcessEngineEventsAggregator eventsAggregator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converterContext"></param>
        /// <param name="eventsAggregator"></param>
        public MessageProducerActivitiEventListener(EventConverterContext converterContext, ProcessEngineEventsAggregator eventsAggregator)
        {
            this.converterContext = converterContext;
            this.eventsAggregator = eventsAggregator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        public virtual void OnEvent(IActivitiEvent @event)
        {
            IProcessEngineEvent newEvent = converterContext.From(@event);
            if (newEvent is object)
            {
                eventsAggregator.Add(newEvent);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool FailOnException
        {
            get
            {
                return false;
            }
        }
    }

}