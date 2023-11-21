using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;

namespace Kariyer.Business.Dtos.CompanyDtos;
public class GetCompanyList : BasePagedResult<GetCompanyItem>
{

    public static GetCompanyList CreateFromBasePagedResult(BasePagedResult<Company> basePagedResult)
    {

        return new GetCompanyList
        {
            Items = GetCompanyItem.CreateFromCompany(basePagedResult.Items.ToList()),
            TotalItems = basePagedResult.TotalItems,
            PageNumber = basePagedResult.PageNumber,
            PageSize = basePagedResult.PageSize,
        };
    }
}
