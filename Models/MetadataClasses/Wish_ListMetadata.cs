/* Filename: Wish_ListMetadata.cs
 * Description: This class is responsible for providing metadata and validation for wish lists.
 * 
 * Revision History:
 *     Ryan Pease, 2016-10-29: Created
*/

using RPClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class Wish_ListMetadata
    {
        [Display(Name = "Wish List ID")]
        public int wish_list_id { get; set; }
        [Required]
        [Display(Name = "User ID")]
        public int user_id { get; set; }
        [Required]
        [Display(Name = "Game ID")]
        public int game_id { get; set; }
        [Required]
        [Display(Name = "Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DateNotInFuture]
        public System.DateTime date_added { get; set; }
    }

    [MetadataType(typeof(Wish_ListMetadata))]
    public partial class Wish_List
    {

    }
}