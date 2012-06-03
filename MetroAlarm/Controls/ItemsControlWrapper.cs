using System;
using System.Windows;
using System.Windows.Controls;

namespace MetroAlarm.Controls
{
	public class ItemsControlWrapper: ItemsControl
	{
		//public event Classes.AlarmEvent RemoveAlarm;
		public event EventHandler<Classes.AlarmArgs> RemoveAlarm;

		public ItemsControlWrapper() : base()
		{
			
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			//base.GetContainerForItemOverride();
			return new AlarmView();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return (item is AlarmView);
		}

		protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
			base.PrepareContainerForItemOverride(element, item);

			((AlarmView)element).RemoveAlarm += new EventHandler<Classes.AlarmArgs>(view_RemoveItem);
		}

		void view_RemoveItem(object sender, Classes.AlarmArgs e)
		{
			if (RemoveAlarm != null)
				RemoveAlarm(sender, e);
		}
	}
}
