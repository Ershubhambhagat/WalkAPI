using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO;
using NZWalk_API.Repositories.Walk_Repository.Interface;

namespace NZWalk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walk;

        #region CTOR
        public WalkController(IMapper mapper,IWalkRepository walk)
        {
            _mapper = mapper;
            _walk = walk;
        }


        #endregion

        #region CreateAsync
        [HttpPost]
        public async Task<IActionResult> CreateAsync(AddWalkRequestDTO addWalkRequestDTO)
        {
            //Mam DTO to Doman  Model
           var walkDomainModel= _mapper.Map<Walk>(addWalkRequestDTO);
            await _walk.CreateAsync(walkDomainModel);

            //Map Domain Model To DTO 

           var WalkDTO=_mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDomainModel);
            


        }
        #endregion


        #region MyRegion

        #endregion


        #region MyRegion

        #endregion

        #region MyRegion

        #endregion

        #region MyRegion

        #endregion

    }
}
