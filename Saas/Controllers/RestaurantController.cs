using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Saas.Entities;
using static Saas.Entities.Restaurants.Types;
using System.Collections.Generic;

namespace Saas.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public class RestaurantController : ControllerBase
  {    
    private readonly AppData _app;
    public RestaurantController(AppData app)
    {
      _app = app;
    }

    // GET: Restaurant/
    [HttpGet]
    public ActionResult<IEnumerable<Restaurant>> Get()
    {
      var ret = Claim.DbContext<Restaurant>(User, _app.ConnectionString).Read();

      if (ret == null)
        return NotFound();

      return new ActionResult<IEnumerable<Restaurant>>(ret);
    }

    // GET: Restaurant/Lookup/value
    [HttpGet("Lookup/{value}", Name = "LookupRestaurant")]
    public ActionResult<IEnumerable<Restaurant>> Lookup(string value)
    {      
      var ret = Claim.DbContext<Restaurant>(User,_app.ConnectionString).Read(value);

      if (ret == null)
        return NotFound();

      return new ActionResult<IEnumerable<Restaurant>>(ret);
    }

    // GET: Restaurant/5
    [HttpGet("{id}", Name = "GetRestaurant")]
    public ActionResult<Restaurant> Get(int id)
    {
      var ret = Claim.DbContext<Restaurant>(User,_app.ConnectionString).Read(id);

      if (ret == null || ret.Id != id)
        return NotFound();

      return Ok(ret);
    }

    // POST: Restaurant
    [HttpPost]
    public IActionResult Post([FromBody] Restaurant value)
    {      
      var id = Claim.DbContext<Restaurant>(User,_app.ConnectionString).Create(value);
      if (id == 0)
        return BadRequest();

      return Ok(id);
    }

    // PUT: Restaurant/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Restaurant value)
    {    
      int nbRowsAffected;
      if (id != value.Id || ((nbRowsAffected = Claim.DbContext<Restaurant>(User,_app.ConnectionString).Update(value)) == 0))
        return BadRequest();

      return Ok(nbRowsAffected);
    }

    // DELETE: ApiWithActions/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {      
      var nbRowsAffected =Claim.DbContext<Restaurant>(User,_app.ConnectionString).Update(id, _app.States.Delete.Id);
      if (nbRowsAffected == 0)
        return new NotFoundResult();

      return Ok(nbRowsAffected);
    }
  }
}