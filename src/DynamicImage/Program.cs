using System;
using System.IO;
using DynamicImage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICounterProvider, InMemoryCounterProvider>();
builder.Services.AddSingleton<DynamicImageGenerator>();

WebApplication app = builder.Build();

app.MapGet("/counter/{id:guid}", (Guid id, string? name) =>
{
    DynamicImageGenerator generator = app.Services.GetRequiredService<DynamicImageGenerator>();
    Stream stream = generator.GenerateImage(id, name);
    return Results.File(stream, "image/png");
});

app.Run();

