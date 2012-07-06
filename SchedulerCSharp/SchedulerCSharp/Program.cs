using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace SchedulerCSharp
{
    public sealed class Scheduler
    {
        private Queue tasks;
        public void Add(Task t)
        {
            tasks.Enqueue(t);
        }

        public Scheduler()
        {
            tasks = Queue.Synchronized(new Queue());
        }

        public void Run()
        {
            while (tasks.Count != 0)
            {
                Task t = tasks.Dequeue() as Task;
                t.Run();
            }
        }
    }

    public class Task
    {
        int Id {get; set;}
        public void Run() { Console.Write("{0}", Id); }
        public Task(int id)
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
                var task = new Task(i);
                s.Add(task);
            }
            s.Run();
        }
    }
}
