using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Request;

namespace Validators.Tests
{
    public class RegisterExpenseValidatorTests
    {
        [Fact]
        public void Success()
        {
            //Arrange -> Instanciamos tudo que nosso teste precisará ter acesso
            RegisterExpenseValidator validator = new();
            RequestExpensesJson request = new() 
            { 
                Amount = 100,
                Date = DateTime.Now,
                Description = "RTX 5080",
                Title = "NVidia",
                PaymentType = CashFlow.Communication.Enums.PaymentType.CreditCard
            };

            //Act -> Invocamos a ação que a classe/método sendo validado irá executar
            var result = validator.Validate(request);

            //Assert -> Dizemos o que esperamos ser retornado do teste
            Assert.True(result.IsValid);
        }

    }
}
