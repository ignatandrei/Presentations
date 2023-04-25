using System;
using System.Data;
using System.Data.Common;

namespace Decorator2Sealed
{
    class DbConnectionLogging :DbConnection
    {
        DbConnection inner;
        public DbConnectionLogging(DbConnection db)
        {
            inner = db;
        }

       

        public override ConnectionState State
        {

            get
            {
                Console.WriteLine("asking state");
                return inner.State;
            }
           
        }
        public override string ConnectionString
        {
            get
            {
                Console.WriteLine("asking connection string");
                return inner.ConnectionString;
            }

            set
            {
                Console.WriteLine("set connection string");

                inner.ConnectionString = value;
            }
        }

        public override string Database => inner.Database;

        public override string DataSource => inner.DataSource;

        public override string ServerVersion => inner.ServerVersion;

      

        public override void ChangeDatabase(string databaseName)
        {
            inner.ChangeDatabase(databaseName);
        }

        public override void Close()
        {
            Console.WriteLine("close");
            inner.Close();
        }

        public override void Open()
        {
            Console.WriteLine("open");
            inner.Open();
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return inner.BeginTransaction();
        }

        protected override DbCommand CreateDbCommand()
        {
            return inner.CreateCommand();
        }
    }
}
