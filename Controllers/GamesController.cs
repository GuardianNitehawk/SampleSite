using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoGameStore.Models;

namespace VideoGameStore.Controllers
{
    public class GamesController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        [AllowAnonymous]
        // GET: Games
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.Developer).Include(g => g.Genre).Include(g => g.Publisher);
            //insertDefaultRatings();
            getAverageGameRatings();
            return View(games.ToList());
        }

        [AllowAnonymous]
        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        [Authorize(Roles = "Admin")]
        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.developer_id = new SelectList(db.Developers, "developer_id", "developer_name");
            ViewBag.genre_id = new SelectList(db.Genres, "genre_id", "genre_name");
            ViewBag.publisher_id = new SelectList(db.Publishers, "publisher_id", "publisher_name");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "game_id,game_name,description,cost,list_price,on_hand,developer_id,publisher_id,genre_id,release_date,is_on_sale,is_discontinued,is_downloadable,is_physical_copy,image_location")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.developer_id = new SelectList(db.Developers, "developer_id", "developer_name", game.developer_id);
            ViewBag.genre_id = new SelectList(db.Genres, "genre_id", "genre_name", game.genre_id);
            ViewBag.publisher_id = new SelectList(db.Publishers, "publisher_id", "publisher_name", game.publisher_id);
            return View(game);
        }

        [Authorize(Roles = "Admin, Employee")]
        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.developer_id = new SelectList(db.Developers, "developer_id", "developer_name", game.developer_id);
            ViewBag.genre_id = new SelectList(db.Genres, "genre_id", "genre_name", game.genre_id);
            ViewBag.publisher_id = new SelectList(db.Publishers, "publisher_id", "publisher_name", game.publisher_id);
            return View(game);
        }

        [Authorize(Roles = "Admin, Employee")]
        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "game_id,game_name,description,cost,list_price,on_hand,developer_id,publisher_id,genre_id,release_date,is_on_sale,is_discontinued,is_downloadable,is_physical_copy,image_location")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.developer_id = new SelectList(db.Developers, "developer_id", "developer_name", game.developer_id);
            ViewBag.genre_id = new SelectList(db.Genres, "genre_id", "genre_name", game.genre_id);
            ViewBag.publisher_id = new SelectList(db.Publishers, "publisher_id", "publisher_name", game.publisher_id);
            return View(game);
        }

        [Authorize(Roles = "Admin")]
        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        [Authorize(Roles = "Admin")]
        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public string[] getGameIDS()
        {
            string[] results;
            int num_of_games = 0;
            SharedDB.setConnectionString();
            SharedDB.connection.Open();
            using (SharedDB.connection)
            {
                SharedDB.command = new MySqlCommand("SELECT COUNT(game_id) FROM Game;", SharedDB.connection);
                MySqlDataReader reader = SharedDB.command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        num_of_games = reader.GetInt32(0);
                    }
                }
            }

            results = new string[num_of_games];
            SharedDB.connection.Open();
            using (SharedDB.connection)
            {

                SharedDB.command = new MySqlCommand("SELECT game_id FROM Game;", SharedDB.connection);
                MySqlDataReader reader = SharedDB.command.ExecuteReader();
                if (reader.HasRows)
                {
                    string[] temp = new string[num_of_games];
                    int counter = 0;
                    while (reader.Read())
                    {
                        temp[counter] = reader.GetInt32(0).ToString();
                        counter++;
                    }
                    results = temp;
                    reader.Close();
                }
            }
            return sanitizeArray(results);
        }

        public void insertDefaultRatings()
        {
            SharedDB.setConnectionString();
            using (SharedDB.connection)
            {
                SharedDB.connection.Open();
                SharedDB.command = new MySqlCommand("INSERT INTO User_Game (user_game_id, user_id, game_id, date_purchased, rating) VALUES (8, 1, 3, 01-01-2001, 5), " +
                    "(9, 2, 3, 01-01-2001, 4), (10, 3, 3, 01-01-2001, 4), (11, 4, 3, 01-01-2001, 4), (12, 5, 3, 01-01-2001, 4)", SharedDB.connection);
                SharedDB.command.ExecuteNonQuery();
            }
        }

        public string[] sanitizeArray(string[] dirty)
        {
            string[] clean;
            List<string> temp = new List<string>();
            foreach (string item in dirty)
            {
                if (item != null)
                {
                    temp.Add(item);
                }
            }

            clean = new string[temp.Count];

            for (int i = 0; i < clean.Length; i++)
            {
                clean[i] = temp[i];
            }

            return clean;
        }

        public void getAverageGameRatings()
        {
            string[] ids = getGameIDS();
            Array.Sort(ids);
            string[] ratingResults = new string[ids.Count()];
            double averageRating = 0f;
            SharedDB.setConnectionString();
            int counter = 0;

            int idCounter = 0;
            foreach (string id in ids)
            {

                if (id == null)
                {
                    continue;
                }
                else
                {
                    using (SharedDB.connection)
                    {
                        SharedDB.connection.Open();
                        //Maybe break it into two queries.
                        SharedDB.command = new MySqlCommand("SELECT rating FROM User_Game WHERE rating IS NOT NULL AND game_id = " + id, SharedDB.connection);
                        MySqlDataReader reader = SharedDB.command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int sum_of_ratings = 0;
                            int num_of_ratings = 0;
                            double[] ratings = new double[ids.Count()];
                            counter = 0;
                            while (reader.Read())
                            {
                                if (string.IsNullOrWhiteSpace(reader.GetString(0)))
                                {
                                    continue;
                                }
                                sum_of_ratings += reader.GetInt32(0);

                                counter++;
                            }

                            num_of_ratings = counter;

                            averageRating = Math.Round((double)sum_of_ratings / (double)num_of_ratings, 1);

                            ratingResults[idCounter] = averageRating.ToString();

                            reader.Close();
                        }

                        else
                        {
                            ratingResults[idCounter] = "N/A";
                        }
                    }
                }
                idCounter++;
            }

            ViewData["AverageRatings"] = sanitizeArray(ratingResults);
        }

    }
}
