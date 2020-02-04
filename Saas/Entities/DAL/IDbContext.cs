using System.Collections.Generic;

namespace Saas.Entities.DAL.Interface
{
  internal interface ITable<T>
  {
    int Create(T obj);

    int Update(T obj);

    int Delete(int id);

    T Read(int id);
  }

  internal interface ILookup<T> : ITable<T>
  {
    IEnumerable<T> Lookup(string value);
  }

  internal interface IDetail<T> : ITable<T>
  {
    IEnumerable<T> ReadByMasterId(string masterKey, int masterId);
  }

  internal interface IReference<T> : ITable<T>
  {
    IEnumerable<T> Read();
  }

  internal interface IJoin<T> : ITable<T>
  {
    IEnumerable<T> ReadByMasterId(IDictionary<string, int> masterInfos);
  }
}