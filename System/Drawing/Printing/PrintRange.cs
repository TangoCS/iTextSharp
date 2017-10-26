using System;

namespace iTextSharp.Drawing.Printing
{
	[Serializable]
	public enum PrintRange
	{
		AllPages,
		Selection,
		SomePages,
		CurrentPage = 4194304
	}
}
