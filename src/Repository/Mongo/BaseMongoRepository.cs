using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Infrastructure.Result.Interfaces;
using Repository.Interfaces;
using Infrastructure.Models;
using System.Linq;
using MongoDB.Driver;
using Infrastructure.Options;

namespace Repository.Mongo
{
    public abstract class BaseMongoRepository<Model> : IRepository<Model> where Model : IBaseModel
    {
        private static MongoClient client;
        private static MongoUrlBuilder connection;
        protected static IMongoDatabase database;

        public BaseMongoRepository(MongoDbOption mongoOption)
        {
            var connectionString = string.Format(mongoOption.ConnectionString, mongoOption.AppName);
            connection ??= new MongoUrlBuilder(connectionString);
            
            client ??=  new MongoClient(mongoOption.ConnectionString);

            database ??= client.GetDatabase(connection.DatabaseName);
        }

        public virtual Task<IResult> AddItemAsync(Model newMenuItem)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResultWithData<Model>> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResultWithData<IList<Model>>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResultWithData<IList<Model>>> GetItemsWithFilterAsync(FilterDefinition<Model> filter)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResultWithData<Model>> GetItemWithFilterAsync(FilterDefinition<Model> filter)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResult> RemoveItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResult> UpdateItemAsync(Model newMenuItem)
        {
            throw new NotImplementedException();
        }

        protected IList<Model> SortById(IList<Model> unsortedList)
        {
            var sortedList = unsortedList.OrderBy(x => x.Id);
            return sortedList.ToList();
        }
    }
}
