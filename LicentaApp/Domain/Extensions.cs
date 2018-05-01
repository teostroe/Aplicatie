using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace LicentaApp.Domain
{
    public static class Extensions
    {
        public static SelectList ToSelectList<T>(this IEnumerable<T> data, Expression<Func<T, string>> id, Expression<Func<T, object>> name) where T : class
        {
            return new SelectList(data, (id.Body as MemberExpression).Member.Name, (name.Body as MemberExpression).Member.Name);
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> data, Expression<Func<T, Guid>> id, Expression<Func<T, object>> name) where T : class
        {
            return new SelectList(data, (id.Body as MemberExpression).Member.Name, (name.Body as MemberExpression).Member.Name);
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> data, Expression<Func<T, int>> id, Expression<Func<T, object>> name) where T : class
        {
            return new SelectList(data, (id.Body as MemberExpression).Member.Name, (name.Body as MemberExpression).Member.Name);
        }

        public static void AddModelErrorsFrom(this ModelStateDictionary dictionary, IEnumerable<ValidationResult> validationResults)
        {
            foreach (var result in validationResults)
            {
                dictionary.AddModelError("ValidationService", result.ErrorMessage);
            }
        }

        public static Exception GetLatestInnerException(this Exception e)
        {
            if (e != null)
            {
                var result = e;
                while (result.InnerException != null) result = result.InnerException;
                return result;
            }
            return e;
        }

    }
}