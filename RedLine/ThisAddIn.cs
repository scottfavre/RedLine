﻿using Microsoft.Office.Tools;
using RedLine.Crutch;
using RedLine.Scanners;
using RedLine.View;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using RedLine.Histogram;

namespace RedLine
{
	public interface IPanelVisiblity
	{
		bool ScannerPanel { get; set; }

		event EventHandler VisibilityChanged;
	}

	[Export(typeof(IPanelVisiblity))]
	public partial class ThisAddIn : IPanelVisiblity
	{
		private RedLineRibbon _ribbon;

		private ScannerTaskPanel _scannerPanel;
		private CustomTaskPane _scannerContainer;

		private CompositionContainer _container;

		[Import]
		public IScannerService ScannerService { private get; set; }

		[Import]
		public ICrutchWordService CrutchService { private get; set; }

		[Import]
		public IHistogramService HistogramService { private get; set; }

		[Import]
		public ISettingsService Settings { private get; set; }

		protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
		{
			_ribbon = new RedLineRibbon();
			_ribbon.StartScan += OnRibbon_StartScan;
			_ribbon.AddCrutch += OnRibbon_AddCrutch;
			_ribbon.BuildHistogram += OnRibbon_BuildHistogram;

			return _ribbon;
		}

		private void ThisAddIn_Startup(object sender, System.EventArgs e)
		{
			var catalog = new AggregateCatalog(
				new AssemblyCatalog(typeof(ThisAddIn).Assembly));
			_container = new CompositionContainer(catalog);

			_container.ComposeParts(this, _ribbon);

			Application.DocumentOpen += Application_DocumentOpen;

			CreateScannerPanel();
		}

		private void OnRibbon_AddCrutch(object sender, EventArgs<string> e)
		{
			CrutchService.AddWord(e.Data);
		}

		private void OnRibbon_BuildHistogram(object sender, EventArgs e)
		{
			HistogramService.BuildHistogram(Application.ActiveDocument);
		}

		void Application_DocumentOpen(Word.Document Doc)
		{
			CreateScannerPanel();
		}

		void OnRibbon_StartScan(object sender, EventArgs e)
		{
			var results = ScannerService.StartScan(Application.ActiveDocument);

			_scannerPanel.SetResults(results);
		}

		private void CreateScannerPanel()
		{
			if (_scannerContainer != null && _scannerPanel != null)
			{
				CustomTaskPanes.Remove(_scannerContainer);
				_scannerPanel = null;
				_scannerContainer = null;
			}

			bool visible = _scannerContainer == null
				? Settings.PanelVisible
				: _scannerContainer.Visible;

			_scannerPanel = new ScannerTaskPanel();
			_container.ComposeParts(_scannerPanel);

			_scannerContainer = CustomTaskPanes.Add(_scannerPanel, "RedLine");
			_scannerContainer.Visible = visible;
			_scannerContainer.VisibleChanged += OnScannerContainer_VisibleChanged;
		}

		private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
		{
		}

		#region VSTO generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(ThisAddIn_Startup);
			this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
		}

		#endregion

		void OnScannerContainer_VisibleChanged(object sender, EventArgs e)
		{
			RaiseVisiblityChanged();
		}

		public bool ScannerPanel
		{
			get
			{
				return _scannerContainer.Visible;
			}
			set
			{
				if (_scannerContainer.Visible != value)
				{
					Settings.PanelVisible = value;
					_scannerContainer.Visible = value;
					RaiseVisiblityChanged();
				}
			}
		}

		public event EventHandler VisibilityChanged;
		private void RaiseVisiblityChanged()
		{
			var handler = VisibilityChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}
	}
}
