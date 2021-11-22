using MySql.Data.MySqlClient;
using SavingCalc.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SavingCalc
{
    public partial class Form7 : Form
    {
        public const string path = "D:\\SavingCalcData.dat";
        public int userID { get; private set; } = default;
        public Form7(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            BinaryReader b = new BinaryReader(File.Open(path, FileMode.Open));
            int language = b.ReadInt32();
            b.Close();
            SQLConnection db = new SQLConnection();
            if (radioButton1.Checked)
            {
                MySqlCommand fixatedCommand = new MySqlCommand("INSERT INTO `userinfo` (`ID`, `Bill`, `Aim`, `Currency`) VALUES (@ID, @Bill, @Aim, @Currency)", db.getConnection());
                fixatedCommand.Parameters.Add("@ID", MySqlDbType.Int32).Value = userID;
                fixatedCommand.Parameters.Add("@Bill", MySqlDbType.Double).Value = 0;
                fixatedCommand.Parameters.Add("@Aim", MySqlDbType.Double).Value = Convert.ToDouble(textBox1.Text);
                fixatedCommand.Parameters.Add("@Currency", MySqlDbType.VarChar).Value = "BR";
                db.openConnection();
                if (fixatedCommand.ExecuteNonQuery() == 1)
                {
                    if (language == 1)
                    {
                        MessageBox.Show($"Вы успешно зарегестрировали средства", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"You have successfully registered funds", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (language == 1)
                    {
                        MessageBox.Show("Мы не смогли зарегестрировать средства", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("We can't registered your funds", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                db.closeConnection();
                textBox1.Clear();
            }
            else
            {
                MySqlCommand fixatedCommand = new MySqlCommand("INSERT INTO `userinfo` (`ID`, `Bill`, `Aim`, `Currency`) VALUES (@ID, @Bill, @Aim, @Currency)", db.getConnection());
                fixatedCommand.Parameters.Add("@ID", MySqlDbType.Int32).Value = userID;
                fixatedCommand.Parameters.Add("@Bill", MySqlDbType.Double).Value = 0;
                fixatedCommand.Parameters.Add("@Aim", MySqlDbType.Double).Value = Convert.ToDouble(textBox1.Text);
                fixatedCommand.Parameters.Add("@Currency", MySqlDbType.VarChar).Value = "USD";
                db.openConnection();
                if (fixatedCommand.ExecuteNonQuery() == 1)
                {
                    if (language == 1)
                    {
                        MessageBox.Show($"Вы успешно зарегестрировали средства", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"You have successfully registered funds", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (language == 1)
                    {
                        MessageBox.Show("Мы не смогли зарегестрировать средства", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("We can't registered your funds", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                db.closeConnection();
                textBox1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 MainForm = new Form5(userID);
            Close();
            MainForm.Show();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            if (File.Exists(path))
            {
                BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open));
                List<string> data = new List<string>();
                while (binaryReader.PeekChar() > -1)
                {
                    data.Add(binaryReader.ReadString());
                }
                binaryReader.Close();
                if (data.Contains("Русский"))
                {
                    label1.Text = "Поставить цель: ";
                    button1.Text = "Добавить";
                    button2.Text = "Назад";
                    settingsToolStripMenuItem.Text = "Настройки";
                    languageToolStripMenuItem.Text = "Язык";
                }
            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter money: ";
            button1.Text = "Add";
            button2.Text = "Back";
            settingsToolStripMenuItem.Text = "Settings";
            languageToolStripMenuItem.Text = "Language";
            BinaryWriter a = new BinaryWriter(File.Open(path, FileMode.Open));
            a.Write(0);
            a.Close();
        }

        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "Поставить цель: ";
            button1.Text = "Добавить";
            button2.Text = "Назад";
            settingsToolStripMenuItem.Text = "Настройки";
            languageToolStripMenuItem.Text = "Язык";
            BinaryWriter a = new BinaryWriter(File.Open(path, FileMode.Open));
            a.Write(1);
            a.Close();
        }
    }
}
