using SavingCalc.IDEntities;
using System;

namespace SavingCalc.Economy
{
    public class UserBalance
    {
        public ID ID { get; private set; } = default;
        public double Bill { get; private set; } = default;
        public string Currency { get; private set; } = default;
        public string Date { get; private set; } = default;
        public UserBalance(ID ID,double Bill, string Currency)
        {
            this.ID = ID;
            this.Bill = Bill;
            this.Currency = Currency;
            Date = Convert.ToString(DateTime.Now.ToShortDateString());
            //DateTime dateTime = DateTime.UtcNow.Date;
            //this.DayMonth = Convert.ToDouble(dateTime.ToString("dd.MM"));
            //this.Year = Convert.ToInt32(dateTime.ToString("yyyy"));
        }
    }
}
