namespace FitBack.Models
{
    public class DividaMensal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateOnly Data { get; set; }
        public decimal Total { get; set; }
        public decimal Entrada { get; set; }
        public decimal Saida { get; set; }
        public List<DividaDiaria> DividaDiaria { get; set; }

    }
}
