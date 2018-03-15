using System.Collections.Generic;
using System.Threading.Tasks;
using CaptchaDemo.Configuration;
using CaptchaDemo.Data.CommonMemebers;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using MongoDB.Driver.Linq;

namespace CaptchaDemo.Data.Repositories
{
	public class Repository<T> : IRepository<T> where T: IEntity
	{
		#region Dependencies

		private readonly IMongoCollection<T> _collection;

		#endregion

		#region .ctor

		public Repository(IDbConfiguration dbConfiguration, string collectionName)
		{
			//TODO: refactor connection to db
			var connectionString = dbConfiguration.GetConnectionString();
			var connection = new MongoUrlBuilder(connectionString);
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase(connection.DatabaseName);

			var isLive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}")
				.Wait(1000);

			if (!isLive)
			{
				return;
			}

			var collections = Task.Run(async () => await database.ListCollectionsAsync(new ListCollectionsOptions
			{
				Filter = new BsonDocument("name", collectionName)
			})).Result;

			if (!collections.Any())
			{
				database.CreateCollection(collectionName);
			}

			_collection = database.GetCollection<T>(collectionName);
		}

		#endregion

		#region Public Methods

		public async Task<T> GetByIdAsync(string id)
		{
			return await _collection.AsQueryable().FirstOrDefaultAsync(t => t.Id.Equals(id));
		}

		public async Task<IList<T>> GetByTypeAsync(string type)
		{
			var filter = new BsonDocument("Type", type);
			return await _collection.Find(filter).ToListAsync();
		}

		public string CreateObjectId()
		{
			return ObjectId.GenerateNewId().ToString();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			var builder = new FilterDefinitionBuilder<T>();
			var filter = builder.Empty; 
			return await _collection.Find(filter).ToListAsync();
		}

		public async Task InsertAsync(T entity)
		{
			await _collection.InsertOneAsync(entity);
		}

		public async Task InsertManyAsync(IEnumerable<T> entities)
		{
			await _collection.InsertManyAsync(entities);
		}

		public async Task UpdateAsync(T entity)
		{
			await _collection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(entity.Id)), entity);
		}

		public async Task DeleteAsync(string id)
		{
			await _collection.DeleteOneAsync(id);
		}

		#endregion
	}
}
