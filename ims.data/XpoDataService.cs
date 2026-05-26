using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using ims.domain;

namespace ims.data
{
    public static class XpoDataService
    {
        private static bool initialized;

        public static void Initialize()
        {
            if (initialized)
                return;

            string databaseFile = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ims-demo.db"
            );

            string connectionStringSqlServer =
                "XpoProvider=MSSqlServer;Server=(localdb)\\MSSQLLocalDB;Database=IMS;Trusted_Connection=True;TrustServerCertificate=True;";

            //string connectionString = SQLiteConnectionProvider.GetConnectionString(databaseFile);

            XpoDefault.DataLayer = XpoDefault.GetDataLayer(
                connectionStringSqlServer,
                AutoCreateOption.DatabaseAndSchema
            );

            XpoDefault.Session = null;

            using var unitOfWork = new UnitOfWork();

            // This code scans the assembly for persistent classes
            // and creates the necessary database schema if it doesn't exist.
            var domainAssembly = typeof(Rolodex).Assembly;

            unitOfWork.UpdateSchema(domainAssembly);

            // Creates XPO metadata records used internally by XPO.
            unitOfWork.CreateObjectTypeRecords(domainAssembly);

            initialized = true;
        }

        public static UnitOfWork CreateUnitOfWork()
        {
            Initialize();
            return new UnitOfWork();
        }
    }
}