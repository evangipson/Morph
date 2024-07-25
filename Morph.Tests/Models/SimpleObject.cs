using System.Text;

namespace Morph.Tests.Models
{
	public class SimpleObject
	{
		public string? Value { get; set; }

		public decimal Amount { get; set; }

		public int Times { get; set; }

		public int[][]? Dimensions { get; set; }

		public Guid Id { get; set; }

		public override string ToString()
		{
			return $"\nValue: {Value}\nAmount: {Amount.ToString("C")}\nTimes: {Times}\nDimensions: {GetDimensionsString()}\nId: {Id}";
		}

		private string GetDimensionsString()
		{
			if(Dimensions == null || !Dimensions.Any())
			{
				return "[]";
			}

			var sb = new StringBuilder();
			foreach (var topLevelEntry in Dimensions)
			{
				sb.Append($"[{string.Join(", ", topLevelEntry)}], ");
			}
			return $"[{sb.ToString().Trim().TrimEnd(',')}]";
		}
	}
}
