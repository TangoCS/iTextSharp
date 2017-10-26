using System;

namespace iTextSharp.Drawing
{
	public struct CharacterRange
	{
		private int first;

		private int length;

		public int First
		{
			get
			{
				return this.first;
			}
			set
			{
				this.first = value;
			}
		}

		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		public CharacterRange(int First, int Length)
		{
			this.first = First;
			this.length = Length;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is CharacterRange))
			{
				return false;
			}
			CharacterRange cr = (CharacterRange)obj;
			return this == cr;
		}

		public override int GetHashCode()
		{
			return this.first ^ this.length;
		}

		public static bool operator ==(CharacterRange cr1, CharacterRange cr2)
		{
			return cr1.first == cr2.first && cr1.length == cr2.length;
		}

		public static bool operator !=(CharacterRange cr1, CharacterRange cr2)
		{
			return cr1.first != cr2.first || cr1.length != cr2.length;
		}
	}
}
