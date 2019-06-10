/* Filename: Credit_CardMetadata.cs
 * Description: This class is responsible for providing metadata and validation for credit cards.
 * 
 * Revision History:
 *     Ryan Pease, 2016-10-29: Created
 *     Ryan Pease, 2016-10-30: Updated
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RPClassLibrary;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class Credit_CardMetadata
    {
        [Display(Name = "Credit Card ID")]
        public int credit_card_id { get; set; }
        [Required]
        [Display(Name = "User ID")]
        public int user_id { get; set; }
        [Required]        
        [RegularExpression(@"\d{16}", ErrorMessage = "Invalid credit card number, please use only 16 digits (e.g. 0000111122223333).")]
        [Display(Name = "Card Number")]
        public long card_number { get; set; }
        [Required]
        [Display(Name = "Expiry Date")]        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DateInFuture]
        public System.DateTime expiry_date { get; set; }
        [Display(Name = "Expired?")]
        public bool is_expired { get; set; }
        [Display(Name = "Flagged?")]
        public bool is_flagged { get; set; }
    }

    [MetadataType(typeof(Credit_CardMetadata))]
    public partial class Credit_Card
    {

    }
}