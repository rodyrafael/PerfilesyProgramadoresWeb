using demoaspcore.Models;
using demoaspcore.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class IndexDModel : PageModel
    {
        private readonly IConfiguration _config;

        public IndexDModel(IConfiguration config)
        {
            _config = config;
        }
        public List<Desarrollador> desarrollador { get; set; }

        //Se ejecuta al cargar la pagina
        public void OnGet()
        {
            string cadena = _config.GetConnectionString("CadenaTaller").ToString();
            SqlConnection cn = new SqlConnection(cadena);
            SqlCommand cmd = new SqlCommand("SELECT * FROM DESARROLLADOR", cn);
            cn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            desarrollador = new List<Desarrollador>();
            try
            {
                while (rd.Read())
                {
                    desarrollador.Add(new Desarrollador
                    {
                        IDDESARROLLADOR = (int)rd[0],
                        NOMBRES = rd[1].ToString(),
                        APELLIDOS = rd[2].ToString(),
                        DNI = rd[3].ToString(),
                        CORREO = rd[4].ToString()
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
