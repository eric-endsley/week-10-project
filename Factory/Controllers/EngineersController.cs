using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
    public class EngineersController: Controller
    {
        private readonly FactoryContext _db;

        public EngineersController(FactoryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Engineer> engList = _db.Engineers.ToList();
            engList.Sort((x, y) => string.Compare(x.EngineerLastName, y.EngineerLastName));
            return View(engList);
        }
    }
}