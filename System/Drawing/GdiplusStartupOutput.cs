using System;

namespace iTextSharp.Drawing
{
	internal struct GdiplusStartupOutput
	{
		internal IntPtr NotificationHook;

		internal IntPtr NotificationUnhook;

		internal static GdiplusStartupOutput MakeGdiplusStartupOutput()
		{
			GdiplusStartupOutput result = default(GdiplusStartupOutput);
			result.NotificationHook = (result.NotificationUnhook = IntPtr.Zero);
			return result;
		}
	}
}
