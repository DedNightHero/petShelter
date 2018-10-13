using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
            int tabWidth = tabMain.Width / tabMain.TabPages.Count - 1;
            tabMain.ItemSize = new Size(tabWidth, tabMain.ItemSize.Height);
        }

    }
}
