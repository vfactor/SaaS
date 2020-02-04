using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Saas.Entities.DAL.Interface;

namespace Saas.Entities.DAL
{
  internal class Table<T> : ITable<T> where T : new()
  {
    private static Table<T> instance = null;

    internal static Table<T> GetInstance(string connectionStr)
    {
      if (instance == null)
        instance = new Table<T>();

      instance.ConnectionString = connectionStr;

      return instance;
    }

    protected Table()
    {
    }

    protected string StoreProcName => typeof(T).Name;
    protected string ConnectionString;

    protected SqlParameter[] CreateParameters(T obj)
    {
      var propInfos = typeof(T).GetProperties();
      var ret = new SqlParameter[propInfos.Length - 2];

      for (int i = 2; i < propInfos.Length; i++)
      {
        ret[i - 2] = (propInfos[i].Name == "Id") ? new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output }
                                                 : new SqlParameter("@" + propInfos[i].Name, propInfos[i].GetValue(obj));
      }

      return ret;
    }

    protected SqlParameter[] UpdateParameters(T obj)
    {
      var propInfos = typeof(T).GetProperties();
      var ret = new SqlParameter[propInfos.Length - 1];

      for (int i = 2; i < propInfos.Length; i++)
      {
        ret[i - 2] = new SqlParameter("@" + propInfos[i].Name, propInfos[i].GetValue(obj));
      }
      ret[propInfos.Length - 2] = new SqlParameter("retVal", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };

      return ret;
    }

    private T ReadSingle(SqlDataReader dtReader)
    {
      var propInfos = typeof(T).GetProperties();
      var ret = new T();

      for (int i = 2; i < propInfos.Length; i++)
      {
        propInfos[i].SetValue(ret, Convert.ChangeType(dtReader[propInfos[i].Name], propInfos[i].PropertyType));
      }

      return ret;
    }

    protected IEnumerable<T> ReadMany(SqlDataReader dtReader)
    {
      var results = new List<T>();
      while (dtReader.Read())
      {
        results.Add(ReadSingle(dtReader));
      }

      return results;
    }

    public int Create(T obj)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(StoreProcName + "_CU", con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { CreateParameters(obj) }
      };

      con.Open();
      cmd.ExecuteNonQuery();

      return (int)cmd.Parameters["@Id"].Value;
    }

    public int Update(T obj)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(StoreProcName + "_CU", con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { UpdateParameters(obj) }
      };

      con.Open();
      cmd.ExecuteNonQuery();

      return (int)cmd.Parameters["retVal"].Value;
    }

    public int Delete(int id)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(StoreProcName + "_D", con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { new SqlParameter("@Id", id) }
      };

      con.Open();
      cmd.ExecuteNonQuery();

      return (int)cmd.Parameters["retVal"].Value;
    }

    public T Read(int id)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(StoreProcName + "_R", con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { new SqlParameter("@Id", id) }
      };

      con.Open();
      using var reader = cmd.ExecuteReader();

      return (reader.Read()) ? ReadSingle(reader) : default;
    }
  }

  internal sealed class LookupTable<T> : Table<T>, ILookup<T> where T : new()
  {
    private static LookupTable<T> instance = null;

    internal new static LookupTable<T> GetInstance(string connectionStr)
    {
      if (instance == null)
        instance = new LookupTable<T>();

      instance.ConnectionString = connectionStr;

      return instance;
    }

    private LookupTable()
    {
    }

    public IEnumerable<T> Lookup(string value)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(StoreProcName + "_L", con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { new SqlParameter("@value", value) }
      };

      con.Open();
      using var reader = cmd.ExecuteReader();

      return ReadMany(reader);
    }
  }

  internal sealed class DetailTable<T> : Table<T>, IDetail<T> where T : new()
  {
    private static DetailTable<T> instance = null;

    internal new static DetailTable<T> GetInstance(string connectionStr)
    {
      if (instance == null)
        instance = new DetailTable<T>();

      instance.ConnectionString = connectionStr;

      return instance;
    }

    private DetailTable()
    {
    }

    public IEnumerable<T> ReadByMasterId(string masterKey, int masterId)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(StoreProcName + "_R", con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { new SqlParameter("@" + masterKey, masterId) }
      };

      con.Open();
      using var reader = cmd.ExecuteReader();

      return ReadMany(reader);
    }
  }

  internal sealed class ReferenceTable<T> : Table<T>, IReference<T> where T : new()
  {
    private static ReferenceTable<T> instance = null;

    internal new static ReferenceTable<T> GetInstance(string connectionStr)
    {
      if (instance == null)
        instance = new ReferenceTable<T>();

      instance.ConnectionString = connectionStr;

      return instance;
    }

    private ReferenceTable()
    {
    }

    public IEnumerable<T> Read()
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(StoreProcName + "_R", con)
      {
        CommandType = CommandType.StoredProcedure,
      };

      con.Open();
      using var reader = cmd.ExecuteReader();

      return ReadMany(reader);
    }
  }

  internal sealed class JoinTable<T> : Table<T>, IJoin<T> where T : new()
  {
    private static JoinTable<T> instance = null;

    internal new static JoinTable<T> GetInstance(string connectionStr)
    {
      if (instance == null)
        instance = new JoinTable<T>();

      instance.ConnectionString = connectionStr;

      return instance;
    }

    private JoinTable()
    {
    }

    public IEnumerable<T> ReadByMasterId(IDictionary<string, int> masterInfos)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(StoreProcName + "_R", con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { masterInfos.Select(k => new SqlParameter("@" + k.Key, k.Value)) }
      };

      con.Open();
      using var reader = cmd.ExecuteReader();

      return ReadMany(reader);
    }
  }
}