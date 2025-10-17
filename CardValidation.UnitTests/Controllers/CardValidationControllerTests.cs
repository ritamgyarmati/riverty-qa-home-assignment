

using CardValidation.Controllers;
using CardValidation.Core.Enums;
using CardValidation.Core.Services.Interfaces;
using Moq;

namespace CardValidation.UnitTests.Controllers
{
    //Did not had time to cover current CardValidationController with unit tests, but added some basics as reference point
    public  class CardValidationControllerTests
    {

        [Fact]
        public void verifyCardValidationControllerShouldReturnBadRequestWhenModelStateIsInvalid()
        {
            var mockService = new Mock<ICardValidationService>();
            var controller = new CardValidationController(mockService.Object);
        }

        [Fact]
        public void verifyCardValidationControllerShouldReturnBadRequestWhenModelStateIsValid()
        {
            var mockService = new Mock<ICardValidationService>();
            mockService.Setup(s => s.GetPaymentSystemType(It.IsAny<string>())).Returns(PaymentSystemType.Visa);

            var controller = new CardValidationController(mockService.Object);
        }
    }
}
