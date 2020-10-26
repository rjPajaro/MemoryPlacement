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
        private List<int> positions = new List<int>();
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

        private List<int> _completed = new List<int>();
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
                    _completed.Add(0);
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

                if (strategy.Equals("First Fit"))
                {
                    ffStrat.Interval = 500;
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
        }

        private string newLine = Environment.NewLine;
        private int reAlloc = 0, allocated = 0, timer = 1;

        private List<int> memoryAlloc = new List<int>();

        private void ffStrat_Tick_1(object sender, EventArgs e)
        {
            int pCount = 0;
            timeUnit++;
            if (allocated == 1) // memory has processes
            {
                if((jTime[timer - 1]-1) == 0) // if the process is complete, output the completion TU
                {
                    compTime[timer - 1].Text = "Completed in " + (timeUnit).ToString() + " TU"; // bug: time unit always updates when something completes
                }
                else // if the process is still running, subtract 1 to its TU
                {
                    compTime[timer - 1].Text = "Remaining " + (jTime[timer - 1] - 1).ToString() + " TU";
                    jTime[timer - 1] -= 1;
                }

                for (int i = 0; i < holes.Count(); i++) // counts all the processes in the array
                {
                    if (holes[i] == 'p')
                    {
                        pCount++;
                    }
                }

                if (timer == pCount) //once timer reaches process count, re-initialize to 0
                {
                    timer = 0;
                }
            }
            else if(allocated == 0) // memory has not been allocated
            {
                int n = 0;
                while (n == 0) // moves to the next iteration if the given size is too big
                {
                    if (jSize[timer - 1] <= ramLeft) // if the current process size is less than the available space
                    {
                        memoryAlloc.Add(jSize[timer - 1]); // add to the memory
                        compTime[timer - 1].Text = "Remaining " + (jTime[timer - 1] - 1).ToString() + " TU";
                        holes.Add('p'); // p = process
                        jTime[timer - 1] -= 1;
                        ramLeft -= jSize[timer - 1];

                        if (jSize[timer] > ramLeft) // if the process' size is greater than the available memory
                        {
                            memoryAlloc.Add(ramLeft); // add the memory left as a 'hole'
                            holes.Add('h'); // hole
                            ramLeft -= ramLeft; // make this = 0
                            timer = 0; //reset the timer
                            allocated = 1; // all memory spaces have been allocated
                        }
                        n = 1; //breaks
                    }
                    else
                    {
                        timer++;
                        n = 0; //loops
                    }
                }
            }

            timer++;
            _ticks++;
            if(_ticks == jSize.Count())
            {
                _ticks = 0;
            }
            // Fail - might delete
            {
                /*
            timeUnit++;

            if(reAlloc == 1)
            {
                ff.checkAlloc();
                
                ff.procHoles();
                //reAlloc = 0;
            }
            else
            {
                ff.ffMemoryAlloc(_ticks);
                //programOutput.Text += timeUnit.ToString() + " TU - Allocate Job #" + (timeDec + 1).ToString() + newLine;

                ramLeft = ff.memoryLeft();
                procSize = ff.processSize();
                jTime = ff.ffRemaining(timeDec);

                { // Comp Time Process
                    if (jTime[timeDec] == 0) //if the job is completed, output the TU it finished
                    {
                        compTime[timeDec].Text = "Completed in " + (_ticks + 1).ToString() + " TU";
                        programOutput.Text += timeUnit.ToString() + " TU - Completed Job #" + (timeDec + 1).ToString() + newLine;

                        _completed[timeDec] = _ticks + 1; //saves completion time in a list to be called
                        _complete++;
                        ff.updateRAM(timeDec, 'h');
                        reAlloc = 1;
                    }
                    else if (jTime[timeDec] < 0) // Prevents Completed jobs to decrement
                    {
                        compTime[timeDec].Text = "Completed in " + _completed[timeDec].ToString() + " TU";
                    }
                    else // Continue to process
                    {
                        compTime[timeDec].Text = "Remaining " + jTime[timeDec].ToString() + " TU";
                        programOutput.Text += timeUnit.ToString() + " TU - Process Job #" + (timeDec + 1).ToString() + newLine;
                    }

                    timeDec++;
                    if (timeDec == procSize && ramLeft == 0)
                    {
                        timeDec = 0;
                    }
                }
            }

            


            _ticks++; // job cycler
            if (_ticks == numOfJobs)
            {
                _ticks = 0; // reset
            }
            if (_complete == numOfJobs)
            {
                ffStrat.Stop();
                MessageBox.Show("Finished in " + _ticks + " TU", "Success!");
            }
             */
            }
        }

        private void comboxFS_SelectedIndexChanged(object sender, EventArgs e)
        {
            strategy = comboxFS.Text;
        }
    }
}