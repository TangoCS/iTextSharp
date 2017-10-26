using System;
using System.ComponentModel;
using iTextSharp.Drawing.Design;

namespace iTextSharp.Drawing
{
	[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design", typeof(UITypeEditor))]
	public enum ContentAlignment
	{
		TopLeft = 1,
		TopCenter,
		TopRight = 4,
		MiddleLeft = 16,
		MiddleCenter = 32,
		MiddleRight = 64,
		BottomLeft = 256,
		BottomCenter = 512,
		BottomRight = 1024
	}
}
