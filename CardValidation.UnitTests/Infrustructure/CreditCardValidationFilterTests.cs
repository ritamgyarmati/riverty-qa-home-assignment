using CardValidation.Core.Services.Interfaces;
using CardValidation.Infrustructure;
using CardValidation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace CardValidation.UnitTests.Infrustructure
{
    public  class CreditCardValidationFilterTests
    {
        private CreditCard creditCard = new CreditCard()
        {
            Owner = "John Doe",
            Date = "03/27",
            Cvv = "456",
            Number = "4111111111111111"
        };
        
        [Fact]
        public void VerifyOnActionExecutingShouldNotReturnErrorWithAllValidFields()
        {
            var mockService = new Mock<ICardValidationService>();
            mockService.Setup(s => s.ValidateOwner(It.IsAny<string>())).Returns(true);
            mockService.Setup(s => s.ValidateNumber(It.IsAny<string>())).Returns(true);
            mockService.Setup(s => s.ValidateIssueDate(It.IsAny<string>())).Returns(true);
            mockService.Setup(s => s.ValidateCvc(It.IsAny<string>())).Returns(true);

            var filter = new CreditCardValidationFilter(mockService.Object);
        
            var executingContext = CreateActionExecutingContext(new Dictionary<string, object?>
            {
                { "creditCard", creditCard }
            });

            filter.OnActionExecuting(executingContext);

            Assert.True(executingContext.ModelState.IsValid);
        }

        [Fact]
        public void VerifyOnActionExecutingShouldReturnErrorWhenAllDataEmpty()
        {
            var mockService = new Mock<ICardValidationService>();
            mockService.Setup(s => s.ValidateOwner(It.IsAny<string>())).Returns(true);
            mockService.Setup(s => s.ValidateNumber(It.IsAny<string>())).Returns(true);
            mockService.Setup(s => s.ValidateIssueDate(It.IsAny<string>())).Returns(true);
            mockService.Setup(s => s.ValidateCvc(It.IsAny<string>())).Returns(true);

            var filter = new CreditCardValidationFilter(mockService.Object);


            var card = new CreditCard()
            {
                Owner = "",
                Date = "",
                Cvv = "",
                Number = ""
            };

            var executingContext = CreateActionExecutingContext(new Dictionary<string, object?>
            {
                { "creditCard", card }
            });

            filter.OnActionExecuting(executingContext);

            Assert.False(executingContext.ModelState.IsValid);
        }

        [Fact]
        public void VerifyOnActionExecutingShouldReturnErrorWithOneInvalidParameter()
        {
            var mockService = new Mock<ICardValidationService>();
            mockService.Setup(s => s.ValidateOwner(It.IsAny<string>())).Returns(true);
            mockService.Setup(s => s.ValidateNumber(It.IsAny<string>())).Returns(true);
            mockService.Setup(s => s.ValidateIssueDate(It.IsAny<string>())).Returns(true);
            mockService.Setup(s => s.ValidateCvc(It.IsAny<string>())).Returns(false);

            var filter = new CreditCardValidationFilter(mockService.Object);

            var executingContext = CreateActionExecutingContext(new Dictionary<string, object?>
            {
                { "creditCard", creditCard }
            });

            filter.OnActionExecuting(executingContext);

            Assert.False(executingContext.ModelState.IsValid);
        }

        [Fact]
        public void VerifyOnActionExecutingThrowsAnExceptionWhenCreditCardIsNull()
        {
            var mockService = new Mock<ICardValidationService>();
            var filter = new CreditCardValidationFilter(mockService.Object);

            var context = CreateActionExecutingContext(new Dictionary<string, object?>
            {
                { "creditCard", null }
            });

            Assert.Throws<InvalidOperationException>(() => filter.OnActionExecuting(context));
        }

        private static ActionExecutingContext CreateActionExecutingContext(IDictionary<string, object?> arguments)
        {
            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor(),
                new ModelStateDictionary()
            );

            return new ActionExecutingContext(actionContext, new List<IFilterMetadata>(), arguments, null!);
        }
    }
}
