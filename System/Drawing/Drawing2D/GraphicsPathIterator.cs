using System;

namespace iTextSharp.Drawing.Drawing2D
{
	public sealed class GraphicsPathIterator : MarshalByRefObject, IDisposable
	{
		private IntPtr nativeObject = IntPtr.Zero;

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

		public int Count
		{
			get
			{
				if (this.nativeObject == IntPtr.Zero)
				{
					return 0;
				}
				int result;
				GDIPlus.CheckStatus(GDIPlus.GdipPathIterGetCount(this.nativeObject, out result));
				return result;
			}
		}

		public int SubpathCount
		{
			get
			{
				int result;
				GDIPlus.CheckStatus(GDIPlus.GdipPathIterGetSubpathCount(this.nativeObject, out result));
				return result;
			}
		}

		internal GraphicsPathIterator(IntPtr native)
		{
			this.nativeObject = native;
		}

		public GraphicsPathIterator(GraphicsPath path)
		{
			if (path != null)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipCreatePathIter(out this.nativeObject, path.NativeObject));
			}
		}

		internal void Dispose(bool disposing)
		{
			if (this.nativeObject != IntPtr.Zero)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDeletePathIter(this.nativeObject));
				this.nativeObject = IntPtr.Zero;
			}
		}

		public int CopyData(ref PointF[] points, ref byte[] types, int startIndex, int endIndex)
		{
			if (points.Length != types.Length)
			{
				throw new ArgumentException("Invalid arguments passed. Both arrays should have the same length.");
			}
			int result;
			GDIPlus.CheckStatus(GDIPlus.GdipPathIterCopyData(this.nativeObject, out result, points, types, startIndex, endIndex));
			return result;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		~GraphicsPathIterator()
		{
			this.Dispose(false);
		}

		public int Enumerate(ref PointF[] points, ref byte[] types)
		{
			int num = points.Length;
			if (num != types.Length)
			{
				throw new ArgumentException("Invalid arguments passed. Both arrays should have the same length.");
			}
			int result;
			GDIPlus.CheckStatus(GDIPlus.GdipPathIterEnumerate(this.nativeObject, out result, points, types, num));
			return result;
		}

		public bool HasCurve()
		{
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipPathIterHasCurve(this.nativeObject, out result));
			return result;
		}

		public int NextMarker(GraphicsPath path)
		{
			IntPtr path2 = (path == null) ? IntPtr.Zero : path.NativeObject;
			int result;
			GDIPlus.CheckStatus(GDIPlus.GdipPathIterNextMarkerPath(this.nativeObject, out result, path2));
			return result;
		}

		public int NextMarker(out int startIndex, out int endIndex)
		{
			int result;
			GDIPlus.CheckStatus(GDIPlus.GdipPathIterNextMarker(this.nativeObject, out result, out startIndex, out endIndex));
			return result;
		}

		public int NextPathType(out byte pathType, out int startIndex, out int endIndex)
		{
			int result;
			GDIPlus.CheckStatus(GDIPlus.GdipPathIterNextPathType(this.nativeObject, out result, out pathType, out startIndex, out endIndex));
			return result;
		}

		public int NextSubpath(GraphicsPath path, out bool isClosed)
		{
			IntPtr path2 = (path == null) ? IntPtr.Zero : path.NativeObject;
			int result;
			GDIPlus.CheckStatus(GDIPlus.GdipPathIterNextSubpathPath(this.nativeObject, out result, path2, out isClosed));
			return result;
		}

		public int NextSubpath(out int startIndex, out int endIndex, out bool isClosed)
		{
			int result;
			GDIPlus.CheckStatus(GDIPlus.GdipPathIterNextSubpath(this.nativeObject, out result, out startIndex, out endIndex, out isClosed));
			return result;
		}

		public void Rewind()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipPathIterRewind(this.nativeObject));
		}
	}
}
