using System;

namespace iTextSharp.Drawing.Design
{
	public class PropertyValueUIItem
	{
		private Image uiItemImage;

		private PropertyValueUIItemInvokeHandler handler;

		private string tooltip;

		public virtual Image Image
		{
			get
			{
				return this.uiItemImage;
			}
		}

		public virtual PropertyValueUIItemInvokeHandler InvokeHandler
		{
			get
			{
				return this.handler;
			}
		}

		public virtual string ToolTip
		{
			get
			{
				return this.tooltip;
			}
		}

		public PropertyValueUIItem(Image uiItemImage, PropertyValueUIItemInvokeHandler handler, string tooltip)
		{
			if (uiItemImage == null)
			{
				throw new ArgumentNullException("uiItemImage");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.uiItemImage = uiItemImage;
			this.handler = handler;
			this.tooltip = tooltip;
		}

		public virtual void Reset()
		{
		}
	}
}
