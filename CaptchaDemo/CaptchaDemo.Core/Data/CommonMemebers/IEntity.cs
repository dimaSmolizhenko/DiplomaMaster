using MongoDB.Bson.Serialization.Attributes;

namespace CaptchaDemo.Core.Data.CommonMemebers
{
	public interface IEntity<T>
	{
		[BsonId]
		T Id { get; set; }
	}

	public interface IEntity : IEntity<string> { }
}
