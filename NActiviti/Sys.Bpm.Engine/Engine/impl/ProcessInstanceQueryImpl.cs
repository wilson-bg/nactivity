﻿using System;
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

namespace org.activiti.engine.impl
{
    using Newtonsoft.Json.Linq;
    using org.activiti.engine.impl.context;
    using org.activiti.engine.impl.interceptor;
    using org.activiti.engine.impl.persistence.entity;
    using org.activiti.engine.runtime;


    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    [Serializable]
    public class ProcessInstanceQueryImpl : AbstractVariableQueryImpl<IProcessInstanceQuery, IProcessInstance>, IProcessInstanceQuery
    {
        private const long serialVersionUID = 1L;
        protected internal string executionId;
        protected internal string businessKey;
        protected internal bool? includeChildExecutionsWithBusinessKeyQuery;

        protected internal string processDefinitionId_Renamed;

        protected internal string[] processDefinitionIds_Renamed;

        protected internal string processDefinitionCategory_Renamed;

        protected internal string processDefinitionName_Renamed;

        protected internal int? processDefinitionVersion_Renamed;

        protected internal string[] processInstanceIds_Renamed;

        protected internal string processDefinitionKey_Renamed;

        protected internal string[] processDefinitionKeys_Renamed;

        protected internal string deploymentId_Renamed;
        protected internal IList<string> deploymentIds;

        protected internal string superProcessInstanceId_Renamed;

        protected internal string subProcessInstanceId_Renamed;

        protected internal bool? excludeSubprocesses_Renamed;

        protected internal string involvedUser_Renamed;
        protected internal ISuspensionState suspensionState;

        protected internal bool? includeProcessVariables_Renamed;
        protected internal int? processInstanceVariablesLimit;

        protected internal bool? withJobException_Renamed;
        protected internal string name;
        protected internal string nameLike;
        protected internal string nameLikeIgnoreCase;

        protected internal string locale_Renamed;

        protected internal bool? withLocalizationFallback_Renamed;

        protected internal string tenantId;
        protected internal string tenantIdLike;
        protected internal bool? withoutTenantId;

        protected internal IList<IProcessInstanceQuery> orQueryObjects = new List<IProcessInstanceQuery>();
        protected internal ProcessInstanceQueryImpl currentOrQueryObject = null;
        protected internal bool inOrStatement = false;

        protected internal DateTime? startedBefore_Renamed;
        protected internal DateTime? startedAfter_Renamed;

        protected internal string startedBy_Renamed;

        // Unused, see dynamic query
        protected internal string activityId;
        protected internal IList<EventSubscriptionQueryValue> eventSubscriptions;
        protected internal bool? onlyChildExecutions;
        protected internal bool? onlyProcessInstanceExecutions;
        protected internal bool? onlySubProcessExecutions;
        protected internal string rootProcessInstanceId;
        protected internal bool onlyProcessInstances = true;

        public ProcessInstanceQueryImpl()
        {
        }

        public ProcessInstanceQueryImpl(ICommandContext commandContext) : base(commandContext)
        {
        }

        public ProcessInstanceQueryImpl(ICommandExecutor commandExecutor) : base(commandExecutor)
        {
        }

