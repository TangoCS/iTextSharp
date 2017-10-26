using System;

namespace iTextSharp.Drawing.Printing
{
	[Serializable]
	public class PaperSource
	{
		private PaperSourceKind kind;

		private string source_name;

		internal bool is_default;

		public PaperSourceKind Kind
		{
			get
			{
				if (this.kind >= (PaperSourceKind)256)
				{
					return PaperSourceKind.Custom;
				}
				return this.kind;
			}
		}

		public string SourceName
		{
			get
			{
				return this.source_name;
			}
			set
			{
				this.source_name = value;
			}
		}

		public int RawKind
		{
			get
			{
				return (int)this.kind;
			}
			set
			{
				this.kind = (PaperSourceKind)value;
			}
		}

		internal bool IsDefault
		{
			get
			{
				return this.is_default;
			}
			set
			{
				this.is_default = value;
			}
		}

		public PaperSource()
		{
		}

		internal PaperSource(string sourceName, PaperSourceKind kind)
		{
			this.source_name = sourceName;
			this.kind = kind;
		}

		internal PaperSource(string sourceName, PaperSourceKind kind, bool isDefault)
		{
			this.source_name = sourceName;
			this.kind = kind;
			this.is_default = this.IsDefault;
		}

		public override string ToString()
		{
			return string.Format("[PaperSource {0} Kind={1}]", this.SourceName, this.Kind);
		}
	}
}
