using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

[Index(nameof(Id))]
public class ConsumerModel
{
	public long Id { get; set; }

	public string Login { get; set; }

	public string PasswordHash { get; set; }
}