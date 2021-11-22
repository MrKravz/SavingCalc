using System;

namespace SavingCalc.IDEntities
{
    public class User
    {
        public ID ID { get; private set; } = default;
        public string Name { get; private set; } = default;
        public string Surname { get; private set; } = default;
        public string Gmail { get; private set; } = default;
        public string Password { get; private set; } = default;
        public string Date { get; private set; } = default;
        public User(ID ID, string Name, string Gmail, string Surname, string Password)
        {
            this.ID = ID;
            this.Name = Name;
            this.Surname = Surname;
            this.Gmail = Gmail;
            this.Password = Password;
            Date = DateTime.Now.ToShortDateString();

        }
    }
}
