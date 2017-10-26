using System;
using System.Collections;

namespace iTextSharp.Drawing.Design
{
	public sealed class CategoryNameCollection : ReadOnlyCollectionBase
	{
		public string this[int index]
		{
			get
			{
				return (string)base.InnerList[index];
			}
		}

		public CategoryNameCollection(CategoryNameCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.InnerList.AddRange(value);
		}

		public CategoryNameCollection(string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.InnerList.AddRange(value);
		}

		public bool Contains(string value)
		{
			return base.InnerList.Contains(value);
		}

		public void CopyTo(string[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		public int IndexOf(string value)
		{
			return base.InnerList.IndexOf(value);
		}
	}
}
