using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SliceCloud.Service.Utils;

public static class ToastExtensions
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static void SetToast(
        this ITempDataDictionary tempData,
        string type,
        string message)
    {
        tempData["Toast"] = JsonSerializer.Serialize(
            new
            {
                type,
                message
            },
            _jsonOptions
        );
    }
}