using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
    public class EngineersController: Controller
    {
        private readonly FactoryContext _db;

        public EngineersController(RegistrarContext db)
        {
            _db = db;
        }
    }
}