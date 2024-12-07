using System.Data;
using MySql.Data.MySqlClient;

namespace Data_reader_data_set_data_adapter_data_view_objects_in_ADO_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string connString = "Server=127.0.0.1;Database=test;Uid=root;Pwd=;";
                const string tableName = "test";
                var query = $"SELECT * FROM {tableName}";
                var filter = "name = 'john'";

                MySqlConnection connection = new MySqlConnection(connString);
                connection.Open();
                Console.WriteLine("Connection established successfully!");

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("DataReader Output:");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]}, {reader[1]}");
                }

                reader.Close(); // Close the reader before using the adapter

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, tableName);
                Console.WriteLine("\nDataSet Output:");
                var table = dataSet.Tables[tableName];

                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Console.WriteLine($"{row[0]}, {row[1]}");
                    }

                    DataView view = new DataView(table)
                    {
                        RowFilter = filter
                    };

                    Console.WriteLine("\nDataView Output:");
                    foreach (DataRowView rowView in view)
                    {
                        Console.WriteLine($"{rowView[0]}, {rowView[1]}");
                    }
                }
                else
                {
                    Console.WriteLine("Table does not exist.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
    }
}