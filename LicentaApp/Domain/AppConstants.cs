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

        public class Alerts
        {
            public const string Success = "Success";
            public const string Error = "Error";
        }

        public const string SQL = "SQL";

        public const string SferaDistantaOptions = "SferaDistantaOptions";
        public const string SferaAproapeOptions = "SferaAproapeOptions";
        public const string CilindruOptions = "CilindruOptions";
        public const string AxOptions = "AxOptions";
        public const string PrismaOptions = "PrismaOptions";
        public const string TipLentilaOptions = "TipLentilaOptions";
        public const string IndiceReractieOptions = "IndiceReractieOptions";
        public const string ProducatorOptions = "ProducatorOptions";
        public const string LentilaOptions = "LentilaOptions";
        public const string TratamentOptions = "TratamentOptions";
        public const string CuloareOptions = "CuloareOptions";


        public const string TelefonClienti = "TelefonClienti";
        public const string CodRame = "CodRame";
        public const string CodRameSiOchelariSoare = "CodRameSiOchelariSoare";

        public const string VerificareProduse = "VerificareProduse";
        public const string TempComandaViewModel = "TempComandaViewModel";

        public const string FurnizorOptions = "FurnizorOptions";
        public const string CodProduseFurnizor = "CodProduseFurnizor";
        public const string MagazineOptions = "MagazineOptions";
        public const string OraseOptions = "OraseOptions";
        public const string ProduseOptions = "ProduseOptions";
    }
}