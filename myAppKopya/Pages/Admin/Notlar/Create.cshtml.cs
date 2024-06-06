using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using myApp.Models;
using myApp.Services;
using Microsoft.Extensions.Caching.Distributed;
namespace myApp.Pages.Admin.Notlar
{
    [Authorize]
    public class CreateModel : PageModel
    {
		private readonly IWebHostEnvironment environment;
		private readonly AppDbc2 context;
        private readonly UserManager<ApplicationUser> userManager;
		private readonly IMemoryCache _memoryCache;

		public ApplicationUser? appUser;

		
		

		public void ClearCache()
		{
			_memoryCache.Remove("CacheKey");
		}
		[BindProperty]
		public AnketDto anketDto { get; set; } = new AnketDto();
		public CreateModel(IWebHostEnvironment environment,AppDbc2 context, UserManager<ApplicationUser> userManager, IMemoryCache memoryCache)
		{
			this.environment = environment;
			this.context = context;
            this.userManager = userManager;
			_memoryCache = memoryCache;
		}

        public void OnGet()
        {
            var task = userManager.GetUserAsync(User);
            task.Wait();
            appUser =task.Result;
        }


        public string errorMessage = "";
        public string successMessage = "";
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Lütfen gerekli alanlarý giriniz";
                return;
            }



			Anket anket = new Anket()
			{
				name = anketDto.name,
				description = anketDto.description,
				gc = anketDto.gc,
				bc = anketDto.bc,
				nc = anketDto.nc,
				birinci = anketDto.birinci,
				ikinci = anketDto.ikinci,
				ucuncu = anketDto.ucuncu,
				CreatedDate = DateTime.Now,
			};

			context.Ankets.Add(anket);
			context.SaveChanges();

			anketDto.name = "";
			anketDto.description = "";
			anketDto.birinci = "";
			anketDto.ikinci = "";
			anketDto.ucuncu = "";
			ModelState.Clear();

            successMessage = "Note created successfully";

            Response.Redirect("/Admin/Notlar/Index");


        }
    }
}
