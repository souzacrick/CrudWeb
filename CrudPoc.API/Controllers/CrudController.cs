using System.Collections.Generic;
using CrudPoc.Domain;
using CrudPoc.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CrudPoc.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        
        private readonly CrudRepository _crudRepository;

        public CrudController(CrudRepository repo, IConfiguration configuration)
        {
            _crudRepository = repo;

        }

        [HttpGet]
        public ActionResult<IEnumerable<AnnouncementWebMotors>> GetAll()
        {
            return _crudRepository.GetAll();
        }

        [HttpGet("{iD}")]
        public ActionResult<AnnouncementWebMotors> Find(int iD)
        {
            AnnouncementWebMotors anuncio = _crudRepository.Find(iD);

            if (anuncio == null)
                return NotFound();

            return anuncio;
        }

        [HttpPost]
        public ActionResult Create([FromBody] AnnouncementWebMotors anuncio)
        {
            if (anuncio == null)
                return BadRequest();

            _crudRepository.Add(anuncio);

            return CreatedAtRoute("Find", new { iD = anuncio.ID }, anuncio);
        }

        [HttpPut("{iD}")]
        public ActionResult Update(int iD, [FromBody] AnnouncementWebMotors anuncio)
        {
            if (anuncio == null || anuncio.ID != iD)
                return BadRequest();

            AnnouncementWebMotors _anuncio = _crudRepository.Find(iD);

            if (_anuncio == null)
                return NotFound();

            _anuncio.Make = anuncio.Make;
            _anuncio.Model = anuncio.Model;
            _anuncio.Version = anuncio.Version;
            _anuncio.Year = anuncio.Year;
            _anuncio.Mileage = anuncio.Mileage;
            _anuncio.Observation = anuncio.Observation;

            _crudRepository.Update(_anuncio);
            return new NoContentResult();
        }

        [HttpDelete("{iD}")]
        public ActionResult Delete(int iD)
        {
            AnnouncementWebMotors anuncio = _crudRepository.Find(iD);

            if (anuncio == null)
                return NotFound();

            _crudRepository.Remove(iD);
            return new NoContentResult();
        }
    }
}