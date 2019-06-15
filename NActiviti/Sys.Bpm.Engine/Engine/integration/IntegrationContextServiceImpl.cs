﻿using System.Collections.Generic;

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

namespace org.activiti.engine.integration
{
    using org.activiti.engine.impl.cmd.integration;
    using org.activiti.engine.impl.interceptor;
    using org.activiti.engine.impl.persistence.entity.integration;

    public class IntegrationContextServiceImpl : IIntegrationContextService
    {

        private ICommandExecutor commandExecutor;

        public IntegrationContextServiceImpl(ICommandExecutor commandExecutor)
        {
            this.commandExecutor = commandExecutor;
        }

        public virtual IList<IIntegrationContextEntity> findIntegrationContextByExecutionId(string executionId)
        {
            return commandExecutor.Execute(new RetrieveIntegrationContextsCmd(executionId));
        }

        public virtual void deleteIntegrationContext(IIntegrationContextEntity integrationContextEntity)
        {
            commandExecutor.Execute(new DeleteIntegrationContextCmd(integrationContextEntity));
        }
    }

}