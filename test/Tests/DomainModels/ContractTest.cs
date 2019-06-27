using System;
using Xunit;

namespace Tripstore.DomainModels
{
    public class ContractTest
    {
        [Fact]
        public void ConstructorWithInvalidNameThrows()
        {
            // Arrange
            var invalidName = "";
            var dummyMobileNumber = MobileNumber.Parse("010-1234-1234");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Contract(invalidName, dummyMobileNumber));
        }

        [Theory]
        [InlineData("정진욱")]
        [InlineData("홍길동")]
        public void ConstructorWithValidNameCorrectlyInitializesName(string validName)
        {
            // Arrange
            var dummyMobileNumber = MobileNumber.Parse("010-1234-1234");

            // Act
            var sut = new Contract(validName, dummyMobileNumber);

            // Assert
            string actual = sut.Name;
            Assert.Equal(validName, actual);
        }

        [Fact]
        public void ConstructorWithMobileNumberCorrectlyInitializesMobileNumber()
        {
            // Arrange
            var dummyName = "정진욱";
            var mobileNumber = MobileNumber.Parse("010-1234-1234");

            // Act
            var sut = new Contract(dummyName, mobileNumber);

            // Assert
            MobileNumber actual = sut.MobileNumber;
            Assert.Equal(mobileNumber.Value, actual.Value);
        }
    }
}
