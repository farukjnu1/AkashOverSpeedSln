using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AkashOverSpeed.DataFactory.BaseFactory
{
    public interface IGenericFactory<T> where T : class
    {
        string ExecuteCommandScaler(string commandText, string commandType, Hashtable ht, string connectionString);
        List<T> ExecuteCommandList(string commandText, string commandType, Hashtable ht, string connectionString);
        List<T> DataReaderToList<Tentity>(IDataReader reader);
    }
}
