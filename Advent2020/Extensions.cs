using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Advent2020
{
    public static class Extensions
    {
        public static IEnumerable<TItem> ToSingleElementSequence<TItem>(this TItem item)
        {
            yield return item;
        }

        public static long Product(this IEnumerable<int> ints) => ints.Aggregate<int, long>(1, (total, next) => total * next);

        public static void Set<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression, TProperty value)
        {
            ((expression.Body as MemberExpression).Member as PropertyInfo).SetValue(model, value);
        }

        public static bool IsValid<TModel>(this TModel model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, results, true);
        }
    }
}
