using System.Reactive.Linq; 
using System.Windows.Threading;

namespace System
{
    public static class ObservableExtensions
    {
        public static IObservable<bool> TupleEquals<T>(this IObservable<(T t1,T t2)> @this, Dispatcher dispatcher = null)
        {
            if(dispatcher == null)
            {
                return @this.Select(input => input.t1.Equals(input.t2));
            }
            else
            {
                return @this.Select(input => input.t1.Equals(input.t2))
                        .ObserveOn(dispatcher);
            }
        }

        public static IObservable<bool> TupleNotEquals<T>(this IObservable<(T t1, T t2)> @this, Dispatcher dispatcher = null)
        {
            return @this.TupleEquals(dispatcher)
                .Select(x=> !x);
        }
    }
}
