using Morph.Logic.Extensions;
using Morph.Tests.Models;

namespace Morph.Tests.Extensions
{
	public class SerializerTests
	{
		[Fact]
		public void Serialization_ShouldSerializeToByteArray_WhenObjectIsSerialized()
		{
			var complexObject = CreateComplexObject();
			var serializedObject = complexObject.Serialize();

			Assert.IsType<byte[]>(serializedObject);
			Assert.NotEmpty(serializedObject);
		}

		[Fact]
		public void Serialization_ShouldSerializeToByteArray_WhenStringIsSerialized()
		{
			var stringToSerialize = "serialize-this-string";
			var serializedObject = stringToSerialize.Serialize();

			Assert.IsType<byte[]>(serializedObject);
			Assert.NotEmpty(serializedObject);
		}

		[Fact]
		public void Deserialization_ShouldDeserializeBackToObject_WhenObjectIsDeserialized()
		{
			var complexObject = CreateComplexObject();
			var serializedObject = complexObject.Serialize();
			var deserializedObject = serializedObject.Deserialize<ComplexObject>();

			Assert.IsType<ComplexObject>(deserializedObject);
			Assert.True(deserializedObject.IsSuccess);
		}

		[Fact]
		public void Deserialization_ShouldDeserializeBackToString_WhenStringIsDeserialized()
		{
			var stringToSerialize = "serialize-this-string";
			var serializedString = stringToSerialize.Serialize();
			string? deserializedString = serializedString.Deserialize();

			Assert.NotNull(deserializedString);
			Assert.IsType<string>(deserializedString);
			Assert.Equal(stringToSerialize, deserializedString);
		}

		[Fact]
		public void Deserialization_ShouldDeserializeBackToInt_WhenIntIsDeserialized()
		{
			var numberToSerialize = 1234;
			var serializedNumber = numberToSerialize.Serialize();
			int? deserializedNumber = serializedNumber.DeserializeStruct<int>();

			Assert.NotNull(deserializedNumber);
			Assert.IsType<int>(deserializedNumber);
			Assert.Equal(numberToSerialize, deserializedNumber);
		}

		[Fact]
		public void Deserialization_ShouldDeserializeBackToList_WhenListIsDeserialized()
		{
			var listToSerialize = new List<int> { 1, 2, 3, 4 };
			var serializedList = listToSerialize.Serialize();
			List<int>? deserializedList = serializedList.Deserialize<List<int>>();

			Assert.NotNull(deserializedList);
			Assert.IsType<List<int>>(deserializedList);
			Assert.Equal(listToSerialize, deserializedList);
		}

		[Fact]
		public void Serialization_ShouldMaintainObjectReferences_WhenObjectIsTransformed()
		{
			var complexObject = CreateComplexObject();
			var serializedObject = complexObject.Serialize();
			var deserializedObject = serializedObject.Deserialize<ComplexObject>();

			Assert.NotNull(deserializedObject);
			Assert.IsType<SimpleObject>(deserializedObject.SimpleObject);
		}

		private SimpleObject CreateSimpleObject() => new()
		{
			Dimensions = [[0, 1, 2], [2, 1, 0]],
			Amount = 15.50m,
			Id = Guid.NewGuid(),
			Times = 5,
			Value = "Simple Object"
		};

		private ComplexObject CreateComplexObject() => new()
		{
			Strings = ["first", "second"],
			IsSuccess = true,
			SimpleObject = CreateSimpleObject()
		};
	}
}