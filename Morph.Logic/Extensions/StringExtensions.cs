using System.Text;

namespace Morph.Logic.Extensions
{
	public static class StringExtensions
	{
		public static string GetByteString(this byte[] bytes)
		{
			var sb = new StringBuilder();
			foreach (var b in bytes)
			{
				sb.Append($"{b:x4}, ");
			}
			return $"[{sb.ToString().Trim().TrimEnd(',')}]";
		}

		public static string GetListString(this ICollection<string> strings)
		{
			return string.Join(", ", strings).Trim().TrimEnd(',');
		}
	}
}
