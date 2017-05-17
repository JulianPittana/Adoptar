using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Adoptar.Models
{
    public class ConexionManager
    {
        public SqlConnection Conectar()
        {
            //SqlConnection conexion = new SqlConnection("Server=Aduki-PC\\SQLEXPRESS;Database=Adoptame;Trusted_Connection=True;");
            SqlConnection conexion = new SqlConnection("Server=Adoptar.mssql.somee.com;Database=Adoptar;User ID = Julip_SQLLogin_1; Password = m8pnvr9q47; Trusted_Connection = False;");
            return conexion;
        }
    }
}


