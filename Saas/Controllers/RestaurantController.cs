using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

using Saas.Entities;
using static Saas.Entities.Restaurants.Types;

namespace Saas.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public class RestaurantController : ControllerBase
  {
    private readonly string _connectionStr;

    public RestaurantController(IConfiguration config)
    {
      _connectionStr = config.GetConnectionString("AllAboutFood");
    }

    // GET: Restaurant/Lookup/value
    [HttpGet("Lookup/{value}", Name = "LookupRestaurant")]
    public ActionResult<Restaurants> Lookup(string value)
    {
      var ret = Restaurant.Lookup(value, _connectionStr);
      if (ret.Values == null || ret.Values.Count == 0)
        return NotFound();

      return new ActionResult<Restaurants>(ret);
    }

    // GET: Restaurant/5
    [HttpGet("{id}", Name = "GetRestaurant")]
    public ActionResult<Restaurant> Get(int id)
    {
      var ret = Restaurant.Read(id, _connectionStr);
      if (ret.Id != id)
        return NotFound();

      return Ok(ret);
    }

    // POST: Restaurant
    [HttpPost]
    public IActionResult Post([FromBody] Restaurant value)
    {
      var id = Restaurant.Create(value, _connectionStr).Value;
      if (id == 0)
        return BadRequest();

      return Ok(id);
    }

    // PUT: Restaurant/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Restaurant value)
    {
      int nbRowsAffected;
      if (id != value.Id || ((nbRowsAffected = Restaurant.Update(value, _connectionStr).Value) == 0))
        return BadRequest();

      return Ok(nbRowsAffected);
    }

    // DELETE: ApiWithActions/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var nbRowsAffected = Restaurant.Delete(id, _connectionStr).Value;
      if (nbRowsAffected == 0)
        return new NotFoundResult();

      return Ok(nbRowsAffected);
    }
  }
}