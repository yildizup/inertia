﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inertia
{
    public partial class main : Form
    {
        Timer mainTimer;
        public main()
        {
            InitializeComponent();

            #region Timer
            mainTimer = new Timer();
            mainTimer.Tick += new EventHandler(TimerEventProcessor);
            mainTimer.Interval = 7;
            mainTimer.Start();
            #endregion
            this.DoubleBuffered = true;
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {

        }


    }
}
