
using JobCandidateHub.API;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();

