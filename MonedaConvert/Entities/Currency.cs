﻿namespace MonedaConvert.Entities
{
    public class Currency
    {
        public string? Id { get; set; }

        public string? Name { get; set; }
        //Nombre de la moneda cortito, USD, EUR, etc

        public string? Symbol { get; set; }
        //Simbolo grafico

        public decimal? IC { get; set; }
        //Indice de convertibilidad
        //El índice de convertibilidad será la
        //relación que existe entre una moneda y el dólar americano expresada en cuanto vale
        //una unidad de dicha moneda en comparación a 1 usd.

        public int? Value { get; set; }  
        //Valor de la moneda

    }
}
