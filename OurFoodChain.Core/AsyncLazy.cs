using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace OurFoodChain {

    // This class was implemented with help from this article: https://devblogs.microsoft.com/pfxteam/asynclazyt/ (Stephen Toub)

    public class AsyncLazy<T> :
        Lazy<Task<T>> {

        // Public members

        public AsyncLazy(Func<T> valueFactory) :
            this(valueFactory, isThreadSafe: true) {
        }
        public AsyncLazy(Func<T> valueFactory, bool isThreadSafe) :
            base(() => Task.Run(valueFactory), isThreadSafe) {
        }
        public AsyncLazy(Func<T> valueFactory, LazyThreadSafetyMode mode) :
            base(() => Task.Run(valueFactory), mode) {
        }
        public AsyncLazy(Func<Task<T>> taskFactory) :
            this(taskFactory, isThreadSafe: true) {
        }
        public AsyncLazy(Func<Task<T>> taskFactory, bool isThreadSafe) :
            base(() => Task.Run(() => taskFactory()), isThreadSafe) {
        }
        public AsyncLazy(Func<Task<T>> taskFactory, LazyThreadSafetyMode mode) :
            base(() => Task.Run(() => taskFactory()), mode) {
        }

        public TaskAwaiter<T> GetAwaiter() {

            return Value.GetAwaiter();

        }

    }

}