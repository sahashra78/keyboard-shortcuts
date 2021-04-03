using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keyboard_Shortcut_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Keyboard_Shortcut_API.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        public Shortcut shortcut;
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new
            {
                Self = Url.Link(nameof(GetRoot), null),
                All_Shortcuts = Url.Link(nameof(ShortcutController.GetAllShortcuts), null),
                Windows_Shortcuts = Url.Link(nameof(ShortcutController.GetShortcutsByOsId), new { operatingId = 1 }),
                MacOS_Shortcuts = Url.Link(nameof(ShortcutController.GetShortcutsByOsId), new { operatingId = 2 })
            };
            return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
        }
    }
}