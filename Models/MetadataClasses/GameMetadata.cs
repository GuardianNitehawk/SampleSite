/* Filename: GameMetadata.cs
 * Description: This class is responsible for providing metadata and validation for games.
 * 
 * Revision History:
 *     Ryan Pease, 2016-10-27: Created
 *     Ryan Pease, 2016-10-28: Updated
 *     Ryan Pease, 2016-10-31: Updated
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using RPClassLibrary;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class GameMetadata
    {        
        [Display(Name = "Game ID")]
        public int game_id { get; set; }
        [Required]
        [Display(Name = "Game Name")]
        public string game_name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [Display(Name = "Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal cost { get; set; }
        [Required]
        [Display(Name = "List Price")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal list_price { get; set; }
        [Required]
        [Display(Name = "Quantity On Hand")]
        [Min(0)]
        public int on_hand { get; set; }
        [Required]
        [Display(Name = "Developer")]
        public int developer_id { get; set; }
        [Required]
        [Display(Name = "Publisher")]
        public int publisher_id { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public int genre_id { get; set; }
        [Required]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]        
        public System.DateTime release_date { get; set; }
        [Display(Name = "On Sale?")]
        public bool is_on_sale { get; set; }
        [Display(Name = "Discontinued?")]
        public bool is_discontinued { get; set; }
        [Display(Name = "Downloadable?")]
        public bool is_downloadable { get; set; }
        [Display(Name = "Physical Copy?")]
        public bool is_physical_copy { get; set; }
        [Display(Name = "Cover")]
        public string image_location { get; set; }
    }

    [MetadataType(typeof(GameMetadata))]
    public partial class Game : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            game_name = RPValidations.Capitalize(game_name, true);
            description = RPValidations.CapitalizeSentences(description);
            yield return ValidationResult.Success;
        }
    }
}