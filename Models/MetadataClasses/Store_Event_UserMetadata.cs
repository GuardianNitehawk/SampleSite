/* Filename: Store_Event_UserMetadata.cs
 * Description: This class is responsible for providing metadata and validation for the users registered for an event.
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
    public class Store_Event_UserMetadata
    {
        [Display(Name = "Store Event User ID")]
        public int store_event_user_id { get; set; }
        [Required]
        [Display(Name = "Store Event ID")]
        public int store_event_id { get; set; }
        [Required]
        [Display(Name = "User ID")]
        public int user_id { get; set; }
    }

    [MetadataType(typeof(Store_Event_UserMetadata))]
    public partial class Store_Event_User
    {

    }
}