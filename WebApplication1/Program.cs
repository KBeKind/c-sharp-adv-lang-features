using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


//app.MapGet("/", () => "Minimal API making an API Response");

app.MapGet("/",([FromHeader] string accept) => $"Header: {accept}");

app.Run();
