using Infrastructure.Result.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<Model>
    {
        Task<IResultWithData<IList<Model>>> GetItemsAsync();
        Task<IResultWithData<IList<Model>>> GetItemsWithFilterAsync(FilterDefinition<Model> filter);
        Task<IResultWithData<Model>> GetItemWithFilterAsync(FilterDefinition<Model> filter);
        Task<IResultWithData<Model>> GetItemAsync(Guid id);
        Task<IResult> AddItemAsync(Model newItem);
        Task<IResult> UpdateItemAsync(Model newItem);
        Task<IResult> RemoveItemAsync(Guid id);
    }
}
