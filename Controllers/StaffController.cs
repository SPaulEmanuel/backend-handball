using aplicatieHandbal.Models;
using aplicatieHandbal.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace aplicatieHandbal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService!;
        }


        [HttpGet]
        [Route("GetStaffByPosition")]
        public async Task<IActionResult> GetStaffByPosition()
        {
            var staffByPosition = await _staffService.GetStaffByPosition();
            return Ok(staffByPosition);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStaff()
        {

            return Ok(await _staffService.GetAllStaff());
        }
        [HttpGet("GetAllInfo")]
        public async Task<IActionResult> GetAllInfoStaff()
        {
            return Ok(await _staffService.GetAllInfoStaff());
        }
        [HttpPost]
        public async Task<IActionResult> AddStaff([FromForm] Staff model)
        {
            return Ok(await _staffService.AddStaff(model));
        }
        [HttpGet]

        [Route("{id:Guid}")]
        public async Task<IActionResult> GetStaff([FromRoute] Guid id)
        {
            return Ok(await _staffService.GetStaffById(id));
        }
        [HttpPatch]
        [Route("{id:Guid}")]

        public async Task<IActionResult> updateStaffPatch([FromRoute] Guid id, JsonPatchDocument updateStaffReq)
        {
            return Ok(await _staffService.updateStaffPatch(id, updateStaffReq));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateStaff([FromRoute] Guid id, Staff updateStaffReq)
        {
            return Ok(await _staffService.UpdateStaff(id, updateStaffReq));
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deletePlayer([FromRoute] Guid id)
        {
            return Ok(await _staffService.DeleteStaff(id));
        }
    }
}
