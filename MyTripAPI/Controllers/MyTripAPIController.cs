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
        public async Task<ActionResult<IEnumerable<SuiteDTO>>> GetSuites()
        {
            IEnumerable<Suite> suiteList = await _db.Suites.ToListAsync();
            _logger.Log("Getting all Suite", "");
            return Ok(_mapper.Map<List<SuiteDTO>>(suiteList));
        } 

        //return one record
        [HttpGet("id:int", Name = "GetSuite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SuiteDTO>> GetSuite(int id)
        {
            if (id == 0)
            {
                _logger.Log("Get Suite Error with Id " + id, "error");
                return BadRequest();
            }

            var suite = await _db.Suites.FirstOrDefaultAsync(u => u.Id == id);

            if (suite == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<SuiteDTO>(suite));
            }
        }

        //add record
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuiteDTO>> CreateSuite([FromBody] SuiteCreateDTO createDto)
        {
            if (await _db.Suites.FirstOrDefaultAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Suite already Exists");
                return BadRequest(ModelState);
            }

            if (createDto == null)
            {
                return BadRequest();
            }

            Suite model = _mapper.Map<Suite>(createDto);

            // * If we didn't use Mappings, we would have to map all columns manually like below

            //Suite model = new()
            //{
            //    Amenity = createDto.Amenity,
            //    Details = createDto.Details,
            //    ImageUrl = createDto.ImageUrl,
            //    Name = createDto.Name,
            //    Occupancy = createDto.Occupancy,
            //    Rate = createDto.Rate,
            //    Sqft = createDto.Sqft
            //};

            await _db.Suites.AddAsync(model);
            await _db.SaveChangesAsync();

            //Invoke create at Route
            return CreatedAtRoute("GetSuite", new { id = model.Id }, model);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteSuite")]
        //you can determine the return type with IActionResult
        public async Task<IActionResult> DeleteSuite(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var suite = await _db.Suites.FirstOrDefaultAsync(u => u.Id == id);

            if (suite == null)
            {
                return NotFound();
            }
            _db.Suites.Remove(suite);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateSuite")]
        public async Task<IActionResult> UpdateSuite(int id, [FromBody] SuiteUpdateDTO updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }

            Suite model = _mapper.Map<Suite>(updateDto);

            _db.Suites.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<SuiteUpdateDTO> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var suite = await _db.Suites.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (suite == null)
            {
                return BadRequest();
            }

            SuiteUpdateDTO villaUpdateDto = _mapper.Map<SuiteUpdateDTO>(suite);
            patchDto.ApplyTo(villaUpdateDto, ModelState);

            Suite model = _mapper.Map<Suite>(villaUpdateDto);

            _db.Suites.Update(model);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
