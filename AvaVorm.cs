using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoeAndmebaas
{
    public partial class AvaVorm : Form
    {
        public AvaVorm()
        {
            InitializeComponent();
        }

        private void kaup_btn_Click(object sender, EventArgs e)
        {
            KorviVorm korviVorm = new KorviVorm();
            korviVorm.Show();
        }

        private void ab_btn_Click(object sender, EventArgs e)
        {
            LoginVorm loginVorm = new LoginVorm();
            loginVorm.Show();
        }
    }
}
