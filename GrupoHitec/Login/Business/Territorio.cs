using System;
using System.Collections.Generic;
using System.Linq;

namespace GrupoHitec.Login.Business
{
    /// <summary>
    /// Clase de lógica de negocio de territorio.
    /// </summary>
    public class Territorio
    {
        /// <summary>
        /// Método estático que permite obtener el listado de los territorios en formato de cadena separados por "coma".
        /// </summary>
        /// <returns>Retorna una cadena con los territorios asociados al usuario.</returns>
        public static string ObtenerTerritoriosComoCadena(Entities.Usuario usuario)
        {
            string Territorios = string.Empty;
            if (usuario.Territorios != null)
            {
                if (usuario.Territorios.Count > 0)
                {
                    if (!usuario.Territorios.Any(v => v.Territory == "*"))
                    {
                        Territorios = "";
                        for (int x = 0; x < usuario.Territorios.Count; x++)
                        {
                            Territorios += "'" + usuario.Territorios.ElementAt(x).Territory + "'";
                            if (x < usuario.Territorios.Count - 1)
                            {
                                Territorios += ",";
                            }
                        }
                    }
                    else
                    {
                        Territorios = "*";
                    }
                }
            }
            return Territorios;
        }

        /// <summary>
        /// Método que permite obtener los territorios asociados al usuario.
        /// </summary>
        /// <param name="empresa">Nombre de la empresa a la cuál se encuentra asociado el usuario.</param>
        /// <returns>Retorna un listado de territorios asociados al usuario.</returns>
        public static List<Entities.Territorio> ObtenerTerritorios(string empresa)
        {
            return new List<Entities.Territorio>();
            /*List<Territorio> dataObjects = null;
            List<string> columnName = null;
            dynamic element = null;
            PropertyInfo propertyInfo = null;
            columnName = new List<string>();
            dynamic value = null;
            Type dataType = null;
            try
            {
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("SELECT Territory, TerritoryId, Empresa FROM ec_UserTerritory WHERE [User] = @Usuario GROUP BY Territory, TerritoryId, Empresa", conn))
                    {
                        if (!string.IsNullOrEmpty(this.UserName))
                        {
                            command.Parameters.Add("Usuario", SqlDbType.NVarChar).Value = this.UserName;
                        }
                        using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                        {
                            for (int x = 0; x < reader.FieldCount; x++)
                            {
                                columnName.Add(reader.GetName(x));
                            }
                            dataObjects = new List<Territorio>();
                            while (reader.Read())
                            {
                                element = Activator.CreateInstance(typeof(Territorio));
                                for (int x = 0; x < columnName.Count; x++)
                                {
                                    propertyInfo = element.GetType().GetProperty(columnName[x]);
                                    dataType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                    value = (reader[columnName[x]] == null ? null : (reader[columnName[x]] == DBNull.Value ? null : Convert.ChangeType(reader[columnName[x]], dataType)));
                                    propertyInfo.SetValue(element, value, null);
                                    propertyInfo = null;
                                }
                                dataObjects.Add(element);
                                element = null;
                            }
                            reader.Close();
                        }
                    }
                    conn.Close();
                }
                if (!string.IsNullOrEmpty(empresa))
                {
                    if (dataObjects != null)
                    {
                        if (dataObjects.Count > 0)
                        {
                            if (dataObjects.Any(v => v.Empresa == "*"))
                            {
                                dataObjects = dataObjects.Where(v => v.Empresa == "*").ToList();
                            }
                            else
                            {
                                dataObjects = dataObjects.Where(v => v.Empresa == empresa).ToList();
                            }
                        }
                        return dataObjects;
                    }
                    else
                    {
                        return new List<Territorio>();
                    }
                }
                else
                {
                    return new List<Territorio>();
                }
            }
            catch
            {
                return new List<Territorio>();
            }
            finally
            {
                dataObjects = null;
                columnName = null;
                element = null;
                propertyInfo = null;
                columnName = null;
                value = null;
                dataType = null;
            }*/
        }
    }
}
