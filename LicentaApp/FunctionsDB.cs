using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace LicentaApp
{
    public partial class LicentaDbContext
    {
        [DbFunction("YouDbContext.Store", "YourScalarFunction")]
        public string YourScalarFunction(string parameter)
        {
            var lObjectContext = ((IObjectContextAdapter)this).ObjectContext;

            return lObjectContext.
                CreateQuery<string>(
                    "YouDbContext.Store.YourScalarFunction",
                    new ObjectParameter("parameterName", parameter)).
                Execute(MergeOption.NoTracking).
                FirstOrDefault();
        }
    }
}