using MySql.Data.MySqlClient;
using SavingCalc.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SavingCalc
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
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
                    Text = "Восстановление";
                    label1.Text = "Введите Gmail:";
                    label2.Text = "Введите новый пароль:";
                    label3.Text = "Повторите пароль:";
                    label4.Text = "Введите ID:";
                    button1.Text = "Найти";
                    button2.Text = "Поменять пароль";
                    button3.Text = "Назад";
                    settingsToolStripMenuItem.Text = "Настройки";
                    languageToolStripMenuItem.Text = "Язык";
                }
            }
            if (Consts.IsVerified)
            {
                button1.Visible = false;
                button2.Visible = true;
                textBox2.Visible = true;
                label1.Visible = false;
                label2.Visible = true;
                label3.Visible = true;
                label3.Visible = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Consts.ID = Convert.ToInt32(textBox1.Text);
                SQLConnection db = new SQLConnection();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT Gmail FROM `users` WHERE `ID` = @id", db.getConnection());
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = Consts.ID;
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                MailRecovery recovery = new MailRecovery(reader.GetString(0));
                recovery.SendMail(recovery.CreateMessage());
                Consts.recoveryCode = recovery.recoveryCode;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    textBox2.Clear();
                    textBox1.Clear();
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
                    textBox1.Clear();
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
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == textBox2.Text)
            {
                string newPass = Convert.ToString(textBox2.Text);
                SQLConnection db = new SQLConnection();
                MySqlCommand command = new MySqlCommand("UPDATE `users` SET `Password` = @pass WHERE `users`.`ID` = @id;", db.getConnection());
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = Consts.ID;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = newPass;
                db.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    if (Consts.language == "Русский")
                    {
                        MessageBox.Show("Вы успешно сменили пароль.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("You successfully change password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Consts.IsVerified = false;
                    button1.Visible = true;
                    button2.Visible = false;
                    textBox1.Visible = true;
                    textBox2.Visible = false;
                    label1.Visible = true;
                    label2.Visible = false;
                    label3.Visible = false;
                    label3.Visible = true;
                    textBox1.Clear();
                    textBox2.Clear();
                    Hide();
                    Fpages.AuthorizationForm.Show();
                }
                else
                {
                    if (Consts.language == "Русский")
                    {
                        MessageBox.Show("Мы не смогли сменить ваш пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("We can't change your password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    textBox1.Clear();
                    textBox2.Clear();
                }
                db.closeConnection();
            }
            else
            {
                if (Consts.language == "Русский")
                {
                    MessageBox.Show("Мы не смогли сменить ваш пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("We can't change your password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form3 AuthorizationForm = new Form3();
            Close();
            AuthorizationForm.Show();
        }
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text = "Recovery";
            label1.Text = "Enter Gmail:";
            label3.Text = "Enter new password:";
            label2.Text = "Repeat password:";
            label4.Text = "Enter ID:";
            button1.Text = "Find";
            button2.Text = "Reset password";
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
            Text = "Восстановление";
            label1.Text = "Введите Gmail:";
            label3.Text = "Введите новый пароль:";
            label2.Text = "Повторите пароль:";
            label2.Text = "Введите ID:";
            button1.Text = "Найти";
            button2.Text = "Поменять пароль";
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
