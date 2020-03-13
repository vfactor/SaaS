using System.Collections.Generic;

namespace Saas.Entities.DAL.Interface
{
  internal interface IDAL<T>
  {
    int Create(T obj);
    int Update(T obj);
    int UpdateState(int id, int stateId);
    T Read(int id);
    IEnumerable<T> Read();
    IEnumerable<T> Read(string value);
    IEnumerable<T> Read(IDictionary<string, int> ids);
  }
}