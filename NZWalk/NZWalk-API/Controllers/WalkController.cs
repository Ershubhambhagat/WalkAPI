using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO;
using NZWalk_API.Repositories.Walk_Repository.Interface;
using System.Net.WebSockets;

namespace NZWalk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        #region CTOR
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walk;


        public WalkController(IMapper mapper, IWalkRepository walk)
        {
            _mapper = mapper;
            _walk = walk;
        }


        #endregion

        #region CreateAsync
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            if (ModelState.IsValid)
            {
                //Mam DTO to Doman  Model
                var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDTO);
                await _walk.CreateWalkAsync(walkDomainModel);

                //Map Domain Model To DTO 

                var WalkDTO = _mapper.Map<WalkDto>(walkDomainModel);
                return Ok(walkDomainModel);
            }
            return BadRequest(ModelState);



        }
        #endregion

        #region GetWalkByID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkByID(Guid id)
        {
            var Walk = await _walk.GetWalkById(id);
            if (Walk is null)
            {
                var x ="Id \t "+ id + " \t is not Found in Database";
                return Ok(x);
            }
            //Map Domain Model To DTO 
            var walk2 = _mapper.Map<WalkDto>(Walk);
            return Ok(walk2);


        }



        #endregion


        #region GetAllWalkAsync
        [HttpGet]

        public async Task<IActionResult> GetAllWalkAsync([FromQuery]string? FilterOn, [FromQuery]string? filterQuary)
        {
            var walkDomainModel = await _walk.GetAllWalkAsync(FilterOn,filterQuary);

            //Maping to DTO 
             return Ok(_mapper.Map<List<WalkDto>>(walkDomainModel));
           

        }
        #endregion

        #region DeleteAsync
        [HttpDelete]
        [Route("{Id:Guid}")]

        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var DeleteData=await _walk.DeleteAsync(Id);

            if (DeleteData is null)
            {
                var x = "Id \t " + Id + " \t is not Found in Database";
                return Ok(x);
            }
            //Map TO DTO
            return Ok(_mapper.Map<WalkDto>(DeleteData));

        }
        #endregion

        #region UpdateAsync
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id,UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            if (ModelState.IsValid)
            {
                var updateDomainModel = _mapper.Map<Walk>(updateWalkRequestDTO);
                var walkDomainModel = await _walk.UpdateWAlkAsync(id, updateDomainModel);
                if (walkDomainModel is null)
                {
                    return NotFound();
                }
                //Map Domain model to DOT 
                return Ok(_mapper.Map<WalkDto>(walkDomainModel));

            }
            return BadRequest(ModelState);

        }
        #endregion

    }
}
