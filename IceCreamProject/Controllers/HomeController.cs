using IceCreamProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace IceCreamProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public static class CommonMethod
        {
            // code that converts a Data Table into a Model List
            public static List<T> ConvertToList<T>(DataTable dt)
            {
                var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
                var properties = typeof(T).GetProperties();
                return dt.AsEnumerable().Select(row => {
                    var objT = Activator.CreateInstance<T>();
                    foreach (var pro in properties)
                    {
                        if (columnNames.Contains(pro.Name.ToLower()))
                        {
                            try
                            {
                                pro.SetValue(objT, row[pro.Name]);
                            }
                            catch (Exception ex) { }
                        }
                    }
                    return objT;
                }).ToList();
            }
        }

        public IActionResult Index()
        {
            //Code that shows us the Main Landing page of Website and the list of recipe contributers over there.
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(projectPath)
        .AddJsonFile("appsettings.json")
        .Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connectionString);
            string a = "Admin";
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Recipes where Recipe_By<>'" +a + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            List<Recipe> asd = new List<Recipe>();
            asd = CommonMethod.ConvertToList<Recipe>(dt);
           
            return View(asd);
        }

        
        public IActionResult AboutUs()
        {
            // code that takes one to the about us page.
            return View();

        }
        
        public IActionResult ContactUs()
        {
            // code that takes us to the contact us page.
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}