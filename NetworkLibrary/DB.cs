using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    public enum AndOr
    {
        AND,
        OR
    }

    class DB
    {
        public static IEnumerable<T> SELECT<T>(Func<IDataRecord, T> objectBuilder, string table, List<(string, string, object)> conditions = null, AndOr andor = AndOr.AND, string config = "Data Source=data_usage.db;")
        {
            string conditionsString = "";
            Dictionary<string, Object> parameters = new Dictionary<string, object>();


            if (conditions != null)
            {
                int i = 0;


                foreach ((string column, string comparisonOp, Object value) in conditions)
                {
                    if (i == 0)
                    {
                        conditionsString += " WHERE ";
                    }

                    parameters.Add("$" + column, value);
                    conditionsString += column.ToString() + comparisonOp + "$" + column.ToString();

                    if (i < conditions.Count - 1)
                    {
                        conditionsString += " " + andor + " ";
                    }

                    if (i == conditions.Count - 1)
                    {
                        conditionsString += ";";
                    }

                    i++;
                }
            }

            using (SqliteConnection Connection = new SqliteConnection(config))
            {
                Connection.Open();
                SqliteCommand command = Connection.CreateCommand();
                command.CommandText = @"SELECT * FROM " + table + conditionsString;

                if (conditions != null)
                {
                    foreach (KeyValuePair<string, Object> keyValue in parameters)
                    {
                        command.Parameters.AddWithValue(keyValue.Key, keyValue.Value);
                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return objectBuilder(reader);
                    }
                }
            }
        }
    }
}
