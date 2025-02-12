using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotLeague.Api.Dtos.Requests;

public class LeagueRequestDto
{
	[Required]
	[MaxLength(50)]
	public required string Name { get; set; }

	[Required]
	public required int ReferenceYear { get; set; }

	[Required]
	public required DateOnly StartDate { get; set; }

	[Required]
	public required DateOnly EndDate { get; set; }
}
