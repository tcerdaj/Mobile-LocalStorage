using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LocalDatabase.Mobile.Helpers.SQLite.Models;

namespace LocalDatabase.Mobile.Services
{
    public interface ILocalDataStore<T>
    {
        Task CreateTableAsync();
        Task ClearTableAsync();
        Task<T> Modify(T model);
        Task Delete(Guid id);
        Task Delete(T model);
        Task<T> Load(Guid id);
        Task<List<T>> List();
        Task<List<T>> List(SQLControllerListCriteriaModel criteria);
        Task InsertAll(List<T> list);
    }
}