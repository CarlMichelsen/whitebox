﻿using System.Diagnostics.CodeAnalysis;

namespace Presentation.Dto;

public record ServiceResponse
{
    public ServiceResponse(params List<string> errors)
    {
        this.Errors = errors;
    }

    public bool Ok => this.Errors.Count == 0;

    public List<string> Errors { get; private init; } = [];

    [SuppressMessage("Editor.Rules", "CA1822", Justification = "Should not be static because of json stringify.")]
    public DateTime NowUtc => DateTime.UtcNow;

    public static ServiceResponse<TR> Convert<TR>(ServiceResponse serviceResponse)
    {
        if (serviceResponse.Ok)
        {
            throw new System.Exception(
                "Attempted to convert an error-serviceResponse but it did not contain an error");
        }

        return new ServiceResponse<TR>([.. serviceResponse.Errors]);
    }
}

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "It makes sense to have these close to each other.")]
public record ServiceResponse<T> : ServiceResponse
{
    public ServiceResponse(params List<string> errors)
        : base(errors)
    {
    }

    public ServiceResponse(T value)
    {
        this.Value = value;
    }

    public T? Value { get; private init; }
}