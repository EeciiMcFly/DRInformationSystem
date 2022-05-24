using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

[Index(nameof(Login))]
public class AggregatorModel
{
	public long Id { get; set; }

	public string Login { get; set; }

	public string PasswordHash { get; set; }
}