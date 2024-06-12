using MonedaConvert.Entities;
using MonedaConvert.Models.Enum;

namespace MonedaConvert.Helper
{
    public class TotalConvertions
    {
        public int GetTotalconvertions(Subscription sub)
        {
            int totalConvertions = 0;

            if (sub == Subscription.Free)
            {
                totalConvertions = 10;
            }
            else if (sub == Subscription.Trial)
            {
                totalConvertions = 100;
            }
            else if (sub == Subscription.Pro)
            {
                totalConvertions = -1;
            }
            else
            {
                totalConvertions = 0;
            }
            return totalConvertions;
        }
    }
}