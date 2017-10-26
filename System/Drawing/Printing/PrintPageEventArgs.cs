using System;

namespace iTextSharp.Drawing.Printing
{
	public class PrintPageEventArgs : EventArgs
	{
		private bool cancel;

		private Graphics graphics;

		private bool hasmorePages;

		private Rectangle marginBounds;

		private Rectangle pageBounds;

		private PageSettings pageSettings;

		private GraphicsPrinter graphics_context;

		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		public bool HasMorePages
		{
			get
			{
				return this.hasmorePages;
			}
			set
			{
				this.hasmorePages = value;
			}
		}

		public Rectangle MarginBounds
		{
			get
			{
				return this.marginBounds;
			}
		}

		public Rectangle PageBounds
		{
			get
			{
				return this.pageBounds;
			}
		}

		public PageSettings PageSettings
		{
			get
			{
				return this.pageSettings;
			}
		}

		internal GraphicsPrinter GraphicsContext
		{
			get
			{
				return this.graphics_context;
			}
			set
			{
				this.graphics_context = value;
			}
		}

		public PrintPageEventArgs(Graphics graphics, Rectangle marginBounds, Rectangle pageBounds, PageSettings pageSettings)
		{
			this.graphics = graphics;
			this.marginBounds = marginBounds;
			this.pageBounds = pageBounds;
			this.pageSettings = pageSettings;
		}

		internal void SetGraphics(Graphics g)
		{
			this.graphics = g;
		}
	}
}
