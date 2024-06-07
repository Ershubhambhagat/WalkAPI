#region using


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NZWalk_API.Data;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO;
using NZWalk_API.Repositories.Interface;
#endregion
namespace NZWalk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : Controller
    {
        #region CTOR
        private readonly NZWalksDBContext _DBContext;
        private readonly IRegionRepository _regionRepository;

        public RegionController(NZWalksDBContext nZWalksDBContext, IRegionRepository regionRepository)
        {
            _DBContext = nZWalksDBContext;
            _regionRepository = regionRepository;
        }
        #endregion

        #region Get all Region
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //get data from databse -Domain Model 
            var regions = _regionRepository.GetAllAsync();
            //Map domain model to DTO
            var ResionDto = new List<RegionDto>();
            foreach (var region in await regions)
            {
                ResionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
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
            var Region = await _regionRepository.GetByIdAsync(id);
            if (Region is not null)
            {
                //Map To DTO 
                var regionDTO = new RegionDto()
                {
                    Id = Region.Id,
                    Code = Region.Code,
                    Name = Region.Name,
                    RegionImageUrl = Region.RegionImageUrl
                };
                return Ok(regionDTO);
            }
            else
            {
                var error = id +  " Not Found ";
                return NotFound(error);

            }

        }
        #endregion

        #region Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            if (addRegionRequestDTO is null)
            {

                return BadRequest(addRegionRequestDTO);
            }
           
               await _regionRepository.CreateAsync(addRegionRequestDTO);
            return Ok();


        }
        #endregion

        #region Update
        [HttpPut]
        [Route("{id:Guid}")]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateResionRequestDTO updateResionRequestDTO)
        {
            //Map DTO To Domain Model
            var RegionDomainModel = new Region
            {
                Id=id,
                Code=updateResionRequestDTO.Code,
                Name=updateResionRequestDTO.Name,
                RegionImageUrl=updateResionRequestDTO.RegionImageUrl
            };

            await _regionRepository.UpdateAsync(id, RegionDomainModel);
          
          //Convert Domain TO DTO
            var regionDTO = new RegionDto
            {
                Id = RegionDomainModel.Id,
                Code = RegionDomainModel.Code,
                Name = RegionDomainModel.Name,
                RegionImageUrl = RegionDomainModel.RegionImageUrl
            };
            return Ok(regionDTO);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
          var ExistingRegion= await _regionRepository.DeleteAsync(id);
            if (ExistingRegion != null)
            {
                //return Delete Model back
                //map Domain Model To DTO
                var regionDTO = new RegionDto
                {
                    Id = ExistingRegion.Id,
                    Code = ExistingRegion.Code,
                    Name = ExistingRegion.Name,
                    RegionImageUrl = ExistingRegion.RegionImageUrl
                };
                return Ok(regionDTO);
            }
            return BadRequest();
        }
        #endregion
    }
}
