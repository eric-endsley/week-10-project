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

        public MachinesController(RegistrarContext db)
        {
            _db = db;
        }
    }
}