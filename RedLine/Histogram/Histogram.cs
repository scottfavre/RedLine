using System.Collections.Generic;

namespace RedLine.Histogram
{
	public class Histogram
	{
		public Histogram()
		{
			Series = new List<Series>();
		}

		public List<Series> Series{ get; set; }
	}

	public class Series
	{
		public Series()
		{
			Buckets = new Dictionary<string, int>();
		}

		public void Add(string word)
		{
			var key = word.ToLowerInvariant();

			if (Buckets.ContainsKey(key))
			{
				Buckets[key]++;
			}
			else
			{
				Buckets[key] = 1;
			}
		}

		public int this[string word]
		{
			get
			{
				int count;

				Buckets.TryGetValue(word, out count);

				return count;
			}
		}

		public string Name { get; set; }
		public Dictionary<string, int> Buckets { get; set; }
	}
}
