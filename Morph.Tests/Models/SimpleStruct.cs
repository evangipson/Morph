namespace Morph.Tests.Models
{
	public struct SimpleStruct(string someString, int someInt)
	{
		public readonly string SomeString => someString;

		public readonly int SomeInt => someInt;
	}
}
