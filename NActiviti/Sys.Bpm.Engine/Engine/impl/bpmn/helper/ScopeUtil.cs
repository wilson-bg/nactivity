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

namespace Sys.Workflow.Engine.Impl.Bpmn.Helper
{
    using Sys.Workflow.Engine.Impl.Contexts;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;
    using Sys.Workflow.Engine.Impl.Util;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// 
    /// 
    public class ScopeUtil
    {

        /// <summary>
        /// we create a separate execution for each compensation handler invocation.
        /// </summary>
        public static void ThrowCompensationEvent(IList<ICompensateEventSubscriptionEntity> eventSubscriptions, IExecutionEntity execution, bool async)
        {

            IExecutionEntityManager executionEntityManager = Context.CommandContext.ExecutionEntityManager;

            // first spawn the compensating executions
            foreach (IEventSubscriptionEntity eventSubscription in eventSubscriptions)
            {
                IExecutionEntity compensatingExecution = null;

                // check whether compensating execution is already created (which is the case when compensating an embedded subprocess,
                // where the compensating execution is created when leaving the subprocess and holds snapshot data).
                if (eventSubscription.Configuration is not null)
                {
                    compensatingExecution = executionEntityManager.FindById<IExecutionEntity>(eventSubscription.Configuration);
                    compensatingExecution.Parent = compensatingExecution.ProcessInstance;
                    compensatingExecution.IsEventScope = false;
                }
                else
                {
                    compensatingExecution = executionEntityManager.CreateChildExecution(execution);
                    eventSubscription.Configuration = compensatingExecution.Id;
                }

            }

            // signal compensation events in reverse order of their 'created' timestamp
            eventSubscriptions.OrderBy(x => x, new ComparatorAnonymousInnerClass());

            foreach (ICompensateEventSubscriptionEntity compensateEventSubscriptionEntity in eventSubscriptions)
            {
                Context.CommandContext.EventSubscriptionEntityManager.EventReceived(compensateEventSubscriptionEntity, null, async);
            }
        }

        private class ComparatorAnonymousInnerClass : IComparer<IEventSubscriptionEntity>
        {
            public ComparatorAnonymousInnerClass()
            {
            }

            public int Compare(IEventSubscriptionEntity x, IEventSubscriptionEntity y)
            {
                return DateTime.Compare(x.Created, y.Created);
            }
        }

        public static void CreateCopyOfSubProcessExecutionForCompensation(IExecutionEntity subProcessExecution)
        {
            IEventSubscriptionEntityManager eventSubscriptionEntityManager = Context.CommandContext.EventSubscriptionEntityManager;
            IList<ICompensateEventSubscriptionEntity> compensateEventSubscriptions = eventSubscriptionEntityManager.FindCompensateEventSubscriptionsByExecutionId(subProcessExecution.Id);

            if (CollectionUtil.IsNotEmpty(compensateEventSubscriptions))
            {
                IExecutionEntity processInstanceExecutionEntity = subProcessExecution.ProcessInstance;

                IExecutionEntity eventScopeExecution = Context.CommandContext.ExecutionEntityManager.CreateChildExecution(processInstanceExecutionEntity);
                eventScopeExecution.IsActive = false;
                eventScopeExecution.IsEventScope = true;
                eventScopeExecution.CurrentFlowElement = subProcessExecution.CurrentFlowElement;

                // copy local variables to eventScopeExecution by value. This way,
                // the eventScopeExecution references a 'snapshot' of the local variables
                (new SubProcessVariableSnapshotter()).SetVariablesSnapshots(subProcessExecution, eventScopeExecution);

                // set event subscriptions to the event scope execution:
                foreach (ICompensateEventSubscriptionEntity eventSubscriptionEntity in compensateEventSubscriptions)
                {
                    eventSubscriptionEntityManager.Delete(eventSubscriptionEntity);

                    ICompensateEventSubscriptionEntity newSubscription = eventSubscriptionEntityManager.InsertCompensationEvent(eventScopeExecution, eventSubscriptionEntity.ActivityId);
                    newSubscription.Configuration = eventSubscriptionEntity.Configuration;
                    newSubscription.Created = eventSubscriptionEntity.Created;
                }

                ICompensateEventSubscriptionEntity eventSubscription = eventSubscriptionEntityManager.InsertCompensationEvent(processInstanceExecutionEntity, eventScopeExecution.CurrentFlowElement.Id);
                eventSubscription.Configuration = eventScopeExecution.Id;

            }
        }
    }
}