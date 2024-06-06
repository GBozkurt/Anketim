using Newtonsoft.Json.Linq;

namespace myApp
{
	public class connectionString
	{

		public static string GetConnectionString() {

			string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
			string jsonContent = File.ReadAllText(jsonFilePath);
				
			JObject json = JObject.Parse(jsonContent);
			string cString = json.SelectToken("ConnectionStrings.DefaultConnection").Value<string>();
			return cString;
		}
	}
}
