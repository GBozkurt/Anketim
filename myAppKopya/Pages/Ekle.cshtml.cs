using Azure.Core;
using Elfie.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using myApp.Models;
using myApp.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace myApp.Pages
{
    [Authorize]
    public class EkleModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly AppDbc2 context;
        private readonly UserManager<ApplicationUser> userManager;

		public bool sonuc ;

		public ApplicationUser? appUser;

        [BindProperty]
        public AnketDto anketDto { get; set; } = new AnketDto();

		public Veri2 Veri2 { get; set; } = new Veri2();
		

        public Anket Anket { get; set; } = new Anket();

		

		public EkleModel(IWebHostEnvironment environment, AppDbc2 context, UserManager<ApplicationUser> userManager)
        {
            this.environment = environment;
            this.context = context;
            this.userManager = userManager;
        }
        public void OnGet(int? id)
        {
            sonuc = true;
            if (id == null)
            {
                Response.Redirect("/Index");
                return;
            }
            var anket = context.Ankets.Find(id);
            if (anket == null)
            {
                Response.Redirect("/Index");
                return;
            }
            
            var task = userManager.GetUserAsync(User);
			var user = userManager.GetUserId(User);
			task.Wait();
			    
			anketDto.name = anket.name;
			anketDto.description = anket.description;
			anketDto.gc = anket.gc;
			anketDto.bc = anket.bc;
			anketDto.nc = anket.nc;
            anketDto.birinci= anket.birinci;
            anketDto.ikinci = anket.ikinci;
            anketDto.ucuncu = anket.ucuncu;
			appUser = task.Result;
			Anket = anket;

			string connStr = connectionString.GetConnectionString();
			SqlConnection conn = new SqlConnection(connStr);
			string sql = "SELECT * FROM Veri2 WHERE AnketId=@AnketId AND KullaniciId=@KullaniciId";
			using SqlCommand cmd = new SqlCommand(sql, conn);
			{
				cmd.Parameters.AddWithValue("@AnketId", anket.id);
                cmd.Parameters.AddWithValue("@KullaniciId", user);
                try
                {
					conn.Open();
					using SqlDataReader reader = cmd.ExecuteReader();
                    
                  
                    if (reader.Read())
                    {
                        sonuc = false;
                    }
					
				}
                finally
                {
					conn.Close();
				}
				
			}
			if (sonuc==false)
            {
				Response.Redirect("/Index");
				return;
			}

		}
        public string errorMessage = "";
        public string successMessage = "";
		
		public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Index");
                return;
            }
			if (id == 0)
			{
				Response.Redirect("/Index");
				return;
			}


			if (!ModelState.IsValid)
            {
                errorMessage = "Lütfen gerekli alanlarý giriniz";
                return;
            }

            var task = userManager.GetUserId(User);
            if (task == null)
            {
				Response.Redirect("/Index");
				return;
            }
			var anket = context.Ankets.Find(id);
            if (anket == null)
            {
                Response.Redirect("/Index");
                return;
            }

            Veri2 veri2 = new Veri2();
            veri2.KullaniciId = task;
            veri2.AnketId = anket.id;

			string secilen = Request.Form["secim"]!;
         
            

			if (secilen == "gc")
            {
                anket.gc += 1;

			}
            if (secilen == "bc")
            {
				anket.bc += 1;
				
			}
            if (secilen == "nc")
            {
				anket.nc += 1;
				
			}

            Anket = anket;
            context.Veri2.Add(veri2);
			context.SaveChanges();

            successMessage = "Note created successfully";

            Response.Redirect("/Index");
        }
    }
}
