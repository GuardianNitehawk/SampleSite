/* Filename: Cart.cs
 * Description: This class is responsible for the characteristics and behaviour of the Cart.
 * 
 * Revision History:
 *     Ryan Pease, 2016-11-29: Created 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoGameStore.Models;

namespace VideoGameStore.Models
{
    public class Cart
    {
        private List<CartLineItem> itemsCollection = new List<CartLineItem>();

        public void AddItem(Game game, int quantity)
        {
            CartLineItem item = itemsCollection.Where(i => i.Game.game_id == game.game_id).FirstOrDefault();

            if (item == null)
            {
                itemsCollection.Add(new CartLineItem { Game = game, Quantity = quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItem(Game game)
        {
            itemsCollection.RemoveAll(i => i.Game.game_id == game.game_id);
        }

        public decimal CalculateTotal()
        {
            return itemsCollection.Sum(i => i.Game.list_price * i.Quantity);
        }

        public void Clear()
        {
            itemsCollection.Clear();
        }

        public IEnumerable<CartLineItem> Items
        {
            get { return itemsCollection; }
        }
    }

    public class CartLineItem
    {
        public Game Game { get; set; }
        public int Quantity { get; set; }
    }
}