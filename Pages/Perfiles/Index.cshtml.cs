using demoaspcore.Models;
using demoaspcore.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//importamos los espacions de nombres para los componentes ADO.NET
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;

        public IndexModel(IConfiguration config)
        {
            _config = config;
        }
        public List<Perfil> perfiles { get; set; }

        //Se ejecuta al cargar la pagina
        public void OnGet()
        {
            string cadena = _config.GetConnectionString("CadenaTaller").ToString();
            SqlConnection cn = new SqlConnection(cadena);
            SqlCommand cmd = new SqlCommand("SELECT * FROM perfil", cn);
            cn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            perfiles = new List<Perfil>();
            try
            {
                while (rd.Read())
                {
                    perfiles.Add(new Perfil
                    {
                        IdPerfil = (int)rd[0],
                        Nombre = rd[1].ToString(),
                        Descripcion = rd[2].ToString()
                    });
                }
            }
            finally
            {
                rd.Close(); // Ensure the reader is closed
                cn.Close(); // Close the connection after the loop
            }
        }
    }
}
