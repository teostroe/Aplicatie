using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.Auth;

namespace LicentaApp.Domain.Filters
{
    public static class UserFilterService
    {
        public static IQueryable<ComenziAprovizionari> FiltreazaComenziAprovizionare(
            this IQueryable<ComenziAprovizionari> query, AppPrincipal user)
        {
            if (user.IsInRole(AuthConstants.Roluri.Utilizator))
            {
                return query.Where(x => x.CatreMagazinId == user.IdMagazin);
            }
            return query;
        }


        public static IQueryable<Comenzi> FiltreazaComenzi(
            this IQueryable<Comenzi> query, AppPrincipal user)
        {
            if (user.IsInRole(AuthConstants.Roluri.Utilizator))
            {
                return query.Where(x => x.Utilizatori.IdMagazin == user.IdMagazin);
            }
            return query;
        }


    }
}