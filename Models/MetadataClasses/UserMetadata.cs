/* Filename: UserMetadata.cs
 * Description: This class is responsible for providing metadata and validation for users.
 * 
 * Revision History:
 *     Ryan Pease, 2016-10-28: Created
 *     Ryan Pease, 2016-10-29: Updated
 */

using RPClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class UserMetadata
    {
        [Display(Name = "User ID")]
        public int user_id { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string username { get; set; }
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
    
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", 
            ErrorMessage = "Invalid email address, please use a valid address (e.g. user@outlook.com).")]
        public string email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string user_password { get; set; }
        [Display(Name = "Login Failures")]
        public int login_failures { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string first_name { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        [Required]
        [Display(Name = "Phone")]
        [PhoneNumberValidation]
        public string phone { get; set; }
        [Required]                                  
        [Display(Name = "Gender")]
        [StringLength(1)]        
        [RegularExpression("^([m | M | f | F])$", ErrorMessage = "Invalid value. Please enter M for Male, or F for Female.")]
        public string gender { get; set; }
        [Required]
        [Display(Name = "Birthdate")]        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DateNotInFuture]
        public System.DateTime birthdate { get; set; }
        [Required]
        [Display(Name = "Date Joined")]        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]        
        [DateNotInFuture]
        public System.DateTime date_joined { get; set; }
        [Display(Name = "Employee?")]
        public bool is_employee { get; set; }
        [Display(Name = "Admin?")]
        public bool is_admin { get; set; }
        [Display(Name = "Member?")]
        public bool is_member { get; set; }
        [Display(Name = "Inactive?")]
        public bool is_inactive { get; set; }
        [Display(Name = "Locked Out?")]
        public bool is_locked_out { get; set; }
        [Display(Name = "On Mailing List?")]
        public bool is_on_email_list { get; set; }
        [Display(Name = "Favourite Platform")]
        public string favorite_platform { get; set; }
        [Display(Name = "Favourite Category")]
        public string favorite_category { get; set; }
        [Display(Name = "Notes")]
        public string notes { get; set; }
    }

    [MetadataType(typeof(UserMetadata))]
    public partial class User : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            first_name = RPValidations.Capitalize(first_name, true);
            last_name = RPValidations.Capitalize(last_name, true);
            gender = RPValidations.Capitalize(gender, false);
            phone = RPValidations.FormatPhoneNumber(phone);
            yield return ValidationResult.Success;
        }
    }
}