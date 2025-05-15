using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class Index1Model : PageModel
    {
        public List<int> ProductIds { get; set; } = new List<int>();

        public void OnGet()
        {
            var json = HttpContext.Session.GetString("winkelmandje");

            if (!string.IsNullOrEmpty(json))
            {
                ProductIds = JsonSerializer.Deserialize<List<int>>(json);
            }
        }
    }
}





