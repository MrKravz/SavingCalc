using System;

namespace SavingCalc.IDEntities
{
    public class ID
    {
        public int IdentifyNumber { get; private set; } = default;
        public ID()
        {
            Random rnd = new Random();
            IdentifyNumber = rnd.Next(0,10000);
        }
        public ID(int IdentifyNumber)
        {
            this.IdentifyNumber = IdentifyNumber;
        }
    }
}
