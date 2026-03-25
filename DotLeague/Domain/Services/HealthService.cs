using DotLeague.Infrastructure.Data;

namespace DotLeague.Domain.Services;

public class HealthService
{
	private readonly DataContext _context;

	public HealthService(DataContext context)
	{
		_context = context;
	}

	public bool TestDatabaseConnection()
	{
		return _context.IsConnectionAvailable();
	}

}