        public virtual IProcessInstanceQuery processInstanceId(string processInstanceId)
        {
            //if (ReferenceEquals(processInstanceId, null))
            //{
            //    throw new ActivitiIllegalArgumentException("Process instance id is null");
            //}
            if (string.IsNullOrWhiteSpace(processInstanceId))
            {
                this.executionId = null;
                if (this.currentOrQueryObject != null)
                {
                    currentOrQueryObject.executionId = null;
                }
                return this;
            }
            if (inOrStatement)
            {
                this.currentOrQueryObject.executionId = processInstanceId;
            }
            else
            {
                this.executionId = processInstanceId;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processInstanceIds(string[] processInstanceIds)
        {
            //if (processInstanceIds == null)
            //{
            //    throw new ActivitiIllegalArgumentException("Set of process instance ids is null");
            //}
            //if (processInstanceIds.Count == 0)
            //{
            //    throw new ActivitiIllegalArgumentException("Set of process instance ids is empty");
            //}
            if ((processInstanceIds ?? new string[0]).Length == 0)
            {
                this.processInstanceIds_Renamed = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.processInstanceIds_Renamed = null;
                }
                return this;
            }
            if (inOrStatement)
            {
                this.currentOrQueryObject.processInstanceIds_Renamed = processInstanceIds;
            }
            else
            {
                this.processInstanceIds_Renamed = processInstanceIds;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processInstanceBusinessKey(string businessKey)
        {
            if (string.IsNullOrWhiteSpace(businessKey))
            {
                this.businessKey = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.businessKey = null;
                }
                //throw new ActivitiIllegalArgumentException("Business key is null");
                return this;
            }
            if (inOrStatement)
            {
                this.currentOrQueryObject.businessKey = businessKey;
            }
            else
            {
                this.businessKey = businessKey;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processInstanceBusinessKey(string businessKey, string processDefinitionKey)
        {
            if (string.IsNullOrWhiteSpace(businessKey))
            {
                this.businessKey = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.businessKey = null;
                }
                return this;
                //throw new ActivitiIllegalArgumentException("Business key is null");
            }
            if (inOrStatement)
            {
                throw new ActivitiIllegalArgumentException("This method is not supported in an OR statement");
            }

            this.businessKey = businessKey;
            this.processDefinitionKey_Renamed = processDefinitionKey;
            return this;
        }

        public virtual IProcessInstanceQuery processInstanceTenantId(string tenantId)
        {
            if (string.IsNullOrWhiteSpace(tenantId))
            {
                this.tenantId = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.tenantId = null;
                }
                return this;
                //throw new ActivitiIllegalArgumentException("process instance tenant id is null");
            }
            if (inOrStatement)
            {
                this.currentOrQueryObject.tenantId = tenantId;
            }
            else
            {
                this.tenantId = tenantId;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processInstanceTenantIdLike(string tenantIdLike)
        {
            if (string.IsNullOrWhiteSpace(tenantIdLike))
            {
                this.tenantIdLike = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.tenantIdLike = null;
                }
                return this;
                //throw new ActivitiIllegalArgumentException("process instance tenant id is null");
            }
            if (inOrStatement)
            {
                this.currentOrQueryObject.tenantIdLike = tenantIdLike;
            }
            else
            {
                this.tenantIdLike = tenantIdLike;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processInstanceWithoutTenantId()
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.withoutTenantId = true;
            }
            else
            {
                this.withoutTenantId = true;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processDefinitionCategory(string processDefinitionCategory)
        {
            if (string.IsNullOrWhiteSpace(processDefinitionCategory))
            {
                this.processDefinitionCategory_Renamed = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.processDefinitionCategory_Renamed = null;
                }
                return this;
                //throw new ActivitiIllegalArgumentException("Process definition category is null");
            }

            if (inOrStatement)
            {
                this.currentOrQueryObject.processDefinitionCategory_Renamed = processDefinitionCategory;
            }
            else
            {
                this.processDefinitionCategory_Renamed = processDefinitionCategory;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processDefinitionName(string processDefinitionName)
        {
            if (string.IsNullOrWhiteSpace(processDefinitionName))
            {
                this.processDefinitionName_Renamed = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.processDefinitionName_Renamed = null;
                }
                return this;
                //throw new ActivitiIllegalArgumentException("Process definition name is null");
            }

            if (inOrStatement)
            {
                this.currentOrQueryObject.processDefinitionName_Renamed = processDefinitionName;
            }
            else
            {
                this.processDefinitionName_Renamed = processDefinitionName;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processDefinitionVersion(int? processDefinitionVersion)
        {
            if (processDefinitionVersion.HasValue == false)
            {
                this.processDefinitionVersion_Renamed = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.processDefinitionVersion_Renamed = null;
                }
                return this;
                //throw new ActivitiIllegalArgumentException("Process definition version is null");
            }

            if (inOrStatement)
            {
                this.currentOrQueryObject.processDefinitionVersion_Renamed = processDefinitionVersion;
            }
            else
            {
                this.processDefinitionVersion_Renamed = processDefinitionVersion;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processDefinitionId(string processDefinitionId)
        {
            if (string.IsNullOrWhiteSpace(processDefinitionId))
            {
                this.processDefinitionId_Renamed = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.processDefinitionId_Renamed = null;
                }
                return this;
                //throw new ActivitiIllegalArgumentException("Process definition id is null");
            }

            if (inOrStatement)
            {
                this.currentOrQueryObject.processDefinitionId_Renamed = processDefinitionId;
            }
            else
            {
                this.processDefinitionId_Renamed = processDefinitionId;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processDefinitionIds(string[] processDefinitionIds)
        {
            if ((processDefinitionIds ?? new string[0]).Length == 0)
            {
                this.processDefinitionIds_Renamed = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.processDefinitionIds_Renamed = null;
                }
                return this;
                //throw new ActivitiIllegalArgumentException("Set of process definition ids is null");
            }

            if (inOrStatement)
            {
                this.currentOrQueryObject.processDefinitionIds_Renamed = processDefinitionIds;
            }
            else
            {
                this.processDefinitionIds_Renamed = processDefinitionIds;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processDefinitionKey(string processDefinitionKey)
        {
            if (string.IsNullOrWhiteSpace(processDefinitionKey))
            {
                this.processDefinitionKey_Renamed = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.processDefinitionKey_Renamed = null;
                }
                return this;
                //throw new ActivitiIllegalArgumentException("Process definition key is null");
            }

            if (inOrStatement)
            {
                this.currentOrQueryObject.processDefinitionKey_Renamed = processDefinitionKey;
            }
            else
            {
                this.processDefinitionKey_Renamed = processDefinitionKey;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processDefinitionKeys(string[] processDefinitionKeys)
        {
            if ((processDefinitionKeys ?? new string[0]).Length == 0)
            {
                this.processDefinitionKeys_Renamed = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.processDefinitionKeys_Renamed = null;
                }
                return this;
                //throw new ActivitiIllegalArgumentException("Set of process definition keys is null");
            }

            if (inOrStatement)
            {
                this.currentOrQueryObject.processDefinitionKeys_Renamed = processDefinitionKeys;
            }
            else
            {
                this.processDefinitionKeys_Renamed = processDefinitionKeys;
            }
            return this;
        }

        public virtual IProcessInstanceQuery deploymentId(string deploymentId)
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.deploymentId_Renamed = deploymentId;
            }
            else
            {
                this.deploymentId_Renamed = deploymentId;
            }
            return this;
        }

        public virtual IProcessInstanceQuery deploymentIdIn(IList<string> deploymentIds)
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.deploymentIds = deploymentIds;
            }
            else
            {
                this.deploymentIds = deploymentIds;
            }
            return this;
        }

        public virtual IProcessInstanceQuery superProcessInstanceId(string superProcessInstanceId)
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.superProcessInstanceId_Renamed = superProcessInstanceId;
            }
            else
            {
                this.superProcessInstanceId_Renamed = superProcessInstanceId;
            }
            return this;
        }

        public virtual IProcessInstanceQuery subProcessInstanceId(string subProcessInstanceId)
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.subProcessInstanceId_Renamed = subProcessInstanceId;
            }
            else
            {
                this.subProcessInstanceId_Renamed = subProcessInstanceId;
            }
            return this;
        }

        public virtual IProcessInstanceQuery excludeSubprocesses(bool excludeSubprocesses)
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.excludeSubprocesses_Renamed = excludeSubprocesses;
            }
            else
            {
                this.excludeSubprocesses_Renamed = excludeSubprocesses;
            }
            return this;
        }

