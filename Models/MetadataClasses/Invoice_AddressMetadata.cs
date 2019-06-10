/* Filename: Invoice_AddressMetadata.cs
 * Description: This class is responsible for providing metadata and validation for invoice addresses.
 * 
 * Revision History:
 *     Ryan Pease, 2016-10-29: Created
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class Invoice_AddressMetadata
    {
        [Display(Name = "Invoice Address ID")]
        public int invoice_address_id { get; set; }
        [Display(Name = "Invoice ID")]
        public int invoice_id { get; set; }
        [Display(Name = "Address ID")]
        public int address_id { get; set; }
        [Display(Name = "Billing Address?")]
        public bool is_billing_address { get; set; }
    }

    [MetadataType(typeof(Invoice_AddressMetadata))]
    public partial class Invoice_Address
    {

    }
}