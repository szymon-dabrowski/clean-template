﻿namespace Clean.Web.Dto.Crm.Customers.Requests;
public record CreateCustomerRequest(
    string Name,
    string TaxId,
    string Address,
    string PostalCode,
    string City,
    List<string> Phones,
    List<string> Emails);