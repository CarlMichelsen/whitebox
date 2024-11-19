using Domain.Dto.Hub;

namespace Interface.Hub;

public interface ISpeechToTextServerMethods
{
    Task SendBlob(SpeechBlob speechBlob);
}