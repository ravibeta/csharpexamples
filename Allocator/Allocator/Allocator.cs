using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Allocator
{
    public class Allocator
    {
        public const int LIMIT = 10; // 10 stands for 64K
        public Allocator()
        {
            Pages = new List<int> { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90 }; // contiguous memory equivalent to 640K
            FreeListSmall = new Queue<int>();
            FreeListLarge = new Queue<int>();
            threshold = 5; // 5 stands for 32K
            small = 0;
            large = 20; // first two pages are reserved for allocating smalls
        }

        public int Alloc(int size)
        {
            if (size > LIMIT) return -1;
            if (size < threshold)
            {
                if (FreeListSmall.Count() > 0)
                    return FreeListSmall.Dequeue();
                int index = small;
                if (index >= 20) return -1; // out of space
                small += 1; // new
                return index;
            }
            else
            {
                if (FreeListLarge.Count() > 0)
                    return FreeListLarge.Dequeue();
                int index = large;
                if (index >= 100) return -1; // out of space
                large += 10; // new
                return index;
            }
        }

        public void Free(int index)
        {
            if (index >= 0 && index < 100) // validation (use reference count)
            {
                if (index < 20)
                    FreeListSmall.Enqueue(index);
                else
                    FreeListLarge.Enqueue(index);
            }            
        }
        internal int small {get; set;} // index to the next available small
        internal int large {get; set;} // index to the next available large
        internal List<int> Pages {get; set;}
        internal Queue<int> FreeListSmall {get; set;}
        internal Queue<int> FreeListLarge {get; set;}
        internal int threshold {get; set;}
    }
    
}
