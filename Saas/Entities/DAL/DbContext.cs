using Saas.Entities.DAL;
using System.Collections.Generic;

namespace Saas.Entities.DAL
{  
  public class DbContext<T> where T : new()
  {
    private readonly DAL<T> _dal;
    public DbContext(int rootId, string connectionStr)
    {
      _dal = new DAL<T>(rootId, connectionStr);
    }
    public T Read(int id)
    {
      return _dal.Read(id);
    }
    public IEnumerable<T> Read()
    {
      return _dal.Read();
    }
    public IEnumerable<T> Read(string value)
    {
      return _dal.Read(value);
    }
    public IEnumerable<T> Read(IDictionary<string, int> ids)
    {
      return _dal.Read(ids);
    }
    public int Create(T obj)
    {
      return _dal.Create(obj);
    }
    public int Update(T obj)
    {
      return _dal.Update(obj);
    }
    public int UpdateState(int id, int stateId)
    {
      return _dal.UpdateState(id, stateId);
    }
  }
}
