namespace FitBack.Models
{
    public class Divida
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Origem { get; set; }
        public decimal ValorTotal { get; set; }
        public int TotalParcelas { get; set; }
        public int ParcelaAtual { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime ProximoVencimento { get; set; }
    }
}
