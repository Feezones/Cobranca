using Bogus;
using FitBack.Models;

namespace FitBack.Testes
{
    public class DividaTest
    {
        public static void Main()
        {
            var faker = new Faker<Divida>("pt_BR")
                .RuleFor(p => p.Nome, f => f.Name.FullName())
                .RuleFor(p => p.Origem, f => f.Finance.CreditCardCvv())
                .RuleFor(p => p.ValorParcela, f => f.Finance.Amount())
                .RuleFor(p => p.DataPagamento, f => f.Date.Past());

            var pessoaFake = faker.Generate();
            Console.WriteLine($"Nome: {pessoaFake.Nome}");
            Console.WriteLine($"Origem: {pessoaFake.Origem}");
            Console.WriteLine($"ValorParcela: {pessoaFake.ValorParcela}");
            Console.WriteLine($"DataPagamento: {pessoaFake.DataPagamento}");
        }
    }
}
