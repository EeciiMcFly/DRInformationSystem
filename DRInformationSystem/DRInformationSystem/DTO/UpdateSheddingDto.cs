using DataAccessLayer.Models;

namespace DRInformationSystem.DTO;

public class UpdateSheddingDto
{
	public long Id {get; set; }
	public SheddingState Status { get; set; }
}