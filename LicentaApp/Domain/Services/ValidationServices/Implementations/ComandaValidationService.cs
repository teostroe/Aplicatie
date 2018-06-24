using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using LicentaApp.Domain.ValueObjects;
using LicentaApp.ViewModels;

namespace LicentaApp.Domain.Services.ValidationServices.Implementations
{
    public class ComandaValidationService : IValidationService
    {
        public IEnumerable<ValidationResult> ValidateData<T>(T item) where T : class
        {
            if (item.GetType() != typeof(ComandaViewModel))
            {
                return Enumerable.Empty<ValidationResult>();
            }
            var obj = item as ComandaViewModel;

            return this.ValidateClient(obj.Client).Concat(
                this.ValidateVizitaMedicala(obj.VizitaMedicala)).Concat(
                this.ValidateComanda(obj)).ToArray();
        }

        private IEnumerable<ValidationResult> ValidateClient(Clienti item)
        {
            if (item.Email.IsNullOrEmpty())
            {
                yield return new ValidationResult("Adresa de email este obligatorie");
            }

            if (item.Nume.IsNullOrEmpty())
            {
                yield return new ValidationResult("Numele este obligatoriu");
            }

            if (item.Prenume.IsNullOrEmpty())
            {
                yield return new ValidationResult("Prenumele este obligatoriu");
            }

            if (item.DataNastere == default(DateTime))
            {
                yield return new ValidationResult("Data Nastere este obligatorie");
            }
        }

        private IEnumerable<ValidationResult> ValidateVizitaMedicala(ViziteMedicale item)
        {
            if (item != null)
            {
                if (item.DistantaPupilara == default(decimal))
                {
                    yield return new ValidationResult("Distanta pupilara este obligatorie");
                }
            }

        }

        private IEnumerable<ValidationResult> ValidateComanda(ComandaViewModel item)
        {
            switch (item.TipComanda)
            {
                case TipComanda.ComandaCuPrelucrare:
                    if (item.Lentila != null)
                    {
                        if (item.Lentila.TipLentila.HasValue && item.Lentila.CodProdus.IsNullOrEmpty())
                        {
                            yield return new ValidationResult("Trebuie sa selectati lentila dorita deoarece ati selectat tipul lentilei");

                        }

                        if (!item.Lentila.TipLentila.HasValue && item.CodProdusRO.IsNullOrEmpty())
                        {
                            yield return new ValidationResult("Trebuie sa selectati o rama deoarece nu ati selectat nicio lentila");
                        }
                    }
                    break;
                case TipComanda.ComandaSimpla:
                    if (item.CodProdusRO.IsNullOrEmpty())
                    {
                        yield return new ValidationResult("Rama sau ochelarii sunt obligatorii");
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }


        }

    }
}