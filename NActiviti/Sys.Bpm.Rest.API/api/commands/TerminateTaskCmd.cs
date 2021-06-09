﻿using Newtonsoft.Json;
using Sys.Workflow.Services.Api.Commands;
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

namespace Sys.Workflow.Cloud.Services.Api.Commands
{
    /// <summary>
    /// 任务更新命令
    /// </summary>
    public class TerminateTaskCmd : ICommand
    {
        private readonly string id = "terminateTaskCmd";
        private string _terminateReason;
        private WorkflowVariable outputVariables;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TerminateTaskCmd()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <param name="terminateReason">终止原因</param>
        //[JsonConstructor]
        public TerminateTaskCmd([JsonProperty("TaskId")] string taskId,
            [JsonProperty("Description")] string terminateReason,
            [JsonProperty("TerminateExecution")] bool terminateExecution,
            [JsonProperty("OutputVariables")] WorkflowVariable outputVariables) : this()
        {
            this.TaskId = taskId;
            this.TerminateReason = terminateReason;
            this.TerminateExecution = terminateExecution;
            this.outputVariables = outputVariables;
        }

        /// <summary>
        /// 命令id
        /// </summary>

        public virtual string Id
        {
            get => id;
        }

        /// <summary>
        /// 任务id
        /// </summary>

        public virtual string TaskId
        {
            get;
            set;
        }

        /// <summary>
        /// 任务终止原因
        /// </summary>

        public virtual string TerminateReason
        {
            get
            {
                return
#if DEBUG
                    _terminateReason = string.IsNullOrWhiteSpace(_terminateReason) ? "已强制终止任务" : _terminateReason;
# else
                _terminateReason;
#endif
            }
            set => _terminateReason = value;
        }

        /// <summary>
        /// 提交的数据
        /// </summary>
        public virtual WorkflowVariable OutputVariables
        {
            get
            {
                outputVariables ??= new WorkflowVariable();
                return outputVariables;
            }
            set
            {
                outputVariables = value;
            }
        }

        /// <summary>
        /// 同时终止任务
        /// </summary>
        public virtual bool TerminateExecution
        {
            get; set;
        }
        public IDictionary<string, object> TransientVariables { get; set; }
    }

}