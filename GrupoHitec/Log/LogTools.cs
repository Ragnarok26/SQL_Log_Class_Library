using System;
using System.Data;
using System.Data.SqlClient;

namespace GrupoHitec.Log
{
    /// <summary>
    /// Clase que permite el registro de eventos en una base de datos.
    /// </summary>
    public class LogTools
    {
        /// <summary>
        /// Cadena de Conexión a Base de Datos.
        /// </summary>
        public static string ConnectionString = string.Empty;

        /// <summary>
        /// Registra en el log lo que ocurre en la aplicación.
        /// </summary>
        /// <param name="parentId">Identificador numérico de la aplicación (Si se desconoce, el valor debe de ser 0).</param>
        /// <param name="user">Usuario que está desencadenando el evento.</param>
        /// <param name="appName">Nombre de la aplicación.</param>
        /// <param name="service">Nombre de la clase donde se está desencadenando el evento.</param>
        /// <param name="serviceMethod">Nombre del método donde se está desencadenando el evento.</param>
        /// <param name="info">Texto descriptivo que indica lo que ocurre en el evento.</param>
        /// <param name="date">Fecha y hora en la que fue desencadenado el evento.</param>
        /// <param name="code">Codigo numérico de la operación (Si no se especifica, el valor por defecto es 0).</param>
        /// <returns>Identificador numérico de la aplicación.</returns>
        public static long RegisterLog(long parentId, string user, string appName, string service, string serviceMethod, string info, DateTime date, int code = 0)
        {
            long logId = 0;
            int maxLen = 2048;
            if (info.Length > maxLen)
            {
                logId = InsertLog(parentId, user, appName, service, serviceMethod, info.Substring(0, maxLen), DateTime.Now, code);
                int enviados = maxLen;

                while (enviados < info.Length)
                {
                    if ((info.Length - enviados) > maxLen)
                    {
                        InsertLog(parentId, user, appName, service, serviceMethod, info.Substring(enviados, maxLen), DateTime.Now, code);
                    }
                    else
                    {
                        InsertLog(parentId, user, appName, service, serviceMethod, info.Substring(enviados), DateTime.Now, code);
                    }
                    enviados += maxLen;
                }
            }
            else
            {
                logId = InsertLog(parentId, user, appName, service, serviceMethod, info, DateTime.Now, code);
            }
            return logId;
        }

        /// <summary>
        /// Registra en el log lo que ocurre en la aplicación.
        /// </summary>
        /// <param name="parentId">Identificador numérico de la aplicación (Si se desconoce, el valor debe de ser 0).</param>
        /// <param name="user">Usuario que está desencadenando el evento.</param>
        /// <param name="appName">Nombre de la aplicación.</param>
        /// <param name="service">Nombre de la clase donde se está desencadenando el evento.</param>
        /// <param name="serviceMethod">Nombre del método donde se está desencadenando el evento.</param>
        /// <param name="info">Texto descriptivo que indica lo que ocurre en el evento.</param>
        /// <param name="date">Fecha y hora en la que fue desencadenado el evento.</param>
        /// <param name="code">Codigo numérico de la operación.</param>
        /// <returns>Identificador numérico de la aplicación.</returns>
        private static long InsertLog(long parentId, string user, string appName, string service, string serviceMethod, string info, DateTime date, int code)
        {
            string insertCmd = @"INSERT INTO [HITEC].[dbo].[Log] ([parentId],[user],[app],[service],[serviceMethod],[info],[code],[date])
                                VALUES (@parentId,@user,@app,@service,@serviceMethod,@info,@code,@date); select @Identity=SCOPE_IDENTITY();";
            long id = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConnectionString))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(insertCmd, conexion))
                    {
                        command.Parameters.AddWithValue("@parentId", parentId);
                        command.Parameters.AddWithValue("@user", user);
                        command.Parameters.AddWithValue("@service", service);
                        command.Parameters.AddWithValue("@serviceMethod", serviceMethod);
                        command.Parameters.AddWithValue("@info", info);
                        command.Parameters.AddWithValue("@code", code);
                        command.Parameters.AddWithValue("@date", date);
                        command.Parameters.AddWithValue("@app", appName);

                        SqlParameter parameter = command.Parameters.Add("@Identity", SqlDbType.BigInt);
                        parameter.Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        id = (long)command.Parameters["@Identity"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }
    }
}
