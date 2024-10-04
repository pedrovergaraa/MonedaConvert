﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyConvert.Entities
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CurrencyId { get; set; }

        public string? Legend { get; set; }

        public string? Symbol { get; set; }
        //Simbolo grafico
        public decimal IC { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int? UserId { get; set; }

    }
}
