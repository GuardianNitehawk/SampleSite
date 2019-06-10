/* Filename: User_AddressMetadata.cs
 * Description: This class is responsible for providing metadata and validation for user addresses.
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
    public class User_AddressMetadata
    {
        [Display(Name = "User ID")]
        public int user_id { get; set; }
        [Display(Name = "Address ID")]
        public int address_id { get; set; }
        [Display(Name = "Address Name")]
        public string address_name { get; set; }
    }

    [MetadataType(typeof(User_AddressMetadata))]
    public partial class User_Address
    {

    }
}