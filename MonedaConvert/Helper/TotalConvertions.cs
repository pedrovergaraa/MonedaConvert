using MonedaConvert.Entities;
using MonedaConvert.Models.Enum;

namespace MonedaConvert.Helper
{
    public class TotalConvertions
    {
        public int GetTotalconvertions(Suscription sub)
        {
            int totalConvertions = 0;

            if (sub == Suscription.Free)
            {
                totalConvertions = 10;
            }
            else if (sub == Suscription.Trial)
            {
                totalConvertions = 100;
            }
            else if (sub == Suscritcion.Pro)
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