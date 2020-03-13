using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Microsoft.Data.SqlClient;

namespace Saas.Entities.DAL
{
  internal abstract class StoreProcedure<T> where T : new()
  {
    private string StoreProcName => typeof(T).Name;
    protected string ReadProcedure => StoreProcName + Constant.READSUFFIX;
    protected string CreateProcedure => StoreProcName + Constant.CREATESUFFIX;
    protected string UpdateProcedure => StoreProcName + Constant.UPDATESUFFIX;
    protected string LookupProcedure => StoreProcName + Constant.LOOKUPSUFFIX;

    protected readonly string ConnectionString;
    protected StoreProcedure(string connectionStr)
    {
      ConnectionString = connectionStr;
    }
    protected SqlParameter[] ParameterForInsert(T obj)
    {
      var propInfos = typeof(T).GetProperties();
      var ret = new SqlParameter[propInfos.Length - 2];

      for (int i = 2; i < propInfos.Length; i++)
      {
        ret[i - 2] = (propInfos[i].Name == Constant.ID) ? new SqlParameter(Constant.KEYID, SqlDbType.Int) { Direction = ParameterDirection.Output }
                                                 : new SqlParameter(Constant.PARAMPREFIX + propInfos[i].Name, propInfos[i].GetValue(obj));
      }

      return ret;
    }
    protected SqlParameter[] ParameterForUpdate(T obj)
    {
      var propInfos = typeof(T).GetProperties();
      var ret = new SqlParameter[propInfos.Length - 1];

      for (int i = 2; i < propInfos.Length; i++)
      {
        ret[i - 2] = new SqlParameter(Constant.PARAMPREFIX + propInfos[i].Name, propInfos[i].GetValue(obj));
      }
      ret[propInfos.Length - 1] = new SqlParameter(Constant.RETVAL, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };

      return ret;
    }
    protected SqlParameter[] ParameterForUpdate(int id, int stateId)
    {     
      var sqlParams = ParameterForUpdate(new T());
      var keyId = sqlParams.FirstOrDefault(p => p.ParameterName == Constant.KEYID);
      var keyStateId = sqlParams.FirstOrDefault(p => p.ParameterName == Constant.KEYSTATE);

      if (keyId == null || keyStateId == null || id <= 0 || stateId <= 0)
        return new SqlParameter[] { new SqlParameter(Constant.ERR, DBNull.Value) };

      keyId.Value = id;
      keyStateId.Value = stateId;

      return sqlParams;
    }
    protected T ReadRecord(SqlDataReader dtReader)
    {
      var propInfos = typeof(T).GetProperties();
      var ret = new T();

      for (int i = 0; i < dtReader.FieldCount; i++)
      {
        var prop = propInfos.FirstOrDefault(p => p.Name == dtReader.GetName(i));
        if (prop != null)
          prop.SetValue(ret, Convert.ChangeType(dtReader[i], prop.PropertyType));

      }

      return ret;
    }
    protected IEnumerable<T> ReadRecords(SqlDataReader dtReader)
    {
      var ret = new List<T>();
      while (dtReader.Read())
      {
        ret.Add(ReadRecord(dtReader));
      }

      return ret;
    }
  }
}
