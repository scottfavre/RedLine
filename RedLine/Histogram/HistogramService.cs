using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Word;

namespace RedLine.Histogram
{
	public interface IHistogramService
	{
		Histogram BuildHistogram(Document document);
	}

	[Export(typeof(IHistogramService))]
	public class HistogramService : IHistogramService
	{
		public Histogram BuildHistogram(Document document)
		{
			var text = document.Content.Text;

			var regex = new Regex(@"\b\w+\b", RegexOptions.Compiled);

			var matches = regex.Matches(text);
			
			var histogram = new Histogram();
			histogram.Exclude("the", "and", "but", "a", "in", "for", "to", "as", "of");

			foreach (Match match in matches)
			{
				histogram.Add(match.Value);
			}

			// TODO - do something other than dump to disk

			using (var stream = File.Create(@"C:\tmp\histogram.csv"))
			using (var writer = new StreamWriter(stream))
			{
				writer.WriteLine("Word,Count");
				foreach (var bucket in histogram.Buckets.OrderByDescending(kvp => kvp.Value))
				{
					writer.WriteLine("{0},{1}", bucket.Key, bucket.Value);
				}
			}

			return histogram;
		}
	}
}
