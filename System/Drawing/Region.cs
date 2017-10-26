using System;
using iTextSharp.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace iTextSharp.Drawing
{
	public sealed class Region : MarshalByRefObject, IDisposable
	{
		private IntPtr nativeRegion = IntPtr.Zero;

		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeRegion;
			}
			set
			{
				this.nativeRegion = value;
			}
		}

		public Region()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegion(out this.nativeRegion));
		}

		internal Region(IntPtr native)
		{
			this.nativeRegion = native;
		}

		public Region(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionPath(path.NativeObject, out this.nativeRegion));
		}

		public Region(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionRectI(ref rect, out this.nativeRegion));
		}

		public Region(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionRect(ref rect, out this.nativeRegion));
		}

		public Region(RegionData rgnData)
		{
			if (rgnData == null)
			{
				throw new ArgumentNullException("rgnData");
			}
			if (rgnData.Data.Length == 0)
			{
				throw new ArgumentException("rgnData");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionRgnData(rgnData.Data, rgnData.Data.Length, out this.nativeRegion));
		}

		public void Union(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionPath(this.nativeRegion, path.NativeObject, CombineMode.Union));
		}

		public void Union(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Union));
		}

		public void Union(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRect(this.nativeRegion, ref rect, CombineMode.Union));
		}

		public void Union(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRegion(this.nativeRegion, region.NativeObject, CombineMode.Union));
		}

		public void Intersect(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionPath(this.nativeRegion, path.NativeObject, CombineMode.Intersect));
		}

		public void Intersect(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Intersect));
		}

		public void Intersect(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRect(this.nativeRegion, ref rect, CombineMode.Intersect));
		}

		public void Intersect(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRegion(this.nativeRegion, region.NativeObject, CombineMode.Intersect));
		}

		public void Complement(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionPath(this.nativeRegion, path.NativeObject, CombineMode.Complement));
		}

		public void Complement(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Complement));
		}

		public void Complement(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRect(this.nativeRegion, ref rect, CombineMode.Complement));
		}

		public void Complement(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRegion(this.nativeRegion, region.NativeObject, CombineMode.Complement));
		}

		public void Exclude(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionPath(this.nativeRegion, path.NativeObject, CombineMode.Exclude));
		}

		public void Exclude(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Exclude));
		}

		public void Exclude(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRect(this.nativeRegion, ref rect, CombineMode.Exclude));
		}

		public void Exclude(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRegion(this.nativeRegion, region.NativeObject, CombineMode.Exclude));
		}

		public void Xor(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionPath(this.nativeRegion, path.NativeObject, CombineMode.Xor));
		}

		public void Xor(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Xor));
		}

		public void Xor(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRect(this.nativeRegion, ref rect, CombineMode.Xor));
		}

		public void Xor(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRegion(this.nativeRegion, region.NativeObject, CombineMode.Xor));
		}

		public RectangleF GetBounds(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			RectangleF result = default(Rectangle);
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionBounds(this.nativeRegion, g.NativeObject, ref result));
			return result;
		}

		public void Translate(int dx, int dy)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateRegionI(this.nativeRegion, dx, dy));
		}

		public void Translate(float dx, float dy)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateRegion(this.nativeRegion, dx, dy));
		}

		public bool IsVisible(int x, int y, Graphics g)
		{
			IntPtr graphics = (g == null) ? IntPtr.Zero : g.NativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPointI(this.nativeRegion, x, y, graphics, out result));
			return result;
		}

		public bool IsVisible(int x, int y, int width, int height)
		{
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRectI(this.nativeRegion, x, y, width, height, IntPtr.Zero, out result));
			return result;
		}

		public bool IsVisible(int x, int y, int width, int height, Graphics g)
		{
			IntPtr graphics = (g == null) ? IntPtr.Zero : g.NativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRectI(this.nativeRegion, x, y, width, height, graphics, out result));
			return result;
		}

		public bool IsVisible(Point point)
		{
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPointI(this.nativeRegion, point.X, point.Y, IntPtr.Zero, out result));
			return result;
		}

		public bool IsVisible(PointF point)
		{
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPoint(this.nativeRegion, point.X, point.Y, IntPtr.Zero, out result));
			return result;
		}

		public bool IsVisible(Point point, Graphics g)
		{
			IntPtr graphics = (g == null) ? IntPtr.Zero : g.NativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPointI(this.nativeRegion, point.X, point.Y, graphics, out result));
			return result;
		}

		public bool IsVisible(PointF point, Graphics g)
		{
			IntPtr graphics = (g == null) ? IntPtr.Zero : g.NativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPoint(this.nativeRegion, point.X, point.Y, graphics, out result));
			return result;
		}

		public bool IsVisible(Rectangle rect)
		{
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRectI(this.nativeRegion, rect.X, rect.Y, rect.Width, rect.Height, IntPtr.Zero, out result));
			return result;
		}

		public bool IsVisible(RectangleF rect)
		{
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRect(this.nativeRegion, rect.X, rect.Y, rect.Width, rect.Height, IntPtr.Zero, out result));
			return result;
		}

		public bool IsVisible(Rectangle rect, Graphics g)
		{
			IntPtr graphics = (g == null) ? IntPtr.Zero : g.NativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRectI(this.nativeRegion, rect.X, rect.Y, rect.Width, rect.Height, graphics, out result));
			return result;
		}

		public bool IsVisible(RectangleF rect, Graphics g)
		{
			IntPtr graphics = (g == null) ? IntPtr.Zero : g.NativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRect(this.nativeRegion, rect.X, rect.Y, rect.Width, rect.Height, graphics, out result));
			return result;
		}

		public bool IsVisible(float x, float y)
		{
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPoint(this.nativeRegion, x, y, IntPtr.Zero, out result));
			return result;
		}

		public bool IsVisible(float x, float y, Graphics g)
		{
			IntPtr graphics = (g == null) ? IntPtr.Zero : g.NativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPoint(this.nativeRegion, x, y, graphics, out result));
			return result;
		}

		public bool IsVisible(float x, float y, float width, float height)
		{
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRect(this.nativeRegion, x, y, width, height, IntPtr.Zero, out result));
			return result;
		}

		public bool IsVisible(float x, float y, float width, float height, Graphics g)
		{
			IntPtr graphics = (g == null) ? IntPtr.Zero : g.NativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRect(this.nativeRegion, x, y, width, height, graphics, out result));
			return result;
		}

		public bool IsEmpty(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsEmptyRegion(this.nativeRegion, g.NativeObject, out result));
			return result;
		}

		public bool IsInfinite(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsInfiniteRegion(this.nativeRegion, g.NativeObject, out result));
			return result;
		}

		public void MakeEmpty()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetEmpty(this.nativeRegion));
		}

		public void MakeInfinite()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetInfinite(this.nativeRegion));
		}

		public bool Equals(Region region, Graphics g)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsEqualRegion(this.nativeRegion, region.NativeObject, g.NativeObject, out result));
			return result;
		}

		public static Region FromHrgn(IntPtr hrgn)
		{
			if (hrgn == IntPtr.Zero)
			{
				throw new ArgumentException("hrgn");
			}
			IntPtr native;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionHrgn(hrgn, out native));
			return new Region(native);
		}

		public IntPtr GetHrgn(Graphics g)
		{
			if (g == null)
			{
				return this.nativeRegion;
			}
			IntPtr zero = IntPtr.Zero;
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionHRgn(this.nativeRegion, g.NativeObject, ref zero));
			return zero;
		}

		public RegionData GetRegionData()
		{
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionDataSize(this.nativeRegion, out num));
			byte[] array = new byte[num];
			int num2;
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionData(this.nativeRegion, array, num, out num2));
			return new RegionData
			{
				Data = array
			};
		}

		public RectangleF[] GetRegionScans(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionScansCount(this.nativeRegion, out num, matrix.NativeObject));
			if (num == 0)
			{
				return new RectangleF[0];
			}
			RectangleF[] array = new RectangleF[num];
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(array[0]) * num);
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetRegionScans(this.nativeRegion, intPtr, out num, matrix.NativeObject));
			}
			finally
			{
				GDIPlus.FromUnManagedMemoryToRectangles(intPtr, array);
			}
			return array;
		}

		public void Transform(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipTransformRegion(this.nativeRegion, matrix.NativeObject));
		}

		public Region Clone()
		{
			IntPtr native;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneRegion(this.nativeRegion, out native));
			return new Region(native);
		}

		public void Dispose()
		{
			this.DisposeHandle();
			GC.SuppressFinalize(this);
		}

		private void DisposeHandle()
		{
			if (this.nativeRegion != IntPtr.Zero)
			{
				GDIPlus.GdipDeleteRegion(this.nativeRegion);
				this.nativeRegion = IntPtr.Zero;
			}
		}

		~Region()
		{
			this.DisposeHandle();
		}

		public void ReleaseHrgn(IntPtr regionHandle)
		{
			if (regionHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("regionHandle");
			}
			Status status = Status.Ok;
			if (GDIPlus.RunningOnUnix())
			{
				status = GDIPlus.GdipDeleteRegion(regionHandle);
			}
			else if (!GDIPlus.DeleteObject(regionHandle))
			{
				status = Status.InvalidParameter;
			}
			GDIPlus.CheckStatus(status);
		}
	}
}
