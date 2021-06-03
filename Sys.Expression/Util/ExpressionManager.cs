﻿using Spring.Expressions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sys.Expressions
{
    public static class ExpressionManager
    {
        private static readonly Regex EXPTOKEN_PATTERN = new(@"[#]{(([\p{L}\p{N}_]+)(\.[\p{L}\p{N}_]+)*)}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex EXPRESSION_PATTERN = new("#{(.*?)}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static object GetValue(object context, string expression, IDictionary<string, object> variables)
        {
            string expr = expression;
            if (EXPTOKEN_PATTERN.IsMatch(expr))
            {
                expr = EXPTOKEN_PATTERN.Replace(expr, match =>
                {
                    var token = match.Groups[1].Value;

                    if (!variables.ContainsKey(token))
                    {
                        return "";
                    }

                    return $"{variables[token]}";
                });
            }
            else if (EXPRESSION_PATTERN.IsMatch(expr))
            {
                expr = EXPRESSION_PATTERN.Replace(expr, m =>
                {
                    var expression = m.Groups[1].Value;

                    return GetValue(context, variables, expression)?.ToString() ?? "";
                });
            }

            object res = GetValue(context, variables, expr);

            return res;

            static object GetValue(object context, IDictionary<string, object> variables, string expr)
            {
                IExpression express = Expression.Parse(expr);

                var res = express.GetValue(context, variables);
                return res;
            }
        }
    }
}
