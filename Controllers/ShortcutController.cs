using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keyboard_Shortcut_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Keyboard_Shortcut_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShortcutController : ControllerBase
    {
        private readonly ShortcutContext _shortcutContext;

        // Creating a controller
        public ShortcutController(ShortcutContext shortcutContext)
        {
            _shortcutContext = shortcutContext;
            _shortcutContext.Database.EnsureCreated();
        }

        // Creating Get Method
        [HttpGet(Name = nameof(GetAllShortcuts))]
        public async Task<IActionResult> GetAllShortcuts()
        {
            IQueryable<Shortcut> shortcuts = _shortcutContext.Shortcuts;
            return Ok(JsonConvert.SerializeObject(await shortcuts.ToArrayAsync(), Formatting.Indented));
        }

        // Creating Get Method to find the shortcut by Id
        [HttpGet("{shortcutId}", Name = nameof(GetShortcutById))]
        public async Task<IActionResult> GetShortcutById(int shortcutId)
        {
            var shortcuts = await _shortcutContext.Shortcuts.FindAsync(shortcutId);
            
            if (shortcuts == null)
            {
                return NotFound();
            }
            return Ok(JsonConvert.SerializeObject(shortcuts, Formatting.Indented));
        }

        // Creating Get Method to find the shortcut by Operating System
        [HttpGet("Os/{operatingId}", Name = nameof(GetShortcutsByOsId))]
        public async Task<IActionResult> GetShortcutsByOsId(int operatingId)
        {
            IQueryable<Shortcut> shortcuts = _shortcutContext.Shortcuts;
            var shortcutByOs = shortcuts.Where(s => s.OsId == operatingId);
            if(shortcutByOs == null)
            {
                return NotFound();
            }
            return Ok(JsonConvert.SerializeObject(await shortcutByOs.ToArrayAsync(), Formatting.Indented));
        }

        // Creating a Post Method to insert a new shortcut
        [HttpPost]
        public async Task<ActionResult<Shortcut>> PostShortcut([FromBody] Shortcut shortcut)
        {
            var newShortcut = shortcut;
            newShortcut.Source = shortcut.Source + " (Contributor)";

            _shortcutContext.Shortcuts.Add(newShortcut);
            await _shortcutContext.SaveChangesAsync();

            return CreatedAtAction(
                    "GetShortcutById",
                    new {id = newShortcut.Id},
                    newShortcut
                );
        }

        // Creating a Put Method to update an existing shortcut
        [HttpPut("{id}")]
        public async Task<ActionResult<Shortcut>> PutShortcut([FromRoute] int id, [FromBody] Shortcut shortcut)
        {
            if(id != shortcut.Id)
            {
                return BadRequest();
            }

            _shortcutContext.Entry(shortcut).State = EntityState.Modified;

            try
            {
                await _shortcutContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(_shortcutContext.Shortcuts.Find(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
            return shortcut;
        }

        // Creating a Delete Method to delete an existing shortcut
        [HttpDelete("{id}")]
        public async Task<ActionResult<Shortcut>> DeleteShortcut([FromRoute] int id)
        {
            var selectedShortcut = await _shortcutContext.Shortcuts.FindAsync(id);

            if(selectedShortcut == null)
            {
                return NotFound();
            }

            _shortcutContext.Shortcuts.Remove(selectedShortcut);

            await _shortcutContext.SaveChangesAsync();
            
            return selectedShortcut;
        }
    }
}