using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Request;
using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromServices] IRegisterExpenseUseCase useCase, RequestExpensesJson request)
        {
            ResponseRegisteredExpenseJson response = useCase.Execute(request);

            return Created(string.Empty, response);
        }
    }
}
