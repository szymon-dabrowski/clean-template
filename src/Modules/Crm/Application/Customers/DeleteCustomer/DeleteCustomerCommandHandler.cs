﻿using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MediatR;

namespace Clean.Modules.Crm.Application.Customers.DeleteCustomer;
internal class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public async Task Handle(
        DeleteCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetById(
            new CustomerId(request.CustomerId));

        customer?.Delete();
    }
}