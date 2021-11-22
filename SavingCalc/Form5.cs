using SavingCalc.IDEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SavingCalc
{
    public partial class Form5 : Form
    {
        public const string path = "D:\\SavingCalcData.dat";
        public int userID;
        public Form5(int IdentifyNumber)
        {
            InitializeComponent();
            userID = IdentifyNumber;
        }
        private void Form5_Load(object sender, EventArgs e)
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
                    button1.Text = "Поставить цель";
                    button2.Text = "Добавить информацию";
                    button4.Text = "Посмотреть информацию";
                    button3.Text = "Выйти";
                    settingsToolStripMenuItem.Text = "Настройки";
                    languageToolStripMenuItem.Text = "Язык";
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form7 Form = new Form7(userID);
            Close();
            Form.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form6 SavingCalcAddInformationForm = new Form6(userID);
            Close();
            SavingCalcAddInformationForm.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form1 Form = new Form1();
            Close();
            Form.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form8 SavingCalcAddInformationForm = new Form8(userID);
            Close();
            SavingCalcAddInformationForm.Show();
        }
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "Direct aim";
            button2.Text = "Add data";
            button4.Text = "Check information";
            button3.Text = "Quit";
            settingsToolStripMenuItem.Text = "Settings";
            languageToolStripMenuItem.Text = "Language";
            int count = default;
            if (File.Exists(path))
            {
                BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open));
                List<string> data = new List<string>();
                while (binaryReader.PeekChar() > -1)
                {
                    data.Add(binaryReader.ReadString());
                }
                count = data.Count;
                binaryReader.Close();
            }
            File.Delete(path);
            if (count < 2)
            {
                BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));
                binaryWriter.Write("English");
                binaryWriter.Close();
            }
            else
            {
                BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.OpenOrCreate));
                List<string> data = new List<string>();
                while (binaryReader.PeekChar() > -1)
                {
                    data.Add(binaryReader.ReadString());
                }
                BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));
                binaryWriter.Write("English");
                binaryWriter.Write(data.Find(x => x != "English"));
                binaryWriter.Close();
            }

        }
        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "Поставить цель";
            button2.Text = "Добавить информацию";
            button4.Text = "Посмотреть информацию";
            button3.Text = "Выйти";
            settingsToolStripMenuItem.Text = "Настройки";
            languageToolStripMenuItem.Text = "Язык";
            int count = default;
            if (File.Exists(path))
            {
                BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open));
                List<string> data = new List<string>();
                while (binaryReader.PeekChar() > -1)
                {
                    data.Add(binaryReader.ReadString());
                    count++;
                }
                binaryReader.Close();
            }
            File.Delete(path);
            if (count < 2)
            {
                BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));
                binaryWriter.Write("Русский");
                binaryWriter.Close();
            }
            else
            {
                BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.OpenOrCreate));
                List<string> data = new List<string>();
                while (binaryReader.PeekChar() > -1)
                {
                    data.Add(binaryReader.ReadString());
                }
                BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));
                binaryWriter.Write("Русский");
                binaryWriter.Write(data.Find(x => x != "Русский"));
                binaryWriter.Close();
            }
        }
    }
}
