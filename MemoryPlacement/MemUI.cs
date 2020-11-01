using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace MemUI
{
    /*
     * OS Lec - OL157 (project)
     *  MEMORY MANAGEMENT PROJECT (FIRST AND BEST FIT) BY:
        - Tech Lead: Randall Joseph Pajaro
        - Bug Catcher: Mac Kristan Isaac
        - Designer: Spencer Ivan Santiago
    */
    public partial class MemUI : Form
    {
        //variables
        private List<TextBox> jobs = new List<TextBox>(); // jobs
        private List<TextBox> timeU = new List<TextBox>(); // time units of jobs
        private List<Label> compTime = new List<Label>(); //completion time labels
        private List<int> jSize = new List<int>(); // copy of job sizes
        private List<int> jTime = new List<int>(); // copy of time units of jobs
        private List<int> positions = new List<int>(); // positions of each jobs in the list
        private List<int> _completed = new List<int>(); // completion time lists
        private List<int> memoryAlloc = new List<int>(); // memory visuals (sizes)
        private List<int> uPos = new List<int>(); // jobs in memory
        private List<int> compression = new List<int>(); 
        private List<int> pogi = new List<int>(); // ORIGINALITY ! ( ﾟεﾟ )
        private List<int> ListOfIndexH = new List<int>();
        private List<char> holes = new List<char>(); // holes
        private double memory = 0; // these are self explanatory (most..)
        private int numOfJobs = 0, compInt = 0, _complete = 0, coalesce = 0, ramLeft = 0, timeUnit = 0, h = 0;
        private int allocated = 0, timer = 1, smallest = 0, _coal = 1, compressor = 1, cCheck = 0, compCount = 0;
        private string strategy;
        private string newLine = Environment.NewLine; // new line for textbox outputs
        private bool compress = false, selectCoa = true, selectComp = true; // validation

        public MemUI()
        {
            InitializeComponent();

            // Processes
            programOutput.Enabled = false;
            btnCont.Enabled = false;
            pauseButton.Enabled = false;
            abortButton.Enabled = false;

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

            // Coalesce
            {
                textboxCoa.Visible = false;
                comboxCoal.Visible = false;
                coalChoice.Items.Add("Defaults");
                coalChoice.Items.Add("User Input");
                comboxCoal.Items.Add("back");
                for(int i = 1; i <= 5; i++)
                    comboxCoal.Items.Add((i).ToString());
            }

            // Compression
            {
                textboxComp.Visible = false;
                comboxComp.Visible = false;
                compChoice.Items.Add("Defaults");
                compChoice.Items.Add("User Input");
                comboxComp.Items.Add("back");
                for (int i = 1; i <= 5; i++)
                    comboxComp.Items.Add((i*10).ToString());
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
            }

            for (int j = 0; j < jobs.Count(); j++)
            {
                jobs[j].Enabled = false;
                timeU[j].Enabled = false;
            }
        }

        private void coalChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (coalChoice.Text.Equals("Defaults"))
            {
                coalChoice.Visible = false;
                comboxCoal.Visible = true;
            }else if(coalChoice.Text.Equals("User Input"))
            {
                coalChoice.Visible = false;
                textboxCoa.Visible = true;
            }
        }

        private void compChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (compChoice.Text.Equals("Defaults"))
            {
                compChoice.Visible = false;
                comboxComp.Visible = true;
            }
            else if (compChoice.Text.Equals("User Input"))
            {
                compChoice.Visible = false;
                textboxComp.Visible = true;
            }
        }

        private void comboxCoal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboxCoal.Text.Equals("back to chocies"))
            {
                comboxCoal.Visible = false;
                coalChoice.Visible = true;
            }
            else
            {
                coalesce = Int32.Parse(comboxCoal.Text);
                selectCoa = false;
            }
        }

        private void comboxComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboxComp.Text.Equals("back to chocies"))
            {
                comboxComp.Visible = false;
                coalChoice.Visible = true;
            }
            else
            {
                compInt = Int32.Parse(comboxComp.Text);
                selectComp = false;
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

        private void startButton_Click(object sender, EventArgs e)
        {
            programOutput.Enabled = true;
            startButton.Enabled = false; //when the programOutput is ready
            btnCont.Enabled = true;
            pauseButton.Enabled = true;
            abortButton.Enabled = true;

            for (int j = 0; j < compTime.Count(); j++)
            {
                compTime[j].Text = "";
            }

            try
            {
                // For Testing (Debugging)
                {
                    /*jSize.Add(500);
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
                */
                }

                for (int i = 0; i < numOfJobs; i++)
                {
                    compTime[i].Text = "Not Started";
                    //_completed.Add(0);
                    // To be added
                    if(Int32.Parse(jobs[i].Text) <= memory)
                    {
                        jSize.Add(Int32.Parse(jobs[i].Text)); // converts the job sizes to int from string and puts them in a list
                        jTime.Add(Int32.Parse(timeU[i].Text));// converts the job time units to int from string and puts them in a list
                    }
                    else
                    {
                        MessageBox.Show("Job size/s are larger than the given memory size", "Allocation Error:");
                    }
                }

                foreach (int i in jSize)
                {
                    int pos = jSize.IndexOf(i);
                    positions.Add(pos);
                }

                if (selectComp == true)
                    compInt = Int32.Parse(textboxComp.Text);
                if (selectCoa == true)
                    coalesce = Int32.Parse(textboxCoa.Text);

                if (strategy.Equals("First Fit"))
                {
                    ffStrat.Interval = 1000;
                    ffStrat.Start();
                }
                else if (strategy.Equals("Best Fit"))
                {
                    bfStrat.Interval = 1000;
                    bfStrat.Start();
                }
            }
            catch // If there are missing values from the try block, it will clear all lists and output an error message
            {
                MessageBox.Show("Re-check Inputs", "ERROR");
                programOutput.Enabled = false;
                programOutput.Text = "";
                startButton.Enabled = true;
                btnCont.Enabled = false;
                pauseButton.Enabled = false;
                abortButton.Enabled = false;
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
            if (strategy.Equals("First Fit"))
                ffStrat.Stop();
            else if (strategy.Equals("Best Fit"))
                bfStrat.Stop();
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnCont_Click(object sender, EventArgs e)
        {
            programOutput.Enabled = true;
            if(strategy.Equals("First Fit"))
                ffStrat.Start();
            else if (strategy.Equals("Best Fit"))
                bfStrat.Start();
        }

        private void bfStrat_Tick(object sender, EventArgs e)
        {
            timeUnit++;

            if (_coal == coalesce)
            {
                _coal = 0;

                if (compress == true) // Compress when the compress variable reaches the given Compression Interval by the user
                {
                    //1st: get the index of the highest value that is a job in the queue... Yes.
                    //2nd: compress will only turn false if all the processes have been arranged in order.
                    if (cCheck == 0)
                    {
                        for (int i = 0; i < holes.Count(); i++) // pushes all the job indexes
                        {
                            if (holes[i].Equals('p'))
                            {
                                compCount++;
                                compression.Add(memoryAlloc[i]);
                            }
                        }
                    }

                    if (compression.Count() > 1)
                    {
                        int m = memoryAlloc.IndexOf(compression.Max()); // grabs the highest value from the memory
                        memoryAlloc.Insert(cCheck, memoryAlloc[m]); // copies the highest value in the memory to the beginning of the list
                        memoryAlloc.RemoveAt(m + 1); // removes its original copy from the list
                        holes.Insert(cCheck, holes[m]); //puts p at the beginning
                        holes.RemoveAt(m + 1);
                        uPos.Insert(cCheck, uPos[m]); // puts the position of the highest value at the beginning
                        uPos.RemoveAt(m + 1);

                        compression.RemoveAt(compression.IndexOf(compression.Max())); // pop!
                        programOutput.Text += timeUnit.ToString() + " TU - Compress" + newLine; // output
                    }
                    else if (compression.Count() == 1) // for when there's only 1 value to compress
                    {
                        int m = memoryAlloc.IndexOf(compression[0]);
                        memoryAlloc.Insert(cCheck, memoryAlloc[m]);
                        memoryAlloc.RemoveAt(m + 1);
                        holes.Insert(cCheck, holes[m]);
                        holes.RemoveAt(m + 1);
                        uPos.Insert(cCheck, uPos[m]);
                        uPos.RemoveAt(m + 1);

                        compression.RemoveAt(0);
                        programOutput.Text += timeUnit.ToString() + " TU - Compress" + newLine;
                    }

                    cCheck++; // increments up to how many process are there to compress
                    if (cCheck == compCount) // stops compressing once it reaches the number of processes
                    {
                        compression.Clear();
                        cCheck = 0;
                        compressor = 0;
                        allocated = 0;
                        compress = false;
                    }
                }

                else
                {
                    int hCount = 0;
                    List<int> coal = new List<int>();
                    for (int i = 0; i < holes.Count(); i++) // counts all the holes in the memory
                    {
                        if (holes[i] == 'h')
                        {
                            coal.Add(i); // adds the position of the hole to a list
                            hCount++; // gets the count of holes
                        }
                    }

                    if (hCount > 1) // if there are more than 1 holes, check if the process shall be coalesced
                    {
                        for (int i = 1; i < coal.Count(); i++)
                        {
                            // To check if 2 holes are next to each other, take their indexes and add 1 to the index with a lower value.
                            // If the index values match, coalesce. Otherwise, skip.
                            if (coal[i - 1] + 1 == coal[i])
                            {
                                hCount = 2; // pauses the process and proceeds to coalesce
                                // coalesce
                                memoryAlloc[coal[i]] += memoryAlloc[coal[i - 1]];
                                holes.RemoveAt(coal[i - 1]);
                                uPos.RemoveAt(coal[i - 1]);
                                memoryAlloc.RemoveAt(coal[i - 1]);
                                programOutput.Text += timeUnit.ToString() + " TU - Coalesce" + newLine;
                                allocated = 0;

                                Console.WriteLine("TU: " + timeUnit + " - " + hCount);
                                if (positions.Count() > 0)
                                    timer = positions[0] + 1; // timer will be initalized to the first value of the position + 1

                                coal.Clear();
                                break;
                            }
                            else
                            {
                                hCount = 0; // continue process
                            }
                        }
                    }
                    if (hCount <= 1) // if there are no holes or 1 hole or no at least 2 holes are next to each other
                    {
                        if (timer > jSize.Count())
                            allocated = 1; // all memory spaces have been allocated
                        else
                            allocated = 0;

                        if (allocated == 0) // memory has not been allocated/reallocated
                        {
                            int n = 0;
                            int pos = 0;
                            ListOfIndexH.Clear();
                            pogi.Clear();

                            for (int z = 0; z < holes.Count(); z++) // Indices of all the holes
                            {
                                if (holes[z].Equals('h'))
                                {
                                    pogi.Add(memoryAlloc[z]); // value
                                }
                            }

                            pogi.Sort();
                            int incr = 0;
                            int a = 0;
                            while (pogi.Count() != ListOfIndexH.Count())
                            {
                                if (holes[incr].Equals('h'))
                                {
                                    if (pogi[a] == memoryAlloc[incr])
                                    {
                                        ListOfIndexH.Add(incr); // position
                                        a++;
                                        incr = 0;
                                    }
                                    else
                                        incr++;
                                }
                                else
                                    incr++;
                            }

                            // Theoretical Science
                            if (ListOfIndexH.Any())
                            {
                                for (int i = 0; i < ListOfIndexH.Count(); i++)
                                {
                                    if (jSize[timer - 1] <= pogi[i])
                                    {
                                        Console.WriteLine("This runs at: " + timeUnit);
                                        h = ListOfIndexH[i]; // problem zzzz
                                        break;
                                    }
                                    else
                                    {
                                        h = holes.IndexOf('h');
                                    }
                                }
                            }

                            while (n == 0) // moves to the next iteration if the given size is too big
                            {

                                if (timer > jSize.Count())
                                {
                                    allocated = 1; // all memory spaces have been allocated
                                    break;
                                }
                                if (jSize[timer - 1] <= memoryAlloc[h] && positions.Any() && positions.Contains(timer - 1)) // if the current process size is less than the available space
                                {
                                    if (jTime[timer - 1] - 1 == 0) // if time unit input = 1
                                    {
                                        memoryAlloc.Insert(h, jSize[timer - 1]); // add to the memory
                                        memoryAlloc[h + 1] -= memoryAlloc[h];
                                        holes.Insert(h, 'p'); // p = process
                                        jTime[timer - 1] -= 1; // decrease by 1
                                        uPos.Insert(h, positions[pos]);

                                        smallest++;

                                        positions.RemoveAt(pos);
                                        pos = 0;
                                        n = 1; //breaks

                                        // problem: 
                                        ///////// allocated and completes the process at the same time
                                        string ffs = "";
                                        for (int i = 0; i < holes.Count(); i++)
                                        {
                                            ffs += holes[i] + " ";
                                        }
                                        Console.WriteLine("out: " + ffs);

                                        _completed.Add(timeUnit); // adds the completed time units (prevents increment bug for completed times
                                        compTime[uPos[h]].Text = "Completed in " + (_completed[_complete]).ToString() + " TU"; // outputs completion time
                                        programOutput.Text += timeUnit.ToString() + " TU - Allocated and Completed Job #" + (uPos[h] + 1) + newLine; // output
                                        holes[uPos.IndexOf(uPos[h])] = 'h'; // turns p to h meaning the process memory space has been freed up
                                        uPos[uPos.IndexOf(uPos[h])] = 9999; // hole value = 9999
                                        if (positions.Count() > 0)
                                            timer = positions[0]; // timer will be initalized to the first value of the position + 1
                                        allocated = 0; //reallocate

                                        _complete++;
                                    }
                                    else
                                    {
                                        memoryAlloc.Insert(h, jSize[timer - 1]); // add to the memory
                                        memoryAlloc[h + 1] -= memoryAlloc[h];
                                        compTime[timer - 1].Text = "Remaining " + (jTime[timer - 1] - 1).ToString() + " TU";
                                        programOutput.Text += timeUnit.ToString() + " TU - Allocate Job #" + (timer) + newLine;
                                        holes.Insert(h, 'p'); // p = process
                                        jTime[timer - 1] -= 1; // decrease by 1
                                        uPos.Insert(h, positions[pos]);

                                        smallest++;

                                        positions.RemoveAt(pos);
                                        pos = 0;
                                        n = 1; //breaks
                                    }
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
                            int l = 0;
                            while (l == 0)
                            {
                                if (smallest >= uPos.Count())
                                    smallest = 0;
                                if (uPos[smallest] == 9999) // checks if the next item on the list to be processed is a hole or not (9999 = hole)
                                {
                                    smallest++; //if the item on the list is a hole, increment the position by 1
                                    if (smallest >= uPos.Count()) // if it reaches the end of the list, position goes back to 0
                                    {
                                        smallest = 0;
                                    }
                                    l = 0;
                                }
                                else
                                {
                                    if (jTime[uPos[smallest]] - 1 == 0) // checks if the time unit to be decreased would be 0 (it means the process has been completed)
                                    {
                                        _completed.Add(timeUnit); // adds the completed time units (prevents increment bug for completed times
                                        compTime[uPos[smallest]].Text = "Completed in " + (_completed[_complete]).ToString() + " TU"; // outputs completion time
                                        programOutput.Text += timeUnit.ToString() + " TU - Completed Job #" + (uPos[smallest] + 1) + newLine; // output
                                        holes[uPos.IndexOf(uPos[smallest])] = 'h'; // turns p to h meaning the process memory space has been freed up
                                        uPos[uPos.IndexOf(uPos[smallest])] = 9999; // hole value = 9999
                                        if (positions.Count() > 0)
                                            timer = positions[0] + 1; // timer will be initalized to the first value of the position + 1
                                        allocated = 0; //reallocate

                                        _complete++;
                                    }
                                    else // continue to process...
                                    {
                                        compTime[uPos[smallest]].Text = "Remaining " + (jTime[uPos[smallest]] - 1).ToString() + " TU"; // decrease by 1
                                        programOutput.Text += timeUnit.ToString() + " TU - Process Job #" + (uPos[smallest] + 1) + newLine;
                                        jTime[uPos[smallest]] -= 1; // store time value decreased by 1

                                        smallest++;
                                        if (smallest >= uPos.Count())
                                            smallest = 0;
                                    }
                                    l = 1;
                                }
                            }
                        }
                    }
                }
            }
            else // this is only called when there is no coalescing happening during the current time unit.
            {
                if (compress == true) // Compress when the compress variable reaches the given Compression Interval by the user
                {
                    //1st: get the index of the highest value that is a job in the queue... Yes.
                    //2nd: compress will only turn false if all the processes have been arranged in order.
                    if (cCheck == 0)
                    {
                        for (int i = 0; i < holes.Count(); i++) // pushes all the job indexes
                        {
                            if (holes[i].Equals('p'))
                            {
                                compCount++;
                                compression.Add(memoryAlloc[i]);
                            }
                        }
                    }

                    if (compression.Count() > 1)
                    {
                        int m = memoryAlloc.IndexOf(compression.Max()); // grabs the highest value from the memory
                        memoryAlloc.Insert(cCheck, memoryAlloc[m]); // copies the highest value in the memory to the beginning of the list
                        memoryAlloc.RemoveAt(m + 1); // removes its original copy from the list
                        holes.Insert(cCheck, holes[m]); //puts p at the beginning
                        holes.RemoveAt(m + 1);
                        uPos.Insert(cCheck, uPos[m]); // puts the position of the highest value at the beginning
                        uPos.RemoveAt(m + 1);

                        compression.RemoveAt(compression.IndexOf(compression.Max())); // pop!
                        programOutput.Text += timeUnit.ToString() + " TU - Compress" + newLine; // output
                    }
                    else if (compression.Count() == 1) // for when there's only 1 value to compress
                    {
                        int m = memoryAlloc.IndexOf(compression[0]);
                        memoryAlloc.Insert(cCheck, memoryAlloc[m]);
                        memoryAlloc.RemoveAt(m + 1);
                        holes.Insert(cCheck, holes[m]);
                        holes.RemoveAt(m + 1);
                        uPos.Insert(cCheck, uPos[m]);
                        uPos.RemoveAt(m + 1);

                        compression.RemoveAt(0);
                        programOutput.Text += timeUnit.ToString() + " TU - Compress" + newLine;
                    }

                    cCheck++; // increments up to how many process are there to compress
                    if (cCheck == compCount) // stops compressing once it reaches the number of processes
                    {
                        compression.Clear();
                        cCheck = 0;
                        compressor = 0;
                        allocated = 0;
                        compress = false;
                    }
                }
                else
                {
                    if (timer > jSize.Count())
                        allocated = 1; // all memory spaces have been allocated
                    else
                        allocated = 0;

                    if (allocated == 0) // memory has not been allocated/reallocated
                    {
                        int n = 0;
                        int pos = 0;
                        ListOfIndexH.Clear();
                        pogi.Clear();

                        for (int z = 0; z < holes.Count(); z++) // Indices of all the holes
                        {
                            if (holes[z].Equals('h'))
                            {
                                pogi.Add(memoryAlloc[z]); // value
                            }
                        }

                        pogi.Sort();
                        int incr = 0;
                        int a = 0;
                        while (pogi.Count() != ListOfIndexH.Count())
                        {
                            if (holes[incr].Equals('h'))
                            {
                                if (pogi[a] == memoryAlloc[incr])
                                {
                                    ListOfIndexH.Add(incr); // position
                                    a++;
                                    incr = 0;
                                }
                                else
                                    incr++;
                            }
                            else
                                incr++;
                        }

                        // Theoretical Science
                        if (ListOfIndexH.Any())
                        {
                            for (int i = 0; i < ListOfIndexH.Count(); i++)
                            {
                                if (jSize[timer - 1] <= pogi[i])
                                {
                                    Console.WriteLine("This runs at: " + timeUnit);
                                    h = ListOfIndexH[i]; // problem zzzz
                                    break;
                                }
                                else
                                {
                                    h = holes.IndexOf('h');
                                }
                            }
                        }

                        while (n == 0) // moves to the next iteration if the given size is too big
                        {

                            if (timer > jSize.Count())
                            {
                                allocated = 1; // all memory spaces have been allocated
                                break;
                            }
                            if (jSize[timer - 1] <= memoryAlloc[h] && positions.Any() && positions.Contains(timer - 1)) // if the current process size is less than the available space
                            {
                                if (jTime[timer - 1] - 1 == 0) // if time unit input = 1
                                {
                                    memoryAlloc.Insert(h, jSize[timer - 1]); // add to the memory
                                    memoryAlloc[h + 1] -= memoryAlloc[h];
                                    holes.Insert(h, 'p'); // p = process
                                    jTime[timer - 1] -= 1; // decrease by 1
                                    uPos.Insert(h, positions[pos]);

                                    smallest++;

                                    positions.RemoveAt(pos);
                                    pos = 0;
                                    n = 1; //breaks

                                    // problem: 
                                    ///////// allocated and completes the process at the same time
                                    string ffs = "";
                                    for (int i = 0; i < holes.Count(); i++)
                                    {
                                        ffs += holes[i] + " ";
                                    }
                                    Console.WriteLine("out: " + ffs);

                                    _completed.Add(timeUnit); // adds the completed time units (prevents increment bug for completed times
                                    compTime[uPos[h]].Text = "Completed in " + (_completed[_complete]).ToString() + " TU"; // outputs completion time
                                    programOutput.Text += timeUnit.ToString() + " TU - Allocated and Completed Job #" + (uPos[h] + 1) + newLine; // output
                                    holes[uPos.IndexOf(uPos[h])] = 'h'; // turns p to h meaning the process memory space has been freed up
                                    uPos[uPos.IndexOf(uPos[h])] = 9999; // hole value = 9999
                                    if (positions.Count() > 0)
                                        timer = positions[0]; // timer will be initalized to the first value of the position + 1
                                    allocated = 0; //reallocate

                                    _complete++;
                                }
                                else
                                {
                                    memoryAlloc.Insert(h, jSize[timer - 1]); // add to the memory
                                    memoryAlloc[h + 1] -= memoryAlloc[h];
                                    compTime[timer - 1].Text = "Remaining " + (jTime[timer - 1] - 1).ToString() + " TU";
                                    programOutput.Text += timeUnit.ToString() + " TU - Allocate Job #" + (timer) + newLine;
                                    holes.Insert(h, 'p'); // p = process
                                    jTime[timer - 1] -= 1; // decrease by 1
                                    uPos.Insert(h, positions[pos]);

                                    smallest++;

                                    positions.RemoveAt(pos);
                                    pos = 0;
                                    n = 1; //breaks
                                }
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
                        int l = 0;
                        while (l == 0)
                        {
                            if (smallest >= uPos.Count())
                                smallest = 0;
                            if (uPos[smallest] == 9999) // checks if the next item on the list to be processed is a hole or not (9999 = hole)
                            {
                                smallest++; //if the item on the list is a hole, increment the position by 1
                                if (smallest >= uPos.Count()) // if it reaches the end of the list, position goes back to 0
                                {
                                    smallest = 0;
                                }
                                l = 0;
                            }
                            else
                            {
                                if (jTime[uPos[smallest]] - 1 == 0) // checks if the time unit to be decreased would be 0 (it means the process has been completed)
                                {
                                    _completed.Add(timeUnit); // adds the completed time units (prevents increment bug for completed times
                                    compTime[uPos[smallest]].Text = "Completed in " + (_completed[_complete]).ToString() + " TU"; // outputs completion time
                                    programOutput.Text += timeUnit.ToString() + " TU - Completed Job #" + (uPos[smallest] + 1) + newLine; // output
                                    holes[uPos.IndexOf(uPos[smallest])] = 'h'; // turns p to h meaning the process memory space has been freed up
                                    uPos[uPos.IndexOf(uPos[smallest])] = 9999; // hole value = 9999
                                    if (positions.Count() > 0)
                                        timer = positions[0] + 1; // timer will be initalized to the first value of the position + 1
                                    allocated = 0; //reallocate

                                    _complete++;
                                }
                                else // continue to process...
                                {
                                    compTime[uPos[smallest]].Text = "Remaining " + (jTime[uPos[smallest]] - 1).ToString() + " TU"; // decrease by 1
                                    programOutput.Text += timeUnit.ToString() + " TU - Process Job #" + (uPos[smallest] + 1) + newLine;
                                    jTime[uPos[smallest]] -= 1; // store time value decreased by 1

                                    smallest++;
                                    if (smallest >= uPos.Count())
                                        smallest = 0;
                                }
                                l = 1;
                            }
                        }
                    }
                }
            }


            if (_completed.Count() == numOfJobs)
            {
                programOutput.Text += "-- Finished --";
                btnCont.Enabled = false;
                pauseButton.Enabled = false;
                holes.Clear();
                uPos.Clear();
                memoryAlloc.Clear();
                memoryAlloc.Add(ramLeft);
                holes.Add('h');
                uPos.Add(9999);
                h = 0;

                bfStrat.Stop();
            }

            _coal++;
            compressor++;
            string mSizes = "";
            string mHoles = "";
            for (int i = 0; i < holes.Count(); i++)
            {
                mSizes += memoryAlloc[i] + "KB |   ";
                if (holes[i].Equals('h'))
                    mHoles += "   " + holes[i] + "        |   ";
                else if (holes[i].Equals('p'))
                    mHoles += "   J" + (uPos[i] + 1) + "       |   ";
            }

            memVis.Text = mSizes + newLine + mHoles;

            programOutput.SelectionStart = programOutput.Text.Length;
            programOutput.ScrollToCaret();
            if (compressor == compInt + 1)
            {
                compress = true;
            }

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
       
        private void ffStrat_Tick_1(object sender, EventArgs e)
        {
            timeUnit++;

            if (_coal == coalesce)
            {
                _coal = 0;

                if (compress == true) // Compress when the compress variable reaches the given Compression Interval by the user
                {
                    //1st: get the index of the highest value that is a job in the queue... Yes.
                    //2nd: compress will only turn false if all the processes have been arranged in order.
                    if (cCheck == 0)
                    {
                        for (int i = 0; i < holes.Count(); i++) // pushes all the job indexes
                        {
                            if (holes[i].Equals('p'))
                            { 
                                compCount++;
                                compression.Add(memoryAlloc[i]);
                            }
                        }
                    }

                    if (compression.Count() > 1)
                    {
                        int m = memoryAlloc.IndexOf(compression.Max()); // grabs the highest value from the memory
                        memoryAlloc.Insert(cCheck, memoryAlloc[m]); // copies the highest value in the memory to the beginning of the list
                        memoryAlloc.RemoveAt(m + 1); // removes its original copy from the list
                        holes.Insert(cCheck, holes[m]); //puts p at the beginning
                        holes.RemoveAt(m + 1);
                        uPos.Insert(cCheck, uPos[m]); // puts the position of the highest value at the beginning
                        uPos.RemoveAt(m + 1);

                        compression.RemoveAt(compression.IndexOf(compression.Max())); // pop!
                        programOutput.Text += timeUnit.ToString() + " TU - Compress" + newLine; // output
                    }
                    else if(compression.Count() == 1) // for when there's only 1 value to compress
                    {
                        int m = memoryAlloc.IndexOf(compression[0]);
                        memoryAlloc.Insert(cCheck, memoryAlloc[m]);
                        memoryAlloc.RemoveAt(m + 1);
                        holes.Insert(cCheck, holes[m]);
                        holes.RemoveAt(m + 1);
                        uPos.Insert(cCheck, uPos[m]);
                        uPos.RemoveAt(m + 1);

                        compression.RemoveAt(0);
                        programOutput.Text += timeUnit.ToString() + " TU - Compress" + newLine;
                    }

                    cCheck++; // increments up to how many process are there to compress
                    if (cCheck == compCount) // stops compressing once it reaches the number of processes
                    {
                        compression.Clear();
                        cCheck = 0;
                        compressor = 0;
                        allocated = 0;
                        compress = false;
                    }
                }

                else
                {
                    int hCount = 0;
                    List<int> coal = new List<int>();
                    for (int i = 0; i < holes.Count(); i++) // counts all the holes in the memory
                    {
                        if (holes[i] == 'h')
                        {
                            coal.Add(i); // adds the position of the hole to a list
                            hCount++; // gets the count of holes
                        }
                    }

                    if (hCount > 1) // if there are more than 1 holes, check if the process shall be coalesced
                    {
                        for (int i = 1; i < coal.Count(); i++)
                        {
                            // To check if 2 holes are next to each other, take their indexes and add 1 to the index with a lower value.
                            // If the index values match, coalesce. Otherwise, skip.
                            if (coal[i - 1] + 1 == coal[i]) 
                            {
                                hCount = 2; // pauses the process and proceeds to coalesce
                                // coalesce
                                memoryAlloc[coal[i]] += memoryAlloc[coal[i - 1]];
                                holes.RemoveAt(coal[i - 1]);
                                uPos.RemoveAt(coal[i - 1]);
                                memoryAlloc.RemoveAt(coal[i - 1]);
                                programOutput.Text += timeUnit.ToString() + " TU - Coalesce" + newLine;
                                allocated = 0;

                                Console.WriteLine("TU: " + timeUnit + " - " + hCount);
                                if (positions.Count() > 0)
                                    timer = positions[0] + 1; // timer will be initalized to the first value of the position + 1

                                coal.Clear();
                                break;
                            }
                            else
                            {
                                hCount = 0; // continue process
                            } 
                        }
                    }
                    if (hCount <= 1) // if there are no holes or 1 hole
                    {
                        if (timer > jSize.Count())
                            allocated = 1; // all memory spaces have been allocated
                        else
                            allocated = 0;

                        if (allocated == 0) // memory has not been allocated/reallocated
                        {
                            int n = 0;
                            int pos = 0;
                            h = holes.IndexOf('h');

                            while (n == 0) // moves to the next iteration if the given size is too big
                            {

                                if (timer > jSize.Count())
                                {
                                    allocated = 1; // all memory spaces have been allocated
                                    break;
                                }
                                if (jSize[timer - 1] <= memoryAlloc[h] && positions.Any() && positions.Contains(timer-1)) // if the current process size is less than the available space
                                {
                                    if (jTime[timer - 1] - 1 == 0) // if time unit input = 1
                                    {
                                        memoryAlloc.Insert(h, jSize[timer - 1]); // add to the memory
                                        memoryAlloc[h + 1] -= memoryAlloc[h];
                                        holes.Insert(h, 'p'); // p = process
                                        jTime[timer - 1] -= 1; // decrease by 1
                                        uPos.Insert(h, positions[pos]);

                                        smallest++;

                                        positions.RemoveAt(pos);
                                        pos = 0;
                                        n = 1; //breaks

                                        ///////// allocated and completes the process at the same time

                                        _completed.Add(timeUnit); // adds the completed time units (prevents increment bug for completed times
                                        compTime[uPos[h]].Text = "Completed in " + (_completed[_complete]).ToString() + " TU"; // outputs completion time
                                        programOutput.Text += timeUnit.ToString() + " TU - Allocated and Completed Job #" + (uPos[h] + 1) + newLine; // output
                                        holes[uPos.IndexOf(uPos[h])] = 'h'; // turns p to h meaning the process memory space has been freed up
                                        uPos[uPos.IndexOf(uPos[h])] = 9999; // hole value = 9999
                                        if (positions.Count() > 0)
                                            timer = positions[0]; // timer will be initalized to the first value of the position + 1
                                        allocated = 0; //reallocate

                                        _complete++;
                                    }
                                    else
                                    {
                                        memoryAlloc.Insert(h, jSize[timer - 1]); // add to the memory
                                        memoryAlloc[h + 1] -= memoryAlloc[h];
                                        compTime[timer - 1].Text = "Remaining " + (jTime[timer - 1] - 1).ToString() + " TU";
                                        programOutput.Text += timeUnit.ToString() + " TU - Allocate Job #" + (timer) + newLine;
                                        holes.Insert(h, 'p'); // p = process
                                        jTime[timer - 1] -= 1; // decrease by 1
                                        uPos.Insert(h, positions[pos]);

                                        smallest++;

                                        positions.RemoveAt(pos);
                                        pos = 0;
                                        n = 1; //breaks
                                    }
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
                            int l = 0;
                            while (l == 0)
                            {
                                if (smallest >= uPos.Count())
                                    smallest = 0;
                                if (uPos[smallest] == 9999) // checks if the next item on the list to be processed is a hole or not (9999 = hole)
                                {
                                    smallest++; //if the item on the list is a hole, increment the position by 1
                                    if (smallest >= uPos.Count()) // if it reaches the end of the list, position goes back to 0
                                    {
                                        smallest = 0;
                                    }
                                    l = 0;
                                }
                                else
                                {
                                    if (jTime[uPos[smallest]] - 1 == 0) // checks if the time unit to be decreased would be 0 (it means the process has been completed)
                                    {
                                        _completed.Add(timeUnit); // adds the completed time units (prevents increment bug for completed times
                                        compTime[uPos[smallest]].Text = "Completed in " + (_completed[_complete]).ToString() + " TU"; // outputs completion time
                                        programOutput.Text += timeUnit.ToString() + " TU - Completed Job #" + (uPos[smallest] + 1) + newLine; // output
                                        holes[uPos.IndexOf(uPos[smallest])] = 'h'; // turns p to h meaning the process memory space has been freed up
                                        uPos[uPos.IndexOf(uPos[smallest])] = 9999; // hole value = 9999
                                        if (positions.Count() > 0)
                                            timer = positions[0] + 1; // timer will be initalized to the first value of the position + 1
                                        allocated = 0; //reallocate

                                        _complete++;
                                    }
                                    else // continue to process...
                                    {
                                        compTime[uPos[smallest]].Text = "Remaining " + (jTime[uPos[smallest]] - 1).ToString() + " TU"; // decrease by 1
                                        programOutput.Text += timeUnit.ToString() + " TU - Process Job #" + (uPos[smallest] + 1) + newLine;
                                        jTime[uPos[smallest]] -= 1; // store time value decreased by 1

                                        smallest++;
                                        if (smallest >= uPos.Count())
                                            smallest = 0;
                                    }
                                    l = 1;
                                }
                            }
                        }
                    }
                }
            }
            else // this is only called when there is no coalescing happening during the current time unit.
            {
                if (compress == true) // Compress when the compress variable reaches the given Compression Interval by the user
                {
                    //1st: get the index of the highest value that is a job in the queue... Yes.
                    //2nd: compress will only turn false if all the processes have been arranged in order.
                    if (cCheck == 0)
                    {
                        for (int i = 0; i < holes.Count(); i++) // pushes all the job indexes
                        {
                            if (holes[i].Equals('p'))
                            {
                                compCount++;
                                compression.Add(memoryAlloc[i]);
                            }
                        }
                    }

                    if (compression.Count() > 1)
                    {
                        int m = memoryAlloc.IndexOf(compression.Max()); // grabs the highest value from the memory
                        memoryAlloc.Insert(cCheck, memoryAlloc[m]); // copies the highest value in the memory to the beginning of the list
                        memoryAlloc.RemoveAt(m + 1); // removes its original copy from the list
                        holes.Insert(cCheck, holes[m]); //puts p at the beginning
                        holes.RemoveAt(m + 1);
                        uPos.Insert(cCheck, uPos[m]); // puts the position of the highest value at the beginning
                        uPos.RemoveAt(m + 1);

                        compression.RemoveAt(compression.IndexOf(compression.Max())); // pop!
                        programOutput.Text += timeUnit.ToString() + " TU - Compress" + newLine; // output
                    }
                    else if (compression.Count() == 1) // for when there's only 1 value to compress
                    {
                        int m = memoryAlloc.IndexOf(compression[0]);
                        memoryAlloc.Insert(cCheck, memoryAlloc[m]);
                        memoryAlloc.RemoveAt(m + 1);
                        holes.Insert(cCheck, holes[m]);
                        holes.RemoveAt(m + 1);
                        uPos.Insert(cCheck, uPos[m]);
                        uPos.RemoveAt(m + 1);

                        compression.RemoveAt(0);
                        programOutput.Text += timeUnit.ToString() + " TU - Compress" + newLine;
                    }

                    cCheck++; // increments up to how many process are there to compress
                    if (cCheck == compCount) // stops compressing once it reaches the number of processes
                    {
                        compression.Clear();
                        cCheck = 0;
                        compressor = 0;
                        allocated = 0;
                        compress = false;
                    }
                }
                else
                {
                    if (timer > jSize.Count())
                        allocated = 1; // all memory spaces have been allocated
                    else
                        allocated = 0;

                    if (allocated == 0) // memory has not been allocated/reallocated
                    {
                        int n = 0;
                        int pos = 0;
                        h = holes.IndexOf('h');

                        while (n == 0) // moves to the next iteration if the given size is too big
                        {

                            if (timer > jSize.Count())
                            {
                                allocated = 1; // all memory spaces have been allocated
                                break;
                            }
                            if (jSize[timer - 1] <= memoryAlloc[h] && positions.Any() && positions.Contains(timer - 1)) // if the current process size is less than the available space
                            {
                                if (jTime[timer - 1] - 1 == 0) // if time unit input = 1
                                {
                                    memoryAlloc.Insert(h, jSize[timer - 1]); // add to the memory
                                    memoryAlloc[h + 1] -= memoryAlloc[h];
                                    holes.Insert(h, 'p'); // p = process
                                    jTime[timer - 1] -= 1; // decrease by 1
                                    uPos.Insert(h, positions[pos]);

                                    smallest++;

                                    positions.RemoveAt(pos);
                                    pos = 0;
                                    n = 1; //breaks

                                    ///////// allocated and completes the process at the same time

                                    _completed.Add(timeUnit); // adds the completed time units (prevents increment bug for completed times
                                    compTime[uPos[h]].Text = "Completed in " + (_completed[_complete]).ToString() + " TU"; // outputs completion time
                                    programOutput.Text += timeUnit.ToString() + " TU - Allocated and Completed Job #" + (uPos[h] + 1) + newLine; // output
                                    holes[uPos.IndexOf(uPos[h])] = 'h'; // turns p to h meaning the process memory space has been freed up
                                    uPos[uPos.IndexOf(uPos[h])] = 9999; // hole value = 9999
                                    if (positions.Count() > 0)
                                        timer = positions[0]; // timer will be initalized to the first value of the position + 1
                                    allocated = 0; //reallocate

                                    _complete++;
                                }
                                else
                                {
                                    memoryAlloc.Insert(h, jSize[timer - 1]); // add to the memory
                                    memoryAlloc[h + 1] -= memoryAlloc[h];
                                    compTime[timer - 1].Text = "Remaining " + (jTime[timer - 1] - 1).ToString() + " TU";
                                    programOutput.Text += timeUnit.ToString() + " TU - Allocate Job #" + (timer) + newLine;
                                    holes.Insert(h, 'p'); // p = process
                                    jTime[timer - 1] -= 1; // decrease by 1
                                    uPos.Insert(h, positions[pos]);

                                    smallest++;

                                    positions.RemoveAt(pos);
                                    pos = 0;
                                    n = 1; //breaks
                                }
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
                        int l = 0;
                        while (l == 0)
                        {
                            if (smallest >= uPos.Count())
                                smallest = 0;
                            if (uPos[smallest] == 9999) // checks if the next item on the list to be processed is a hole or not (9999 = hole)
                            {
                                smallest++; //if the item on the list is a hole, increment the position by 1
                                if (smallest >= uPos.Count()) // if it reaches the end of the list, position goes back to 0
                                {
                                    smallest = 0;
                                }
                                l = 0;
                            }
                            else
                            {
                                if (jTime[uPos[smallest]] - 1 == 0) // checks if the time unit to be decreased would be 0 (it means the process has been completed)
                                {
                                    _completed.Add(timeUnit); // adds the completed time units (prevents increment bug for completed times
                                    compTime[uPos[smallest]].Text = "Completed in " + (_completed[_complete]).ToString() + " TU"; // outputs completion time
                                    programOutput.Text += timeUnit.ToString() + " TU - Completed Job #" + (uPos[smallest] + 1) + newLine; // output
                                    holes[uPos.IndexOf(uPos[smallest])] = 'h'; // turns p to h meaning the process memory space has been freed up
                                    uPos[uPos.IndexOf(uPos[smallest])] = 9999; // hole value = 9999
                                    if (positions.Count() > 0)
                                        timer = positions[0] + 1; // timer will be initalized to the first value of the position + 1
                                    allocated = 0; //reallocate

                                    _complete++;
                                }
                                else // continue to process...
                                {
                                    compTime[uPos[smallest]].Text = "Remaining " + (jTime[uPos[smallest]] - 1).ToString() + " TU"; // decrease by 1
                                    programOutput.Text += timeUnit.ToString() + " TU - Process Job #" + (uPos[smallest] + 1) + newLine;
                                    jTime[uPos[smallest]] -= 1; // store time value decreased by 1

                                    smallest++;
                                    if (smallest >= uPos.Count())
                                        smallest = 0;
                                }
                                l = 1;
                            }
                        }
                    }
                }
            }

            if (_completed.Count() == numOfJobs)
            {
                programOutput.Text += "-- Finished --";
                btnCont.Enabled = false;
                pauseButton.Enabled = false;
                startButton.Enabled = true;
                holes.Clear();
                uPos.Clear();
                memoryAlloc.Clear();
                memoryAlloc.Add(ramLeft);
                holes.Add('h');
                uPos.Add(9999);
                h = 0;

                ffStrat.Stop();
            }

            _coal++;
            compressor++;
            programOutput.SelectionStart = programOutput.Text.Length;
            programOutput.ScrollToCaret();

            // memory visuals (beta)
            string mSizes = "";
            string mHoles = "";
            for(int i = 0; i < holes.Count(); i++)
            {
                mSizes += memoryAlloc[i] + "KB |   ";
                if (holes[i].Equals('h'))
                    mHoles += "   " + holes[i] + "        |   ";
                else if (holes[i].Equals('p'))
                    mHoles += "   J" + (uPos[i]+1) + "       |   ";
            }

            memVis.Text = mSizes + newLine + mHoles;
            if (compressor == compInt + 1)
                compress = true;
        }

        private void comboxFS_SelectedIndexChanged(object sender, EventArgs e)
        {
            strategy = comboxFS.Text;
        }
    }
}