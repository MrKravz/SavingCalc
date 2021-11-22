using System;
using System.Windows.Forms;

namespace SavingCalc
{
    public partial class Form8 : Form
    {
        public const string path = "D:\\SavingCalcData.dat";
        public int userID;
        public Form8(int IdentifyNumber)
        {
            InitializeComponent();
            userID = IdentifyNumber;

        }
        private void Form8_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 mainForm = new Form5(userID);
            mainForm.Show();
            Close();
        }
    }
}
