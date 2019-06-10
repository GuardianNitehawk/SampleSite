/* Filename: ProvinceMetadata.cs
 * Description: This class is responsible for providing metadata and validation for provinces.
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
    public class ProvinceMetadata
    {
        [Display(Name = "Province ID")]
        public int province_id { get; set; }
        [Required]
        [Display(Name = "Province Code")]
        public string province_code { get; set; }
        [Required]
        [Display(Name = "Province Name")]
        public string province_name { get; set; }
    }

    [MetadataType(typeof(ProvinceMetadata))]
    public partial class Province
    {

    }
}