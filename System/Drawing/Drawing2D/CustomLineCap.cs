using System;

namespace iTextSharp.Drawing.Drawing2D
{
	public class CustomLineCap : MarshalByRefObject, ICloneable, IDisposable
	{
		private bool disposed;

		internal IntPtr nativeObject;

		public LineCap BaseCap
		{
			get
			{
				LineCap result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetCustomLineCapBaseCap(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetCustomLineCapBaseCap(this.nativeObject, value));
			}
		}

		public LineJoin StrokeJoin
		{
			get
			{
				LineJoin result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetCustomLineCapStrokeJoin(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetCustomLineCapStrokeJoin(this.nativeObject, value));
			}
		}

		public float BaseInset
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetCustomLineCapBaseInset(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetCustomLineCapBaseInset(this.nativeObject, value));
			}
		}

		public float WidthScale
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetCustomLineCapWidthScale(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetCustomLineCapWidthScale(this.nativeObject, value));
			}
		}

		internal CustomLineCap()
		{
		}

		internal CustomLineCap(IntPtr ptr)
		{
			this.nativeObject = ptr;
		}

		public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath) : this(fillPath, strokePath, LineCap.Flat, 0f)
		{
		}

		public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath, LineCap baseCap) : this(fillPath, strokePath, baseCap, 0f)
		{
		}

		public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath, LineCap baseCap, float baseInset)
		{
			IntPtr fillPath2 = IntPtr.Zero;
			IntPtr strokePath2 = IntPtr.Zero;
			if (fillPath != null)
			{
				fillPath2 = fillPath.nativePath;
			}
			if (strokePath != null)
			{
				strokePath2 = strokePath.nativePath;
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateCustomLineCap(fillPath2, strokePath2, baseCap, baseInset, out this.nativeObject));
		}

		public object Clone()
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneCustomLineCap(this.nativeObject, out ptr));
			return new CustomLineCap(ptr);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDeleteCustomLineCap(this.nativeObject));
				this.disposed = true;
				this.nativeObject = IntPtr.Zero;
			}
		}

		~CustomLineCap()
		{
			this.Dispose(false);
		}

		public void GetStrokeCaps(out LineCap startCap, out LineCap endCap)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipGetCustomLineCapStrokeCaps(this.nativeObject, out startCap, out endCap));
		}

		public void SetStrokeCaps(LineCap startCap, LineCap endCap)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetCustomLineCapStrokeCaps(this.nativeObject, startCap, endCap));
		}
	}
}
