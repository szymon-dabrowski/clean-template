using FluentValidation;
using System.Net.Mail;

namespace Clean.Modules.Crm.Application.Customers.UpdateCustomer;
internal class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.TaxId).NotEmpty();
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.PostalCode).NotEmpty();
        RuleFor(c => c.City).NotEmpty();
        RuleForEach(c => c.Phones).Matches("^\\d{9}$");
        RuleForEach(c => c.Emails).Must(e => MailAddress.TryCreate(e, out var _));
    }
}