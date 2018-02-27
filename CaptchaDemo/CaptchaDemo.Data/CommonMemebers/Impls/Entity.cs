using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CaptchaDemo.Data.CommonMemebers.Impls
{
	public class Entity : IEntity
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public virtual string Id { get; set; }
	}
}
