using System.ComponentModel.DataAnnotations;

namespace Stock_trading_2.Models
{
    public class Aksje
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Aksjenavn { get; set; }

        [RegularExpression(@"^[0-9.,]{1,20}$")]
        public string Pris { get; set; }
        
        [RegularExpression(@"^[0-9]{1,20}$")]
        public string Antall { get; set; }

        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Fornavn { get; set; }
        
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Etternavn { get; set; }
    }
}
