using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotLeague.Infrastructure.Entities;

[Table("leagues")]
public class League
{
	[Key]
	[Column("id")]
	[StringLength(50)]
	public Guid Id { get; set; }

	[Required]
	[Column("name")]
	[StringLength(100)]
	public string Name { get; set; }

	[Required]
	[Column("reference_year")]
	public int ReferenceYear { get; set; }

	[Required]
	[Column("start_date")]
	public DateOnly StartDate { get; set; }

	[Required]
	[Column("end_date")]
	public DateOnly EndDate { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime CreatedAt { get; set; }

	public League()
	{
		Id 		 = Guid.NewGuid();
		Name      = String.Empty;
		CreatedAt = DateTime.UtcNow;
	}
}

