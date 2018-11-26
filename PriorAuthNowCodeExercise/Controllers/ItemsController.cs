using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PriorAuthNowCodeExercise.Models;

namespace PriorAuthNowCodeExercise.Controllers
{
    public class ItemsController : ApiController
    {
        private PriorAuthNowCodeExerciseContext db = new PriorAuthNowCodeExerciseContext();
        private Item[] items = new Item[]
        {
            new Item { Id=1, Name="Widget1", Description="Widget 1 Part", Price=9.99M },
            new Item { Id=2, Name="Widget2", Description="Widget 2 Part", Price=8.99M },
            new Item { Id=3, Name="Widget3", Description="Widget 3 Part", Price=7.99M }
        };

        // GET: api/Items
        public IEnumerable<Item> GetItems()
        {
            //return db.Items;
            return items;
        }

        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(int id)
        {
            //Item item = db.Items.Find(id);
            Item item = items.FirstOrDefault((p) => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [ResponseType(typeof(Item))]
        public IHttpActionResult UpdatePrice1(decimal newPrice)
        {
            items[0].Price = newPrice;
            return StatusCode(HttpStatusCode.OK);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Items
        [ResponseType(typeof(Item))]
        public IHttpActionResult PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult DeleteItem(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            db.SaveChanges();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.Id == id) > 0;
        }
    }
}