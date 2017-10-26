using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace iTextSharp.Drawing.Printing
{
	[DefaultEvent("PrintPage"), DefaultProperty("DocumentName"), ToolboxItemFilter("")]
	public class PrintDocument : Component
	{
		private PageSettings defaultpagesettings;

		private PrinterSettings printersettings;

		private PrintController printcontroller;

		private string documentname;

		private bool originAtMargins;

		[SRDescription("Raised when printing begins")]
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event PrintEventHandler BeginPrint;

		[SRDescription("Raised when printing ends")]
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event PrintEventHandler EndPrint;

		[SRDescription("Raised when printing of a new page begins")]
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event PrintPageEventHandler PrintPage;

		[SRDescription("Raised before printing of a new page begins")]
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event QueryPageSettingsEventHandler QueryPageSettings;

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), SRDescription("The settings for the current page.")]
		public PageSettings DefaultPageSettings
		{
			get
			{
				return this.defaultpagesettings;
			}
			set
			{
				this.defaultpagesettings = value;
			}
		}

		[DefaultValue("document"), SRDescription("The name of the document.")]
		public string DocumentName
		{
			get
			{
				return this.documentname;
			}
			set
			{
				this.documentname = value;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), SRDescription("The print controller object.")]
		public PrintController PrintController
		{
			get
			{
				return this.printcontroller;
			}
			set
			{
				this.printcontroller = value;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), SRDescription("The current settings for the active printer.")]
		public PrinterSettings PrinterSettings
		{
			get
			{
				return this.printersettings;
			}
			set
			{
				this.printersettings = ((value == null) ? new PrinterSettings() : value);
			}
		}

		[DefaultValue(false), SRDescription("Determines if the origin is set at the specified margins.")]
		public bool OriginAtMargins
		{
			get
			{
				return this.originAtMargins;
			}
			set
			{
				this.originAtMargins = value;
			}
		}

		public PrintDocument()
		{
			this.documentname = "document";
			this.printersettings = new PrinterSettings();
			this.defaultpagesettings = (PageSettings)this.printersettings.DefaultPageSettings.Clone();
			this.printcontroller = new StandardPrintController();
		}

		public void Print()
		{
			PrintEventArgs printEventArgs = new PrintEventArgs();
			this.OnBeginPrint(printEventArgs);
			if (printEventArgs.Cancel)
			{
				return;
			}
			this.PrintController.OnStartPrint(this, printEventArgs);
			if (printEventArgs.Cancel)
			{
				return;
			}
			Graphics graphics = null;
			if (printEventArgs.GraphicsContext != null)
			{
				graphics = Graphics.FromHdc(printEventArgs.GraphicsContext.Hdc);
				printEventArgs.GraphicsContext.Graphics = graphics;
			}
			PrintPageEventArgs printPageEventArgs;
			do
			{
				QueryPageSettingsEventArgs queryPageSettingsEventArgs = new QueryPageSettingsEventArgs(this.DefaultPageSettings.Clone() as PageSettings);
				this.OnQueryPageSettings(queryPageSettingsEventArgs);
				PageSettings pageSettings = queryPageSettingsEventArgs.PageSettings;
				printPageEventArgs = new PrintPageEventArgs(graphics, pageSettings.Bounds, new Rectangle(0, 0, pageSettings.PaperSize.Width, pageSettings.PaperSize.Height), pageSettings);
				printPageEventArgs.GraphicsContext = printEventArgs.GraphicsContext;
				Graphics graphics2 = this.PrintController.OnStartPage(this, printPageEventArgs);
				printPageEventArgs.SetGraphics(graphics2);
				if (!printPageEventArgs.Cancel)
				{
					this.OnPrintPage(printPageEventArgs);
				}
				this.PrintController.OnEndPage(this, printPageEventArgs);
			}
			while (!printPageEventArgs.Cancel && printPageEventArgs.HasMorePages);
			this.OnEndPrint(printEventArgs);
			this.PrintController.OnEndPrint(this, printEventArgs);
		}

		public override string ToString()
		{
			return "[PrintDocument " + this.DocumentName + "]";
		}

		protected virtual void OnBeginPrint(PrintEventArgs e)
		{
			if (this.BeginPrint != null)
			{
				this.BeginPrint(this, e);
			}
		}

		protected virtual void OnEndPrint(PrintEventArgs e)
		{
			if (this.EndPrint != null)
			{
				this.EndPrint(this, e);
			}
		}

		protected virtual void OnPrintPage(PrintPageEventArgs e)
		{
			if (this.PrintPage != null)
			{
				this.PrintPage(this, e);
			}
		}

		protected virtual void OnQueryPageSettings(QueryPageSettingsEventArgs e)
		{
			if (this.QueryPageSettings != null)
			{
				this.QueryPageSettings(this, e);
			}
		}
	}
}
