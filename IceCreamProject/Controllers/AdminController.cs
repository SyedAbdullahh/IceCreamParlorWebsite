using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace IceCreamProject.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Login()
        {
            // code that takes us to the login page
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string UserName,string Password)
        {
            //Code that Verifies that Username password is correct.
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(projectPath)
        .AddJsonFile("appsettings.json")
        .Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connectionString);
            string a = "Active";
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from [Admins] where UserName ='" + UserName + "' and Password='" + Password + "'", con);
            SqlDataReader sddr = cmd.ExecuteReader();
            if (sddr.Read())
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return NotFound();
            }

            return View();

        }
        public IActionResult Index()
        {
            // code that takes us to the Admin Dashboard(Index)
            return View();
        }
    }
}
