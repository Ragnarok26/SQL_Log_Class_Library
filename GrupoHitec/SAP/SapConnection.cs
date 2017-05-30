using System;

namespace GrupoHitec.SAP
{
    /// <summary>
    /// Clase que permite la conexión con SAP Business One.
    /// </summary>
    public sealed class SapConnection
    {
        private static object syncRoot = new Object();
        /// <summary>
        /// Instancia de la clase SapConnection.
        /// </summary>
        private static volatile SapConnection instance;
        /// <summary>
        /// Dirección IP o Nombre del servidor al cual se hará la conexión a SAP.
        /// </summary>
        public static string Server = string.Empty;
        /// <summary>
        /// Nombre de usuario con el cual se hará la conexión a SAP.
        /// </summary>
        public static string UserName = string.Empty;
        /// <summary>
        /// Contraseña del usuario con la cual se hará la conexión a SAP.
        /// </summary>
        public static string Password = string.Empty;
        /// <summary>
        /// Nombre de la compañía (base de datos), a la cual se hará la conexión a SAP.
        /// </summary>
        public static string CompanyDB = string.Empty;
        /// <summary>
        /// Nombre de usuario con el cual se hará la conexión a la base de datos de SAP.
        /// </summary>
        public static string DbUserName = string.Empty;
        /// <summary>
        /// Contraseña del usuario con la cual se hará la conexión a la base de datos de SAP.
        /// </summary>
        public static string DbPassword = string.Empty;
        /// <summary>
        /// Tipo de manejador de base de datos con el cual se hará la conexión a la base de datos de SAP.
        /// </summary>
        public static string DbServerType = string.Empty;
        /// <summary>
        /// Dirección IP o Nombre del servidor donde se tomarán las licencias de SAP.
        /// </summary>
        public static string LicenseServer = string.Empty;
        /// <summary>
        /// Fecha de la última conexión a SAP.
        /// </summary>
        public DateTime lastConnection { get; private set; }
        /// <summary>
        /// Instancia de la conexión realizada en SAP.
        /// </summary>
        public SAPbobsCOM.Company company { get; private set; }

        /// <summary>
        /// Constructor de la clase el cual realiza la conexión a SAP.
        /// </summary>
        private SapConnection()
        {
            Conectar();
        }

        /// <summary>
        /// Realiza la conexión a SAP.
        /// </summary>
        /// <returns>Retorna verdadero en caso de que la conexión haya sido exitosa, en caso contrario, retorna falso.</returns>
        public bool Conectar()
        {
            bool connected = false;
            company = new SAPbobsCOM.Company();
            SAPbobsCOM.BoDataServerTypes DbType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
            if (!Enum.TryParse(DbServerType, out DbType))
            {
                DbType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
            }
            company.Server = Server;
            company.UserName = UserName;
            company.Password = Password;
            company.CompanyDB = CompanyDB;
            company.DbServerType = DbType;
            company.DbUserName = DbUserName;
            company.DbPassword = DbPassword;
            company.LicenseServer = LicenseServer;
            if (company.Connect() != 0)
            {
                string mensaje = company.GetLastErrorDescription();
                connected = false;
            }
            else
            {
                connected = true;
                this.lastConnection = DateTime.Now;
            }
            return connected;
        }

        /// <summary>
        /// Obtiene la instancia de la clase SapConnection.
        /// </summary>
        public static SapConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SapConnection();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
