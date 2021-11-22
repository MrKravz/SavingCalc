using MySql.Data.MySqlClient;
using SavingCalc.IDEntities;
using SavingCalc.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SavingCalc
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            if (File.Exists(Consts.path))
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
                    Text = "Авторизация";
                    label2.Text = "Пароль:";
                    button1.Text = "Войти";
                    button2.Text = "У меня нет аккаунта";
                    button3.Text = "Я забыл пароль";
                    button4.Text = "Назад";
                    settingsToolStripMenuItem.Text = "Настройки";
                    languageToolStripMenuItem.Text = "Язык";
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                User registratedUser = new User(new ID(Convert.ToInt32(textBox1.Text)),default, default, default, Convert.ToString(textBox2.Text));
                SQLConnection db = new SQLConnection();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `ID` = @id AND `Password` = @pass", db.getConnection());
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = registratedUser.ID.IdentifyNumber;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = registratedUser.Password;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    Form5 SavingCalcForm = new Form5(registratedUser.ID.IdentifyNumber);
                    Close();
                    SavingCalcForm.Show();
                }
                else
                {
                    if (Consts.language == "Русский")
                    {
                        MessageBox.Show("Мы не можем найти ваш аккаунт, пожалуйста проверьте введенную информацию или зарегестрируйтесь", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("We can't find your account, please check data or registrate first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                if (Consts.language == "Русский")
                {
                    MessageBox.Show("Вы ввели неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("You enter incorrect data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Fpages.RegistrationForm.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            Fpages.ReceiveForm.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            Fpages.MainForm.Show();
        } 
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text = "Authorization";
            label2.Text = "Password:";
            button1.Text = "Login";
            button2.Text = "I dont have account";
            button3.Text = "I forgot my password";
            button4.Text = "Back";
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
        }
        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text = "Авторизация";
            label2.Text = "Пароль:";
            button1.Text = "Войти";
            button2.Text = "У меня нет аккаунта";
            button3.Text = "Я забыл пароль";
            button4.Text = "Назад";
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
        }
    }
}
