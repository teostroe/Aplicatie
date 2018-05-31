using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain
{
    public class AppConstants
    {
        public class Pagination
        {
            public static readonly int ElementsOnPage = 10;
            public const string EnablePagination = "EnablePagination";
            public const string CurrentPage = "CurrentPage";
            public const string TotalPages = "TotalPages";
            public const string ActionName = "ActionName";
        }

        public class CRUD
        {
            public const string Create = "CREATE";
            public const string Edit = "EDIT";
            public const string Delete = "DELETE";
            public const string Read = "READ";
        }

        public const string SQL = "SQL";

        public const string SferaDistantaOptions = "SferaDistantaOptions";
        public const string SferaAproapeOptions = "SferaAproapeOptions";
        public const string CilindruOptions = "CilindruOptions";
        public const string AxOptions = "AxOptions";
        public const string PrismaOptions = "PrismaOptions";
        public const string CodRama = "CodRama";

    }
}