/* Filename: CartController.cs
 * Description: This class is responsible for handing the interaction between the user and the cart model.
 * 
 * Revision History:
 *     Ryan Pease, 2016-11-29: Created 
*/

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoGameStore.Models;

namespace VideoGameStore.Controllers
{
    public class CartController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        [AllowAnonymous]
        public PartialViewResult Summary(Cart cart)
        {            
            return PartialView(cart);
        }     

        [AllowAnonymous]
        //GET: Cart
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = GetCart(), ReturnUrl = returnUrl });
        }

        //GET: Checkout
        [Authorize(Roles ="Customer, Admin, Employee, Member")]  //user must be logged in/registered in order to check out
        public ViewResult Checkout()
        {
            // get current user_id
            int user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;            
        
            // create a list of the user's addresses and credit cards - these will be displayed in a dropdown list
            var addresses = db.User_Address.Where(a => a.user_id == user_id).ToList();
            var creditcards = db.Credit_Card.Where(c => c.user_id == user_id).ToList();
            int numAddresses = addresses.Count();
            int numCards = creditcards.Count();
            ViewBag.numCards = numCards;
            ViewBag.numAddresses = numAddresses;
            ViewBag.user_id = user_id;
            ViewBag.address_id = new SelectList(addresses, "address_id", "Address.street_address");
            ViewBag.credit_card_id = new SelectList(creditcards, "credit_card_id", "card_number");
     
            return View();
        }

        //POST: Checkout
        [HttpPost]
        [Authorize(Roles = "Customer, Admin, Employee, Member")]  
        public ActionResult Checkout(int address_id, int credit_card_id)
        {
            int user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;            
            Cart cart = GetCart();
            if (cart.Items == null || cart.Items.Count() == 0)            // if there aren't any items in the cart - send back to index and show message
            {
                TempData["Message"] = "Invalid Submission: You cannot checkout without any items in your cart...";
                return Checkout();
            }
            else
            {
                // create invoice + populate with data from select list in view
                Invoice invoice = new Invoice();
                invoice.user_id = user_id;
                invoice.credit_card_id = credit_card_id;
                invoice.invoice_date = DateTime.Now;

                // add invoice and save changes
                db.Invoices.Add(invoice);
                db.SaveChanges();

                // get id of most recently inserted invoice
                int invoiceNumber = db.Invoices.Max(i => i.invoice_id);

                // create invoice address based on user's selected address
                Invoice_Address invoiceAddress = new Invoice_Address();
                invoiceAddress.address_id = address_id;
                invoiceAddress.invoice_id = invoiceNumber;
                invoiceAddress.is_billing_address = true;
                db.Invoice_Address.Add(invoiceAddress);                

                // get items in cart
                foreach (CartLineItem item in cart.Items)
                {
                    // create line items and add to db
                    Line_Item line_item = new Line_Item();
                    line_item.invoice_id = invoiceNumber;
                    line_item.game_id = item.Game.game_id;
                    line_item.quantity = item.Quantity;
                    line_item.price = item.Game.list_price;
                    db.Line_Item.Add(line_item);
                    db.SaveChanges();                        
                }
            // clear out cart data
            Session["Cart"] = new Cart();
            return RedirectToAction("DisplayUserInvoice", "Invoices", new { invoice_id = invoiceNumber });                
            }
        }

        //[AllowAnonymous]
        //public ActionResult Test()      //just for testing
        //{
        //    int invoiceNumber = 3;
        //    return RedirectToAction("DisplayUserInvoice", "Invoices", new { invoice_id = invoiceNumber }); 
        //    //return Redirect("/Invoices/DisplayUserInvoice/" + invoiceNumber);
        //}

        [AllowAnonymous]
        public RedirectToRouteResult AddToCart(int game_id, string returnUrl)
        {
            Game game = db.Games.Where(g => g.game_id == game_id).FirstOrDefault();

            if (game != null)
            {
                GetCart().AddItem(game, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [AllowAnonymous]
        public RedirectToRouteResult RemoveFromCart(int game_id, string returnUrl)
        {
            Game game = db.Games.Where(g => g.game_id == game_id).FirstOrDefault();

            if (game != null)
            {
                GetCart().RemoveItem(game);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }        
    }
}