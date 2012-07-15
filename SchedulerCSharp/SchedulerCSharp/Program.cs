using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.Threading.Tasks;

namespace SchedulerCSharp
{
//Scheduler Algorithms
//1) uniprocessor non-preemptive scheduler has no breaks
//2) synchronized (prioritized) queue of non-blocking tasks
//3) a thread pool manager with an event or mutex for each thread or monitor
//4) task or time parallelization
//5) timeslicing vs timesharing
    public sealed class Scheduler
    {
        private SortedList<int, MyTask> tasks;
        List<Task> threads;
        public void Add(MyTask t)
        {
            lock (tasks)
            {
                tasks[t.Id] = t;
            }
            Run();
        }

        public Scheduler()
        {
            tasks = new SortedList<int,MyTask>();
            threads = new List<Task>();
        }

        ~Scheduler()
        {
            for (int i = 0; i < threads.Count; i++)
                // if(threads[i].IsCompleted == false)
                    threads[i].Wait();
        }

        private void Run()
        {
            Action<object> action = (object arg) =>
            {
                var tasks = arg as SortedList<int, MyTask>;
                
                while (tasks.Count != 0)
                {
                    MyTask t;
                    lock (tasks)
                    {
                        t = tasks.ElementAt(0).Value as MyTask;
                        tasks.RemoveAt(0);
                    }
                    t.Run();
                }
            };
            threads.Add(Task.Factory.StartNew(action, this.tasks as object));
        }
    }

    public class MyTask
    {
        public int Id {get; private set;}
        public void Run() { Console.Write("{0}", Id); }
        public MyTask(int id)
        {
            Id = id;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var s = new Scheduler();
            for (int i = 0; i < 10; i++)
            {
                var task = new MyTask(i);
                s.Add(task);
            }
            Console.ReadKey();

        }
    }
}
