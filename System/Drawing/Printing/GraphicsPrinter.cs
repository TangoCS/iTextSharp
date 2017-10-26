using System;

namespace iTextSharp.Drawing.Printing
{
	internal class GraphicsPrinter
	{
		private Graphics graphics;

		private IntPtr hDC;

		internal Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
			set
			{
				this.graphics = value;
			}
		}

		internal IntPtr Hdc
		{
			get
			{
				return this.hDC;
			}
		}

		internal GraphicsPrinter(Graphics gr, IntPtr dc)
		{
			this.graphics = gr;
			this.hDC = dc;
		}
	}
}
