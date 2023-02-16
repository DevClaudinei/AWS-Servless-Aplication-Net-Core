using DomainModels.Entities;
using DomainModels.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Models;

public class UpdateVendaRequest
{
    [JsonPropertyName("status")]
    public Status Status { get; init; } = default!;
}