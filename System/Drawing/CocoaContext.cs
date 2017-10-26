using System;

namespace iTextSharp.Drawing
{
	internal struct CocoaContext : IMacContext
	{
		public IntPtr ctx;

		public int width;

		public int height;

		public CocoaContext(IntPtr ctx, int width, int height)
		{
			this.ctx = ctx;
			this.width = width;
			this.height = height;
		}

		public void Synchronize()
		{
			MacSupport.CGContextSynchronize(this.ctx);
		}

		public void Release()
		{
			MacSupport.CGContextRestoreGState(this.ctx);
		}
	}
}
