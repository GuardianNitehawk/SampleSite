///Created by David Klumpenhower
///Created Dec 3rd
///Custom model to handle the needs of userGame index View
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoGameStore.Models
{
    public class UserGameViewModel
    {
        public int userGameID { get; set; }
        public int gameID { get; set; }
        public string imageLocation { get; set; }
        public string description { get; set; }
        public string gameName { get; set; }
        public int? reviewID { get; set; }
        public int? rating { get; set; }
        public DateTime datePurchased { get;set; }
    }
}