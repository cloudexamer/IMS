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

            string connectionString =
                "XpoProvider=MSSqlServer;Server=(localdb)\\MSSQLLocalDB;Database=IMS;Trusted_Connection=True;TrustServerCertificate=True;";

            // TO USE SQLITE, UNCOMMENT THE FOLLOWING LINES AND COMMENT OUT THE SQL SERVER CONNECTION STRING ABOVE
            //string databaseFile = Path.Combine(
            //    AppDomain.CurrentDomain.BaseDirectory,
            //    "ims-demo.db"
            //);
            //string connectionString = SQLiteConnectionProvider.GetConnectionString(databaseFile);

            XpoDefault.DataLayer = XpoDefault.GetDataLayer(
                connectionString,
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