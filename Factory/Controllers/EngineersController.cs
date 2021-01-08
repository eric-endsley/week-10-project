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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Engineer engineer)
        {
            _db.Engineers.Add(engineer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisEng = _db.Engineers
                .Include(engineer => engineer.Machines)
                .ThenInclude(join => join.Machine)
                .FirstOrDefault(engineer => engineer.EngineerId == id);
            return View(thisEng);
        }

        public ActionResult AddMachine(int id)
        {
            var thisEng = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
            return View(thisEng);
        }

        [HttpPost]
        public ActionResult AddMachine
        (Engineer engineer, int MachineId)
        {
            if (MachineId != 0)
            {
                var returnedJoin = _db.EngineerMachine
                    .Any(join => join.MachineId == MachineId && join.EngineerId == engineer.EngineerId);
                if (!returnedJoin)
                {
                    _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
                }
            }
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = engineer.EngineerId });
        }    
    }
}