using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using modelo; 




namespace negocio
{
    public class PokemonNegocio
    {

        public List<Pokemon> listar()
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "data source=.\\SQLEXPRESS; initial catalog=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                //comando.CommandText = "select nombre, descripcion, Numero, UrlImagen from pokemons";
                comando.CommandText = "select nombre, p.descripcion, Numero, UrlImagen, idTipo, t.Descripcion as tipo, d.descripcion as debilidad, idTipo, idDebilidad, p.id from pokemons p, elementos t, elementos d where p.IdTipo = t.Id and p.IdDebilidad = d.Id and p.Activo = 1";
                                 
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)lector["id"]; 
                    aux.Nombre = lector.GetString(0);
                    aux.Descripcion = (string)lector["descripcion"];
                    aux.Numero = lector.GetInt32(2);
                    aux.UrlImagen = (string)lector["UrlImagen"];
                    aux.Tipo = new Elemento();
                    aux.Tipo.Descripcion = (string)lector["tipo"];
                    aux.Tipo.Id = (int)lector["idTipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Descripcion = (string)lector["debilidad"];
                    aux.Debilidad.Id = (int)lector["idDebilidad"];

                    lista.Add(aux);

                }



                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificar(Pokemon pokemon)
        {
            AccesoDatos datos = new AccesoDatos();


            try
            {

                datos.setearconsulta("update POKEMONS set numero = @numero, nombre = @nombre, Descripcion = @descripcion, UrlImagen = @urlImagen, IdTipo = @idTipo, IdDebilidad = @idDebilidad Where id = @id ");             
                   
                datos.agregarparametro("@numero", pokemon.Numero);
                datos.agregarparametro("@nombre", pokemon.Nombre);
                datos.agregarparametro("@descripcion", pokemon.Descripcion);
                datos.agregarparametro("@urlImagen", pokemon.UrlImagen);
                datos.agregarparametro("@idTipo", pokemon.Tipo.Id);
                datos.agregarparametro("@idDebilidad", pokemon.Debilidad.Id);
                datos.agregarparametro("@id", pokemon.Id);

                datos.ejecutarAccion(); 




            }
            catch (Exception ex)
            {

                throw ex; 

            }
            finally
            {
                datos.cerrarConexion(); 
            }
        }

        public void eliminarFisico(int id)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearconsulta("Delete from POKEMONS Where id = "+ id );
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion(); 
            }



        }

        public void eliminarLogico(int id)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearconsulta("update POKEMONS set Activo = 0 Where id = " + id);
                datos.ejecutarAccion(); 
            }
            catch (Exception ex)
            {
                throw ex; 
            }

            finally
            {
                datos.cerrarConexion();   
            }

        }

        public void agregar(Pokemon Pokemon)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "data source=.\\SQLEXPRESS; initial catalog=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " Insert into POKEMONS Values (" + Pokemon.Numero + ",'" + Pokemon.Nombre + "', '" + Pokemon.Descripcion + "', '" + Pokemon.UrlImagen + "', @idTipo, @idDebilidad, null, 1)";
                comando.Parameters.AddWithValue("@idTipo", Pokemon.Tipo.Id);
                comando.Parameters.AddWithValue("@idDebilidad", Pokemon.Debilidad.Id); 

                
                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;     
                

            }
            finally
            {
                conexion.Close();
            }


                    
           
           
        }

    }


}
