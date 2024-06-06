using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using myApp.Models;
using myApp.Services;

namespace myApp.Pages
{
    public class OylayanlarModel : PageModel
    {
		private readonly AppDbc2 context;
		public OylayanlarModel(AppDbc2 context)
        {
			this.context = context;
		}

		List<string> idler = new List<string>();

		public Veri2 Veri2 { get; set; } = new Veri2();

		[BindProperty]
		public AnketDto anketDto { get; set; } = new AnketDto();
		public void OnGet(int? id)
		{
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
			anketDto.name = anket.name;

			string connStr = connectionString.GetConnectionString();
			SqlConnection conn = new SqlConnection(connStr);
			SqlConnection conn2 = new SqlConnection(connStr);
			string sql = "SELECT KullaniciId FROM Veri2 WHERE AnketId=@AnketId";
			using SqlCommand cmd = new SqlCommand(sql, conn);
			{
				cmd.Parameters.AddWithValue("@AnketId", id);
				try
				{
					conn.Open();
					using SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						idler.Add(reader.GetString(0));
					}
						
					
					
					
				}
				finally
				{
					conn.Close();
				}
			}
			foreach (var item in idler)
			{
				if (item != null)
				{
					string userData = "SELECT * FROM AspNetUsers WHERE Id=@Id";
					using SqlCommand command = new SqlCommand(userData, conn);
					{
						string a = item;
						command.Parameters.AddWithValue("@Id", a);
						try
						{
							conn.Open();
							using SqlDataReader reader2 = command.ExecuteReader();
							while (reader2.Read())
							{
								string ad = reader2["FirstName"].ToString()!;
								string soyAd = reader2["LastName"].ToString()!;
								anketDto.description += ad +" "+soyAd+ ", ";
							}
						}
						finally { conn.Close(); }
					}

				}
			}
			
		}
    }
}
