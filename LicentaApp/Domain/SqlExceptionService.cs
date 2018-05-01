using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace LicentaApp.Domain
{
    public static class SqlExceptionService
    {
        private static readonly int[] HandledSqlErrorCodes = { 20202 };
        private static readonly string ORARegex = @"ORA-.*:";

        public static string GetHandledSqlError(DbUpdateException e)
        {
            var exception = e.GetLatestInnerException() as OracleException;
            if (exception != null && HandledSqlErrorCodes.Contains(exception.Number))
            {
                var message = exception.Message.Split('\n').First();
                return Regex.Replace(message, ORARegex, string.Empty);
            }
            return "Eroare necunoscuta. Va rugam sa contactati administratorul aplicatiei.";
        }
    }
}