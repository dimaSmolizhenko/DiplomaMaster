using System.Collections.Generic;
using System.Threading.Tasks;
using CaptchaDemo.Configuration;
using CaptchaDemo.Data.CommonMemebers;
using MongoDB.Bson;
using MongoDB.Driver;
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
			_collection = database.GetCollection<T>(collectionName);
		}

		#endregion

		#region Public Methods

		public async Task<T> GetByIdAsync(string id)
		{
			return await _collection.AsQueryable().FirstOrDefaultAsync(t => t.Id.Equals(id));
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

		public string CreateObjectId() //TODO: move out
		{
			return ObjectId.GenerateNewId().ToString();
		}

		#endregion
	}
}
