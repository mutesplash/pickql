namespace pickql;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]

public class ElementQueryController : ControllerBase {
    
    ElementDbContext context;

    public ElementQueryController() {
        context = new ElementDbContext();
    }

    public IActionResult NoQuery() {
        return Ok(new { Nope = "That's not how this works"} );
    
    }

    [Route("element/{id}")]
    //[HttpGet("{id}")]
    public IActionResult SingleElement(int id) {

        // Default is null
        LegoElement ?q = context.Elements.Where(x => x.Id == id).FirstOrDefault();
        
        // Moral of the story is LINQ gets big mad when it encounters a null result set on .Find() and .Single() (also if multiple in single?)
        // Blows up with "System.InvalidOperationException: Sequence contains no elements"

        if ( q != null )  {
            return Ok( q.Json );
        } else {
            return NotFound();
        }
        //ColorNumber CatalogType PurchaseType, Available= true, Json = "{}"
    }

    [Route("design/{id}")]
    public IActionResult ElementsForDesign(int id) {

        // For now, Don't use FirstOrDefault because you'll forget that DefaultIfEmpty exists and you need that
        // Hey, turns out DefaultIfEmpty returns IQueryable which is lazy and nullable and JsonSerializer.Deserialize does not like nullables
        // ToList synthesizes the query into IEnumerable
        IEnumerable<LegoElement> q = context.Elements.Where(x => x.DesignNumber == id).ToList();
        
        if ( q != null )  {
            var result = q.Select(
                item => 
                    JsonSerializer.Deserialize<object>(item.Json)
                
            );
            
            return Ok( result );
        } else {
            return NotFound();
        }
    }

 [Route("color/{id}")]
    public IActionResult ElementsForColor(int id) {

        IEnumerable<LegoElement> q = context.Elements.Where(x => x.ColorNumber == id).ToList();
        
        if ( q != null )  {
            var result = q.Select(
                item => 
                    JsonSerializer.Deserialize<object>(item.Json)
                
            );
            
            return Ok( result );
        } else {
            return NotFound();
        }
    }


// curl --header "Content-Type: application/json" --request POST --data '["6482958","6385644","6335232"]'  http://localhost:5256/api/ElementQuery/elementList
// curl --header "Content-Type: application/json" --request POST --data '["5","7","9"]'  http://localhost:5256/api/ElementQuery/elementList
 [Route("elementList")]
 //[Consumes(MediaTypeNames.Application.Json)]
    public IActionResult ElementsFromPost([FromBody] List<int> elementIds) {

        IEnumerable<LegoElement> q = context.Elements.Where(x => elementIds.Contains(x.Id)).ToList();
        
        if ( q != null && q.Count() > 0 )  {
            var result = q.Select(
                item => 
                    JsonSerializer.Deserialize<object>(item.Json)
                
            );
            
            return Ok( result );
        } else {
            return NotFound();
        }
    }

    [Route("Everything")]
    [HttpGet]
    public async Task<IActionResult> Get() {
        return Ok( new { context.Elements } );
    }
}