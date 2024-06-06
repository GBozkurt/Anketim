using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myApp.Models;
using myApp.Services;

namespace myApp.Pages.Admin.Notlar
{
    public class IndexModel : PageModel
    {
        private readonly AppDbc2 context;

		public List<Anket> Ankets { get; set; } = new List<Anket>();
		public IndexModel(AppDbc2 context)
        {
            this.context = context;
        }
        public void OnGet()
        {
			Ankets = context.Ankets.OrderByDescending(p => p.id).ToList();
		}
    }
}
