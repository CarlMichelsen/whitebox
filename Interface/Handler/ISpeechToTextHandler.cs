using Interface.Dto;
using Microsoft.AspNetCore.Http;

namespace Interface.Handler;

public interface ISpeechToTextHandler
{
    Task<IResult> ToText(SpeechBlob speechBlob);
}