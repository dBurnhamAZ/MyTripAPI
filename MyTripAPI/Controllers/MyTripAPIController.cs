using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTripAPI.Data;
using MyTripAPI.Logging;
using MyTripAPI.Model;
using MyTripAPI.Model.DTO;

namespace MyTripAPI.Controllers
{
    //[Route("api/[controller]"] we dont want to name the route as controller, if there is a controller name change,
    //then all the api calls or clients will get an error. we'd have to notify them instead just hard code it
    [Route("api/MyTripAPI")]
    [ApiController]
    public class MyTripAPIController : ControllerBase
    {
        //Dependancy Injection
        private readonly ILogging _logger;
        private readonly MyTripAPIDbContext _db;
        private readonly IMapper _mapper;

        public MyTripAPIController(MyTripAPIDbContext db, IMapper mapper, ILogging logger)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        //return all records
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CabinDTO>>> GetCabins()
        {
            IEnumerable<Cabin> cabinList = await _db.Cabins.ToListAsync();
            _logger.Log("Getting all Cabin", "");
            return Ok(_mapper.Map<List<CabinDTO>>(cabinList));
        } 

        //return one record
        [HttpGet("id:int", Name = "GetCabin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CabinDTO>> GetCabin(int id)
        {
            if (id == 0)
            {
                _logger.Log("Get Cabin Error with Id " + id, "error");
                return BadRequest();
            }

            var suite = await _db.Cabins.FirstOrDefaultAsync(u => u.Id == id);

            if (suite == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<CabinDTO>(suite));
            }
        }

        //add record
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CabinDTO>> CreateCabin([FromBody] CabinCreateDTO createDto)
        {
            if (await _db.Cabins.FirstOrDefaultAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Cabin already Exists");
                return BadRequest(ModelState);
            }

            if (createDto == null)
            {
                return BadRequest();
            }

            Cabin model = _mapper.Map<Cabin>(createDto);

            // * If we didn't use Mappings, we would have to map all columns manually like below

            //Cabin model = new()
            //{
            //    Amenity = createDto.Amenity,
            //    Details = createDto.Details,
            //    ImageUrl = createDto.ImageUrl,
            //    Name = createDto.Name,
            //    Occupancy = createDto.Occupancy,
            //    Rate = createDto.Rate,
            //    Sqft = createDto.Sqft
            //};

            await _db.Cabins.AddAsync(model);
            await _db.SaveChangesAsync();

            //Invoke create at Route
            return CreatedAtRoute("GetCabin", new { id = model.Id }, model);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteCabin")]

        //you can determine the return type with IActionResult
        public async Task<IActionResult> DeleteCabin(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var suite = await _db.Cabins.FirstOrDefaultAsync(u => u.Id == id);

            if (suite == null)
            {
                return NotFound();
            }
            _db.Cabins.Remove(suite);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateCabin")]
        public async Task<IActionResult> UpdateCabin(int id, [FromBody] CabinUpdateDTO updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }

            Cabin model = _mapper.Map<Cabin>(updateDto);

            _db.Cabins.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<CabinUpdateDTO> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            //*** AsNoTracking() will not allow more than one instatnce at a time. Saving Changes will
            //*** throw errors without this Tracking set. 
            var suite = await _db.Cabins.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (suite == null)
            {
                return BadRequest();
            }

            CabinUpdateDTO villaUpdateDto = _mapper.Map<CabinUpdateDTO>(suite);
            patchDto.ApplyTo(villaUpdateDto, ModelState);

            Cabin model = _mapper.Map<Cabin>(villaUpdateDto);

            //_db.Cabins.Update(model);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
