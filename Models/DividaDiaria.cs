namespace FitBack.Models
{
    public class DividaDiaria
    {
        public int Id { get; set; }
        public int IdDividaMensal { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string FormaPagamento { get; set; }
        public bool Entrada { get; set; }
        public bool Saida { get; set; }
    }
}
