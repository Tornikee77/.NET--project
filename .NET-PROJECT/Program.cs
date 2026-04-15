using _NET_PROJECT.Endpoints;

var buiilder = WebApplication.CreateBuilder(args);  
var app = buiilder.Build();
buiilder.Services.AddValidation();

app.MapGameEndpoints();
app.Run(); 