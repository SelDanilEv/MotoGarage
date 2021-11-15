using Infrastructure.Models;
using Infrastructure.Result.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICommonRepositoryService<T> where T:IBaseModel
    {
        Task<IResultWithData<IList<T>>> GetItems();

        Task<IResultWithData<T>> GetItemById(Guid id);

        Task<IResult> AddItem(T newItem);

        Task<IResult> UpdateItem(T newItem);

        Task<IResult> RemoveItem(Guid id);
    }
}
