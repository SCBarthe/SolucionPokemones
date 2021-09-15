using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modelo;


namespace negocio 
{
    public class ElementoNegocio
    {

      public List<Elemento> listar()
      {
            List<Elemento> lista = new List<Elemento>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearconsulta("select Id,Descripcion from elementos ");

                datos.ejecutarconsulta();
                while (datos.Lector.Read())
                {
                    //lista.Add(new Elemento((int)datos.Lector["Id"], (string)datos.Lector["Descripcion"]));

                    Elemento aux = new Elemento();
                    aux.Id = datos.Lector.GetInt32(0);
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(aux); 


                }


                return lista; 
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
        
                  
        


    }
}
