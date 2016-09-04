using System;
using System.Windows.Forms;
using DATASCAN.Context;

namespace DATASCAN.View
{
    public partial class DATASCAN : Form
    {
        public DATASCAN()
        {
            InitializeComponent();

            DataContext context = new DataContext();
          
        }
    }
}
