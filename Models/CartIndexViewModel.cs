/* Filename: CartIndexViewModel.cs
 * Description: This class is responsible for the cart characteristics needed for the index view.
 * 
 * Revision History:
 *     Ryan Pease, 2016-11-29: Created 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}