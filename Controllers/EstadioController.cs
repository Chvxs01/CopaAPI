using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CopaApi.Models;
using CopaHAS.Data;
using CopaHAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CopaHAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadioController : ControllerBase
    {
        private readonly DataContext _context;

        public EstadioController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")] //Buscar pelo id
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                EstadioController e = await _context.TB_ESTADIO
                    .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                return Ok(e);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Estadio> lista = await _context.TB_ESTADIO.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Estadio novoEstadio)
        {
           
      

                await _context.TB_ESTADIO.AddAsync(novoEstadio);
                await _context.SaveChangesAsync();

                return Ok(novoEstadio.Id);
            
        }

        [HttpPut]
        public async Task<IActionResult> Update(Estadio estadio)
        {
            try
            {
                if (estadio.Capacidade >= 100000)
                    return BadRequest("Capacidade não pode ser maior/igual a 100000.");

                _context.TB_ESTADIO.Update(estadio);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Estadio eRemover = await _context.TB_ESTADIO
                    .FirstOrDefaultAsync(p => p.Id == id);

                _context.TB_ESTADIO.Remove(eRemover);
                int linhaAfetadas = await _context.SaveChangesAsync();
                return Ok(linhaAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        public static implicit operator EstadioController(Estadio v)
        {
            throw new NotImplementedException();
        }
    }
    //Fim da classe controller. Não programe nada aqui.
}



