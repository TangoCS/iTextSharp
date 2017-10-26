using System;
using System.ComponentModel;

namespace iTextSharp.Drawing
{
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		private bool isReplaced;

		public override string Description
		{
			get
			{
				if (!this.isReplaced)
				{
					this.isReplaced = true;
					base.DescriptionValue = Locale.GetText(base.DescriptionValue);
				}
				return base.DescriptionValue;
			}
		}

		public SRDescriptionAttribute(string description) : base(description)
		{
		}
	}
}
