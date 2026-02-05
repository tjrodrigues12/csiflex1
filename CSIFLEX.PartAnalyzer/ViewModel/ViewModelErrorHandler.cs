using CSIFLEX.Library.Commands;
using System;
namespace CSIFLEX.PartAnalyzer.ViewModel
{
    public class ViewModelErrorHandler : IErrorHandler
    {
        public Action<Exception> errorHandler;
        public ViewModelErrorHandler(Action<Exception> errorHandler)
        {
            this.errorHandler = errorHandler;
        }
        public void HandleError(Exception ex)
        {
            errorHandler ?.Invoke (ex);
        }
    }
}
