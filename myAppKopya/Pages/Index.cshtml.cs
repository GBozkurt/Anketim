using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myApp.Models;
using myApp.Services;

namespace myApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDbc2 context;

       
        public List<Anket> Ankets { get; set; } = new List<Anket>();

        public IndexModel(ILogger<IndexModel> logger,AppDbc2 context)
        {
            _logger = logger;
            this.context = context;
        }

        public void OnGet()
        {
            Ankets= context.Ankets.OrderByDescending(p=>p.id).ToList();

        }
    }
}