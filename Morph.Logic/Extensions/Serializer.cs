using System.IO.Compression;
using System.Runtime.Serialization;

namespace Morph.Logic.Extensions
{
	/// <summary>
	/// A static reference to the Morph serialization and
	/// deserialization functionality.
	/// </summary>
	public static class Serializer
	{
		/// <summary>
		/// Compresses and serializes the provided object.
		/// </summary>
		/// <typeparam name="SerializedType">
		/// The type of the object to serialize.
		/// </typeparam>
		/// <param name="objectToSerialize">
		/// A required object to serialize.
		/// </param>
		/// <param name="compressionLevel">
		/// An optional compression level to use in serialization.
		/// Defaults to <see cref="CompressionLevel.SmallestSize"/>.
		/// </param>
		/// <returns>
		/// A compressed serialized array of <see cref="byte"/>.
		/// </returns>
		/// <exception cref="Exception"></exception>
		public static byte[] Serialize<SerializedType>(this SerializedType objectToSerialize, CompressionLevel compressionLevel = CompressionLevel.SmallestSize)
		{
			var serializer = new DataContractSerializer(typeof(SerializedType));
			try
			{
				using (MemoryStream stream = new())
				{
					using (GZipStream compressionStream = new(stream, compressionLevel))
					{
						serializer.WriteObject(compressionStream, objectToSerialize);
					}
					return stream.ToArray();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Deserializes the provided array of <see cref="byte"/>
		/// into an object of <typeparamref name="SerializedType"/>.
		/// Defaults to <c>null</c> if deserialization was unsuccessful.
		/// </summary>
		/// <typeparam name="SerializedType">
		/// The type of the object to deserialize.
		/// </typeparam>
		/// <param name="bytes">
		/// The array of <see cref="byte"/> to deserialize.
		/// </param>
		/// <returns>
		/// An object of <typeparamref name="SerializedType"/> if
		/// deserialization was successful. Defaults to <c>null</c>.
		/// </returns>
		/// <exception cref="Exception"></exception>
		public static SerializedType? Deserialize<SerializedType>(this byte[] bytes) where SerializedType : class, new()
		{
			var serializer = new DataContractSerializer(typeof(SerializedType));
			try
			{
				using (MemoryStream stream = new MemoryStream(bytes))
				{
					using (GZipStream decompressionStream = new(stream, CompressionMode.Decompress))
					{
						return serializer.ReadObject(decompressionStream) as SerializedType;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Deserializes the provided array of <see cref="byte"/>
		/// into a <see cref="string"/>. Defaults to <c>null</c>
		/// if deserialization was unsuccessful.
		/// </summary>
		/// <param name="bytes">
		/// The array of <see cref="byte"/> to deserialize.
		/// </param>
		/// <returns>
		/// A <see cref="string"/> if deserialization was successful.
		/// Defaults to <c>null</c>.
		/// </returns>
		/// <exception cref="Exception"></exception>
		public static string? Deserialize(this byte[] bytes)
		{
			var serializer = new DataContractSerializer(typeof(string));
			try
			{
				using (MemoryStream stream = new MemoryStream(bytes))
				{
					using (GZipStream decompressionStream = new(stream, CompressionMode.Decompress))
					{
						return serializer.ReadObject(decompressionStream)?.ToString();
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Deserializes the provided array of <see cref="byte"/>
		/// into a struct of <typeparamref name="SerializedStruct"/>.
		/// </summary>
		/// <typeparam name="SerializedStruct">
		/// The type of the struct to deserialize.
		/// </typeparam>
		/// <param name="bytes">
		/// The array of <see cref="byte"/> to deserialize.
		/// </param>
		/// <returns>
		/// A struct of <typeparamref name="SerializedStruct"/> if
		/// deserialization was successful.
		/// </returns>
		/// <exception cref="Exception"></exception>
		public static SerializedStruct? DeserializeStruct<SerializedStruct>(this byte[] bytes) where SerializedStruct : struct
		{
			var serializer = new DataContractSerializer(typeof(SerializedStruct));
			try
			{
				using (MemoryStream stream = new MemoryStream(bytes))
				{
					using (GZipStream decompressionStream = new(stream, CompressionMode.Decompress))
					{
						return (SerializedStruct)serializer.ReadObject(decompressionStream);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
