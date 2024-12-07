namespace Domain.Exception;

public class IncompletePromptEntityException(string message) : System.Exception(message);