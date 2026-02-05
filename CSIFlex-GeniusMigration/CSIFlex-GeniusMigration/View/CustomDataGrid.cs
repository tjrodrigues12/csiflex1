using System;
using System.Collections; 
using System.Reactive.Linq; 
using System.Windows;
using System.Windows.Controls;

namespace CSIFlex_GeniusMigration.View
{
	public class CustomDataGrid	: DataGrid
	{

		private IObservable<SelectionChangedEventArgs> selectedChangedEventArgs;

		public CustomDataGrid()
		{
			selectedChangedEventArgs = Observable.FromEvent<SelectionChangedEventHandler, SelectionChangedEventArgs>(
					conversion: h => (sender, args) => h(args),
					handler =>  this.SelectionChanged += handler,
					handler => this.SelectionChanged -= handler
				);

			selectedChangedEventArgs.Subscribe(x => CustomDataGrid_SelectionChanged(x)); 
		}

		void CustomDataGrid_SelectionChanged(SelectionChangedEventArgs e)
		{
			SelectedItemsList = SelectedItems;
		}

		public IList SelectedItemsList
		{
			get { return (IList)GetValue(SelectedItemsListProperty); }
			set { SetValue(SelectedItemsListProperty, value); }
		}

		public static readonly DependencyProperty SelectedItemsListProperty = DependencyProperty.Register(
			nameof(SelectedItemsList), 
			typeof(IList),
			typeof(CustomDataGrid), 
			new PropertyMetadata(null)
		); 
	}

}
