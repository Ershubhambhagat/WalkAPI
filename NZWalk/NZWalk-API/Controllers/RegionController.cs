using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NZWalk_API.Data;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO;
namespace NZWalk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : Controller
    {
        #region CTOR
        private readonly NZWalksDBContext _DBContext;
        public RegionController(NZWalksDBContext nZWalksDBContext)
        {
            _DBContext = nZWalksDBContext;
        }
        #endregion

        #region Get Region
        [HttpGet]
        public  IActionResult GetAll()
        {
            //get data from databse -Domain Model 
            var regions=_DBContext.Regions.ToList();
            //Map domain model to DTO
            var ResionDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                ResionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code=region.Code,
                    Name=region.Name,
                    RegionImageUrl=region.RegionImageUrl,
                });
            }
            return Ok(ResionDto);
        }
        #endregion

        #region By ID
        [HttpGet]
        [Route("{id:Guid}")]
        public  IActionResult GetById(Guid id)
        {
            //var regions=_DBContext.Regions.Find(id);
            var regionsDomain = _DBContext.Regions.FirstOrDefault(x=>x.Id == id);
            if(regionsDomain == null)
            {
                NotFound();
            }
            // Map to DTo 
            var regionDto = new RegionDto()
            {
                Id = regionsDomain.Id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl,
                };
            return Ok(regionDto);
        }
        #endregion

        #region Create
        [HttpPost]
        public IActionResult Create([FromBody]AddRegionRequestDTO addRegionRequestDTO)
        {
            if (addRegionRequestDTO is not null)
            {
                //DTO to Domain
                var regionDomainModels = new RegionDto
                {
                    //Id = addRegionRequestDTO.Id,
                    Code = addRegionRequestDTO.Code,
                    Name = addRegionRequestDTO.Name,
                    RegionImageUrl = addRegionRequestDTO.RegionImageUrl,
                };
                //use Domain Model to Create Region
                _DBContext.Regions.Add(regionDomainModels);
                _DBContext.SaveChanges();
                //Map Domain Model back to DTO
                var regionDTO = new RegionDto
                {
                    Id = regionDomainModels.Id,
                    Code = regionDomainModels.Code,
                    Name = regionDomainModels.Name,
                    RegionImageUrl = regionDomainModels.RegionImageUrl,
                };
                return CreatedAtAction(nameof(GetById),new {id= regionDTO.Id}, regionDTO);
            }
            else
            {
                    BadRequest();
            }
            return Ok();
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("{id:Guid}")]
        [ActionName("Update")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateResionRequestDTO updateResionRequestDTO)
        {
            var ResionDomainModel= _DBContext.Regions.FirstOrDefault(x => x.Id == id);
            if (ResionDomainModel is null)
            {
               return NotFound();
            }
            // DTO to Domain
            ResionDomainModel.Code=updateResionRequestDTO.Code;
           ResionDomainModel.Name=updateResionRequestDTO.Name;
           ResionDomainModel.RegionImageUrl = updateResionRequestDTO.RegionImageUrl;
            _DBContext.SaveChanges();
            //Convert Domain TO DTO
            var regionDTO = new RegionDto
            {
                Id = ResionDomainModel.Id,
                Code = ResionDomainModel.Code,
                Name = ResionDomainModel.Name,
                RegionImageUrl = ResionDomainModel.RegionImageUrl
            };
            return Ok(regionDTO);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var ResionDomainModel = _DBContext.Regions.FirstOrDefault(c=>c.Id == id);
            if(ResionDomainModel is null)
            {
                NotFound();
            }
            _DBContext.Regions.Remove(ResionDomainModel);
            _DBContext.SaveChanges();
            //return Delete Model back
            //map Domain Model To DTO
            var regionDTO = new RegionDto
            {
                Id = ResionDomainModel.Id,
                Code = ResionDomainModel.Code,
                Name = ResionDomainModel.Name,
                RegionImageUrl = ResionDomainModel.RegionImageUrl
            };
            return Ok(regionDTO);
        }
        #endregion
    }
}
