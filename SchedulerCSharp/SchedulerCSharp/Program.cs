using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchedulerCSharp
{
    class Program
    {
        static void Main()
        {
            TimeBasedTaskScheduler lcts = new TimeBasedTaskScheduler(1);
            Action action = () =>
            {
                    Console.WriteLine("Run on thread {0}", Thread.CurrentThread.ManagedThreadId);
            };
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(2000);
                MyTask t = new MyTask(action);
                tasks.Add(t);
            }
            tasks.ForEach(x => { x.Start(lcts); Thread.Sleep(1000); });
            tasks.ForEach(x => x.Wait());
            Console.ReadKey();
        }
    }

    public class MyTask : Task
    {
        public DateTime RunAt = DateTime.Now;
        public MyTask(Action action) : base(action) { }
    }

    /// <summary>
    /// Provides a task scheduler that executes task at specified datetime
    /// </summary>
    public class TimeBasedTaskScheduler : TaskScheduler
    {
        /// <summary>Whether the current thread is processing work items.</summary>
        [ThreadStatic]
        private static bool _currentThreadIsProcessingItems;
        /// <summary>The list of tasks to be executed.</summary>
        private readonly LinkedList<Task> _tasks = new LinkedList<Task>(); // protected by lock(_tasks)
        /// <summary>The maximum concurrency level allowed by this scheduler.</summary>
        private readonly int _maxDegreeOfParallelism;
        /// <summary>Whether the scheduler is currently processing work items.</summary>
        private int _delegatesQueuedOrRunning = 0; // protected by lock(_tasks)
        private static AutoResetEvent itemEvent = new AutoResetEvent(false);
        private Timer timer;

        /// <summary>
        /// Initializes an instance of the TimeBasedTaskScheduler class with the
        /// specified degree of parallelism.
        /// </summary>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism provided by this scheduler.</param>
        public TimeBasedTaskScheduler(int maxDegreeOfParallelism)
        {
            if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException("maxDegreeOfParallelism");
            _maxDegreeOfParallelism = maxDegreeOfParallelism;
            timer = new Timer((TimerCallback)CheckPendingWork, null, 0, 1000);
            ++_delegatesQueuedOrRunning;
        }

        /// <summary>Queues a task to the scheduler.</summary>
        /// <param name="task">The task to be queued.</param>
        protected sealed override void QueueTask(Task task)
        {
            lock (_tasks)
            {
                var runAt = (task as MyTask).RunAt;
                foreach ( var t in _tasks)
                    if ((t as MyTask).RunAt > runAt)
                        _tasks.AddBefore(_tasks.Find(t), task);
                if(_tasks.Find(task) == null)
                    _tasks.AddLast(task);
                var timeleft = runAt - DateTime.Now;
                if (_tasks.First.Value == task)
                    timer.Change(timeleft.Milliseconds > 0 ? timeleft.Milliseconds : 0, 1000);
            }
        }

        /// <summary>
        /// Informs the ThreadPool that there's work to be executed for this scheduler.
        /// </summary>
        private void CheckPendingWork(Object stateInfo)
        {
            Console.WriteLine("Timer fired");

            // Note that the current thread is now processing work items.
            // This is necessary to enable inlining of tasks into this thread.
            _currentThreadIsProcessingItems = true;
            try
            {
                // Process all available items in the queue.
                while (true)
                {
                    Task item;
                    lock (_tasks)
                    {
                        // When there are no more items to be processed,
                        // note that we're done processing, and get out.
                        if (_tasks.Count == 0)
                            break;

                        // Get the next item from the queue
                        item = _tasks.First.Value;
                        var runAt = (item as MyTask).RunAt;
                        if ( runAt < DateTime.Now)
                        {
                            Console.WriteLine("Task to RunAt {0} executed at {1}", runAt, DateTime.Now);
                            _tasks.RemoveFirst();
                        }
                        else
                            break;
                    }

                    // Execute the task we pulled out of the queue
                    Console.WriteLine("Item found");
                    base.TryExecuteTask(item);
                }
            }
            // We're done processing items on the current thread
            finally { _currentThreadIsProcessingItems = false; }
        }

        private void CheckStatus(Object stateInfo)
        {
        }
        /// <summary>Attempts to execute the specified task on the current thread.</summary>
        /// <param name="task">The task to be executed.</param>
        /// <param name="taskWasPreviouslyQueued"></param>
        /// <returns>Whether the task could be executed on the current thread.</returns>
        protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            // If this thread isn't already processing a task, we don't support inlining
            if (!_currentThreadIsProcessingItems) return false;

            // If the task was previously queued, remove it from the queue
            if (taskWasPreviouslyQueued) TryDequeue(task);

            // Try to run the task.
            return base.TryExecuteTask(task);
        }

        /// <summary>Attempts to remove a previously scheduled task from the scheduler.</summary>
        /// <param name="task">The task to be removed.</param>
        /// <returns>Whether the task could be found and removed.</returns>
        protected sealed override bool TryDequeue(Task task)
        {
            lock (_tasks) return _tasks.Remove(task);
        }

        /// <summary>Gets the maximum concurrency level supported by this scheduler.</summary>
        public sealed override int MaximumConcurrencyLevel { get { return _maxDegreeOfParallelism; } }

        /// <summary>Gets an enumerable of the tasks currently scheduled on this scheduler.</summary>
        /// <returns>An enumerable of the tasks currently scheduled.</returns>
        protected sealed override IEnumerable<Task> GetScheduledTasks()
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(_tasks, ref lockTaken);
                if (lockTaken) return _tasks.ToArray();
                else throw new NotSupportedException();
            }
            finally
            {
                if (lockTaken) Monitor.Exit(_tasks);
            }
        }
    }
}
