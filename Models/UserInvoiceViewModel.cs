/* Filename: UserInvoiceViewModel.cs
 * Description: This class is responsible for the Invoice characteristics needed for displaying an invoice for a user.
 * 
 * Revision History:
 *     Ryan Pease, 2016-12-02: Created 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class UserInvoiceViewModel
    {
        public Invoice invoice;

        public User user;

        public Address address;

        public Credit_Card credit_card;

        //public DateTime invoice_date;

        public IEnumerable<Line_Item> items;

        public decimal CalculateSubtotal(Line_Item line_item)
        {
            //return items.Sum(i => i.price * i.quantity);
            return line_item.price * line_item.quantity;
        }

        public decimal CalculateTotal()
        {            
            decimal total = 0;
            foreach (var item in items)
            {
                //total += items.Sum(i => i.price * i.quantity);
                total += item.price * item.quantity;
            }            
            return total;
        }

        public string GetDate()
        {
            return invoice.invoice_date.ToString("MM/dd/yyyy");
        }
    }
}