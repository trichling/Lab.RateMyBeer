using Lab.RateMyBeer.Checkins.Api.Checkins;
using Lab.RateMyBeer.Checkins.Data.Checkins;
using Lab.RateMyBeer.Framework;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf;
using Google.Protobuf.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseNServiceBus(context =>
{
    var configuration = new EndpointConfiguration("Lab.RateMyBeer.Checkins.Api");
    configuration.SendOnly();
    configuration.Configure(context, routing => { });
    
    return configuration;
});

builder.Services.RegisterCheckinsModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine($"Development: {app.Configuration.GetConnectionString("CheckinsDbConnectionString")}");

    using (var scope = app.Services.CreateScope())
    {
        var checkinsContext = scope.ServiceProvider.GetRequiredService<CheckinsContext>();
        checkinsContext.Database.Migrate();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapCheckinsEndpoints();

app.Run();

async Task<Person> GetPersonAsync(Stream stream)
{
    var parser = new MessageParser<Person>(() => new Person());
    var person = await parser.ParseFromStreamAsync(stream);
    return person;
}

public class Person : IMessage<Person>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public void MergeFrom(Person other)
    {
        FirstName = other.FirstName;
        LastName = other.LastName;
    }

    public void MergeFrom(CodedInputStream input)
    {
        input.ReadMessage(this);
    }

    public void WriteTo(CodedOutputStream output)
    {
        output.WriteMessage(this);
    }

    public int CalculateSize()
    {
        FirstName.CalculateSize();
        return this.CalculateSize();
    }

    public MessageDescriptor Descriptor => throw new NotImplementedException();
    public Person Clone() => new Person { FirstName = FirstName, LastName = LastName };
    public bool Equals(Person other) => FirstName == other.FirstName && LastName == other.LastName;
}

