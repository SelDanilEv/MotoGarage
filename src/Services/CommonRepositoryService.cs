using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Interfaces;
using Infrastructure.Models;
using Infrastructure.Result.Interfaces;
using Repository.Interfaces;
using Repository.Mongo;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using System;

namespace Services
{
    public class CommonRepositoryService<T> : ICommonRepositoryService<T> where T : IBaseModel
    {
        private IRepository<T> _repository;

        public CommonRepositoryService(IOptions<MongoDbOption> mongoOption)
        {
            _repository = new CommonMongoRepository<T>(mongoOption.Value);
        }

        #region CRUD
        public virtual async Task<IResultWithData<IList<T>>> GetItems()
        {
            var itemsResult = await _repository.GetItemsAsync();

            return itemsResult;
        }

        public virtual async Task<IResultWithData<T>> GetItemById(Guid id)
        {
            var itemResult = await _repository.GetItemAsync(id);

            return itemResult;
        }

        public virtual async Task<IResult> AddItem(T newItem)
        {
            var result = await _repository.AddItemAsync(newItem);
            return result;
        }

        public virtual async Task<IResult> UpdateItem(T newItem)
        {
            var result = await _repository.UpdateItemAsync(newItem);
            return result;
        }

        public virtual async Task<IResult> RemoveItem(Guid id)
        {
            var result = await _repository.RemoveItemAsync(id);
            return result;
        }
        #endregion
    }
}
