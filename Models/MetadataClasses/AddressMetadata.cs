/* Filename: AddressMetadata.cs
 * Description: This class is responsible for providing metadata and validation for addresses.
 * 
 * Revision History:
 *     Ryan Pease, 2016-10-28: Created
 *     Ryan Pease, 2016-10-29: Updated
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RPClassLibrary;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class AddressMetadata
    {
        [Display(Name = "Address ID")]
        public int address_id { get; set; }
        [Required]
        [Display(Name = "Street Address")]
        public string street_address { get; set; }
        [Required]
        [Display(Name = "City")]
        public string city { get; set; }
        [Required]
        [Display(Name = "Province ID")]
        public int province_id { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
        [PostalCodeValidation]
        public string postal_code { get; set; }
    }

    [MetadataType(typeof(AddressMetadata))]
    public partial class Address : IValidatableObject
    {     
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            street_address = RPValidations.Capitalize(street_address, true);
            city = RPValidations.Capitalize(city, true);
            postal_code = RPValidations.FormatPostalCode(postal_code);
            yield return ValidationResult.Success;
        }
    }
}