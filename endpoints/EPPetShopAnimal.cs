using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using minard_teste_2.models;


namespace minard_teste_2.endpoints
{
    public static class EPPetShopAnimal
    {
        public static void RegistrarEndPointAnimais(this IEndpointRouteBuilder rotas)
        {
            RouteGroupBuilder rotasAnimais = rotas.MapGroup("/animais");

            rotasAnimais.MapGet("/", async (PetShopContext DbContext) =>
            {
                var animais = await DbContext.Animais
                .Include(a => a.Dono)
                .OrderBy(a => a.Nome)
                .Select(a => new AnimalDTO
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Tipo = a.Tipo,
                    DonoId = a.DonoId,
                    Dono = new DonoDTO
                    {
                        Id = a.Dono.Id,
                        Nome = a.Dono.Nome,
                    }
                })
                .ToListAsync();
                return TypedResults.Ok(animais);
            });

            //rotasAnimais.MapGet("/{Id}", async (PetShopContext DbContext, int Id ) =>
            //{
            //    var animal = await DbContext.Animais
            //        .Include(a => a.Dono)
            //        .Where(a => a.Id == Id)
            //        .Select(a => new AnimalDTO
            //        {
            //            Id = a.Id,
            //            Nome = a.Nome,
            //            Tipo = a.Tipo,
            //            DonoId = a.DonoId,
            //            Dono = new DonoDTO
            //            {
            //                Id = a.Dono.Id,
            //                Nome = a.Dono.Nome,
            //            }
            //        })
            //        .FirstOrDefaultAsync();

            //    return animal is not null ? TypedResults.Ok(animal) : TypedResults.NotFound();
            //});

            rotasAnimais.MapPost("/seed", async(PetShopContext DbContext, bool excluirExistentes = false) =>
            {
                Animal novoAnimal1 = new Animal { Nome = "Pitucha",  DonoId = 1};
                Animal novoAnimal2 = new Animal { Nome = "Salsicha", DonoId = 1 };
                Animal novoAnimal3 = new Animal { Nome = "Pitucha", Id = 2 };

                if (excluirExistentes) {
                    DbContext.Animais.RemoveRange(DbContext.Animais);
                }

                DbContext.Animais.AddRange([novoAnimal1, novoAnimal2, novoAnimal3]);
                await DbContext.SaveChangesAsync();
            });

            rotasAnimais.MapPost("/", async(PetShopContext DbContext, Animal animal) =>
            {
                bool donoExiste = await DbContext.Donos.AnyAsync(d => d.Id == animal.DonoId);
                if (!donoExiste)
                {
                    return Results.NotFound();
                }
                var novoAnimal = DbContext.Animais.Add(animal);
                await DbContext.SaveChangesAsync();
                return TypedResults.Created($"/animais/{novoAnimal.Entity.Id}", animal);
            });

            rotasAnimais.MapPut("/{id}", async(PetShopContext DbContext, int id, Animal animal) =>
            {
                Animal? animalEncontrado = await DbContext.Animais.FindAsync(id);
                if (animalEncontrado is null)
                {
                    return Results.NotFound();
                }
                animal.Id = id;
                DbContext.Entry(animalEncontrado).CurrentValues.SetValues(animal);
                await DbContext.SaveChangesAsync();
                return TypedResults.NoContent();
            });

            rotasAnimais.MapDelete("/{id}", async (PetShopContext DbContext, int id) =>
            {
                Animal? animalEncontrado = await DbContext.Animais.FindAsync(id);
                if (animalEncontrado is null)
                {
                    return Results.NotFound();
                }
                DbContext.Animais.Remove(animalEncontrado);
                await DbContext.SaveChangesAsync();
                return TypedResults.NoContent();

            });
        }
    }
}
