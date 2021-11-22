using MySql.Data.MySqlClient;
using SavingCalc.Economy;
using SavingCalc.IDEntities;
using SavingCalc.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Windows.Forms;

namespace SavingCalc
{
    public partial class Form6 : Form
    {
        public const string path = "D:\\SavingCalcData.dat";
        public int userID { get; private set; } = default;
        public Form6(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }
        private void Form6_Load(object sender, EventArgs e)
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
                    label1.Text = "Введите деньги: ";
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
            label1.Text = "Введите деньги: ";
            button1.Text = "Добавить";
            button2.Text = "Назад";
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

        private void button1_Click(object sender, EventArgs e)
        {
            BinaryReader b = new BinaryReader(File.Open(path, FileMode.Open));
            int language = b.ReadInt32();
            b.Close();
            SQLConnection db = new SQLConnection();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand checkCommand = new MySqlCommand("SELECT * FROM `userinfo` WHERE `ID` = @ID", db.getConnection());
            checkCommand.Parameters.Add("@ID", MySqlDbType.VarChar).Value = userID;
            adapter.SelectCommand = checkCommand;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                UserBalance userBalance = new UserBalance(new ID(userID),Convert.ToDouble(textBox1.Text),"BR");
                MySqlCommand fixatedCommand = new MySqlCommand("INSERT INTO `allinformation` (`ID`, `Bill`, `Currency`, `Date`) VALUES (@ID, @Bill, @Currency, @Date)", db.getConnection());
                fixatedCommand.Parameters.Add("@ID", MySqlDbType.Int32).Value = userBalance.ID.IdentifyNumber;
                fixatedCommand.Parameters.Add("@Bill", MySqlDbType.Float).Value = userBalance.Bill;
                fixatedCommand.Parameters.Add("@Currency", MySqlDbType.VarChar).Value = userBalance.Currency;
                fixatedCommand.Parameters.Add("@Date", MySqlDbType.VarChar).Value = userBalance.Date;
                db.openConnection();
                if (fixatedCommand.ExecuteNonQuery() == 1)
                {

                    MySqlCommand billCommand = new MySqlCommand("SELECT `Bill` FROM `userinfo` WHERE `ID` = @ID", db.getConnection());
                    billCommand.Parameters.Add("@ID", MySqlDbType.VarChar).Value = userID;
                    MySqlDataReader reader = billCommand.ExecuteReader();
                    reader.Read();
                    double bill = reader.GetDouble("Bill");
                    MySqlCommand commandUpdate = new MySqlCommand("UPDATE `userinfo` SET `Bill` = @Bill WHERE `userinfo`.`ID` = @id;", db.getConnection());
                    commandUpdate.Parameters.Add("@id", MySqlDbType.Int32).Value = userID;
                    commandUpdate.Parameters.Add("@Bill", MySqlDbType.Int32).Value = bill + Convert.ToDouble(textBox1.Text);
                    db.closeConnection();
                    db.openConnection();
                    if (commandUpdate.ExecuteNonQuery() == 1)
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
                    if (language == 1)
                    {
                        MessageBox.Show($"Вы не поставили цель", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"You ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                db.closeConnection();
            }
            else
            {
                if (language == 1)
                {
                    MessageBox.Show("Мы не можем найти ваш аккаунт, пожалуйста проверьте введенную информацию или зарегестрируйтесь", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("We can't find your account, please check data or registrate first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form5 MainForm = new Form5(userID);
            Close();
            MainForm.Show();
        }
    }
}
