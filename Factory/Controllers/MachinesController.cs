using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Factory.Models;

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
            ViewBag.EngineerId = new SelectList(engineers, "EngineerId", "Text" );
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