using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace MemUI.Strategy
{
    class FirstFit
    {
        private List<int> remaining;
        private List<int> memoryAlloc;
        private List<int> memo = new List<int>();
        private List<int> holes = new List<int>();
        private int RAM = 0;
        private int memorySum = 0, hole = 0;

        public void ffConstructor(List<int> jSize, List<int> jTime, double memory) // Constructor that is being updated every TU
        {
            memoryAlloc = new List<int>(jSize);
            remaining = new List<int>(jTime);
            RAM = Convert.ToInt32(memory);
            hole = 0;
        }

        public List<int> ffMemory(int inc)
        {
            if(memoryAlloc[inc] <= RAM)
            {
                memo.Add(memoryAlloc[inc]);
                RAM -= memoryAlloc[inc];
                memorySum = memo.Sum();
                memoryAlloc.RemoveAt(inc);
            }
            else
            {
                hole = memorySum - RAM;
            }

            return memo;
        }

        public double memoryLeft()
        {
            return RAM;
        }

        public List<int> ffRemaining() // remaining time units
        {
            for(int i = 0; i < remaining.Count(); i++)
            {
                remaining[i] -= 1;
            }
            return remaining;
        }

    }
}
