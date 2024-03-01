namespace MonedaConvert.Entities
{
    public class Currency
    {
        public string? Code { get; set; }

        public string? Legend { get; set; }
        //Nombre de la moneda cortito, USD, EUR, etc

        public string? Simbol { get; set; }
        //Simbolo grafico

        public decimal IC { get; set; }
        //Indice de convertibilidad
    }
}
