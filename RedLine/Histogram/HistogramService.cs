using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Word;

namespace RedLine.Histogram
{
	public interface IHistogramService
	{
		Histogram BuildHistogram(Document document);
		void ExportCsv(string outputFilePath, Histogram histogram);
	}

	[Export(typeof(IHistogramService))]
	public class HistogramService : IHistogramService
	{
		[Import]
		public IFileSystemService FileSystem { private get; set; }

		public HistogramService()
		{
			_histogram = new Histogram();
			_exclusions = new HashSet<string> { "the", "and", "but", "a", "in", "for", "to", "as", "of", "an" };
		}
		private Histogram _histogram;

		public Histogram BuildHistogram(Document document)
		{
			var showRevisions = document.ShowRevisions;
			document.ShowRevisions = false;

			var text = document.Content.Text;

			text = text.Replace('’', '\'');

			document.ShowRevisions = showRevisions;

			var regex = new Regex(@"\b[\w']+", RegexOptions.Compiled);

			var matches = regex.Matches(text);

			//var histogram = new Histogram();
			_histogram.Exclude();

			var series = new Series()
			{
				Name = Path.GetFileName(document.FullName)
			};
			_histogram.Series.Add(series);

			foreach (Match match in matches)
			{
				series.Add(match.Value);
			}

			ExportCsv(@"c:\tmp\histogram.csv", _histogram);

			return _histogram;
		}

		public void ExportCsv(string outputFilePath, Histogram histogram)
		{
			using (var stream = FileSystem.CreateFile(outputFilePath))
			using (var writer = new StreamWriter(stream))
			{
				var headers = new List<string> { "Word" };
				headers.AddRange(histogram.Series.Select(s => "\"" + s.Name + "\""));
				writer.WriteLine(string.Join(",", headers));

				var allWords = histogram.Series
					.SelectMany(s => s.Buckets.Keys)
					.Distinct()
					.OrderBy(w => w);

				foreach (var word in allWords.Except(_exclusions))
				{
					var line = new List<string> { word };
					line.AddRange(histogram.Series.Select(s => s[word].ToString()));

					writer.WriteLine(string.Join(",", line));
				}
			}
		}

		private readonly HashSet<string> _exclusions;
	}
}
