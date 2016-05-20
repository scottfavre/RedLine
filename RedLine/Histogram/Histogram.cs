using System.Collections.Generic;

namespace RedLine.Histogram
{
	public class Histogram
	{
		public Histogram()
		{
			Buckets = new Dictionary<string, int>();
			Exclusions = new HashSet<string>();
		}

		public void Exclude(params string[] exclusions)
		{
			foreach (var exclusion in exclusions)
			{
				Exclusions.Add(exclusion.ToLowerInvariant());
			}
		}

		public void Add(string word)
		{
			var key = word.ToLowerInvariant();

			if (Exclusions.Contains(key)) return;

			if (Buckets.ContainsKey(key))
			{
				Buckets[key]++;
			}
			else
			{
				Buckets[key] = 1;
			}
		}

		public Dictionary<string, int> Buckets { get; }
		public HashSet<string> Exclusions { get; }
	}
}
