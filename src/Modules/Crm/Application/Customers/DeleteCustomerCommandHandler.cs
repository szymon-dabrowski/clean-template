﻿using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Dto.Commands.Customers;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MediatR;

namespace Clean.Modules.Crm.Application.Customers;
internal class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(
        DeleteCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetById(request.CustomerId);

        customer?.Delete();

        return Unit.Value;
    }
}