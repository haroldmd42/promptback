using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisageAI.Api.Data;
using VisageAI.Api.Models;

namespace VisageAI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromptsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PromptsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /prompts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prompt>>> GetPrompts()
        {
            return await _context.Prompts.ToListAsync();
        }

        // POST /prompts
        [HttpPost]
        public async Task<ActionResult<Prompt>> CreatePrompt(Prompt prompt)
        {
            _context.Prompts.Add(prompt);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPrompts), new { id = prompt.Id }, prompt);
        }
    }
}
