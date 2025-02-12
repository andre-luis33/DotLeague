using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotLeague.Api.Dtos.Requests;

public class TeamRequestDto
{
	[Required]
	[MaxLength(50)]
	public required string Name { get; set; }

	[Required]
	[MaxLength(100)]
	public required string State { get; set; }

	[Required]
	[MaxLength(100)]
	public required string City { get; set; }

	[Required]
	[MaxLength(100)]
	public required string Stadium { get; set; }
}
