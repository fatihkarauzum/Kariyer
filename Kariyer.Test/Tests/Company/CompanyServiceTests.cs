using Kariyer.Business.Dtos.CompanyDtos;
using Kariyer.Business.Services;
using NSubstitute;
using Xunit;

namespace Kariyer.Test.Tests.Company;

public class CompanyServiceTests {

    private readonly CompanyService companyService;

    public CompanyServiceTests()
    {
        companyService = Substitute.For<CompanyService>();
    }

    [Fact]
    public async Task Create() {

        string name = "Test";
		string address = "Test Adresi";
		long phone = 05396777163;
        string description = "Test Açıklama";
        string webAddress = "www.test.com";
        int establishmentYear = 2010;

		PostCompanyItem postCompanyItem = new PostCompanyItem() {
            Name = name,
            Address = address, 
            Phone = phone,
            Description = description,
            WebAddress = webAddress,
            EstablishmentYear = establishmentYear
        };

        GetCompanyItem getCompanyItem = new GetCompanyItem();

		getCompanyItem = await companyService.Create(postCompanyItem);

        Assert.NotNull(getCompanyItem);
        Assert.Greater(getCompanyItem.Id, 0);
	}
}
