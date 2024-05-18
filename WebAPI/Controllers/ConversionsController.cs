using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using DTO;
using WebApi.Mapper;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionsController : ControllerBase
    {
        private readonly PrintOMatic_Context _context;

        public ConversionsController(PrintOMatic_Context context)
        {
            _context = context;
        }

        // GET: api/Conversions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConversionDTO>>> GetConversions()
        {
            IEnumerable<Conversion> conversions = await _context.Conversions.ToListAsync();
            List<ConversionDTO> result = new List<ConversionDTO>();
            if (conversions != null && conversions.Count() > 0)
            {
                foreach (Conversion conversion in conversions)
                {
                    result.Add(ConversionMapper.toDTO(conversion));
                }
            }
            return result;
        }

        // GET: api/Conversions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConversionDTO>> GetConversion(int id)
        {
            var conversion = await _context.Conversions.FindAsync(id);

            if (conversion == null)
            {
                return NotFound();
            }
            
            ConversionDTO conversionDTO = ConversionMapper.toDTO(conversion);

            return conversionDTO;
        }

        // PUT: api/Conversions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConversion(int id, ConversionDTO conversionDTO)
        {
            if (id != conversionDTO.ConversionId)
            {
                return BadRequest();
            }

            Conversion conversion;

            try { 
                conversion = ConversionMapper.toDAL(conversionDTO);
            } catch (Exception e)
            {
                return StatusCode(500);
            }

            _context.Entry(conversion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Conversions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConversionDTO>> PostConversion(ConversionDTO conversionDTO)
        {
            Conversion conversion;

            try
            {
                conversion = ConversionMapper.toDAL(conversionDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

            _context.Conversions.Add(conversion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConversion", new { id = conversion.Id }, conversion);
        }

        // DELETE: api/Conversions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConversion(int id)
        {
            var conversion = await _context.Conversions.FindAsync(id);
            if (conversion == null)
            {
                return NotFound();
            }

            _context.Conversions.Remove(conversion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConversionExists(int id)
        {
            return _context.Conversions.Any(e => e.Id == id);
        }
    }
}
