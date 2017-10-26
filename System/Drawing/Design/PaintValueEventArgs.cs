using System;
using System.ComponentModel;

namespace iTextSharp.Drawing.Design
{
	public class PaintValueEventArgs : EventArgs
	{
		private ITypeDescriptorContext context;

		private object value;

		private Graphics graphics;

		private Rectangle bounds;

		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		public ITypeDescriptorContext Context
		{
			get
			{
				return this.context;
			}
		}

		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		public object Value
		{
			get
			{
				return this.value;
			}
		}

		public PaintValueEventArgs(ITypeDescriptorContext context, object value, Graphics graphics, Rectangle bounds)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			this.context = context;
			this.value = value;
			this.graphics = graphics;
			this.bounds = bounds;
		}
	}
}
