using System;
using System.ComponentModel;

namespace iTextSharp.Drawing.Design
{
	public class ToolboxComponentsCreatedEventArgs : EventArgs
	{
		private IComponent[] components;

		public IComponent[] Components
		{
			get
			{
				return this.components;
			}
		}

		public ToolboxComponentsCreatedEventArgs(IComponent[] components)
		{
			this.components = components;
		}
	}
}
