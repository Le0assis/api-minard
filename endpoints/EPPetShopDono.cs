using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using minard_teste_2.models;

namespace minard_teste_2.endpoints
{
    public static class EPPetShopDono
    {
        public static void RegistrarEndPointDono(this IEndpointRouteBuilder rotas)
        {
            RouteGroupBuilder rotasDono = rotas.MapGroup("/donos");

            rotasDono.MapGet("/seed", async(PetShopContext DbContext, bool excluirExistentes = false) =>
            {
                Dono novoDono1 = new Dono (0, "João");
                Dono novoDono2 = new Dono (0, "Maria");
                Dono novoDono3 = new Dono (0, "José");
                if (excluirExistentes)
                {
                    DbContext.Donos.RemoveRange(DbContext.Donos);
                }
                DbContext.Donos.AddRange([novoDono1, novoDono2, novoDono3]);
                await DbContext.SaveChangesAsync();
            });

            rotasDono.MapGet("/", async(PetShopContext DbContext, string? nomeDono) =>
            {
                IEnumerable<Dono> donosFiltrados = await DbContext.Donos.ToListAsync();
                if (!string.IsNullOrEmpty(nomeDono))
                {
                    donosFiltrados = donosFiltrados.Where(d => d.Nome.Contains(nomeDono, StringComparison.OrdinalIgnoreCase));
                }
                return TypedResults.Ok(donosFiltrados);
            });


        }
    }
}
