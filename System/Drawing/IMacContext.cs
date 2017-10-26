using System;

namespace iTextSharp.Drawing
{
	internal interface IMacContext
	{
		void Synchronize();

		void Release();
	}
}
