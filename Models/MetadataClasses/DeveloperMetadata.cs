/* Filename: DeveloperMetadata.cs
 * Description: This class is responsible for providing metadata and validation for developers.
 * 
 * Revision History:
 *     Ryan Pease, 2016-10-29: Created
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RPClassLibrary;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class DeveloperMetadata
    {
        [Display(Name = "Developer ID")]
        public int developer_id { get; set; }
        [Required]
        [Display(Name = "Developer Name")]
        public string developer_name { get; set; }
        [Display(Name = "Contact Name")]
        public string contact_name { get; set; }
        [Display(Name = "Contact Phone")]
        public string contact_phone { get; set; }
        [Display(Name = "Contact Email")]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            ErrorMessage = "Invalid email address, please use a valid address (e.g. user@outlook.com).")]
        public string contact_email { get; set; }
    }

    [MetadataType(typeof(DeveloperMetadata))]
    public partial class Developer : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            developer_name = RPValidations.Capitalize(developer_name, true);
            if (!string.IsNullOrWhiteSpace(contact_name))
            {
                contact_name = RPValidations.Capitalize(contact_name, true);
            }
            if (!string.IsNullOrWhiteSpace(contact_phone))
            {
                contact_phone = RPValidations.FormatPhoneNumber(contact_phone);
            }
            yield return ValidationResult.Success;
        }
    }
}