var buiilder = WebApplication.CreateBuilder(args);  
var app = buiilder.Build();

app.MapGet("/", () => "Hello World!");
app.Run();