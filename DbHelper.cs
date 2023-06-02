using Microsoft.Data.SqlClient;
using System.Data;

namespace MyFirstProject
{
    public class DbHelper
    {
        const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Admin;Integrated Security=True";
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand;
        
        public int ExecuteNonQuery(string sql, List<SqlParameter> list)
        {
            sqlConnection.ConnectionString = ConnectionString;
            sqlConnection.Open();
            sqlCommand = new SqlCommand(sql, sqlConnection);

            foreach (var item in list)
                sqlCommand.Parameters.AddWithValue(item.ParameterName, item.Value);

            int result = sqlCommand.ExecuteNonQuery();

            sqlCommand.Dispose();
            sqlConnection.Close();

            return result;
        }

        public int ExecuteScalar(string sql, List<SqlParameter> list)
        {
            sqlConnection.ConnectionString = ConnectionString;
            sqlConnection.Open();
            sqlCommand = new SqlCommand(sql, sqlConnection);

            foreach (var item in list)
                sqlCommand.Parameters.AddWithValue(item.ParameterName, item.Value);

            int result = Convert.ToInt32(sqlCommand.ExecuteScalar());

            sqlCommand.Dispose();
            sqlConnection.Close();

            return result;
        }

        public DataTable ExecuteReader(string sql, List<SqlParameter> list)
        {
            DataTable dataTable = new DataTable();
            sqlConnection.ConnectionString = ConnectionString;
            sqlConnection.Open();
            sqlCommand = new SqlCommand(sql, sqlConnection);

            foreach (var item in list)
                sqlCommand.Parameters.AddWithValue(item.ParameterName, item.Value);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            sqlCommand.Dispose();
            sqlDataReader.Close();
            sqlConnection.Close();

            return dataTable;
        }
    }
}
