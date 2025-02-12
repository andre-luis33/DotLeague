using System;

namespace DotLeague.Domain.Exceptions;

public class ServiceException : Exception
{
	public ServiceException(string message) : base(message) { }

	public ServiceException(string message, Exception innerException): base(message, innerException) { }
}
