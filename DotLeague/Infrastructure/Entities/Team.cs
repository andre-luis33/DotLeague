using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotLeague.Infrastructure.Entities;

[Table("teams")]
public class Team
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
	[Column("state")]
	[StringLength(100)]
	public string State { get; set; }

	[Required]
	[Column("city")]
	[StringLength(100)]
	public string City { get; set; }

	[Required]
	[Column("stadium")]
	[StringLength(100)]
	public string Stadium { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime CreatedAt { get; set; }

	public Team()
	{
		Id 		 = Guid.NewGuid();
		Name      = String.Empty;
		State     = String.Empty;
		City      = String.Empty;
		Stadium   = String.Empty;
		CreatedAt = DateTime.UtcNow;
	}
}

