using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaHAS.Data;
using Microsoft.AspNetCore.Mvc;

namespace CopaApi.Controllers
{
[ApiController]
[Route("[COntroller]")]

    public class JogosSelecoesController : ControllerBase
    {
        private readonly DataContext _context;

        public JogosSelecoesController(DataContext context)
        {
            _context = context;
        }
    }
}