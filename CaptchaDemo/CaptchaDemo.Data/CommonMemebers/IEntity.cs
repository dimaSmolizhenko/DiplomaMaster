using MongoDB.Bson.Serialization.Attributes;

namespace CaptchaDemo.Data.CommonMemebers
{
	public interface IEntity<T>
	{
		[BsonId]
		T Id { get; set; }
	}

	public interface IEntity : IEntity<string> { }
}
