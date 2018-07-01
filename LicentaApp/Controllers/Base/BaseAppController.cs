using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LicentaApp.Domain;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.Controllers.Base
{
    public class BaseAppController : Controller
    {
        protected LicentaDbContext db = new LicentaDbContext();

        protected DbSaveResult SaveChanges()
        {
            try
            {
                this.db.SaveChanges();
                ViewData.AddOrUpdate(AppConstants.Alerts.Success, new[] { "Operatie efectuata cu succes in baza de date." });
                return DbSaveResult.Success;
            }
            catch (DbUpdateException ex)
            {
                ViewData.AddOrUpdate(AppConstants.Alerts.Error, ex.GetBaseException().Message.ToValidationResultSqlError());
                return DbSaveResult.HandeledSqlException;
            }
            catch (Exception ex)
            {
                ViewData.AddOrUpdate(AppConstants.Alerts.Error, new[] { new ValidationResult(ex.GetBaseException().Message) });
                return DbSaveResult.UnknownException;
            }
        }
    }
}