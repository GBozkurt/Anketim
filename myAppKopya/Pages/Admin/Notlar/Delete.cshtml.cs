using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myApp.Services;

namespace myApp.Pages.Admin.Notlar
{
	[Authorize(Roles = "admin")]
	public class DeleteModel : PageModel
    {
		private readonly IWebHostEnvironment environment;
		private readonly AppDbc2 context;

		public DeleteModel(IWebHostEnvironment environment, AppDbc2 context)
        {
			this.environment = environment;
			this.context = context;
		}
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Notlar/Index");
                return;
            }

            var anket = context.Ankets.Find(id);
            if (anket == null)
            {
                Response.Redirect("(Admin/Notlar/Index");
                return; 
            }

            context.Ankets.Remove(anket);
            context.SaveChanges();

            Response.Redirect("/Admin/Notlar/Index");   
        }
    }
}
