using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lunch_Box.Controllers
{
    public class HomeController : Controller
    {
        private lunchboxEntities db = new lunchboxEntities();

        public async Task<ActionResult> Index()
        {
            return View(await db.items.ToListAsync());
        }

    }
}