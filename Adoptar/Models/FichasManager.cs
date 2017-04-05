using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Adoptar.Models
{   
    public class FichasManager
    {
        public void Insertar (Ficha ficha)
        {
            //SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBaseDeDatos"]);
            //SqlConnection conexion = new SqlConnection("Server=CPX-CYKSMSMJ1BK\\PEPITO;Database=Adoptame;Trusted_Connection=True;");
            SqlConnection conexion = new SqlConnection("Server=Aduki-PC\\SQLEXPRESS;Database=Adoptame;Trusted_Connection=True;");
            conexion.Open();
            SqlCommand sentencia = conexion.CreateCommand();
            sentencia.CommandText = "Insert into Fichas (Fecha, Tipo, Titulo, Texto, Imagen, Autor, Sexo, Ubicacion, Edad) VALUES (GETDATE(), @Tipo, @Titulo, @Texto, @Imagen, @Autor, @Sexo, @Ubicacion, @Edad)";
            //sentencia.Parameters.AddWithValue("@ID", ficha.ID);
            sentencia.Parameters.AddWithValue("@Titulo", ficha.Nombre);
            sentencia.Parameters.AddWithValue("@Texto", ficha.Texto);
            sentencia.Parameters.AddWithValue("@Imagen", ficha.Imagen);
            sentencia.Parameters.AddWithValue("@Autor", ficha.Autor.Mail);
            sentencia.Parameters.AddWithValue("@Sexo", ficha.Sexo);
            sentencia.Parameters.AddWithValue("@Ubicacion", ficha.Ubicacion);
            sentencia.Parameters.AddWithValue("@Edad", ficha.Edad);
            sentencia.Parameters.AddWithValue("@Tipo", ficha.Tipo);
            sentencia.ExecuteNonQuery();
            conexion.Close();
        }



        public void Modificar (Ficha ficha)
        {
            //SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBaseDeDatos"]);

            //SqlConnection conexion = new SqlConnection("Server=CPX-CYKSMSMJ1BK\\PEPITO;Database=Adoptame;Trusted_Connection=True;");
            SqlConnection conexion = new SqlConnection("Server=Aduki-PC\\SQLEXPRESS;Database=Adoptame;Trusted_Connection=True;");
            conexion.Open();
            SqlCommand sentencia = conexion.CreateCommand();
            sentencia.CommandText = "UPDATE Fichas set Tipo=@Tipo, Titulo=@Titulo, Texto=@Texto, Imagen=@Imagen, Autor=@Autor, Sexo=@Sexo, Ubicacion=@Ubicacion, Edad=@Edad WHERE ID = @ID";

            sentencia.Parameters.AddWithValue("@Titulo", ficha.Nombre);
            sentencia.Parameters.AddWithValue("@Texto", ficha.Texto);
            sentencia.Parameters.AddWithValue("@Imagen", ficha.Imagen);
            sentencia.Parameters.AddWithValue("@Autor", "jorgegutierrez@gmail.com");
            sentencia.Parameters.AddWithValue("@Sexo", ficha.Sexo);
            sentencia.Parameters.AddWithValue("@Ubicacion", ficha.Ubicacion);
            sentencia.Parameters.AddWithValue("@Edad", ficha.Edad);
            sentencia.Parameters.AddWithValue("@Tipo", ficha.Tipo);
            sentencia.Parameters.AddWithValue("@ID", ficha.ID);

            sentencia.ExecuteNonQuery();
            conexion.Close();

        }

        public List<Ficha> ConsultarTodos()
        {
            List<Ficha> fichas = new List<Ficha>();

            //1-Conexión.. a qué BBDD
            //SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBaseDeDatos"]);
            //SqlConnection conexion = new SqlConnection("Server=CPX-CYKSMSMJ1BK\\PEPITO;Database=Adoptame;Trusted_Connection=True;");
            SqlConnection conexion = new SqlConnection("Server=Aduki-PC\\SQLEXPRESS;Database=Adoptame;Trusted_Connection=True;");

            //2-nos conectamos
            conexion.Open();
            //3-creamos el objeto que nos permite escribir la sentencia
            SqlCommand sentencia = conexion.CreateCommand();
            //4-escribrimos la sentencia
            sentencia.CommandText = "SELECT * FROM Fichas ORDER by ID DESC";
            //5-ejecutamos la consulta
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Ficha ficha = new Ficha();
                ficha.Nombre= reader["Titulo"].ToString();
                ficha.Texto = (string)reader["Texto"];
                ficha.Imagen = reader["Imagen"].ToString();
                ficha.ID = (int)reader["Id"];
                ficha.Sexo = (string)reader["Sexo"];
                ficha.Ubicacion = (string)reader["Ubicacion"];
                ficha.Edad = (string)reader["Edad"];
                //ficha.Fecha = (DateTime)reader["Fecha"];

                fichas.Add(ficha);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            conexion.Close();

            return fichas;
        }
        public List<Ficha> ConsultarMios(Usuario usuario)
        {
            List<Ficha> fichas = new List<Ficha>();

            //1-Conexión.. a qué BBDD
            //SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBaseDeDatos"]);
            //SqlConnection conexion = new SqlConnection("Server=CPX-CYKSMSMJ1BK\\PEPITO;Database=Adoptame;Trusted_Connection=True;");
            SqlConnection conexion = new SqlConnection("Server=Aduki-PC\\SQLEXPRESS;Database=Adoptame;Trusted_Connection=True;");

            //2-nos conectamos
            conexion.Open();
            //3-creamos el objeto que nos permite escribir la sentencia
            SqlCommand sentencia = conexion.CreateCommand();
            //4-escribrimos la sentencia
            sentencia.CommandText = "SELECT * FROM Fichas WHERE Autor = @Autor";
            //5-agregamos los parametros
            sentencia.Parameters.AddWithValue("@Autor", usuario.Nombre);

            //6-ejecutamos la consulta


            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Ficha ficha = new Ficha();
                ficha.Nombre = reader["Titulo"].ToString();
                ficha.Texto = (string)reader["Texto"];
                ficha.Imagen = reader["Imagen"].ToString();
                ficha.ID = (int)reader["Id"];
                ficha.Sexo = (string)reader["Sexo"];
                ficha.Ubicacion = (string)reader["Ubicacion"];
                ficha.Edad = (string)reader["Edad"];
                //ficha.Fecha = (DateTime)reader["Fecha"];

                fichas.Add(ficha);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            conexion.Close();

            return fichas;
        }
        public List<Ficha> Buscar(Ficha ficha)
        {
            List<Ficha> fichas = new List<Ficha>();

            //1-Conexión.. a qué BBDD
            //SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBaseDeDatos"]);
            //SqlConnection conexion = new SqlConnection("Server=CPX-CYKSMSMJ1BK\\PEPITO;Database=Adoptame;Trusted_Connection=True;");
            SqlConnection conexion = new SqlConnection("Server=Aduki-PC\\SQLEXPRESS;Database=Adoptame;Trusted_Connection=True;");
            //2-nos conectamos
            conexion.Open();
            //3-creamos el objeto que nos permite escribir la sentencia
            SqlCommand sentencia = conexion.CreateCommand();
            //4-escribrimos la sentencia
            sentencia.CommandText = "SELECT * FROM Fichas";
            //sentencia.CommandText = "SELECT * FROM Fichas WHERE Tipo = @Tipo , Edad = @Edad, Sexo = @Sexo";
            //5-agregamos los parametros
            //sentencia.Parameters.AddWithValue("@Tipo", ficha.Tipo);
            //sentencia.Parameters.AddWithValue("@Edad", ficha.Edad);
            //sentencia.Parameters.AddWithValue("@Sexo", ficha.Sexo);
            //6-ejecutamos la consulta


            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Ficha fichan = new Ficha();
                fichan.Nombre = reader["Titulo"].ToString();
                fichan.Texto = (string)reader["Texto"];
                fichan.Imagen = reader["Imagen"].ToString();
                fichan.ID = (int)reader["Id"];
                fichan.Sexo = (string)reader["Sexo"];
                fichan.Ubicacion = (string)reader["Ubicacion"];
                fichan.Edad = (string)reader["Edad"];
                fichan.Tipo = (string)reader["Tipo"];
                //ficha.Fecha = (DateTime)reader["Fecha"];

                fichas.Add(fichan);
            }
    
            List<Ficha> fichasfiltradas = new List<Ficha>();

            foreach (Ficha fichan in fichas)
            {
                if ((ficha.Tipo.Equals("-1") || (fichan.Tipo.Contains(ficha.Tipo))) && (ficha.Edad.Equals("-1") || fichan.Edad.Contains(ficha.Edad))
                    && (ficha.Sexo.Equals("-1") || fichan.Sexo.Contains(ficha.Sexo)))
                
                {
                    fichasfiltradas.Add(fichan);
                }

            }
                //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
                reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            conexion.Close();

            return fichasfiltradas;
        }

        public Ficha Consultar(long id)
        {
            //SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBaseDeDatos"]);
            //SqlConnection conexion = new SqlConnection("Server=CPX-CYKSMSMJ1BK\\PEPITO;Database=Adoptame;Trusted_Connection=True;");
            SqlConnection conexion = new SqlConnection("Server=Aduki-PC\\SQLEXPRESS;Database=Adoptame;Trusted_Connection=True;");
            conexion.Open();
            SqlCommand sentencia = conexion.CreateCommand();
            sentencia.CommandText = "SELECT* FROM Fichas WHERE ID = @ID";
            sentencia.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = sentencia.ExecuteReader();
            reader.Read();
            Ficha ficha = new Ficha();
            ficha.Nombre = reader["Titulo"].ToString();
            ficha.Texto = (string)reader["Texto"];
            ficha.Imagen = reader["Imagen"].ToString();
            ficha.ID = (int)reader["Id"];
            ficha.Sexo = (string)reader["Sexo"];
            ficha.Ubicacion = (string)reader["Ubicacion"];
            ficha.Edad = (string)reader["Edad"];
           
            //Usuario autor = new Usuario();
            //autor.Nombre = (string)reader["Autor"];
            ficha.Autor = new Usuario();
            ficha.Autor.Nombre = (string)reader["Autor"];
            conexion.Close();


            return ficha;
        }

        public void Eliminar(long id)
        {
            ////SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBaseDeDatos"]);
            //SqlConnection conexion = new SqlConnection("Server=CPX-CYKSMSMJ1BK\\PEPITO;Database=Adoptame;Trusted_Connection=True;");
            SqlConnection conexion = new SqlConnection("Server=Aduki-PC\\SQLEXPRESS;Database=Adoptame;Trusted_Connection=True;");
            conexion.Open();
            SqlCommand sentencia = conexion.CreateCommand();
            sentencia.CommandText = "DELETE FROM Fichas WHERE ID = @ID";
            sentencia.Parameters.AddWithValue("@id", id);
            sentencia.ExecuteNonQuery();
            conexion.Close();
        }
    }
        
        
}