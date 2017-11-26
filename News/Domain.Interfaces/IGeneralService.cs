using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGeneralService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T FindSync(object id);
        Task<T> Find(object id);
        void Add(T entity);
        void RemoveById(object id);
        void Remove(T entityToDelete);
        void UpdateEntity(T entityToUpdate);
        void SaveChanges();
    }
}
