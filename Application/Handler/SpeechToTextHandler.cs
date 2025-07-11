﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Presentation.Dto;
using Presentation.Handler;

namespace Application.Handler;

public class SpeechToTextHandler(
    ILogger<SpeechToTextHandler> logger) : ISpeechToTextHandler
{
    private const string LoremIpsum = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
    
    public Task<IResult> ToText(SpeechBlob speechBlob)
    {
        var res = new SpeechToTextResponse(
            Identifier: speechBlob.Identifier,
            Text: LoremIpsum);
        
        logger.LogInformation(
            "Received {ByteCount} bytes of speech-data and responded with: {Response}",
            speechBlob.Data.Length,
            res);
        
        return Task.FromResult(Results.Ok(res));
    }
}