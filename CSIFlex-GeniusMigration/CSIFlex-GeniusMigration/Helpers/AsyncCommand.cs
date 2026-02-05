using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CSIFlex_GeniusMigration.Helpers
{
	public class AsyncCommand: IAsyncCommand
	{
		public event EventHandler CanExecuteChanged;

		private bool isExecuting;
		private readonly Func<Task> execute;
		private readonly Func<bool> canExecute;
		private readonly IErrorHandler errorHandler;

		public AsyncCommand(
			Func<Task> execute,
			Func<bool> canExecute = null,
			IErrorHandler errorHandler = null)
		{
			this.execute = execute;
			this.canExecute = canExecute;
			this.errorHandler = errorHandler;
		}

		public bool CanExecute()
		{
			return !isExecuting && (canExecute?.Invoke() ?? true);
		}

		public async Task ExecuteAsync()
		{
			if (CanExecute())
			{
				try
				{
					isExecuting = true;
					await execute();
				}
				finally
				{
					isExecuting = false;
				}
			}

			RaiseCanExecuteChanged();
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		#region Explicit implementations
		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute();
		}

		void ICommand.Execute(object parameter)
		{
			ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
		}
		#endregion
	}
}
