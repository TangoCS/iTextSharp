using System;
using System.ComponentModel;

namespace iTextSharp.Drawing.Printing
{
	public class PrintEventArgs : CancelEventArgs
	{
		private GraphicsPrinter graphics_context;

		private PrintAction action;

		public PrintAction PrintAction
		{
			get
			{
				return this.action;
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

		public PrintEventArgs()
		{
		}

		internal PrintEventArgs(PrintAction action)
		{
			this.action = action;
		}
	}
}
