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
        private List<char> holes = new List<char>();
        private int RAM = 0;
        private int memorySum = 0, hole = 0;

        public void ffConstructor(List<int> jSize, List<int> jTime, double memory)
        {
            memoryAlloc = new List<int>(jSize);
            remaining = new List<int>(jTime);
            RAM = Convert.ToInt32(memory);
        }

        public void ffMemoryAlloc(int inc)
        {
            if (memoryAlloc[inc] <= RAM)
            {
                RAM -= memoryAlloc[inc];
                //memoryAlloc.RemoveAt(inc);

                if (memoryAlloc[inc + 1] > RAM)
                {
                    memo.Add(RAM);
                    holes.Add('h');
                    RAM -= RAM;
                }
                else
                {
                    memo.Add(memoryAlloc[inc]);
                    holes.Add('p'); // if 'p', it's part of the process. If 'h', it's a hole
                }
            }
            
        }

        public int memoryLeft()
        {
            return RAM;
        }

        public int processSize()
        {
            return memo.Count();
        }

        public List<int> ffRemaining(int timeDec) // remaining time units
        {
            remaining[timeDec] -= 1;
            return remaining;
        }

    }
}
