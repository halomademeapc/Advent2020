using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
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

        public static Vector2 RotateRight(this Vector2 v, int degrees)
        {
            for (int i = 0; i < degrees / 90; i++)
                v = v.RotateRight();
            return v;
        }

        public static Vector2 RotateLeft(this Vector2 v, int degrees)
        {
            for (int i = 0; i < degrees / 90; i++)
                v = v.RotateLeft();
            return v;
        }

        public static Vector2 RotateRight(this Vector2 v) => new Vector2(v.Y, -v.X);

        public static Vector2 RotateLeft(this Vector2 v) => new Vector2(-v.Y, v.X);

        public static float ManhattanDistance(this Vector2 v) => Math.Abs(v.X) + Math.Abs(v.Y);
    }
}
