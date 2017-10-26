using System;
using System.Runtime.InteropServices;

namespace iTextSharp.Drawing.Imaging
{
	public sealed class ColorPalette
	{
		private int flags;

		private Color[] entries;

		public Color[] Entries
		{
			get
			{
				return this.entries;
			}
		}

		public int Flags
		{
			get
			{
				return this.flags;
			}
		}

		internal ColorPalette()
		{
			this.entries = new Color[0];
		}

		internal ColorPalette(int flags, Color[] colors)
		{
			this.flags = flags;
			this.entries = colors;
		}

		internal IntPtr getGDIPalette()
		{
			GdiColorPalette gdiColorPalette = default(GdiColorPalette);
			Color[] array = this.Entries;
			int num = 0;
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(gdiColorPalette) + Marshal.SizeOf(num) * array.Length);
			gdiColorPalette.Flags = this.Flags;
			gdiColorPalette.Count = array.Length;
			int[] array2 = new int[gdiColorPalette.Count];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = array[i].ToArgb();
			}
			Marshal.StructureToPtr(gdiColorPalette, intPtr, false);
			Marshal.Copy(array2, 0, (IntPtr)(intPtr.ToInt64() + (long)Marshal.SizeOf(gdiColorPalette)), array2.Length);
			return intPtr;
		}

		internal void setFromGDIPalette(IntPtr palette)
		{
			IntPtr intPtr = palette;
			this.flags = Marshal.ReadInt32(intPtr);
			intPtr = (IntPtr)(intPtr.ToInt64() + 4L);
			int num = Marshal.ReadInt32(intPtr);
			intPtr = (IntPtr)(intPtr.ToInt64() + 4L);
			this.entries = new Color[num];
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				int num3 = Marshal.ReadInt32(intPtr, num2);
				this.entries[i] = Color.FromArgb(num3);
				num2 += 4;
			}
		}
	}
}
