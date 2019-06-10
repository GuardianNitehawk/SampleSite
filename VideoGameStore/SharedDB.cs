using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoGameStore
{
    public class SharedDB
    {
        public static MySqlConnection connection = new MySqlConnection();
        public const string CONNECTION_STRING = "server=cvgs.ddns.net;user id=cvgs;password=Prog3050;persistsecurityinfo=True;database=VideoGameStoreDB";
        public static MySqlCommand command;

        public static void setConnectionString()
        {
            connection.ConnectionString = CONNECTION_STRING;
        }
    }
}