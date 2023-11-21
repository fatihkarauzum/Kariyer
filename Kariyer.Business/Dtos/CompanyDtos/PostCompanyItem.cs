using Kariyer.Model.Entities;

namespace Kariyer.Business.Dtos.CompanyDtos;

public class PostCompanyItem {

    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required long Phone { get; set; }
    public string? Description { get; set; }
    public string? WebAddress { get; set; }
    public int? EstablishmentYear { get; set; }
    public List<int> IndustryIds { get; set; } = new List<int>();

    public static Company CreateFromCompanyItem(PostCompanyItem createCompanyItem)
    {

        return new Company
        {
            Id = createCompanyItem.Id,
            Name = createCompanyItem.Name,
            Address = createCompanyItem.Address,
            Phone = createCompanyItem.Phone,
            Description = createCompanyItem.Description,
            WebAddress = createCompanyItem.WebAddress,
            EstablishmentYear = createCompanyItem.EstablishmentYear,
            UpdatedDate = createCompanyItem.Id > 0 ? DateTime.Now : null,
        };
    }
}
