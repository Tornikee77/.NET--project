using _NET_PROJECT.Endpoints;

var buiilder = WebApplication.CreateBuilder(args);  
var app = buiilder.Build();

app.MapGameEndpoints();
app.Run(); 