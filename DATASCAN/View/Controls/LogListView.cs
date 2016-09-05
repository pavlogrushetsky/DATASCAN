using System.Windows.Forms;

namespace DATASCAN.View.Controls
{
    public partial class LogListView : ListView
    {
        public LogListView()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }

        protected sealed override bool DoubleBuffered
        {
            get { return base.DoubleBuffered; }
            set { base.DoubleBuffered = value; }
        }
    }
}
