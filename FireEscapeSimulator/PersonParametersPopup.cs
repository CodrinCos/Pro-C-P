using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FireEscapeSimulator
{
    public partial class PersonParametersPopup : Form
    {
        public int FormHealth, FormSpeed, FormPanic = 0;

        public PersonParametersPopup(int health, int speed, int panic)
        {
            InitializeComponent();
            this.healthUpDown.Value = health;
            this.speedUpDown.Value = speed;
            this.panicUpDown.Value = panic;

            FormHealth = health;
            FormSpeed = speed;
            FormPanic = panic;
        }

        private void healthUpDown_ValueChanged(object sender, EventArgs e)
        {
            FormHealth = ((int)healthUpDown.Value);
        }

        private void speedUpDown_ValueChanged(object sender, EventArgs e)
        {
            FormSpeed = ((int)speedUpDown.Value);

        }

        private void panicUpDown_ValueChanged(object sender, EventArgs e)
        {
            FormPanic = ((int)panicUpDown.Value);

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
