using Clean.Web.Dto.Crm.Customers.Model;

namespace Clean.Web.Dto.Crm.Customers.Responses;
public record GetCustomersResponse(List<CustomerDto> Customers);