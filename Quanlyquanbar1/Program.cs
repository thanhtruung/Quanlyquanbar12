﻿using Quanlyquanbar1;
using System;
using System.Windows.Forms;

namespace Quanlyquanbar
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
