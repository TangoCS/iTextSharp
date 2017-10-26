using System;

namespace iTextSharp.Drawing.Printing
{
	public class QueryPageSettingsEventArgs : PrintEventArgs
	{
		private PageSettings pageSettings;

		public PageSettings PageSettings
		{
			get
			{
				return this.pageSettings;
			}
			set
			{
				this.pageSettings = value;
			}
		}

		public QueryPageSettingsEventArgs(PageSettings pageSettings)
		{
			this.pageSettings = pageSettings;
		}
	}
}
