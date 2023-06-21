using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CtaCargo.CctImportacao.Infrastructure.Data
{
    public class QueryJunction<T>
    {
        List<Expression<Func<T, bool>>> lista;

        public QueryJunction()
        {
            lista = new List<Expression<Func<T, bool>>>();
        }

        public void Add(Expression<Func<T, bool>> predicate) {
            lista.Add(predicate);
        }

        public Expression<Func<T, bool>> ToPredicate()
        {
            if (lista.Count > 1)
            {
                var mainPre = lista[0];
                for( int i = 1; i < lista.Count; i++)
                {
                    mainPre = AndAlso(mainPre, lista[i]);
                }
                return mainPre;
            }
            if (lista.Count == 1)
                return lista[0];
            return null;
        }

        private Expression<Func<T, bool>> AndAlso(
            Expression<Func<T, bool>> expr1, 
            Expression<Func<T, bool>> expr2)
        {
            // need to detect whether they use the same
            // parameter instance; if not, they need fixing
            ParameterExpression param = expr1.Parameters[0];
            if (ReferenceEquals(param, expr2.Parameters[0]))
            {
                // simple version
                return Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(expr1.Body, expr2.Body), param);
            }
            // otherwise, keep expr1 "as is" and invoke expr2
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    expr1.Body,
                    Expression.Invoke(expr2, param)), param);
        }

    }
}