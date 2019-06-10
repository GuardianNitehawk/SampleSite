/* Filename: GenreMetadata.cs
 * Description: This class is responsible for providing metadata and validation for genres.
 * 
 * Revision History:
 *     Ryan Pease, 2016-10-29: Created
 *     Ryan Pease, 2016-10-31: Updated
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RPClassLibrary;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class GenreMetadata
    {
        [Display(Name = "Genre ID")]
        public int genre_id { get; set; } 
        [Required]       
        [Display(Name = "Genre Name")]
        public string genre_name { get; set; }
        [Display(Name = "Description")]
        public string description { get; set; }
    }

    [MetadataType(typeof(GenreMetadata))]
    public partial class Genre : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            genre_name = RPValidations.Capitalize(genre_name, true);
            if (!String.IsNullOrWhiteSpace(description))
            {
                description = RPValidations.CapitalizeSentences(description);
            }            
            yield return ValidationResult.Success;
        }
    }
}