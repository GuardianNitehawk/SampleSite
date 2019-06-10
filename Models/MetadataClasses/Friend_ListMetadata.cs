/* Filename: Friend_ListMetadata.cs
 * Description: This class is responsible for providing metadata and validation for friend lists.
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
    public class Friend_ListMetadata
    {
        [Display(Name = "User ID")]
        public int user_id { get; set; }
        [Display(Name = "Friend ID")]
        public int friend_id { get; set; }
        [Display(Name = "Family?")]
        public bool is_family { get; set; }
        [Display(Name = "Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime date_added { get; set; }
    }

    [MetadataType(typeof(Friend_ListMetadata))]
    public partial class Friend_List
    {

    }
}