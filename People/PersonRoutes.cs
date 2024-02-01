using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCrud.Database;
using ApiCrud.People;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.People;

public static class PersonRoutes
{
    public static void AddPersonRoutes(this WebApplication app)
    {
        var routesPerson = app.MapGroup("person");

        //Rota para Insert
        routesPerson.MapPost("",
        async (AddPeopleRequest request, AppDbContext context, CancellationToken ct) =>
        {
            var personContains = await context.People
            .AnyAsync(person => person.Name == request.Name, ct);

            if (personContains)
                return Results.Conflict("Pessoa já cadastrada");

            var newPerson = new Person(request.Name);
            await context.People.AddAsync(newPerson, ct);
            await context.SaveChangesAsync(ct);

            var personDTO = new PersonDTO(newPerson.Id, newPerson.Name);

            return Results.Ok(personDTO);
        });


        //Rota para GetAll
        routesPerson.MapGet("", async (AppDbContext context, CancellationToken ct) =>
        {
            var people = await context.People
            .Where(person => person.Active)
            .Select(person => new PersonDTO(person.Id, person.Name))
            .ToListAsync(ct);

            return Results.Ok(people);
        });


        //Rota para Atualizar
        routesPerson.MapPut("{id:guid}",
        async (Guid id, updatePersonRequest request, AppDbContext context, CancellationToken ct) =>
        {
            var person = await context.People
            .SingleOrDefaultAsync(person => person.Id == id, ct);

            if (person == null)
                return Results.NotFound();

            person.UpdateName(request.Name);

            await context.SaveChangesAsync(ct);

            return Results.Ok(new PersonDTO(person.Id, person.Name));
        });


        //Rota para Atualizar
        routesPerson.MapDelete("{id}", async (Guid id, AppDbContext context) =>
        {
            var person = await context.People
            .SingleOrDefaultAsync(person => person.Id == id);

            if (person == null)
                return Results.NotFound();

            person.Desactive();

            await context.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}
