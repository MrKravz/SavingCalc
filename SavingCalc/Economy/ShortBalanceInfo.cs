using SavingCalc.IDEntities;

namespace SavingCalc.Economy
{
    public class ShortBalanceInfo : UserBalance
    {
        public double LastDayPercent { get; private set; } = default;
        public int DaysPassed { get; private set; } = default;
        public ShortBalanceInfo(ID ID,double MainPercent, double Bill, string Currency,double LastDayPercent,int DaysPassed) : base(ID,Bill,Currency)
        {

        }
    }
}
