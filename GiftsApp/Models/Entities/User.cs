// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Identity;

namespace GiftsApp.Models.Entities;

public class User : IdentityUser<Guid>
{
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public int Age { get; set; }
}
