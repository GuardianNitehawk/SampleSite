/* Filename: InvoiceMetadata.cs
 * Description: This class is responsible for providing metadata and validation for invoices.
 * 
 * Revision History:
 *     Ryan Pease, 2016-10-29: Created
 *     Ryan Pease, 2016-10-31: Created

*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RPClassLibrary;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class InvoiceMetadata
    {
        [Display(Name = "Invoice ID")]
        public int invoice_id { get; set; }
        [Required]
        [Display(Name = "User ID")]
        public int user_id { get; set; }
        [Required]
        [Display(Name = "Credit Card ID")]
        public int credit_card_id { get; set; }
        [Required]
        [Display(Name = "Invoice Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DateNotInFuture]
        public System.DateTime invoice_date { get; set; }
        [Display(Name = "Ship Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DateNotInFuture]
        public Nullable<System.DateTime> ship_date { get; set; }
        [Display(Name = "Closed?")]
        public bool is_invoice_closed { get; set; }
    }

    [MetadataType(typeof(InvoiceMetadata))]
    public partial class Invoice
    {

    }
}