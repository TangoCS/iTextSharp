using System;

namespace iTextSharp.Drawing
{
	public sealed class BufferedGraphicsManager
	{
		private static BufferedGraphicsContext graphics_context;

		public static BufferedGraphicsContext Current
		{
			get
			{
				return BufferedGraphicsManager.graphics_context;
			}
		}

		static BufferedGraphicsManager()
		{
			BufferedGraphicsManager.graphics_context = new BufferedGraphicsContext();
		}

		private BufferedGraphicsManager()
		{
		}
	}
}
