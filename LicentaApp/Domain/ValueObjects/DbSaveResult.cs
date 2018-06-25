using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.ValueObjects
{
    public enum DbSaveResult
    {
        Success = 1,
        HandeledSqlException = 2,
        UnknownException = 3
        
    }
}