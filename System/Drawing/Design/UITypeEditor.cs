using System;
using System.Collections;
using System.ComponentModel;

namespace iTextSharp.Drawing.Design
{
	public class UITypeEditor
	{
		public virtual bool IsDropDownResizable
		{
			get
			{
				return false;
			}
		}

		static UITypeEditor()
		{
			Hashtable hashtable = new Hashtable();
			hashtable[typeof(Array)] = "System.ComponentModel.Design.ArrayEditor, System.Design";
			hashtable[typeof(byte[])] = "System.ComponentModel.Design.BinaryEditor, System.Design";
			hashtable[typeof(DateTime)] = "System.ComponentModel.Design.DateTimeEditor, System.Design";
			hashtable[typeof(IList)] = "System.ComponentModel.Design.CollectionEditor, System.Design";
			hashtable[typeof(ICollection)] = "System.ComponentModel.Design.CollectionEditor, System.Design";
			hashtable[typeof(string[])] = "System.Windows.Forms.Design.StringArrayEditor, System.Design";
			TypeDescriptor.AddEditorTable(typeof(UITypeEditor), hashtable);
		}

		public virtual object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			return value;
		}

		public object EditValue(IServiceProvider provider, object value)
		{
			return this.EditValue(null, provider, value);
		}

		public virtual UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.None;
		}

		public UITypeEditorEditStyle GetEditStyle()
		{
			return this.GetEditStyle(null);
		}

		public bool GetPaintValueSupported()
		{
			return this.GetPaintValueSupported(null);
		}

		public virtual bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		public void PaintValue(object value, Graphics canvas, Rectangle rectangle)
		{
			this.PaintValue(new PaintValueEventArgs(null, value, canvas, rectangle));
		}

		public virtual void PaintValue(PaintValueEventArgs e)
		{
		}
	}
}
