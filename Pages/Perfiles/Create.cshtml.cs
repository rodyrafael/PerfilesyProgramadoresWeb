using demoaspcore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _config;
        public CreateModel(IConfiguration config)
        {
            _config = config;
        }
        [BindProperty]
        public Perfil perfil { get; set; }
        public IActionResult OnPost()
        {
            string cadena = _config.GetConnectionString("CadenaTaller").ToString();
            SqlConnection cn = new SqlConnection(cadena);
            SqlCommand cmd = new SqlCommand("insert into perfil values (@nom,@des)", cn);
            cmd.Parameters.AddWithValue("@nom", perfil.Nombre);
            cmd.Parameters.AddWithValue("@des", perfil.Descripcion);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToPage("Index"); 
        }
    }
}
