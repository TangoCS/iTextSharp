using System;
using iTextSharp.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace iTextSharp.Drawing.Imaging
{
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ImageAttributes : ICloneable, IDisposable
	{
		private IntPtr nativeImageAttr;

		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeImageAttr;
			}
		}

		internal ImageAttributes(IntPtr native)
		{
			this.nativeImageAttr = native;
		}

		public ImageAttributes()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateImageAttributes(out this.nativeImageAttr));
		}

		public void ClearBrushRemapTable()
		{
			this.ClearRemapTable(ColorAdjustType.Brush);
		}

		public void ClearColorKey()
		{
			this.ClearColorKey(ColorAdjustType.Default);
		}

		public void ClearColorKey(ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesColorKeys(this.nativeImageAttr, type, false, 0, 0));
		}

		public void ClearColorMatrix()
		{
			this.ClearColorMatrix(ColorAdjustType.Default);
		}

		public void ClearColorMatrix(ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesColorMatrix(this.nativeImageAttr, type, false, IntPtr.Zero, IntPtr.Zero, ColorMatrixFlag.Default));
		}

		public void ClearGamma()
		{
			this.ClearGamma(ColorAdjustType.Default);
		}

		public void ClearGamma(ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesGamma(this.nativeImageAttr, type, false, 0f));
		}

		public void ClearNoOp()
		{
			this.ClearNoOp(ColorAdjustType.Default);
		}

		public void ClearNoOp(ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesNoOp(this.nativeImageAttr, type, false));
		}

		public void ClearOutputChannel()
		{
			this.ClearOutputChannel(ColorAdjustType.Default);
		}

		public void ClearOutputChannel(ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesOutputChannel(this.nativeImageAttr, type, false, ColorChannelFlag.ColorChannelLast));
		}

		public void ClearOutputChannelColorProfile()
		{
			this.ClearOutputChannelColorProfile(ColorAdjustType.Default);
		}

		public void ClearOutputChannelColorProfile(ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesOutputChannelColorProfile(this.nativeImageAttr, type, false, null));
		}

		public void ClearRemapTable()
		{
			this.ClearRemapTable(ColorAdjustType.Default);
		}

		public void ClearRemapTable(ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesRemapTable(this.nativeImageAttr, type, false, 0u, IntPtr.Zero));
		}

		public void ClearThreshold()
		{
			this.ClearThreshold(ColorAdjustType.Default);
		}

		public void ClearThreshold(ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesThreshold(this.nativeImageAttr, type, false, 0f));
		}

		public void SetColorKey(Color colorLow, Color colorHigh)
		{
			this.SetColorKey(colorLow, colorHigh, ColorAdjustType.Default);
		}

		public void SetColorMatrix(ColorMatrix newColorMatrix)
		{
			this.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
		}

		public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag flags)
		{
			this.SetColorMatrix(newColorMatrix, flags, ColorAdjustType.Default);
		}

		public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag mode, ColorAdjustType type)
		{
			IntPtr intPtr = ColorMatrix.Alloc(newColorMatrix);
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesColorMatrix(this.nativeImageAttr, type, true, intPtr, IntPtr.Zero, mode));
			}
			finally
			{
				ColorMatrix.Free(intPtr);
			}
		}

		public void Dispose()
		{
			if (this.nativeImageAttr != IntPtr.Zero)
			{
				Status arg_28_0 = GDIPlus.GdipDisposeImageAttributes(this.nativeImageAttr);
				this.nativeImageAttr = IntPtr.Zero;
				GDIPlus.CheckStatus(arg_28_0);
			}
			GC.SuppressFinalize(this);
		}

		~ImageAttributes()
		{
			this.Dispose();
		}

		public object Clone()
		{
			IntPtr native;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneImageAttributes(this.nativeImageAttr, out native));
			return new ImageAttributes(native);
		}

		[MonoTODO("Not supported by libgdiplus")]
		public void GetAdjustedPalette(ColorPalette palette, ColorAdjustType type)
		{
			IntPtr gDIPalette = palette.getGDIPalette();
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageAttributesAdjustedPalette(this.nativeImageAttr, gDIPalette, type));
				palette.setFromGDIPalette(gDIPalette);
			}
			finally
			{
				if (gDIPalette != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(gDIPalette);
				}
			}
		}

		public void SetBrushRemapTable(ColorMap[] map)
		{
			GdiColorMap gdiColorMap = default(GdiColorMap);
			int num = Marshal.SizeOf(gdiColorMap);
			IntPtr intPtr2;
			IntPtr intPtr = intPtr2 = Marshal.AllocHGlobal(num * map.Length);
			try
			{
				for (int i = 0; i < map.Length; i++)
				{
					gdiColorMap.from = map[i].OldColor.ToArgb();
					gdiColorMap.to = map[i].NewColor.ToArgb();
					Marshal.StructureToPtr(gdiColorMap, intPtr, false);
					intPtr = (IntPtr)(intPtr.ToInt64() + (long)num);
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesRemapTable(this.nativeImageAttr, ColorAdjustType.Brush, true, (uint)map.Length, intPtr2));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr2);
			}
		}

		public void SetColorKey(Color colorLow, Color colorHigh, ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesColorKeys(this.nativeImageAttr, type, true, colorLow.ToArgb(), colorHigh.ToArgb()));
		}

		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix)
		{
			this.SetColorMatrices(newColorMatrix, grayMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
		}

		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag flags)
		{
			this.SetColorMatrices(newColorMatrix, grayMatrix, flags, ColorAdjustType.Default);
		}

		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag mode, ColorAdjustType type)
		{
			IntPtr intPtr = ColorMatrix.Alloc(newColorMatrix);
			Status status;
			try
			{
				if (grayMatrix == null)
				{
					status = GDIPlus.GdipSetImageAttributesColorMatrix(this.nativeImageAttr, type, true, intPtr, IntPtr.Zero, mode);
				}
				else
				{
					IntPtr intPtr2 = ColorMatrix.Alloc(grayMatrix);
					try
					{
						status = GDIPlus.GdipSetImageAttributesColorMatrix(this.nativeImageAttr, type, true, intPtr, intPtr2, mode);
					}
					finally
					{
						ColorMatrix.Free(intPtr2);
					}
				}
			}
			finally
			{
				ColorMatrix.Free(intPtr);
			}
			GDIPlus.CheckStatus(status);
		}

		public void SetGamma(float gamma)
		{
			this.SetGamma(gamma, ColorAdjustType.Default);
		}

		public void SetGamma(float gamma, ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesGamma(this.nativeImageAttr, type, true, gamma));
		}

		public void SetNoOp()
		{
			this.SetNoOp(ColorAdjustType.Default);
		}

		public void SetNoOp(ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesNoOp(this.nativeImageAttr, type, true));
		}

		[MonoTODO("Not supported by libgdiplus")]
		public void SetOutputChannel(ColorChannelFlag flags)
		{
			this.SetOutputChannel(flags, ColorAdjustType.Default);
		}

		[MonoTODO("Not supported by libgdiplus")]
		public void SetOutputChannel(ColorChannelFlag flags, ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesOutputChannel(this.nativeImageAttr, type, true, flags));
		}

		[MonoTODO("Not supported by libgdiplus")]
		public void SetOutputChannelColorProfile(string colorProfileFilename)
		{
			this.SetOutputChannelColorProfile(colorProfileFilename, ColorAdjustType.Default);
		}

		[MonoTODO("Not supported by libgdiplus")]
		public void SetOutputChannelColorProfile(string colorProfileFilename, ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesOutputChannelColorProfile(this.nativeImageAttr, type, true, colorProfileFilename));
		}

		public void SetRemapTable(ColorMap[] map)
		{
			this.SetRemapTable(map, ColorAdjustType.Default);
		}

		public void SetRemapTable(ColorMap[] map, ColorAdjustType type)
		{
			GdiColorMap gdiColorMap = default(GdiColorMap);
			int num = Marshal.SizeOf(gdiColorMap);
			IntPtr intPtr2;
			IntPtr intPtr = intPtr2 = Marshal.AllocHGlobal(num * map.Length);
			try
			{
				for (int i = 0; i < map.Length; i++)
				{
					gdiColorMap.from = map[i].OldColor.ToArgb();
					gdiColorMap.to = map[i].NewColor.ToArgb();
					Marshal.StructureToPtr(gdiColorMap, intPtr, false);
					intPtr = (IntPtr)(intPtr.ToInt64() + (long)num);
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesRemapTable(this.nativeImageAttr, type, true, (uint)map.Length, intPtr2));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr2);
			}
		}

		[MonoTODO("Not supported by libgdiplus")]
		public void SetThreshold(float threshold)
		{
			this.SetThreshold(threshold, ColorAdjustType.Default);
		}

		[MonoTODO("Not supported by libgdiplus")]
		public void SetThreshold(float threshold, ColorAdjustType type)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesThreshold(this.nativeImageAttr, type, true, 0f));
		}

		public void SetWrapMode(WrapMode mode)
		{
			this.SetWrapMode(mode, Color.Black);
		}

		public void SetWrapMode(WrapMode mode, Color color)
		{
			this.SetWrapMode(mode, color, false);
		}

		public void SetWrapMode(WrapMode mode, Color color, bool clamp)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetImageAttributesWrapMode(this.nativeImageAttr, mode, color.ToArgb(), clamp));
		}
	}
}
