using System;

namespace iTextSharp.Drawing
{
	public abstract class Brush : MarshalByRefObject, ICloneable, IDisposable
	{
		internal IntPtr nativeObject;

		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeObject;
			}
			set
			{
				this.nativeObject = value;
			}
		}

		public abstract object Clone();

		internal Brush(IntPtr ptr)
		{
			this.nativeObject = ptr;
		}

		protected Brush()
		{
		}

		protected internal void SetNativeBrush(IntPtr brush)
		{
			this.nativeObject = brush;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (this.nativeObject != IntPtr.Zero)
			{
				Status arg_28_0 = GDIPlus.GdipDeleteBrush(this.nativeObject);
				this.nativeObject = IntPtr.Zero;
				GDIPlus.CheckStatus(arg_28_0);
			}
		}

		~Brush()
		{
			this.Dispose(false);
		}
	}
}
