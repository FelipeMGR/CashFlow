using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Request;
using CashFlow.Exceptions;
using CommonUseTests.Requests;
using FluentAssertions;

namespace Validators.Tests
{
    public class RegisterExpenseValidatorTests
    {
        [Fact]
        public void Success()
        {
            //Arrange -> Instanciamos tudo que nosso teste precisará ter acesso
            RegisterExpenseValidator validator = new();
            RequestExpensesJson request = RequestExpensesJsonBuilder.Build();

            //Act -> Invocamos a ação que a classe/método sendo validado irá executar
            var result = validator.Validate(request);

            //Assert -> Dizemos o que esperamos ser retornado do teste
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ErrorOnTitle()
        {
            //Arrange -> Instanciamos tudo que nosso teste precisará ter acesso
            RegisterExpenseValidator validator = new();
            RequestExpensesJson request = RequestExpensesJsonBuilder.Build();
            request.Title = string.Empty;

            //Act -> Invocamos a ação que a classe/método sendo validado irá executar
            var result = validator.Validate(request);

            //Assert -> Dizemos o que esperamos ser retornado do teste
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourcesErrorMessages.TITLE_REQUIRED));
        }

        [Fact]
        public void ErrorOnDateTime()
        {
            //Arrange -> Instanciamos tudo que nosso teste precisará ter acesso
            RegisterExpenseValidator validator = new();
            RequestExpensesJson request = RequestExpensesJsonBuilder.Build();
            request.Date = DateTime.UtcNow.AddDays(1);

            //Act -> Invocamos a ação que a classe/método sendo validado irá executar
            var result = validator.Validate(request);

            //Assert -> Dizemos o que esperamos ser retornado do teste
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourcesErrorMessages.DATETIME_ERROR));
        }

        [Fact]
        public void ErrorOnPaymentType()
        {
            RegisterExpenseValidator validator = new();
            RequestExpensesJson request = RequestExpensesJsonBuilder.Build();
            request.PaymentType = (PaymentType)7;

            //Act -> Invocamos a ação que a classe/método sendo validado irá executar
            var result = validator.Validate(request);

            //Assert -> Dizemos o que esperamos ser retornado do teste
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourcesErrorMessages.PAYMENTTYPE_ERROR));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void ErrorOnAmount(decimal amount)
        {
            //Arrange -> Instanciamos tudo que nosso teste precisará ter acesso
            RegisterExpenseValidator validator = new();
            RequestExpensesJson request = RequestExpensesJsonBuilder.Build();
            request.Amount = amount;

            //Act -> Invocamos a ação que a classe/método sendo validado irá executar
            var result = validator.Validate(request);

            //Assert -> Dizemos o que esperamos ser retornado do teste
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourcesErrorMessages.AMOUNT_LOWER_THAN_0));
        }
    }
}
