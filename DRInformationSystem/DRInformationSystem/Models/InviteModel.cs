using Microsoft.EntityFrameworkCore;

namespace DRInformationSystem.Models;

[Index(nameof(Code))]
public class InviteModel
{
	public long Id { get; set; }

	public string Code { get; set; }

	public bool IsActivated { get; set; }
}