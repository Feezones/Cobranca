using FitBack.Models;
using FitBack.Repositories;

namespace FitBack.Controllers
{
    public static class DividaEndpoints
    {
        public static void MapDividaEndpoints(this WebApplication app, DividaRepository repository)
        {
            app.MapGet("/dividas", () =>
            {
                var dividas = repository.GetAll();
                return Results.Ok(dividas);
            });

            app.MapPost("/dividas", (Divida divida) =>
            {
                repository.Add(divida);
                return Results.Created($"/dividas/{divida.Id}", divida);
            });

            app.MapPut("/dividas/{id}", (int id, Divida divida) =>
            {
                divida.Id = id;
                repository.Update(divida);
                return Results.NoContent();
            });

            app.MapDelete("/dividas/{id}", (int id) =>
            {
                repository.Delete(id);
                return Results.NoContent();
            });
        }
    }
}
