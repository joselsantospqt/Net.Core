using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Service
{
    public abstract class BaseService: TrataErro
    {
        protected SqlCommand command { get; set; }

        public SqlCommand ConnectDataBase()
        {
            var dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=localhost;Initial Catalog=LojaVirtualDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            dbConnection.Open();
            var command = dbConnection.CreateCommand();

            return (command);
        }

        public SqlDataReader ObterPorID(int id)
        {
            command.Parameters.AddWithValue("@ID", id);
            return command.ExecuteReader();
        }

        public int ExecutarQuery()
        {
            return command.ExecuteNonQuery();
        }

        public void FecharQuery()
        {
           command.Connection.Close();
        }
    }
}
