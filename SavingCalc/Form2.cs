using System;
using System.IO;
using SavingCalc.Tools;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SavingCalc.IDEntities;
using System.Data;
using System.Collections.Generic;

namespace SavingCalc
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
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
                    Text = "Регистрация";
                    label1.Text = "Имя:";
                    label2.Text = "Фамилия:";
                    label3.Text = "Gmail:";
                    label4.Text = "Пароль:";
                    label5.Text = "Повторите пароль:";
                    button1.Text = "Зарегестрироваться";
                    button2.Text = "У меня уже есть аккаунт";
                    button3.Text = "Назад";
                    settingsToolStripMenuItem.Text = "Настройки";
                    languageToolStripMenuItem.Text = "Язык";
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            BinaryReader b = new BinaryReader(File.Open(Consts.path, FileMode.Open));
            int language = b.ReadInt32();
            b.Close();
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    if (language == 1)
                    {
                        MessageBox.Show("Вы не заполнили одно из полей", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("You have not filled one of the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (textBox4.Text == textBox5.Text)
                {
                    User registeringUser = new User(new ID(),Convert.ToString(textBox1.Text), Convert.ToString(textBox2.Text), Convert.ToString(textBox3.Text), Convert.ToString(textBox4.Text));
                    SQLConnection db = new SQLConnection();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand checkCommand = new MySqlCommand("SELECT * FROM `users` WHERE `Name` = @name AND `Surname` = @surname AND `Gmail` = @gmail", db.getConnection());
                    checkCommand.Parameters.Add("@name", MySqlDbType.VarChar).Value = registeringUser.Name;
                    checkCommand.Parameters.Add("@surname", MySqlDbType.VarChar).Value = registeringUser.Surname;
                    checkCommand.Parameters.Add("@gmail", MySqlDbType.VarChar).Value = registeringUser.Gmail;
                    adapter.SelectCommand = checkCommand;
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        if (language == 1)
                        {
                            MessageBox.Show("Этот пользователь уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("This user is already registrated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                    }
                    else
                    {
                        MySqlCommand registrateCommand = new MySqlCommand("INSERT INTO `users` (`ID`, `Name`, `Surname`, `Gmail`, `Password`, `Date`) VALUES (@ID, @Name, @Surname, @Gmail, @Password, @Date)", db.getConnection());
                        registrateCommand.Parameters.Add("@ID", MySqlDbType.Int32).Value = registeringUser.ID.IdentifyNumber;
                        registrateCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = registeringUser.Name;
                        registrateCommand.Parameters.Add("@Surname", MySqlDbType.VarChar).Value = registeringUser.Surname;
                        registrateCommand.Parameters.Add("@Gmail", MySqlDbType.VarChar).Value = registeringUser.Gmail;
                        registrateCommand.Parameters.Add("@Password", MySqlDbType.VarChar).Value = registeringUser.Password;
                        registrateCommand.Parameters.Add("@Date", MySqlDbType.VarChar).Value = registeringUser.Date;
                        db.openConnection();
                        if (registrateCommand.ExecuteNonQuery() == 1)
                        {
                            if (language == 1)
                            {
                                MessageBox.Show($"Вы успешно зарегестрировались, ваш ID:{registeringUser.ID.IdentifyNumber}, пожалуйста запомните его.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"You have successfully registered, your ID:{registeringUser.ID.IdentifyNumber}, please remember it.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            if (language == 1)
                            {
                                MessageBox.Show("Мы не смогли зарегестрировать вас", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("We could not register you", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        db.closeConnection();
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                    }
                }
                else
                {
                    if (language == 1)
                    {
                        MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Passwords are not equal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    textBox4.Clear();
                    textBox5.Clear();
                }
            }
            catch (Exception ex)
            {
                if (Consts.language == "Русский")
                {
                    MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {

            Fpages.AuthorizationForm.Show();
            Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Fpages.MainForm.Show();
            Hide();
        }
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text = "Registration";
            label1.Text = "Name:";
            label2.Text = "Surname:";
            label3.Text = "Gmail:";
            label4.Text = "Password:";
            label5.Text = "Repeat password:";
            button1.Text = "Registrate";
            button2.Text = "I alredy have account";
            button3.Text = "Back";
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
            Text = "Регистрация";
            label1.Text = "Имя:";
            label2.Text = "Фамилия:";
            label3.Text = "Gmail:";
            label4.Text = "Пароль:";
            label5.Text = "Повторите пароль:";
            button1.Text = "Зарегестрироваться";
            button2.Text = "У меня уже есть аккаунт";
            button3.Text = "Назад";
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
