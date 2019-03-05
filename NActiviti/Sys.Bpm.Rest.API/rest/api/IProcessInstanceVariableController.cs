﻿using Microsoft.AspNetCore.Mvc;
using org.activiti.cloud.services.api.commands;
using org.activiti.cloud.services.rest.api.resources;
using org.springframework.hateoas;
using System.Threading.Tasks;

namespace org.activiti.cloud.services.rest.api
{
    /// <summary>
    /// 流程实例过程变量管理RestAPI
    /// </summary>
    public interface IProcessInstanceVariableController
    {
        /// <summary>
        /// 读取流程实例变量列表,流程实例全局变量,作用域范围为整个工作流实例
        /// </summary>
        /// <param name="processInstanceId">流程实例id</param>
        /// <returns></returns>
        Task<Resources<ProcessVariableResource>> getVariables(string processInstanceId);

        /// <summary>
        /// 读取流程实例本地变量列表,本地变量具体指的是在处理某个任务时所涉及的变量,作用域范围仅限任务范围内.
        /// </summary>
        /// <param name="processInstanceId">流程实例id</param>
        /// <returns></returns>
        Task<Resources<ProcessVariableResource>> getVariablesLocal(string processInstanceId);

        /// <summary>
        /// 设置某个工作流实例变量
        /// </summary>
        /// <param name="processInstanceId">流程实例id</param>
        /// <param name="setTaskVariablesCmd">流程变量命令</param>
        /// <returns></returns>
        Task<IActionResult> setVariables(string processInstanceId, SetProcessVariablesCmd setTaskVariablesCmd);

        /// <summary>
        /// 移除某个工作流实例变量
        /// </summary>
        /// <param name="processInstanceId">流程实例id</param>
        /// <param name="removeProcessVariablesCmd">移除命令</param>
        /// <returns></returns>
        Task<IActionResult> removeVariables(string processInstanceId, RemoveProcessVariablesCmd removeProcessVariablesCmd);
    }
}