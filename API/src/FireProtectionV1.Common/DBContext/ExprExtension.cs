using System;
using System.Linq.Expressions;

namespace FireProtectionV1.Common.DBContext
{
    public static class ExprExtension
    {
        /// <summary>
        /// 初始化一个恒为真的表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            Expression<Func<T, bool>> result = x => true;
            return result;
        }
        /// <summary>
        /// 初始化一个恒为假的表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            Expression<Func<T, bool>> result = x => false;
            return result;
        }

        /// <summary>
        /// 有条件拼接And
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceExpr">原表达式</param>
        /// <param name="isAppend">是否需要拼接</param>
        /// <param name="express">要拼接的表达式</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> IfAnd<T>(this Expression<Func<T, bool>> sourceExpr, bool isAppend, Expression<Func<T, bool>> express)
            where T : EntityBase
        {
            return isAppend ? sourceExpr.And(express) : sourceExpr;
        }
        /// <summary>
        /// 有条件拼接Or
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceExpr">原表达式</param>
        /// <param name="isAppend">是否需要拼接</param>
        /// <param name="express">要拼接的表达式</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> IfOr<T>(this Expression<Func<T, bool>> sourceExpr, bool isAppend, Expression<Func<T, bool>> express)
             where T : EntityBase
        {
            return isAppend ? sourceExpr.Or(express) : sourceExpr;
        }
        /// <summary>
        /// 拼接Or表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (null == expr2) return expr1;//表达式2为null时直接返回表达式1
            return expr1.AndAlso<T>(expr2, Expression.OrElse);
        }
        /// <summary>
        /// 拼接And表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (null == expr2) return expr1;//表达式2为null时直接返回表达式1
            return expr1.AndAlso<T>(expr2, Expression.AndAlso);
        }

        /// <summary>
        /// 合并表达式以及参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2,
        Func<Expression, Expression, BinaryExpression> func)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(
                func(left, right), parameter);
        }
    }

    /// <summary>
    /// 替换表达式的访问器
    /// </summary>
    public class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }
    }
}
