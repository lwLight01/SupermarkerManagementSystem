﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SupermarketManagmentSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Application.Run(new Product());
            //Application.Run(new Customer_Dashboard());
            //Application.Run(new Admin_dashboard());
            //Application.Run(new sellerdashboard());
            //Application.Run(new Staffdashboad());
        }
    }
}
