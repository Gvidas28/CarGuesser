using System;

namespace CarGuesser.Model.Entities.Exceptions;

public class InternalException : Exception
{
	public InternalException(string message) : base(message) { }
}