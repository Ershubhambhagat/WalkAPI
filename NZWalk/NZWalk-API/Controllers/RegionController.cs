#region using


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NZWalk_API.Data;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO;
#endregion
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

        #region Get all Region
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //get data from databse -Domain Model 
            var regions=await _DBContext.Regions.ToListAsync();
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
        public async Task<IActionResult> GetById(Guid id)
        {
            var regionsDomain =  await _DBContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
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
        public async Task<IActionResult> Create([FromBody]AddRegionRequestDTO addRegionRequestDTO)
        {
            if (addRegionRequestDTO is not null)
            {
                //DTO to Domain
                var regionDomainModel = new RegionDto
                {
                    //Id = addRegionRequestDTO.Id,
                    Code = addRegionRequestDTO.Code,
                    Name = addRegionRequestDTO.Name,
                    RegionImageUrl = addRegionRequestDTO.RegionImageUrl,
                };
                //use Domain Model to Create Region
                await _DBContext.Regions.AddAsync(regionDomainModel);
                await _DBContext.SaveChangesAsync();
                //Map Domain Model back to DTO
                var regionDTO = new RegionDto
                {
                    Id = regionDomainModel.Id,
                    Code = regionDomainModel.Code,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl,
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
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateResionRequestDTO updateResionRequestDTO)
        {
            var ResionDomainModel=await _DBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (ResionDomainModel is null)
            {
               return NotFound();
            }
            // DTO to Domain
            ResionDomainModel.Code=updateResionRequestDTO.Code;
           ResionDomainModel.Name=updateResionRequestDTO.Name;
           ResionDomainModel.RegionImageUrl = updateResionRequestDTO.RegionImageUrl;
           await _DBContext.SaveChangesAsync();
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
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var ResionDomainModel = await _DBContext.Regions.FirstOrDefaultAsync(c=>c.Id == id);
            if(ResionDomainModel is null)
            {
                NotFound();
            }
             _DBContext.Regions.Remove(ResionDomainModel);
            await _DBContext.SaveChangesAsync();
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
