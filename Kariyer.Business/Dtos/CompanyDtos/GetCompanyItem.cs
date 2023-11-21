using Kariyer.Common.Utils;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;

namespace Kariyer.Business.Dtos.CompanyDtos;

public class GetCompanyItem : BaseDto<GetCompanyItem>
{

    public string Name { get; set; }
    public string Address { get; set; }
    public long Phone { get; set; }
    public int? RemainingPublishRight { get; set; }
    public string? Description { get; set; }
    public string? WebAddress { get; set; }
    public int? EstablishmentYear { get; set; }

    public static GetCompanyItem CreateFromCompany(Company company)
    {

        return new GetCompanyItem
        {
            Id = company.Id,
            Name = company.Name,
            Address = company.Address,
            Phone = company.Phone,
            RemainingPublishRight = company.RemainingPublishRight,
            Description = company.Description,
            WebAddress = company.WebAddress,
            EstablishmentYear = company.EstablishmentYear,
			IsActive = company.IsActive,
			CreatedDate = company.CreatedDate.SetKindLocal(),
            UpdatedDate = company.UpdatedDate.SetKindLocal(),
        };
    }

	public static List<GetCompanyItem> CreateFromCompany(List<Company> companies)
    {

        return companies.Select(CreateFromCompany).ToList();
    }
}
