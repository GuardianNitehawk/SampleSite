/* Filename: ReviewMetadata.cs
 * Description: This class is responsible for providing metadata and validation for reviews.
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
    public class ReviewMetadata
    {
        [Display(Name = "Review ID")]
        public int review_id { get; set; }
        [Required]
        [Display(Name = "User ID")]
        public int user_id { get; set; }
        [Required]
        [Display(Name = "Game ID")]
        public int game_id { get; set; }
        [Required]
        [Display(Name = "Review Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DateNotInFuture]
        public System.DateTime review_date { get; set; }
        [Display(Name = "Review")]
        public string review_content { get; set; }

        [Display(Name = "Approved?")]
        public bool is_approved { get; set; }
        [Display(Name = "Deleted?")]
        public bool is_deleted { get; set; }
    }

    [MetadataType(typeof(ReviewMetadata))]
    public partial class Review
    {

    }
}