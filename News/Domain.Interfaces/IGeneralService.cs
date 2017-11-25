using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGeneralService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Find(object id);
        void Add(T entity);
        void RemoveById(object id);
        void Remove(T entityToDelete);
        void UpdateEntity(T entityToUpdate);
        void SaveChanges();
    }
}
