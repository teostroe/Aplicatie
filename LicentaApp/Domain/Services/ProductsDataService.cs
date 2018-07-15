using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace LicentaApp.Domain.Services
{
    public class ProductsDataService
    {

        public static decimal GetTotal(int idComanda, LicentaDbContext dbContext)
        {
            var dataComanda = dbContext.Comenzi.Find(idComanda).Data;
            var produse = dbContext.RandComenziProduse
                .Include(x => x.Produse)
                .Where(x => x.IdComanda == idComanda)
                .ToList();
            return produse.Sum(x => x.Cantitate * GetPret(x.IdProdus, dataComanda, dbContext));
        }

        public static decimal GetPret(int idProdus, DateTime data, LicentaDbContext dbContext)
        {
            return dbContext.Preturi.Where(x => x.Id == idProdus)
                .OrderByDescending(x => x.DataActualizare)
                .FirstOrDefault(x => data >= x.DataActualizare)
                .Valoare;
        }

        public static IEnumerable<Produse> GetProducts(Dictionary<string, string> details, LicentaDbContext dbContext)
        {
            var productIds = GetProductIds(details, dbContext);

            return dbContext.Produse.Where(x => productIds.Contains(x.Id));
        }

        public static IEnumerable<Furnizori> GetFurnizori(Dictionary<string, string> details,
            LicentaDbContext dbContext)
        {
            var productIds = GetProductIds(details, dbContext);
            return dbContext.Furnizori.Where(x => x.Produse.Any(y => productIds.Contains(y.Id))).Distinct().ToArray();
        }

        private static IEnumerable<int> GetProductIds(Dictionary<string, string> details,
            LicentaDbContext dbContext)
        {
            var productIds = new List<int>();

            foreach (var keyValuePair in details)
            {
                var tempIds = dbContext.Produse
                    .Where(x => x.Inventar.Any(y => y.CantitateDisponibila > 0))
                    .Include(x => x.DetaliiProdus)
                    .Where(x => x.DetaliiProdus.Any(y => keyValuePair.Key == y.Denumire &&
                                                         keyValuePair.Value == y.Valoare))
                    .Select(x => x.Id).ToList();

                if (!productIds.Any())
                {
                    productIds.AddRange(tempIds);
                }
                else
                {
                    productIds = productIds.Intersect(tempIds).ToList();
                }
            }

            return productIds;
        }
    }
}