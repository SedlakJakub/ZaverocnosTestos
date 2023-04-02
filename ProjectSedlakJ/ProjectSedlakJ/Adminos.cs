using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectSedlakJ
{
    public partial class Adminos : Form
    {
        public Adminos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PracantosEditos PracantosEditos = new PracantosEditos();
            PracantosEditos.FormClosed += (s, args) => this.Close();
            PracantosEditos.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Contractos Contractos = new Contractos();
            Contractos.FormClosed += (s, args) => this.Close();
            Contractos.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Zamestnanos Zamestnanos = new Zamestnanos();
            Zamestnanos.FormClosed += (s, args) => this.Close();
            Zamestnanos.Show();
        }
    }
}
