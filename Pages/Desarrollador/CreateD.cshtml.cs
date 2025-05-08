using demoaspcore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class CreateDModel : PageModel
    {
        private readonly IConfiguration _config;
        public CreateDModel(IConfiguration config)
        {
            _config = config;
        }
        [BindProperty]
        public Desarrollador desarrollador { get; set; }
        public IActionResult OnPost()
        {
            string cadena = _config.GetConnectionString("CadenaTaller").ToString();
            SqlConnection cn = new SqlConnection(cadena);
            SqlCommand cmd = new SqlCommand("INSERT INTO DESARROLLADOR VALUES (@nom, @ape, @dni, @correo)", cn); // Corrected table name
            cmd.Parameters.AddWithValue("@nom", desarrollador.NOMBRES);
            cmd.Parameters.AddWithValue("@ape", desarrollador.APELLIDOS);
            cmd.Parameters.AddWithValue("@dni", desarrollador.DNI);
            cmd.Parameters.AddWithValue("@correo", desarrollador.CORREO);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToPage("IndexD");
        }
    }
}
