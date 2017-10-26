using System;
using iTextSharp.Drawing.Imaging;

namespace iTextSharp.Drawing
{
	internal struct GdipEncoderParameter
	{
		internal Guid guid;

		internal uint numberOfValues;

		internal EncoderParameterValueType type;

		internal IntPtr value;
	}
}