        public virtual IProcessInstanceQuery involvedUser(string involvedUser)
        {
            if (string.IsNullOrWhiteSpace(involvedUser))
            {
                this.involvedUser_Renamed = null;
                if (this.currentOrQueryObject != null)
                {
                    this.currentOrQueryObject.involvedUser_Renamed = null;
                }
                return this;
                //throw new ActivitiIllegalArgumentException("Involved user is null");
            }

            if (inOrStatement)
            {
                this.currentOrQueryObject.involvedUser_Renamed = involvedUser;
            }
            else
            {
                this.involvedUser_Renamed = involvedUser;
            }
            return this;
        }

        public virtual IProcessInstanceQuery active()
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.suspensionState = SuspensionStateProvider.ACTIVE;
            }
            else
            {
                this.suspensionState = SuspensionStateProvider.ACTIVE;
            }
            return this;
        }

        public virtual IProcessInstanceQuery suspended()
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.suspensionState = SuspensionStateProvider.SUSPENDED;
            }
            else
            {
                this.suspensionState = SuspensionStateProvider.SUSPENDED;
            }
            return this;
        }

        public virtual IProcessInstanceQuery includeProcessVariables()
        {
            this.includeProcessVariables_Renamed = true;
            return this;
        }

        public virtual IProcessInstanceQuery limitProcessInstanceVariables(int? processInstanceVariablesLimit)
        {
            this.processInstanceVariablesLimit = processInstanceVariablesLimit;
            return this;
        }

        public virtual int? ProcessInstanceVariablesLimit
        {
            get
            {
                return processInstanceVariablesLimit;
            }
        }

        public virtual IProcessInstanceQuery withJobException()
        {
            this.withJobException_Renamed = true;
            return this;
        }

        public virtual IProcessInstanceQuery processInstanceName(string name)
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.name = name;
            }
            else
            {
                this.name = name;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processInstanceNameLike(string nameLike)
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.nameLike = nameLike;
            }
            else
            {
                this.nameLike = nameLike;
            }
            return this;
        }

        public virtual IProcessInstanceQuery processInstanceNameLikeIgnoreCase(string nameLikeIgnoreCase)
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.nameLikeIgnoreCase = nameLikeIgnoreCase.ToLower();
            }
            else
            {
                this.nameLikeIgnoreCase = nameLikeIgnoreCase.ToLower();
            }
            return this;
        }

        public virtual IProcessInstanceQuery or()
        {
            if (inOrStatement)
            {
                throw new ActivitiException("the query is already in an or statement");
            }

            inOrStatement = true;
            currentOrQueryObject = new ProcessInstanceQueryImpl();
            orQueryObjects.Add(currentOrQueryObject);
            return this;
        }

        public virtual IProcessInstanceQuery endOr()
        {
            if (!inOrStatement)
            {
                throw new ActivitiException("endOr() can only be called after calling or()");
            }

            inOrStatement = false;
            currentOrQueryObject = null;
            return this;
        }

        public override IProcessInstanceQuery variableValueEquals(string variableName, object variableValue)
        {
            if (inOrStatement)
            {
                currentOrQueryObject.variableValueEquals(variableName, variableValue, false);
                return this;
            }
            else
            {
                return variableValueEquals(variableName, variableValue, false);
            }
        }

        public override IProcessInstanceQuery variableValueNotEquals(string variableName, object variableValue)
        {
            if (inOrStatement)
            {
                currentOrQueryObject.variableValueNotEquals(variableName, variableValue, false);
                return this;
            }
            else
            {
                return variableValueNotEquals(variableName, variableValue, false);
            }
        }

        public override IProcessInstanceQuery variableValueEquals(object variableValue)
        {
            if (inOrStatement)
            {
                currentOrQueryObject.variableValueEquals(variableValue, false);
                return this;
            }
            else
            {
                return variableValueEquals(variableValue, false);
            }
        }

        public override IProcessInstanceQuery variableValueEqualsIgnoreCase(string name, string value)
        {
            if (inOrStatement)
            {
                currentOrQueryObject.variableValueEqualsIgnoreCase(name, value, false);
                return this;
            }
            else
            {
                return variableValueEqualsIgnoreCase(name, value, false);
            }
        }

        public override IProcessInstanceQuery variableValueNotEqualsIgnoreCase(string name, string value)
        {
            if (inOrStatement)
            {
                currentOrQueryObject.variableValueNotEqualsIgnoreCase(name, value, false);
                return this;
            }
            else
            {
                return variableValueNotEqualsIgnoreCase(name, value, false);
            }
        }

        public override IProcessInstanceQuery variableValueGreaterThan(string name, object value)
        {
            if (inOrStatement)
            {
                currentOrQueryObject.variableValueGreaterThan(name, value, false);
                return this;
            }
            else
            {
                return variableValueGreaterThan(name, value, false);
            }
        }

        public override IProcessInstanceQuery variableValueGreaterThanOrEqual(string name, object value)
        {
            if (inOrStatement)
            {
                currentOrQueryObject.variableValueGreaterThanOrEqual(name, value, false);
                return this;
            }
            else
            {
                return variableValueGreaterThanOrEqual(name, value, false);
            }
        }

        public override IProcessInstanceQuery variableValueLessThan(string name, object value)
        {
            if (inOrStatement)
            {
                currentOrQueryObject.variableValueLessThan(name, value, false);
                return this;
            }
            else
            {
                return variableValueLessThan(name, value, false);
            }
        }

        public override IProcessInstanceQuery variableValueLessThanOrEqual(string name, object value)
        {
            if (inOrStatement)
            {
                currentOrQueryObject.variableValueLessThanOrEqual(name, value, false);
                return this;
            }
            else
            {
                return variableValueLessThanOrEqual(name, value, false);
            }
        }

        public override IProcessInstanceQuery variableValueLike(string name, string value)
        {
            if (inOrStatement)
            {
                currentOrQueryObject.variableValueLike(name, value, false);
                return this;
            }
            else
            {
                return variableValueLike(name, value, false);
            }
        }

        public override IProcessInstanceQuery variableValueLikeIgnoreCase(string name, string value)
        {
            if (inOrStatement)
            {
                currentOrQueryObject.variableValueLikeIgnoreCase(name, value, false);
                return this;
            }
            else
            {
                return variableValueLikeIgnoreCase(name, value, false);
            }
        }

        public virtual IProcessInstanceQuery locale(string locale)
        {
            this.locale_Renamed = locale;
            return this;
        }

        public virtual IProcessInstanceQuery withLocalizationFallback()
        {
            withLocalizationFallback_Renamed = true;
            return this;
        }

        public virtual IProcessInstanceQuery startedBefore(DateTime beforeTime)
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.startedBefore_Renamed = beforeTime;
            }
            else
            {
                this.startedBefore_Renamed = beforeTime;
            }
            return this;
        }

        public virtual IProcessInstanceQuery startedAfter(DateTime afterTime)
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.startedAfter_Renamed = afterTime;
            }
            else
            {
                this.startedAfter_Renamed = afterTime;
            }
            return this;
        }

        public virtual IProcessInstanceQuery startedBy(string userId)
        {
            if (inOrStatement)
            {
                this.currentOrQueryObject.startedBy_Renamed = userId;
            }
            else
            {
                this.startedBy_Renamed = userId;
            }
            return this;
        }

        public virtual IProcessInstanceQuery orderByProcessInstanceId()
        {
            this.orderProperty = ProcessInstanceQueryProperty.PROCESS_INSTANCE_ID;
            return this;
        }

        public virtual IProcessInstanceQuery orderByProcessDefinitionId()
        {
            this.orderProperty = ProcessInstanceQueryProperty.PROCESS_DEFINITION_ID;
            return this;
        }

        public virtual IProcessInstanceQuery orderByProcessDefinitionKey()
        {
            this.orderProperty = ProcessInstanceQueryProperty.PROCESS_DEFINITION_KEY;
            return this;
        }

        public virtual IProcessInstanceQuery orderByTenantId()
        {
            this.orderProperty = ProcessInstanceQueryProperty.TENANT_ID;
            return this;
        }

        public virtual string MssqlOrDB2OrderBy
        {
            get
            {
                string specialOrderBy = base.OrderBy;
                if (!ReferenceEquals(specialOrderBy, null) && specialOrderBy.Length > 0)
                {
                    specialOrderBy = specialOrderBy.Replace("RES.", "TEMPRES_");
                    specialOrderBy = specialOrderBy.Replace("ProcessDefinitionKey", "TEMPP_KEY_");
                    specialOrderBy = specialOrderBy.Replace("ProcessDefinitionId", "TEMPP_ID_");
                }
                return specialOrderBy;
            }
        }

        // results /////////////////////////////////////////////////////////////////

        public override long executeCount(ICommandContext commandContext)
        {
            checkQueryOk();
            ensureVariablesInitialized();
            return commandContext.ExecutionEntityManager.findProcessInstanceCountByQueryCriteria(this);
        }

        public override IList<IProcessInstance> executeList(ICommandContext commandContext, Page page)
        {
            checkQueryOk();
            ensureVariablesInitialized();
            IList<IProcessInstance> processInstances = null;
            if (includeProcessVariables_Renamed.GetValueOrDefault())
            {
                processInstances = commandContext.ExecutionEntityManager.findProcessInstanceAndVariablesByQueryCriteria(this);
            }
            else
            {
                processInstances = commandContext.ExecutionEntityManager.findProcessInstanceByQueryCriteria(this);
            }

            if (Context.ProcessEngineConfiguration.PerformanceSettings.EnableLocalization)
            {
                foreach (IProcessInstance processInstance in processInstances)
                {
                    localize(processInstance);
                }
            }

            return processInstances;
        }

        protected internal override void ensureVariablesInitialized()
        {
            base.ensureVariablesInitialized();

            foreach (ProcessInstanceQueryImpl orQueryObject in orQueryObjects)
            {
                orQueryObject.ensureVariablesInitialized();
            }
        }

        protected internal virtual void localize(IProcessInstance processInstance)
        {
            IExecutionEntity processInstanceExecution = (IExecutionEntity)processInstance;
            processInstanceExecution.LocalizedName = null;
            processInstanceExecution.LocalizedDescription = null;

            if (!string.IsNullOrWhiteSpace(locale_Renamed))
            {
                string processDefinitionId = processInstanceExecution.ProcessDefinitionId;
                if (!string.IsNullOrWhiteSpace(processDefinitionId))
                {
                    JToken languageNode = Context.getLocalizationElementProperties(locale_Renamed, processInstanceExecution.ProcessDefinitionKey, processDefinitionId, withLocalizationFallback_Renamed.GetValueOrDefault());
                    if (languageNode != null)
                    {
                        JToken languageNameNode = languageNode[DynamicBpmnConstants_Fields.LOCALIZATION_NAME];
                        if (languageNameNode != null)
                        {
                            processInstanceExecution.LocalizedName = languageNameNode.ToString();
                        }

                        JToken languageDescriptionNode = languageNode[DynamicBpmnConstants_Fields.LOCALIZATION_DESCRIPTION];
                        if (languageDescriptionNode != null)
                        {
                            processInstanceExecution.LocalizedDescription = languageDescriptionNode.ToString();
                        }
                    }
                }
            }
        }

        // getters /////////////////////////////////////////////////////////////////

        public virtual bool? OnlyProcessInstances
        {
            get
            {
                return onlyProcessInstances; // See dynamic query in runtime.mapping.xml
            }
            set
            {
                onlyProcessInstances = value.GetValueOrDefault(true);
            }
        }

        public virtual string ProcessInstanceId
        {
            get
            {
                return executionId;
            }
            set => processInstanceId(value);
        }

        public virtual string RootProcessInstanceId
        {
            get
            {
                return rootProcessInstanceId;
            }
            set => rootProcessInstanceId = value;
        }

        public virtual string[] ProcessInstanceIds
        {
            get
            {
                return processInstanceIds_Renamed;
            }
            set => processInstanceIds(value);
        }

        public virtual string BusinessKey
        {
            get
            {
                return businessKey;
            }
            set => processInstanceBusinessKey(value);
        }

        public virtual bool? IncludeChildExecutionsWithBusinessKeyQuery
        {
            get
            {
                return includeChildExecutionsWithBusinessKeyQuery;
            }
            set => includeChildExecutionsWithBusinessKeyQuery = value;
        }

        public virtual string ProcessDefinitionId
        {
            get
            {
                return processDefinitionId_Renamed;
            }
            set => processDefinitionId(value);
        }

        public virtual string[] ProcessDefinitionIds
        {
            get
            {
                return processDefinitionIds_Renamed;
            }
            set => processDefinitionIds(value);
        }

        public virtual string ProcessDefinitionCategory
        {
            get
            {
                return processDefinitionCategory_Renamed;
            }
            set => processDefinitionCategory(value);
        }

        public virtual string ProcessDefinitionName
        {
            get
            {
                return processDefinitionName_Renamed;
            }
            set => processDefinitionName(value);
        }

        public virtual int? ProcessDefinitionVersion
        {
            get
            {
                return processDefinitionVersion_Renamed;
            }
            set => processDefinitionVersion(value);
        }

        public virtual string ProcessDefinitionKey
        {
            get
            {
                return processDefinitionKey_Renamed;
            }
            set => processDefinitionKey(value);
        }

        public virtual string[] ProcessDefinitionKeys
        {
            get
            {
                return processDefinitionKeys_Renamed;
            }
            set => processDefinitionKeys(value);
        }

        public virtual string ActivityId
        {
            get
            {
                return null; // Unused, see dynamic query
            }
        }

        public virtual string SuperProcessInstanceId
        {
            get
            {
                return superProcessInstanceId_Renamed;
            }
            set => superProcessInstanceId(value);
        }

        public virtual string SubProcessInstanceId
        {
            get
            {
                return subProcessInstanceId_Renamed;
            }
            set => subProcessInstanceId(value);
        }

        public virtual bool? ExcludeSubprocesses
        {
            get
            {
                return excludeSubprocesses_Renamed;
            }
            set => excludeSubprocesses(value.GetValueOrDefault());
        }

        public virtual string InvolvedUser
        {
            get
            {
                return involvedUser_Renamed;
            }
            set => involvedUser(value);
        }

        public virtual ISuspensionState SuspensionState
        {
            get
            {
                return suspensionState;
            }
            set
            {
                this.suspensionState = value;
            }
        }


        public virtual IList<EventSubscriptionQueryValue> EventSubscriptions
        {
            get
            {
                return eventSubscriptions;
            }
            set
            {
                this.eventSubscriptions = value;
            }
        }


        public virtual string TenantId
        {
            get
            {
                return tenantId;
            }
            set => processInstanceTenantId(value);
        }

        public virtual string TenantIdLike
        {
            get
            {
                return tenantIdLike;
            }
            set => processInstanceTenantIdLike(value);
        }

        public virtual bool? WithoutTenantId
        {
            get
            {
                return withoutTenantId;
            }
            set
            {
                if (value.GetValueOrDefault())
                {
                    processInstanceWithoutTenantId();
                }
                else
                {
                    withoutTenantId = false;
                    if (currentOrQueryObject != null)
                    {
                        currentOrQueryObject.withoutTenantId = false;
                    }
                }
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

        public virtual string NameLike
        {
            get
            {
                return nameLike;
            }
            set
            {
                this.nameLike = value;
            }
        }



        public virtual string ExecutionId
        {
            get
            {
                return executionId;
            }
            set => processInstanceId(value);
        }

        public virtual string DeploymentId
        {
            get
            {
                return deploymentId_Renamed;
            }
            set => deploymentId(value);
        }

        public virtual IList<string> DeploymentIds
        {
            get
            {
                return deploymentIds;
            }
            set => deploymentIdIn(value);
        }

        public virtual bool? IncludeProcessVariables
        {
            get
            {
                return includeProcessVariables_Renamed;
            }
            set => includeProcessVariables_Renamed = value;
        }

        public virtual bool? IsWithException
        {
            get
            {
                return withJobException_Renamed;
            }
            set => withJobException_Renamed = value;
        }

        public virtual string NameLikeIgnoreCase
        {
            get
            {
                return nameLikeIgnoreCase;
            }
            set => processInstanceNameLikeIgnoreCase(value);
        }

        public virtual IList<IProcessInstanceQuery> OrQueryObjects
        {
            get
            {
                return orQueryObjects;
            }
        }

        /// <summary>
        /// Methods needed for ibatis because of re-use of query-xml for executions. ExecutionQuery contains a parentId property.
        /// </summary>

        public virtual string ParentId
        {
            get
            {
                return null;
            }
        }

        public virtual bool? OnlyChildExecutions
        {
            get
            {
                return onlyChildExecutions;
            }
            set => onlyChildExecutions = value;
        }

        public virtual bool? OnlyProcessInstanceExecutions
        {
            get
            {
                return onlyProcessInstanceExecutions;
            }
            set => onlyProcessInstanceExecutions = value;
        }

        public virtual bool? OnlySubProcessExecutions
        {
            get
            {
                return onlySubProcessExecutions;
            }
            set => onlySubProcessExecutions = value;
        }

        public virtual DateTime? StartedBefore
        {
            get
            {
                return startedBefore_Renamed;
            }
            set
            {
                this.startedBefore_Renamed = value;
            }
        }


        public virtual DateTime? StartedAfter
        {
            get
            {
                return startedAfter_Renamed;
            }
            set
            {
                this.startedAfter_Renamed = value;
            }
        }


        public virtual string StartedBy
        {
            get
            {
                return startedBy_Renamed;
            }
            set
            {
                this.startedBy_Renamed = value;
            }
        }

    }

}