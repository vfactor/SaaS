using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Saas.Entities.DAL
{
  internal sealed class DAL<T> : StoreProcedure<T> where T : new()
  {
    private readonly int _rootId;
    public DAL(int rootId, string connectionStr) : base(connectionStr)
    {
      _rootId = rootId;
    }        
    public int Create(T obj)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(CreateProcedure, con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { ParameterForInsert(obj), new SqlParameter(Constant.KEYROOT, _rootId) }
      };

      con.Open();
      cmd.ExecuteNonQuery();

      return (int)cmd.Parameters[Constant.KEYID].Value;
    }
    public int Update(T obj)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(UpdateProcedure, con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { ParameterForUpdate(obj), new SqlParameter(Constant.KEYROOT, _rootId) }
      };

      con.Open();
      cmd.ExecuteNonQuery();

      return (int)cmd.Parameters[Constant.RETVAL].Value;
    }    
    public int Update(int id, int stateId)
    {      
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(UpdateProcedure, con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { ParameterForUpdate(id, stateId), new SqlParameter(Constant.KEYROOT, _rootId) }
      };

      if (cmd.Parameters[0].ParameterName == Constant.ERR || cmd.Parameters.Count < 3)
        return -1;

      con.Open();
      cmd.ExecuteNonQuery();

      return (int)cmd.Parameters[Constant.RETVAL].Value;
    }
    public T Read(int id)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(ReadProcedure, con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { new SqlParameter(Constant.KEYID, id), new SqlParameter(Constant.KEYROOT, _rootId) }
      };

      con.Open();
      using var reader = cmd.ExecuteReader();

      return (reader.Read()) ? ReadRecord(reader) : default;
    }
    public IEnumerable<T> Read()
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(ReadProcedure, con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { new SqlParameter(Constant.KEYROOT, _rootId) }
      };
     
      con.Open();
      using var reader = cmd.ExecuteReader();

      return ReadRecords(reader);
    }
    public IEnumerable<T> Read(string value)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(LookupProcedure, con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { new SqlParameter(Constant.KEYVALUE, value), new SqlParameter(Constant.KEYROOT, _rootId) }
      };

      con.Open();
      using var reader = cmd.ExecuteReader();

      return ReadRecords(reader);
    }    
    public IEnumerable<T> Read(IDictionary<string, int> ids)
    {
      using var con = new SqlConnection(ConnectionString);
      using var cmd = new SqlCommand(ReadProcedure, con)
      {
        CommandType = CommandType.StoredProcedure,
        Parameters = { ids.Select(id => new SqlParameter(id.Key, id.Value)).Append(new SqlParameter(Constant.KEYROOT, _rootId)) }
      };

      con.Open();
      using var reader = cmd.ExecuteReader();

      return ReadRecords(reader);
    }
  }
}