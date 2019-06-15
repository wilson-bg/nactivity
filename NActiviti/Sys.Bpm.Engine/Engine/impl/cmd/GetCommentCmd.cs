﻿using System;

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

namespace org.activiti.engine.impl.cmd
{

    using org.activiti.engine.impl.interceptor;
    using org.activiti.engine.task;

    /// 
    [Serializable]
    public class GetCommentCmd : ICommand<IComment>
    {

        private const long serialVersionUID = 1L;
        protected internal string commentId;

        public GetCommentCmd(string commentId)
        {
            this.commentId = commentId;

            if (commentId is null)
            {
                throw new ActivitiIllegalArgumentException("commentId is null");
            }
        }

        public  virtual IComment  Execute(ICommandContext  commandContext)
        {
            return commandContext.CommentEntityManager.FindComment(commentId);
        }
    }

}