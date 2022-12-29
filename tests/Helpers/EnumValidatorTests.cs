using Kudos.Domain.Exceptions;
using Kudos.Services.Validators.Helpers;
using static Kudos.Services.Services.KudoService;

namespace Tests.Helpers
{
    public class EnumValidatorTests
    {

        [Fact]
        public void GetEnumValueByDescription_DidNotGetData_KudoNotFoundException()
        {
            //result 
            Assert.Throws<KudoReasonDescriptionNotFoundException>(() => EnumValidator.GetEnumValueByDescription<Reason>(""));
        }

        [Fact]
        public void GetEnumValueByDescription_GivenReasonString_ReturnsResult()
        {
            //act 
            var result = EnumValidator.GetEnumValueByDescription<Reason>("Team Player");

            //result
            Assert.NotNull(result);
            Assert.Equal("Team Player", result);
        }
        [Fact]
        public void GetEnumDescription_GivenEnum_ReturnResult() 
        {
            //act 
            var result = EnumValidator.GetEnumDescription(Reason.TeamPlayer);

            //result
            Assert.NotNull(result);
            Assert.Equal("Team Player", result);

        }
    }
}
