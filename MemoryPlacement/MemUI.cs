﻿using System;
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
        private List<int> jSize = new List<int>();
        private List<int> jTime = new List<int>();
        private List<Label> compTime = new List<Label>();
        private FirstFit ff = new FirstFit();
        private BestFit bf = new BestFit();
        private double memory = 0;
        private int numOfJobs = 0, compInt = 0, _ticks = 0, _complete = 0;
        private string strategy;

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

        List<int> test = new List<int>();
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
                //Debug
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
                    test.Add(0);
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

                compInt = Int32.Parse(textboxComp.Text);
                memLbl.Text += memory.ToString() + " KB";

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
        }
        
        private void ffStrat_Tick_1(object sender, EventArgs e)
        {
            ff.ffConstructor(jSize, jTime, memory);
            jSize = ff.ffMemory(_ticks);
            memory = ff.memoryLeft();

            //jTime = ff.ffRemaining();

            // TODO: OUTPUT LOGIC
            {
                /*if (jTime[i] == 0) //if the job is completed, output the TU it finished
           {
               compTime[i].Text = "Completed in " + _ticks.ToString() + " TU";
               test[i] = _ticks;
               _complete++;
           }
           else if (jTime[i] < 0) // Prevents Completion TU to decrement
           {
               compTime[i].Text = "Completed in " + test[i].ToString() + " TU";
           }
           else // Continue to coalesce
           {
               compTime[i].Text = "Remaining " + jTime[i].ToString() + " TU";
           }*/
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
        }

        private void comboxFS_SelectedIndexChanged(object sender, EventArgs e)
        {
            strategy = comboxFS.Text;
        }
    }
}