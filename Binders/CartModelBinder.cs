/* Filename: CartModelBinder.cs
 * Description: This class is responsible for providing model binding for Carts.
 * 
 * Revision History:
 *     Ryan Pease, 2016-11-29: Created 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoGameStore.Models;

namespace VideoGameStore.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, 
            ModelBindingContext bindingContext)
        {
            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }

            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session[sessionKey] != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }
            return cart;
        }
    }
}