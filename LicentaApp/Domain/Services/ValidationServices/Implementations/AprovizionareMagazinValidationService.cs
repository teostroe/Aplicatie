using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LicentaApp.ViewModels.ComandaAprovizionare;

namespace LicentaApp.Domain.Services.ValidationServices.Implementations
{
    public class AprovizionareMagazinValidationService : IValidationService
    {
        private LicentaDbContext db = new LicentaDbContext();

        public IEnumerable<ValidationResult> ValidateData<T>(T item) where T : class
        {
            if (item.GetType() != typeof(ComandaAprivizionareCreate))
            {
                return Enumerable.Empty<ValidationResult>();
            }
            var obj = item as ComandaAprivizionareCreate;
            return this.ValidateComanda(obj).ToArray();

        }

        private IEnumerable<ValidationResult> ValidateComanda(ComandaAprivizionareCreate obj)
        {
            if (obj.Cantitati.Length != obj.Coduri.Length)
            {
                yield return new ValidationResult("Cantitatile si codurile nu sunt ok");
            }

            if (obj.Cantitati.Any(x => x <= 0))
            {
                yield return new ValidationResult("Cantitatile nu sunt corecte");
            }

            if (obj.Coduri.Any(x => x.IsNullOrEmpty()))
            {
                yield return new ValidationResult("Codurile nu pot fi nule");
            }

            var prodQuery = db.Inventar.Where(y => y.IdMagazin == obj.IdExpeditor)
                .Select(x => x.Produse);
            var coduriNeexistente = obj.Coduri.Where(x => prodQuery.FirstOrDefault(y => y.Cod == x) == null).ToArray();
            if (coduriNeexistente.Any())
            {
                yield return new ValidationResult("Codurile nu exista la depozitul central: " + coduriNeexistente.ToCommaSeparatedString());
            }
            
            var codCantitateMapping  = this.GetCodCantitateMapping(obj.Coduri, obj.Cantitati);
            var faraStoc = this.db.Inventar.Where(x => x.IdMagazin == obj.IdExpeditor)
                .ToArray()
                .Where(x => codCantitateMapping.Keys.Contains(x.Produse.Cod))
                .Where(x => x.CantitateDisponibila < codCantitateMapping[x.Produse.Cod])
                .Select(x => x.Produse.Cod)
                .ToArray();
            if (faraStoc.Any())
            {
                yield return new ValidationResult("Codurile nu sunt in stoc la depozitul central: " + faraStoc.ToCommaSeparatedString());
            }

        }

        private Dictionary<string, int> GetCodCantitateMapping(string[] coduri, int[] cantitati)
        {
            var codCantitateMapping = new Dictionary<string, int>();
            for (int i = 0; i < coduri.Length; ++i)
            {
                codCantitateMapping.Add(coduri[i], cantitati[i]);
            }

            return codCantitateMapping;
        }
    }
}