using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.Sqlite;

internal class Program
{
    private static void Main(string[] args)
    {
        using var connection = new SqliteConnection("Data Source=products.db");
        connection.Open();

        string createTableSql = "CREATE TABLE IF NOT EXISTS Products (Id INTEGER, Name TEXT, Price INTEGER);";
        using var createCommand = new SqliteCommand(createTableSql, connection);
        createCommand.ExecuteNonQuery(); // This execute command in database

        string insertSql = "INSERT INTO Products (Id, Name, Price) VALUES (@id, @name, @price);";
        using var insertCommand = new SqliteCommand(insertSql, connection);

        string line;
        try
        {
            // Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("products.csv");
            StreamWriter sw = new StreamWriter("prices.csv");
            
            /* If I filter all products to new prices.csv file, new file also needs
               header rows, for example Id,Name,Price so that it is approved CSV file.
               solution to this problem is to write first read row straight into file
               before while loop
             */
            line = sr.ReadLine();
            if (line != null)
            {
                sw.WriteLine(line);
                line = sr.ReadLine();
            }


            while (line != null)
            {
                {
                    string[] strPrices = line.Split(",");
                    int[] prices = new int[10];

                    if (strPrices.Length > 2 && int.TryParse(strPrices[2], out int price))
                    {
                        if (price < 50)
                        {
                            // Empty last iteration's parameters and set new ones
                            insertCommand.Parameters.Clear();
                            insertCommand.Parameters.AddWithValue("@id", strPrices[0]);
                            insertCommand.Parameters.AddWithValue("@name", strPrices[1]);
                            insertCommand.Parameters.AddWithValue("@price", price);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        Console.WriteLine("CSV row contained a non number character. Continuing parsing.");
                    }
                }
                line = sr.ReadLine();
            }
            sw.Close();
            sr.Close();
            Console.WriteLine("Parsing has finished!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception " + e.Message);
        }
    }
}