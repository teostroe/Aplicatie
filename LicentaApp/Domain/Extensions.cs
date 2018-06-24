using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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

        public static SelectList ToSelectList(this Type enumType, params object[] valuesToExclude)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("The type must be an enumeration");
            }

            return new SelectList(Enum.GetValues(enumType)
                    .Cast<object>()
                    .Where(x => valuesToExclude.IsNullOrEmpty() || valuesToExclude.Contains(x) == false)
                    .Select(value =>
                    {
                        var text = EnumHelper.GetDisplayName(enumType, value);
                        return new { Text = text, Value = Convert.ChangeType(value, Enum.GetUnderlyingType(enumType)) };
                    }),
                "Value",
                "Text");
        }

        public static SelectList ToSelectListWithValue(this Type enumType, object selectedValue, params object[] valuesToExclude)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("The type must be an enumeration");
            }

            return new SelectList(Enum.GetValues(enumType)
                    .Cast<object>()
                    .Where(x => valuesToExclude.IsNullOrEmpty() || valuesToExclude.Contains(x) == false)
                    .Select(value =>
                    {
                        var text = EnumHelper.GetDisplayName(enumType, value);
                        return new { Text = text, Value = Convert.ChangeType(value, Enum.GetUnderlyingType(enumType)) };
                    }),
                "Value",
                "Text", selectedValue);
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

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrEmpty(this ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> data, int currentPage)
        {
            if (data.Any())
            {
                return data.Skip((currentPage - 1) * AppConstants.Pagination.ElementsOnPage).Take(AppConstants.Pagination.ElementsOnPage);
            }
            return data;
        }

        public static IEnumerable<T> ToPagedList<T>(this IQueryable<T> data, int? currentPage)
        {
            if (!currentPage.HasValue)
            {
                currentPage = 1;
            }
            return data.ToList().Skip((currentPage.Value - 1) * AppConstants.Pagination.ElementsOnPage).Take(AppConstants.Pagination.ElementsOnPage);
        }

        public static void InitializePagination(this ViewDataDictionary viewData, int? currentPage, int totalData, ControllerContext controllerContext)
        {
            if (!currentPage.HasValue) { currentPage = 1; }
            viewData.Add(AppConstants.Pagination.EnablePagination, true);
            viewData.Add(AppConstants.Pagination.CurrentPage, currentPage);
            viewData.Add(AppConstants.Pagination.TotalPages, totalData / AppConstants.Pagination.ElementsOnPage + 1);
            viewData.Add(AppConstants.Pagination.ActionName, controllerContext.RouteData.Values["action"].ToString());
        }

        public static void AddOrUpdate(this ViewDataDictionary viewData, string key, object value)
        {
            if (viewData.ContainsKey(key))
            {
                viewData[key] = value;
            }
            else
            {
                viewData.Add(key, value);
            }
        }

        public static string ToCommaSeparatedString(this ICollection<string> col)
        {
            return string.Join(",", col);
        }
    }
}