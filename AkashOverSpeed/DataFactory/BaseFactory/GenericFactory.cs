using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AkashOverSpeed.DataFactory.BaseFactory
{
    //public class GenericFactory<T> where T : class, new() //No Interface
    public class GenericFactory<T> : IGenericFactory<T> where T : class, new()
    {
        public string ExecuteCommandScaler(string cmdText, string cmdType, Hashtable ht, string connString)
        {
            string result = "";
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType == CommandType.StoredProcedure.ToString() ? CommandType.StoredProcedure
                        : cmdType == CommandType.TableDirect.ToString() ? CommandType.TableDirect : CommandType.Text;
                    cmd.Connection = con;
                    if (ht != null)
                    {
                        foreach (object obj in ht.Keys)
                        {
                            string str = Convert.ToString(obj);
                            SqlParameter parameter = new SqlParameter("@" + str, ht[obj]);
                            cmd.Parameters.Add(parameter);
                        }
                    }
                    IDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        result = dr.GetString(0);
                    }
                    if (ht != null)
                    {
                        cmd.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        
        public List<T> ExecuteCommandList(string cmdText, string cmdType, Hashtable ht, string connString)
        {
            List<T> Results = null;
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType == CommandType.StoredProcedure.ToString() ? CommandType.StoredProcedure
                        : cmdType == CommandType.TableDirect.ToString() ? CommandType.TableDirect : CommandType.Text;
                    cmd.Connection = con;
                    if (ht != null)
                    {
                        foreach (object obj in ht.Keys)
                        {
                            string str = Convert.ToString(obj);
                            SqlParameter parameter = new SqlParameter("@" + str, ht[obj]);
                            cmd.Parameters.Add(parameter);
                        }
                    }
                    Results = DataReaderToList<T>(cmd.ExecuteReader());
                    if (ht != null)
                    {
                        cmd.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Results;
        }
        
        public List<T> DataReaderToList<Tentity>(IDataReader reader)
        {
            var results = new List<T>();
            var columnCount = reader.FieldCount;
            while (reader.Read())
            {
                var item = Activator.CreateInstance<T>();
                try
                {
                    var rdrProperties = Enumerable.Range(0, columnCount).Select(i => reader.GetName(i)).ToArray();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if ((typeof(T).GetProperty(property.Name).GetGetMethod().IsVirtual) || (!rdrProperties.Contains(property.Name)))
                        {
                            continue;
                        }
                        else
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                    }
                    results.Add(item);
                }
                catch (Exception ex)
                {
                }
            }
            return results;
        }
    }
}
