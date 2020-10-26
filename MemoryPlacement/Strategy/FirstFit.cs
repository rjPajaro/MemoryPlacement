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
        private List<int> positions = new List<int>();
        private List<int> memo = new List<int>();
        private List<char> holes = new List<char>();
        private int RAM = 0;
        private int memorySum = 0, hole = 0;

        public void ffConstructor(List<int> jSize, List<int> jTime, double memory)
        {
            memoryAlloc = new List<int>(jSize);
            remaining = new List<int>(jTime);
            RAM = Convert.ToInt32(memory);

            foreach (int i in memoryAlloc)
            {
                int pos = memoryAlloc.IndexOf(i);
                positions.Add(pos);
            }
        }

        public void ffMemoryAlloc(int inc)
        {
            if (memoryAlloc[0] <= RAM)
            {
                memo.Add(memoryAlloc[0]);
                holes.Add('p'); // if 'p', it's part of the process. If 'h', it's a hole
                RAM -= memoryAlloc[0];

                if (memoryAlloc[1] > RAM)
                {
                    memo.Add(RAM);
                    holes.Add('h');
                    RAM -= RAM;
                }

                memoryAlloc.RemoveAt(0); //pops the first element from the size list (first fit logic)
            }
        }

        public void checkAlloc()
        {
            int idx = holes.IndexOf('h');
            if(memoryAlloc[0] < memo[idx])
            {
                memo.Insert(idx, memoryAlloc[0]);
                memo[idx + 1] -= memo[idx];
                memoryAlloc.RemoveAt(0);
                holes.Insert(idx, 'p');
            }
        }

        public List<char> procHoles()
        {
            return holes;
        }

        public int memoryLeft()
        {
            return RAM;
        }

        public int processSize()
        {
            int countSize = 0;
            for(int i = 0; i < holes.Count(); i++)
            {
                if (holes[i] == 'p')
                    countSize++;
            }
            return countSize;
        }

        public List<int> ffRemaining(int timeDec) // remaining time units
        {
            remaining[timeDec] -= 1;
            return remaining;
        }

        public void updateRAM(int time, char h)
        {
            holes[time] = h;
        }

    }
}
