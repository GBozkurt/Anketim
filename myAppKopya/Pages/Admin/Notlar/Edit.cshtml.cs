using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myApp.Models;
using myApp.Services;

namespace myApp.Pages.Admin.Notlar
{
	[Authorize(Roles = "admin")]
	public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly AppDbc2 context;

        [BindProperty]
        public AnketDto AnketDto { get; set; } = new AnketDto();


        public Anket Anket { get; set; } = new Anket();
       

        public string errorMessage = "";
        public string successMessage = "";

        public EditModel(IWebHostEnvironment environment, AppDbc2 context)
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
                Response.Redirect("/Admin/Notlar/Index");
                return;
            }
            AnketDto.name = anket.name;
            AnketDto.description = anket.description;
            AnketDto.id=anket.id;
            Anket = anket;

        }
        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Notlar/Index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Lütfen gerekli alanlarý doldurunuz.";
                return;
            }

            var anket = context.Ankets.Find(id);
            if (anket == null) {
                Response.Redirect("/Admin/Notlar/Index");
                return;
            }
            anket.name = AnketDto.name;
            anket.description = AnketDto.description;

            Anket = anket;
            context.SaveChanges();


			successMessage = "Not baþarýyla güncellendi!";

            Response.Redirect("/Admin/Notlar/Index");
        }
    }
}
