using SavingCalc.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SavingCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(Consts.path))//Перевод при запуске если до этого пользователь уже устанавливал язык 
            {
                BinaryReader binaryReader = new BinaryReader(File.Open(Consts.path, FileMode.Open));
                List<string> data = new List<string>();
                while (binaryReader.PeekChar() > -1)
                {
                    data.Add(binaryReader.ReadString());
                }
                binaryReader.Close();
                if (data.Contains("Русский"))
                {
                    button1.Text = "Регистрация";
                    button2.Text = "Авторизация";
                    button3.Text = "Выход";
                    settingsToolStripMenuItem.Text = "Настройки";
                    languageToolStripMenuItem.Text = "Язык";
                    Consts.language = "Русский";
                }
            }

            Fpages.MainForm = this; //пуллинг главных страниц для оптимизации дальнейшего пользования
            Fpages.RegistrationForm = new Form2(); //пуллинг главных страниц для оптимизации дальнейшего пользования
            Fpages.AuthorizationForm = new Form3();
            Fpages.ReceiveForm = new Form4();
            Fpages.RegistrationForm.Hide();
            Fpages.AuthorizationForm.Hide();
            Fpages.ReceiveForm.Hide();
        } 
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "Registration";
            button2.Text = "Authorization";
            button3.Text = "Exit";
            settingsToolStripMenuItem.Text = "Settings";
            languageToolStripMenuItem.Text = "Language";
            if (File.Exists(Consts.path))
            {
                BinaryReader binaryReader = new BinaryReader(File.Open(Consts.path, FileMode.Open));
                List<string> data = new List<string>();
                while (binaryReader.PeekChar() > -1)
                {
                    data.Add(binaryReader.ReadString());
                }
                binaryReader.Close();
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i] == "Русский")
                    {
                        data[i] = "English";
                    }
                }
                BinaryWriter binaryWriter = new BinaryWriter(File.Open(Consts.path, FileMode.OpenOrCreate));
                foreach (var item in data)
                {
                    binaryWriter.Write(item);
                }
                Consts.language = "English";
                binaryWriter.Close();
            }
        } // Перевод на английский
        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "Регистрация";
            button2.Text = "Авторизация";
            button3.Text = "Выход";
            settingsToolStripMenuItem.Text = "Настройки";
            languageToolStripMenuItem.Text = "Язык";
            if (File.Exists(Consts.path))
            {
                BinaryReader binaryReader = new BinaryReader(File.Open(Consts.path, FileMode.Open));
                List<string> data = new List<string>();
                while (binaryReader.PeekChar() > -1)
                {
                    data.Add(binaryReader.ReadString());
                }
                binaryReader.Close();
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i] == "English")
                    {
                        data[i] = "Русский";
                    }
                }
                BinaryWriter binaryWriter = new BinaryWriter(File.Open(Consts.path, FileMode.OpenOrCreate));
                foreach (var item in data)
                {
                    binaryWriter.Write(item);
                }
                Consts.language = "Русский";
                binaryWriter.Close();
            }
        } // Перевод на русский
        private void button1_Click(object sender, EventArgs e) 
        {
            Hide();
            Fpages.RegistrationForm.Show();
        } // показываем окно регистрации
        private void button2_Click(object sender, EventArgs e) 
        {
            Hide();
            Fpages.AuthorizationForm.Show();
        } // показываем окно авторизации
        private void button3_Click(object sender, EventArgs e) 
        {
            Application.Exit();
        } // выход из приложения
    }
}
