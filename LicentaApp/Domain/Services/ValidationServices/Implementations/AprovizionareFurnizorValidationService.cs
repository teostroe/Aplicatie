using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LicentaApp.ViewModels.ComandaAprovizionare;

namespace LicentaApp.Domain.Services.ValidationServices.Implementations
{
    public class AprovizionareFurnizorValidationService : IValidationService
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

            var prodQuery = db.Produse.Where(y => y.IdFurnizor == obj.IdExpeditor);
            var coduriNeexistente = obj.Coduri.Where(x => prodQuery.FirstOrDefault(y => y.Cod == x) == null).ToArray();
           
            if (coduriNeexistente.Any())
            {
                yield return new ValidationResult("Codurile nu exista la furnizorul cerut: "+ coduriNeexistente.ToCommaSeparatedString());
            }


        }
    }
}