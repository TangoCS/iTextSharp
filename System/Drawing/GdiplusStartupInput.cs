using System;

namespace iTextSharp.Drawing
{
	internal struct GdiplusStartupInput
	{
		internal uint GdiplusVersion;

		internal IntPtr DebugEventCallback;

		internal int SuppressBackgroundThread;

		internal int SuppressExternalCodecs;

		internal static GdiplusStartupInput MakeGdiplusStartupInput()
		{
			return new GdiplusStartupInput
			{
				GdiplusVersion = 1u,
				DebugEventCallback = IntPtr.Zero,
				SuppressBackgroundThread = 0,
				SuppressExternalCodecs = 0
			};
		}
	}
}
