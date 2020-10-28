using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MemUI.Strategy;

namespace MemUI
{
    public partial class MemUI : Form
    {
        //variables
        private List<TextBox> jobs = new List<TextBox>();
        private List<TextBox> timeU = new List<TextBox>();
        private List<Label> compTime = new List<Label>();
        private List<int> jSize = new List<int>();
        private List<int> jTime = new List<int>();
        private List<int> cTime = new List<int>();
        private List<int> positions = new List<int>();
        private List<int> _completed = new List<int>();
        private List<char> holes = new List<char>();
        private FirstFit ff = new FirstFit();
        private BestFit bf = new BestFit();
        private double memory = 0;
        private int numOfJobs = 0, compInt = 0, _ticks = 0, _complete = 0;
        private string strategy;
        private int ramLeft = 0, timeDec = 0, procSize = 0, timeUnit = 0;

        public MemUI()
        {
            InitializeComponent();

            // Processes
            programOutput.Enabled = false;

            // Fit Strategy
            {
                comboxFS.Items.Add("First Fit");
                comboxFS.Items.Add("Best Fit");
            }

            // Memory Size (MB)
            {
                comboxMB.Items.Add("0.5");
                comboxMB.Items.Add("1");
                comboxMB.Items.Add("2");
                comboxMB.Items.Add("4");
            }

            // Number of Jobs
            {
                comboxNJ.Items.Add("1");
                comboxNJ.Items.Add("2");
                comboxNJ.Items.Add("3");
                comboxNJ.Items.Add("4");
                comboxNJ.Items.Add("5");
                comboxNJ.Items.Add("6");
                comboxNJ.Items.Add("7");
                comboxNJ.Items.Add("8");
                comboxNJ.Items.Add("9");
                comboxNJ.Items.Add("10");
                comboxNJ.Items.Add("11");
                comboxNJ.Items.Add("12");
                comboxNJ.Items.Add("13");
                comboxNJ.Items.Add("14");
                comboxNJ.Items.Add("15");
                comboxNJ.Items.Add("16");
                comboxNJ.Items.Add("17");
                comboxNJ.Items.Add("18");
                comboxNJ.Items.Add("19");
                comboxNJ.Items.Add("20");
            }

            // Job Highlights      TU Highlights       CT Highlights
            {
                jobs.Add(job1S); timeU.Add(job1T); compTime.Add(job1Comp);
                jobs.Add(job2S); timeU.Add(job2T); compTime.Add(job2Comp);
                jobs.Add(job3S); timeU.Add(job3T); compTime.Add(job3Comp);
                jobs.Add(job4S); timeU.Add(job4T); compTime.Add(job4Comp);
                jobs.Add(job5S); timeU.Add(job5T); compTime.Add(job5Comp);
                jobs.Add(job6S); timeU.Add(job6T); compTime.Add(job6Comp);
                jobs.Add(job7S); timeU.Add(job7T); compTime.Add(job7Comp);
                jobs.Add(job8S); timeU.Add(job8T); compTime.Add(job8Comp);
                jobs.Add(job9S); timeU.Add(job9T); compTime.Add(job9Comp);
                jobs.Add(job10S); timeU.Add(job10T); compTime.Add(job10Comp);
                jobs.Add(job11S); timeU.Add(job11T); compTime.Add(job11Comp);
                jobs.Add(job12S); timeU.Add(job12T); compTime.Add(job12Comp);
                jobs.Add(job13S); timeU.Add(job13T); compTime.Add(job13Comp);
                jobs.Add(job14S); timeU.Add(job14T); compTime.Add(job14Comp);
                jobs.Add(job15S); timeU.Add(job15T); compTime.Add(job15Comp);
                jobs.Add(job16S); timeU.Add(job16T); compTime.Add(job16Comp);
                jobs.Add(job17S); timeU.Add(job17T); compTime.Add(job17Comp);
                jobs.Add(job18S); timeU.Add(job18T); compTime.Add(job18Comp);
                jobs.Add(job19S); timeU.Add(job19T); compTime.Add(job19Comp);
                jobs.Add(job20S); timeU.Add(job20T); compTime.Add(job20Comp);
            }

            for (int j = 0; j < jobs.Count(); j++)
            {
                jobs[j].Enabled = false;
                timeU[j].Enabled = false;
            }
        }

