// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CompanyManagerApp.Models.ViewModels;

public class EditEmployeeViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid DepartmentId { get; set; }
}
