using CSharpApp.Application.Implementations;
using CSharpApp.Core.Interfaces;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class TokenStorageUnitTests
    {
        public TokenStorageUnitTests() { }

        [Fact]
        public void TokenStorage_WhenTimePass_TokenIsNotValid()
        {
            //Arrange
            var mockatetimeprovider = new Mock<IDateTimeProvider>();
            var counter = 0;
            mockatetimeprovider.Setup(d => d.Now).Returns(() => { counter++; return counter == 1 ? DateTime.Now : DateTime.Now.AddDays(20);});
            var tokenstorage = new TokenStorage(mockatetimeprovider.Object);
            tokenstorage.SaveToken("aatoken");//we are saving right now

            //Act
            var result = tokenstorage.IsTokenValid();//after 21 days is still valid?

            //Assert
            result.Should().BeFalse();
        }
    }
}
