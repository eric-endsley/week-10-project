using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Factory.Controllers
{
    public class MachinesController: Controller
    {
        private readonly FactoryContext _db;

        public MachinesController(FactoryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View (_db.Machines.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Machine machine)
        {
            _db.Machines.Add(machine);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisMachine = _db.Machines
                .Include(machine => machine.Engineers)
                .ThenInclude(join => join.Engineer)
                .FirstOrDefault(machine=> machine.MachineId == id);
            return View(thisMachine);
        }

        public ActionResult AddEngineer(int id)
        {
            var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
            var engineers = _db.Engineers
                .Select(s => new 
                {
                    EngineerId = s.EngineerId,
                    Text = s.EngineerFirstName + " " + s.EngineerLastName
                })
                .ToList();
            List<SelectListItem> db = new SelectList(engineers, "EngineerId", "Text" ).ToList();
            SelectListItem blank = new SelectListItem () {
                Value = "0",
                Text = "--Select Engineer--"
            };
            db.Insert(0, blank);
            ViewBag.EngineerId = new SelectList(db, "Value", "Text");

            return View(thisMachine);
        }

        [HttpPost]
        public ActionResult AddEngineer
        (Machine machine, int EngineerId)
        {
            if (EngineerId != 0)
            {
                var returnedJoin = _db.EngineerMachine
                    .Any(join => join.EngineerId == EngineerId && join.MachineId == machine.MachineId);
                if (!returnedJoin)
                {
                    _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId =  machine.MachineId });
                }
            }
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = machine.MachineId });
        }    
    }
}