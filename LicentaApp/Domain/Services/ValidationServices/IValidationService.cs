using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicentaApp.Domain.Services.ValidationServices
{
    public interface IValidationService
    {
        IEnumerable<ValidationResult> ValidateData<T>(T item) where T : class;
    }
}
