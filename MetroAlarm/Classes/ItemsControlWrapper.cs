using System;
using System.Windows;
using System.Windows.Controls;

namespace MetroAlarm.Classes
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
			return new Controls.AlarmView();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return (item is Controls.AlarmView);
		}

		protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
			base.PrepareContainerForItemOverride(element, item);

			((Controls.AlarmView)element).RemoveAlarm += new EventHandler<AlarmArgs>(view_RemoveItem);
		}

		void view_RemoveItem(object sender, AlarmArgs e)
		{
			if (RemoveAlarm != null)
				RemoveAlarm(sender, e);
		}
	}
}
