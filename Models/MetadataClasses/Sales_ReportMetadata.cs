/* Filename: Sales_ReportMetadata.cs
 * Description: This class is responsible for providing metadata and validation for the sales report.
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
    public class Sales_ReportMetadata
    {
        [Display(Name = "Game Name")]
        public string game_name { get; set; }
        [Display(Name = "Units Sold")]
        [DisplayFormat(DataFormatString = "{0}")]
        public Nullable<decimal> Units_Sold { get; set; }
        [Display(Name = "Profit Margin")]
        [DisplayFormat(DataFormatString = "{0:0.00}%")]
        public Nullable<decimal> Profit_Margin { get; set; }
        [Display(Name = "Sales Volume")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Nullable<decimal> Sales_Volume { get; set; }
        [Display(Name = "Total Profit")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Nullable<decimal> Total_Profit { get; set; }
    }

    [MetadataType(typeof(Sales_ReportMetadata))]
    public partial class Sales_Report
    {

    }
}