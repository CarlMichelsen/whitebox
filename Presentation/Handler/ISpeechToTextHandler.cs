using Microsoft.AspNetCore.Http;
using Presentation.Dto;

namespace Presentation.Handler;

public interface ISpeechToTextHandler
{
    Task<IResult> ToText(SpeechBlob speechBlob);
}