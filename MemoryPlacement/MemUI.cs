using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MemUI
{
    public partial class MemUI : Form
    {
        private List<TextBox> jobs = new List<TextBox>();
        private List<TextBox> timeU = new List<TextBox>();
        private int numOfJobs = 0;

        public MemUI()
        {
            InitializeComponent();

            // Fit Strategy
            comboxFS.Items.Add("First Fit");
            comboxFS.Items.Add("Best Fit");

            // Number of Jobs
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

            // Job Highlights   Time Unit Highlights
            jobs.Add(job1S);    timeU.Add(job1T);
            jobs.Add(job2S);    timeU.Add(job2T);
            jobs.Add(job3S);    timeU.Add(job3T);
            jobs.Add(job4S);    timeU.Add(job4T);
            jobs.Add(job5S);    timeU.Add(job5T);
            jobs.Add(job6S);    timeU.Add(job6T);
            jobs.Add(job7S);    timeU.Add(job7T);
            jobs.Add(job8S);    timeU.Add(job8T);
            jobs.Add(job9S);    timeU.Add(job9T);
            jobs.Add(job10S);   timeU.Add(job10T);
            jobs.Add(job11S);   timeU.Add(job11T);
            jobs.Add(job12S);   timeU.Add(job12T);
            jobs.Add(job13S);   timeU.Add(job13T);
            jobs.Add(job14S);   timeU.Add(job14T);
            jobs.Add(job15S);   timeU.Add(job15T);
            jobs.Add(job16S);   timeU.Add(job16T);
            jobs.Add(job17S);   timeU.Add(job17T);
            jobs.Add(job18S);   timeU.Add(job18T);
            jobs.Add(job19S);   timeU.Add(job19T);
            jobs.Add(job20S);   timeU.Add(job20T);

            for (int j = 0; j < jobs.Count(); j++)
            {
                jobs[j].Enabled = false;
                timeU[j].Enabled = false;
            }
        }

        private void comboxNJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            numOfJobs = Int32.Parse(comboxNJ.Text);
            for (int j = 0; j < jobs.Count(); j++)
            {
                jobs[j].Enabled = false;
                timeU[j].Enabled = false;
                
            }
            for(int i = 0; i < numOfJobs; i++)
            {
                jobs[i].Enabled = true;
                timeU[i].Enabled = true;
            }
        }
    }
}
