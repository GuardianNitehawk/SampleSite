/* Filename: Store_EventMetadata.cs
 * Description: This class is responsible for providing metadata and validation for store events.
 * 
 * Revision History:
 *     Ryan Pease, 2016-10-27: Created
 *     Ryan Pease, 2016-10-28: Updated
 */

using RPClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class Store_EventMetadata
    {
        [Display(Name = "Store Event ID")]
        public int store_event_id { get; set; }
        [Required]
        [Display(Name = "Store Event Name")]
        public string store_event_name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [Display(Name = "Address ID")]
        public int address_id { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]      // switch to include time?
        [DateInFuture]
        public System.DateTime start_date { get; set; }
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]      // switch to include time?
        [DateInFuture]
        public System.DateTime end_date { get; set; }
        [Required]
        [Display(Name = "Max. Registrants")]
        [Min(1)]
        public int max_registrants { get; set; }
        [Display(Name = "Full?")]
        public bool is_full { get; set; }
        [Display(Name = "Members Only?")]
        public bool is_members_only { get; set; }
        [Display(Name = "Cancelled?")]
        public bool is_cancelled { get; set; }
    }

    [MetadataType(typeof(Store_EventMetadata))]
    public partial class Store_Event : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            store_event_name = RPValidations.Capitalize(store_event_name, true);
            description = RPValidations.CapitalizeSentences(description);
            yield return ValidationResult.Success;
        }
    }
}