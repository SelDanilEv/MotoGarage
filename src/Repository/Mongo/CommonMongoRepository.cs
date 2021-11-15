using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Result.Interfaces;
using Infrastructure.Models;
using Infrastructure.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Infrastructure.Result;

namespace Repository.Mongo
{
    public class CommonMongoRepository<T> : BaseMongoRepository<T> where T : IBaseModel
    {
        protected IMongoCollection<T> _mongoCollection;

        public CommonMongoRepository(MongoDbOption mongoOption) : base(mongoOption)
        {
            _mongoCollection = database.GetCollection<T>(typeof(T).Name);
        }

        public override async Task<IResultWithData<IList<T>>> GetItemsAsync()
        {
            var result = new Result<IList<T>>();

            try
            {
                var unsortedResult = await _mongoCollection.Find(new BsonDocument()).ToListAsync();
                result.Data = unsortedResult;
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResultWithData<IList<T>>> GetItemsWithFilterAsync(FilterDefinition<T> filter)
        {
            var result = new Result<IList<T>>();

            try
            {
                var unsortedResult = await _mongoCollection.Find(filter).ToListAsync();
                result.Data = unsortedResult;
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResultWithData<T>> GetItemWithFilterAsync(FilterDefinition<T> filter)
        {
            var result = new Result<T>();

            try
            {
                result.Data = await _mongoCollection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResultWithData<T>> GetItemAsync(Guid id)
        {
            var result = new Result<T>();

            try
            {
                result.Data = await _mongoCollection.Find(new BsonDocument("_id", new BsonBinaryData(id, GuidRepresentation.Standard))).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResult> AddItemAsync(T newUserInfo)
        {
            var result = new Result();

            try
            {
                await _mongoCollection.InsertOneAsync(newUserInfo);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResult> UpdateItemAsync(T updatedUserInfo)
        {
            var result = new Result();

            try
            {
                await _mongoCollection.ReplaceOneAsync(
                    new BsonDocument(
                        "_id",
                        new BsonBinaryData(updatedUserInfo.Id, GuidRepresentation.Standard)),
                        updatedUserInfo);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResult> RemoveItemAsync(Guid id)
        {
            var result = new Result();

            try
            {
                await _mongoCollection.DeleteOneAsync(new BsonDocument("_id", new BsonBinaryData(id,GuidRepresentation.Standard)));
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }
    }
}
