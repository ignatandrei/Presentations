using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLReturnCode
{
    public class Person : IValidatableObject
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.ID %2 ==0 )
                yield return new ValidationResult("not valid ",new[] { nameof(ID) });
        }
    }

}
