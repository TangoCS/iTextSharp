using System;

namespace iTextSharp.Drawing
{
	public interface IDeviceContext : IDisposable
	{
		IntPtr GetHdc();

		void ReleaseHdc();
	}
}
