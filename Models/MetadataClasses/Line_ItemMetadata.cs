/* Filename: Line_ItemMetadata.cs
 * Description: This class is responsible for providing metadata and validation for line items.
 * 
 * Revision History:
 *     Ryan Pease, 2016-10-29: Created
 *     Ryan Pease, 2016-10-31: Updated
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class Line_ItemMetadata
    {
        [Display(Name = "Line Item ID")]
        public int line_item_id { get; set; }
        [Required]
        [Display(Name = "Invoice ID")]
        public int invoice_id { get; set; }
        [Required]
        [Display(Name = "Game ID")]
        public int game_id { get; set; }
        [Display(Name = "Quantity")]
        [Min(0)]
        public int quantity { get; set; }
        [Display(Name = "Price")]
        [Min(0)]
        public decimal price { get; set; }

    }

    [MetadataType(typeof(Line_ItemMetadata))]
    public partial class Line_Item
    {

    }
}