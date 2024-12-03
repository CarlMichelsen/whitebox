namespace Domain.Exception;

public class ItemNotFoundException(string message) : System.Exception(message);