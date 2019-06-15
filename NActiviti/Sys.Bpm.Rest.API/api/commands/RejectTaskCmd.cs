﻿using Newtonsoft.Json;
using org.activiti.services.api.commands;
using System;

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

namespace org.activiti.cloud.services.api.commands
{
    /// <summary>
    /// 任务更新命令
    /// </summary>
    public class RejectTaskCmd : ICommand
    {
        private readonly string id = "rejectTaskCmd";

        public RejectTaskCmd()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <param name="rejectReason">拒绝原因</param>
        /// <param name="variables">任务附件变量</param>
        //[JsonConstructor]
        public RejectTaskCmd([JsonProperty("TaskId")] string taskId,
            [JsonProperty("ReturnReason")] string rejectReason,
            [JsonProperty("Variables")] WorkflowVariable variables)
        {
            this.TaskId = taskId;
            this.RejectReason = rejectReason;
            this.Variables = variables;
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
        public virtual string TaskId { get; set; }

        /// <summary>
        /// 拒绝原因
        /// </summary>

        public virtual string RejectReason { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>

        public virtual WorkflowVariable Variables { get; set; }
    }
}