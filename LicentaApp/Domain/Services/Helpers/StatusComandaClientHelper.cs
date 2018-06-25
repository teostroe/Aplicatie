using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.Domain.Services.Helpers
{
    public static class StatusComandaClientHelper
    {
        public static StatusComanda GetNextState(this StatusComanda statusCurent)
        {
            return GetNextStatusTuple(statusCurent).Item1;
        }

        public static string GetInputMessage(this StatusComanda statusCurent)
        {
            return GetNextStatusTuple(statusCurent).Item2;
        }

        public static bool HasNextStatus(this StatusComanda statusCurent)
        {
            return statusCurent != StatusComanda.Finalizata;
        }

        private static Tuple<StatusComanda, string> GetNextStatusTuple(StatusComanda statusCurent)
        {
            switch (statusCurent)
            {
                case StatusComanda.Creata:
                    return new Tuple<StatusComanda, string>(StatusComanda.InTranzit, "Trimite comanda pentru prelucrare");
                case StatusComanda.InTranzit:
                    return new Tuple<StatusComanda, string>(StatusComanda.InPrelucrare, "Incepe prelucrare");
                case StatusComanda.InPrelucrare:
                    return new Tuple<StatusComanda, string>(StatusComanda.InTranzitMagazin, "Trimite comanda la magazin");
                case StatusComanda.InTranzitMagazin:
                    return new Tuple<StatusComanda, string>(StatusComanda.TrimisaInMagazin, "Primeste comanda in magazin");
                case StatusComanda.TrimisaInMagazin:
                    return new Tuple<StatusComanda, string>(StatusComanda.Finalizata, "Finalizeaza comanda");
                default:
                    throw new NotImplementedException();
            }
        }
    }
}