        private void comboxNJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            numOfJobs = Int32.Parse(comboxNJ.Text);
            for (int j = 0; j < compTime.Count(); j++)
            {
                compTime[j].Text = "";
            }
            for (int j = 0; j < jobs.Count(); j++)
            {
                jobs[j].Enabled = false;
                timeU[j].Enabled = false;
                
            }
            for (int i = 0; i < numOfJobs; i++)
            {
                jobs[i].Enabled = true;
                timeU[i].Enabled = true;
            }
        }

        private void startButton_Click(object sender, EventArgs e) // This should also call the threadProc
        {
            programOutput.Enabled = true;
            startButton.Enabled = false; //when the programOutput is ready
            _ticks = 0;
            
            for (int j = 0; j < compTime.Count(); j++)
            {
                compTime[j].Text = "";
            }

            try
            {
                // For Testing (Debugging)
                {
                    jSize.Add(500);
                    jSize.Add(250);
                    jSize.Add(200);
                    jSize.Add(350);
                    jSize.Add(60);
                    jSize.Add(300);
                    jSize.Add(400);

                    jTime.Add(3);
                    jTime.Add(4);
                    jTime.Add(5);
                    jTime.Add(3);
                    jTime.Add(5);
                    jTime.Add(3);
                    jTime.Add(2);
                }
                for (int i = 0; i < numOfJobs; i++)
                {
                    compTime[i].Text = "Not Started";
                    //_completed.Add(0);
                    // To be added
                    {
                        /*if(Int32.Parse(jobs[i].Text) <= memory)
                    {
                        //jSize.Add(Int32.Parse(jobs[i].Text)); // converts the job sizes to int from string and puts them in a list
                        //jTime.Add(Int32.Parse(timeU[i].Text));// converts the job time units to int from string and puts them in a list
                    }
                    else
                    {
                        MessageBox.Show("Job size/s are larger than the given memory size", "MemoryError:");
                    }*/
                    }
                }

                foreach (int i in jSize)
                {
                    int pos = jSize.IndexOf(i);
                    positions.Add(pos);
                }

                compInt = Int32.Parse(textboxComp.Text);
                memLbl.Text += memory.ToString() + " KB";
                cTime = jTime;

                if (strategy.Equals("First Fit"))
                {
                    ffStrat.Interval = 1000;
                    ffStrat.Start();
                }
            }
            catch // If there are missing values from the try block, it will clear all lists and output an error message
            {
                MessageBox.Show("Invalid Inputs", "ERROR");
                programOutput.Enabled = false;
                programOutput.Text = "";
                startButton.Enabled = true;
                jSize.Clear();
                jTime.Clear();
                memLbl.Text = "Memory: ";
                for(int err = 0; err < numOfJobs; err++)
                {
                    compTime[err].Text = "";
                }
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            programOutput.Enabled = false;
            ffStrat.Stop();
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            programOutput.Enabled = false;
            programOutput.Text = "";
            startButton.Enabled = true;
            jSize.Clear();
            jTime.Clear();
            memLbl.Text = "Memory: ";
        }

        private void comboxMB_SelectedIndexChanged(object sender, EventArgs e)
        {
            memory = Convert.ToDouble(comboxMB.Text);
            memory = memory * 1000;
            ramLeft = Convert.ToInt32(memory);
            memoryAlloc.Add(ramLeft);
            holes.Add('h');
            uPos.Add(9999);
        }

        private string newLine = Environment.NewLine;
        private int allocated = 0, timer = 1, smallest = 0;

        private List<int> memoryAlloc = new List<int>();
        private List<int> uPos = new List<int>();

        private void ffStrat_Tick_1(object sender, EventArgs e)
        {
            timeUnit++; 
            int hCount = 0;
            List<int> coal = new List<int>();
            for(int i = 0; i < holes.Count(); i++) // counts all the holes in the memory
            {
                if(holes[i] == 'h')
                {
                    coal.Add(i); // adds the position of the hole to a list
                    hCount++; // gets the count of holes
                }
            }

            if (hCount > 1) // if there are more than 1 holes
            {
                // TODO: Coallescing
            }
            else if (hCount <= 1) // if there are no holes or less than 1 hole
            {
                if (allocated == 0) // memory has not been allocated
                {
                    int n = 0;
                    int h = holes.IndexOf('h');
                    int pos = 0;
                    while (n == 0) // moves to the next iteration if the given size is too big
                    {
                        if (timer > jSize.Count())
                        {
                            timer = positions[0] + 1;
                            allocated = 1; // all memory spaces have been allocated
                            smallest = uPos.IndexOf(uPos.Min());
                            break;
                        }
                        if (jSize[timer - 1] <= memoryAlloc[h]) // if the current process size is less than the available space
                        {
                            memoryAlloc.Insert(h, jSize[timer - 1]); // add to the memory
                            memoryAlloc[h + 1] -= memoryAlloc[h];
                            compTime[timer - 1].Text = "Remaining " + (jTime[timer - 1] - 1).ToString() + " TU";
                            programOutput.Text += timeUnit.ToString() + " TU - Allocate Job #" + (timer) + newLine;
                            holes.Insert(h, 'p'); // p = process
                            jTime[timer - 1] -= 1; // decrease by 1
                            uPos.Insert(h, positions[pos]);

                            positions.RemoveAt(pos);

                            pos = 0;
                            n = 1; //breaks
                        }
                        else
                        {
                            timer++;
                            pos++;
                            n = 0; //loops
                        }
                    }

                    timer++;
                }
                if (allocated == 1)
                {
                    if (uPos[smallest] == 9999) // checks if the next item on the list to be processed is a hole or not (9999 = hole)
                    {
                        smallest++; //if the item on the list is a hole, increment the position by 1
                        if (smallest >= uPos.Count()) // if it reaches the end of the list, position goes back to 0
                        {
                            smallest = 0;
                        }
                    }

                    if (jTime[uPos[smallest]] - 1 == 0) // checks if the time unit to be decreased would be 0 (it means the process has been completed)
                    {
                        _completed.Add(timeUnit); // adds the completed time units (prevents increment bug for completed times
                        compTime[uPos[smallest]].Text = "Completed in " + (_completed[uPos[smallest]]).ToString() + " TU"; // outputs completion time
                        programOutput.Text += timeUnit.ToString() + " TU - Completed Job #" + (uPos[smallest] + 1) + newLine; // output
                        holes[uPos[smallest]] = 'h'; // turns p to h meaning the process memory space has been freed up
                        uPos[uPos[smallest]] = 9999; // hole value = 9999
                        timer = positions[0] + 1; // timer will be initalized to the first value of the position + 1
                        allocated = 0; //reallocate

                        string test = ""; //debugger
                        for (int i = 0; i < positions.Count(); i++)
                        {
                            test += positions[i].ToString() + " ";
                        }
                        //lblPosition.Text = test.ToString();
                    }
                    else // continue to process...
                    {
                        string test = ""; // debugger
                        for (int i = 0; i < uPos.Count(); i++)
                        {
                            test += uPos[i].ToString() + " ";
                        }
                        //lblPosition.Text = test.ToString();
                        compTime[uPos[smallest]].Text = "Remaining " + (jTime[uPos[smallest]] - 1).ToString() + " TU"; // decrease by 1
                        programOutput.Text += timeUnit.ToString() + " TU - Process Job #" + (uPos[smallest] + 1) + newLine;
                        jTime[uPos[smallest]] -= 1; // store time value decreased by 1
                    }

                    smallest++;
                    if (smallest >= uPos.Count())
                        smallest = 0;
                }
            }

            
        
        }

        private void comboxFS_SelectedIndexChanged(object sender, EventArgs e)
        {
            strategy = comboxFS.Text;
        }
    }
}