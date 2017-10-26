using System;
using iTextSharp.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace iTextSharp.Drawing
{
	internal struct GdipPropertyItem
	{
		internal int id;

		internal int len;

		internal short type;

		internal IntPtr value;

		internal static void MarshalTo(GdipPropertyItem gdipProp, PropertyItem prop)
		{
			prop.Id = gdipProp.id;
			prop.Len = gdipProp.len;
			prop.Type = gdipProp.type;
			prop.Value = new byte[gdipProp.len];
			Marshal.Copy(gdipProp.value, prop.Value, 0, gdipProp.len);
		}
	}
}
