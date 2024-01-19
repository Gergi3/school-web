// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GiftsApp.ViewModels;

public class ManageRoleViewModel
{
	public Guid RoleId { get; set; }
	public string RoleName { get; set; }
	public CheckboxViewModel[] UserIds { get; set; }
}
