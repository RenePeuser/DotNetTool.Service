using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotNetTool.Service.ArgumentCheck;

namespace DotNetTool.Service.Extensions
{
    public static class ExpressionExtensions
    {
        public static string NameOf(this Expression expression)
        {
            Throw.IfNull(() => expression);

            var lambdaExpression = expression.As<LambdaExpression>();

            if (lambdaExpression == null)
            {
                throw new ArgumentException("Expression is not a LambdaExpression");
            }

            string name = null;

            var memberExpression = lambdaExpression.Body.As<MemberExpression>();

            if (memberExpression != null)
            {
                name = memberExpression.Member.Name;
            }

            var unaryExpression = lambdaExpression.Body.As<UnaryExpression>();

            if (unaryExpression != null)
            {
                var member = unaryExpression.Operand.As<MemberExpression>();

                if (member != null)
                {
                    name = member.Member.Name;
                }
            }

            var methodCallExpression = lambdaExpression.Body.As<MethodCallExpression>();

            if (methodCallExpression != null)
            {
                name = methodCallExpression.Method.Name;
            }

            if (name == null)
            {
                throw new ArgumentException("Unknown expression type for extracting name.", nameof(expression));
            }

            return name;
        }

        public static Dictionary<string, Func<T, object>> ToCompiledExpressionWithInfo<T>(this Expression<Func<T, object>>[] expressions)
        {
            Throw.IfNull(() => expressions);

            var result = expressions.ToDictionary(item => item.NameOf(), item => item.Compile());

            return result;
        }
    }
}
