using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace GrupoHitec.MSSQLServer
{
    /// <summary>
    /// Clase que permite la conexión a la base de datos.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Cadena de conexión a la base de datos.
        /// </summary>
        private string ConnectionString { get; set; }

        /// <summary>
        /// Constructor de la clase el cual inicializa las propiedades de la misma.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión a la base de datos.</param>
        public Connection(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        /// <summary>
        /// Método que permite obtener los datos de un SELECT en SQL.
        /// </summary>
        /// <param name="query">Cadena que contiene la consulta SQL.</param>
        /// <param name="type">Tipo de dato para mapear los resultados a un listado de objetos.</param>
        /// <returns>Retorna una instancia de la clase SqlCollectionResult.</returns>
        public SqlCollectionResult GetData(string query, Type type)
        {
            Type dataType = null;
            dynamic value = null;
            Type listType = null;
            dynamic element = null;
            dynamic dataObjects = null;
            PropertyInfo propertyInfo = null;
            List<string> columnName = new List<string>();
            SqlCollectionResult result = new SqlCollectionResult();
            try
            {
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, conn))
                    {
                        using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                        {
                            for (int x = 0; x < reader.FieldCount; x++)
                            {
                                columnName.Add(reader.GetName(x));
                            }
                            listType = typeof(List<>).MakeGenericType(new Type[] { type });
                            dataObjects = Activator.CreateInstance(listType);
                            while (reader.Read())
                            {
                                element = Activator.CreateInstance(type);
                                for (int x = 0; x < columnName.Count; x++)
                                {
                                    propertyInfo = element.GetType().GetProperty(columnName[x]);
                                    if (propertyInfo != null)
                                    {
                                        dataType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                        value = (reader[columnName[x]] == null ? null : (reader[columnName[x]] == DBNull.Value ? null : Convert.ChangeType(reader[columnName[x]], dataType)));
                                        propertyInfo.SetValue(element, value, null);
                                        propertyInfo = null;
                                    }
                                }
                                dataObjects.Add(element);
                                element = null;
                            }
                            reader.Close();
                        }
                    }
                    conn.Close();
                }
                result.Collection = dataObjects;
                return result;
            }
            catch (Exception ex)
            {
                result.Collection = (IEnumerable)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { type }));
                result.HasError = true;
                result.Message = "No se ha realizado la operación: " + ex.Message + ".";
                return result;
            }
            finally
            {
                type = null;
                query = null;
                value = null;
                result = null;
                element = null;
                dataType = null;
                listType = null;
                columnName = null;
                columnName = null;
                dataObjects = null;
                propertyInfo = null;
            }
        }

        /// <summary>
        /// Método que permite obtener los datos resultantes de un procedimiento almacenado en SQL.
        /// </summary>
        /// <param name="spName">Nombre del procedimiento almacenado que se desea invocar.</param>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en el procedimiento almacenado.</param>
        /// <param name="type">Tipo de dato para mapear los resultados a un listado de objetos.</param>
        /// <returns>Retorna una instancia de la clase SqlCollectionResult.</returns>
        public SqlCollectionResult GetDataFromSP(string spName, List<SqlParam> parameters, Type type)
        {
            SqlDbType dbType;
            Type dataType = null;
            dynamic value = null;
            Type listType = null;
            dynamic element = null;
            SqlParameter param = null;
            dynamic dataObjects = null;
            ParameterDirection direction;
            PropertyInfo propertyInfo = null;
            List<string> columnName = new List<string>();
            SqlCollectionResult result = new SqlCollectionResult();
            try
            {
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(spName, conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            for (int x = 0; x < parameters.Count; x++)
                            {
                                if (!string.IsNullOrEmpty(parameters[x].Name) && !string.IsNullOrEmpty(parameters[x].Type) && parameters[x].Value != null)
                                {
                                    if (Enum.TryParse(parameters[x].Type, out dbType))
                                    {
                                        param = command.Parameters.Add(parameters[x].Name, dbType);
                                        param.Value = parameters[x].Value;
                                        if (!string.IsNullOrEmpty(parameters[x].Direction))
                                        {
                                            if (Enum.TryParse(parameters[x].Direction, out direction))
                                            {
                                                param.Direction = direction;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(parameters[x].TypeName))
                                        {
                                            param.TypeName = parameters[x].TypeName;
                                        }
                                    }
                                }
                            }
                        }
                        using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                        {
                            for (int x = 0; x < reader.FieldCount; x++)
                            {
                                columnName.Add(reader.GetName(x));
                            }
                            listType = typeof(List<>).MakeGenericType(new Type[] { type });
                            dataObjects = Activator.CreateInstance(listType);
                            while (reader.Read())
                            {
                                element = Activator.CreateInstance(type);
                                for (int x = 0; x < columnName.Count; x++)
                                {
                                    propertyInfo = element.GetType().GetProperty(columnName[x]);
                                    if (propertyInfo != null)
                                    {
                                        dataType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                        value = (reader[columnName[x]] == null ? null : (reader[columnName[x]] == DBNull.Value ? null : Convert.ChangeType(reader[columnName[x]], dataType)));
                                        propertyInfo.SetValue(element, value, null);
                                        propertyInfo = null;
                                    }
                                }
                                dataObjects.Add(element);
                                element = null;
                            }
                            reader.Close();
                        }
                    }
                    conn.Close();
                }
                result.Collection = dataObjects;
                return result;
            }
            catch (Exception ex)
            {
                result.Collection = (IEnumerable)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { type }));
                result.HasError = true;
                result.Message = "No se ha realizado la operación: " + ex.Message + ".";
                return result;
            }
            finally
            {
                type = null;
                value = null;
                param = null;
                result = null;
                spName = null;
                element = null;
                dataType = null;
                listType = null;
                parameters = null;
                columnName = null;
                columnName = null;
                dataObjects = null;
                propertyInfo = null;
            }
        }

        /// <summary>
        /// Método que permite aplicar cambios mediante un procedimiento almacenado en SQL.
        /// </summary>
        /// <param name="query">Cadena que contiene la consulta SQL.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public SqlChangesResult ApplyChanges(string query)
        {
            SqlChangesResult result = new SqlChangesResult();
            try
            {
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, conn))
                    {
                        result.RowsAffected = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                if (result.RowsAffected.HasValue)
                {
                    if (result.RowsAffected.Value <= 0)
                    {
                        result.HasError = true;
                        result.Message = "Se realizó la operación pero sin afectar registro alguno.";
                    }
                    return result;
                }
                else
                {
                    result.RowsAffected = -1;
                    result.HasError = true;
                    result.Message = "Se realizó la operación pero sin afectar registro alguno.";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.RowsAffected = -2;
                result.HasError = true;
                result.Message = "No se realizó la operación: " + ex.Message + ".";
                return result;
            }
            finally
            {
                query = null;
                result = null;
            }
        }

        /// <summary>
        /// Método que permite aplicar cambios mediante un procedimiento almacenado en SQL.
        /// </summary>
        /// <param name="spName">Nombre del procedimiento almacenado que se desea invocar.</param>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en el procedimiento almacenado.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public SqlChangesResult ApplyChangesFromSP(string spName, List<SqlParam> parameters)
        {
            SqlDbType dbType;
            SqlParameter param = null;
            ParameterDirection direction;
            SqlChangesResult result = new SqlChangesResult();
            try
            {
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(spName, conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            for (int x = 0; x < parameters.Count; x++)
                            {
                                if (!string.IsNullOrEmpty(parameters[x].Name) && !string.IsNullOrEmpty(parameters[x].Type) && parameters[x].Value != null)
                                {
                                    if (Enum.TryParse(parameters[x].Type, out dbType))
                                    {
                                        param = command.Parameters.Add(parameters[x].Name, dbType);
                                        param.Value = parameters[x].Value;
                                        if (!string.IsNullOrEmpty(parameters[x].Direction))
                                        {
                                            if (Enum.TryParse(parameters[x].Direction, out direction))
                                            {
                                                param.Direction = direction;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(parameters[x].TypeName))
                                        {
                                            param.TypeName = parameters[x].TypeName;
                                        }
                                    }
                                }
                            }
                        }
                        result.RowsAffected = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                if (result.RowsAffected.HasValue)
                {
                    if (result.RowsAffected.Value <= 0)
                    {
                        result.HasError = true;
                        result.Message = "Se realizó la operación pero sin afectar registro alguno.";
                    }
                    return result;
                }
                else
                {
                    result.RowsAffected = -1;
                    result.HasError = true;
                    result.Message = "Se realizó la operación pero sin afectar registro alguno.";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.RowsAffected = -2;
                result.HasError = true;
                result.Message = "No se realizó la operación: " + ex.Message + ".";
                return result;
            }
            finally
            {
                param = null;
                result = null;
                spName = null;
                parameters = null;
            }
        }

        /// <summary>
        /// Método que permite aplicar cambios mediante un procedimiento almacenado en SQL.
        /// </summary>
        /// <param name="spName">Nombre del procedimiento almacenado que se desea invocar.</param>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en el procedimiento almacenado.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public SqlChangesResult ApplyChangesWithValueFromSP(string spName, List<SqlParam> parameters)
        {
            SqlDbType dbType;
            SqlParameter param = null;
            ParameterDirection direction;
            SqlChangesResult result = new SqlChangesResult();
            try
            {
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(spName, conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            for (int x = 0; x < parameters.Count; x++)
                            {
                                if (!string.IsNullOrEmpty(parameters[x].Name) && !string.IsNullOrEmpty(parameters[x].Type) && parameters[x].Value != null)
                                {
                                    if (Enum.TryParse(parameters[x].Type, out dbType))
                                    {
                                        param = command.Parameters.Add(parameters[x].Name, dbType);
                                        param.Value = parameters[x].Value;
                                        if (!string.IsNullOrEmpty(parameters[x].Direction))
                                        {
                                            if (Enum.TryParse(parameters[x].Direction, out direction))
                                            {
                                                param.Direction = direction;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(parameters[x].TypeName))
                                        {
                                            param.TypeName = parameters[x].TypeName;
                                        }
                                    }
                                }
                            }
                        }
                        result.RowsAffected = (int)command.ExecuteScalar();
                    }
                    conn.Close();
                }
                if (result.RowsAffected.HasValue)
                {
                    if (result.RowsAffected.Value <= 0)
                    {
                        result.HasError = true;
                        result.Message = "Se realizó la operación pero sin afectar registro alguno.";
                    }
                    return result;
                }
                else
                {
                    result.RowsAffected = -1;
                    result.HasError = true;
                    result.Message = "Se realizó la operación pero sin afectar registro alguno.";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.RowsAffected = -2;
                result.HasError = true;
                result.Message = "No se realizó la operación: " + ex.Message + ".";
                return result;
            }
            finally
            {
                param = null;
                result = null;
                spName = null;
                parameters = null;
            }
        }
    }
}
