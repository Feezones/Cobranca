using FitBack.Models;
using FitBack.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DividasController : ControllerBase
{
    private readonly DividaRepository _repository;

    public DividasController(DividaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var dividas = _repository.GetAll();
        return Ok(dividas);
    }

    [HttpPost]
    public IActionResult Create(Divida divida)
    {
        _repository.Add(divida);
        return CreatedAtAction(nameof(GetAll), new { id = divida.Id }, divida);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Divida divida)
    {
        divida.Id = id;
        _repository.Update(divida);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _repository.Delete(id);
        return NoContent();
    }
}
