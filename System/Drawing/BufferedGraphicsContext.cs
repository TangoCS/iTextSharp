using System;

namespace iTextSharp.Drawing
{
	public sealed class BufferedGraphicsContext : IDisposable
	{
		private Size max_buffer;

		public Size MaximumBuffer
		{
			get
			{
				return this.max_buffer;
			}
			set
			{
				if (value.Width <= 0 || value.Height <= 0)
				{
					throw new ArgumentException("The height or width of the size is less than or equal to zero.");
				}
				this.max_buffer = value;
			}
		}

		public BufferedGraphicsContext()
		{
			this.max_buffer = Size.Empty;
		}

		~BufferedGraphicsContext()
		{
		}

		public BufferedGraphics Allocate(Graphics targetGraphics, Rectangle targetRectangle)
		{
			return new BufferedGraphics(targetGraphics, targetRectangle);
		}

		[MonoTODO("The targetDC parameter has no equivalent in libgdiplus.")]
		public BufferedGraphics Allocate(IntPtr targetDC, Rectangle targetRectangle)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public void Invalidate()
		{
		}
	}
}
