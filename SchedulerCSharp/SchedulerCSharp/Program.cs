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
        private Queue tasks;
        public void Add(MyTask t)
        {
            tasks.Enqueue(t);
        }

        public Scheduler()
        {
            tasks = Queue.Synchronized(new Queue());
        }

        public void Run()
        {
            Action<object> action = (object arg) =>
            {
                Queue tasks = arg as Queue;
                while(tasks.Count != 0)
                {
                    MyTask t = tasks.Dequeue() as MyTask;
                    t.Run();
                }
            };
            Task.Factory.StartNew(action, this.tasks as object).Wait();
        }
    }

    public class MyTask
    {
        int Id {get; set;}
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
            s.Run();
            Console.ReadKey();

        }
    }
}
