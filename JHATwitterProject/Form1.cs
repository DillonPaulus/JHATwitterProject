using JHATwitterProject.Objects;
using System;
using System.Configuration;
using System.Windows.Forms;
using Tweetinvi.Events.V2;

namespace JHATwitterProject
{
    /// <summary>
    /// This class holds all the view events. It will load the basic components and hold the main thread/child threads alive
    /// </summary>
    /// <scope>public</scope>
    /// <type>partial class</type>
    /// <inherits><see cref="Form"/></inherits>
    public partial class MainForm : Form
    {
        //This timer is used to control the auto page refresh
        private Timer RefreshTimer { get; set; }

        //This is the reference for the time left till refresh
        private int TimeToRefresh { get; set; }

        //This is the page refresh interval
        private int RefreshMax { get; }

        private Objects.Interfaces.IBusinessLayer BusinessLayer { get; set; }

        /// <summary>
        /// This constructor will initilize all the main components and will start the tweet stream
        /// </summary>
        /// <scope>public</scope>
        public MainForm()
        {
            //system inherited do not remove
            InitializeComponent();
            ConnectionState.Text = "Connecting";
            //set up the page refresh timer
            RefreshMax = Convert.ToInt32(ConfigurationManager.AppSettings["AutoRefreshInterval"]);
            TimeToRefresh = RefreshMax;
            RefreshTimer = new Timer
            {
                Interval = 1000
            };
            RefreshTimer.Tick += RefreshTimer_Tick;
            RefreshTimer.Start();

            //begin the stream consumption
            BusinessLayer = new TweetBusinessLayer();
            BusinessLayer.Run();
            UpdateGrid(true);
            ConnectionState.Text = "Connected";
        }

        //This will handle the timer tick event
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        //This will handle the UI refresh page override click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateGrid(true);
        }

        //This will refresh the UI grid so that the information about the tweets can be updated
        private void UpdateGrid(bool refreshNow = false)
        {
            RefreshTimer.Stop();
            if (TimeToRefresh == 0 || refreshNow)
            {
                gridDisplayFeed.DataSource = BusinessLayer.GetView();
                TimeToRefresh = 0;
            }
            TimeToRefresh = TimeToRefresh == 0 ? RefreshMax : TimeToRefresh - 1;
            RefreshTime.Text = $"{TimeToRefresh} seconds";
            RefreshTimer.Start();
        }

        //This will exit the form/threads and release resources when the X button is clicked on the form
        private void Form_Close(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
