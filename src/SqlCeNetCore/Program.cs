using System;
using System.Data.SqlServerCe;
using System.IO;

namespace SqlCeNetCore
{
    class Program
    {
        static void Main()
        {
            var dbFile = "netcore-sqlce.sdf";
            File.Delete(dbFile);
            
            var connectionString = $"Data Source={dbFile}";

            using SqlCeEngine engine = new SqlCeEngine(connectionString);
            engine.CreateDatabase();

            using SqlCeConnection connection = new SqlCeConnection(connectionString);

            connection.Open();

            using SqlCeCommand command = connection.CreateCommand();

            command.CommandText = "CREATE TABLE Blogs ( Id INT, Title NVARCHAR(256), Url NVARCHAR(4000) )";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Blogs VALUES (1, 'ErikEJ blog', 'https://erikej.github.io/' ) ";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Blogs VALUES (1, '.NET blog', 'https://devblogs.microsoft.com/dotnet/' ) ";
            command.ExecuteNonQuery();

            command.CommandText = "SELECT * FROM Blogs";
            using SqlCeDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Blog: {reader[1]} - Url: {reader[2]}");
            }

            Console.ReadKey();
        }
    }
}
