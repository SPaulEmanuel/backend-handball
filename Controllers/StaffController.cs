using aplicatieHandbal.Helpers;
using aplicatieHandbal.Models;
using aplicatieHandbal.Services;
using CSU_Suceava_BE.Application.JwtUtils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;


namespace aplicatieHandbal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService!;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetStaffByPosition")]
        public async Task<IActionResult> GetStaffByPosition()
        {
            var staffByPosition = await _staffService.GetStaffByPosition();
            return Ok(staffByPosition);
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStaff()
        {

            return Ok(await _staffService.GetAllStaff());
        }

        [AllowAnonymous]
        [HttpGet("GetAllInfo")]
        public async Task<IActionResult> GetAllInfoStaff()
        {
            return Ok(await _staffService.GetAllInfoStaff());
        }

        [AuthorizeMultiplePolicy(Policies.Administrator, true)]
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

        [AuthorizeMultiplePolicy(Policies.Administrator, true)]
        [HttpPatch]
        [Route("{id:Guid}")]

        public async Task<IActionResult> updateStaffPatch([FromRoute] Guid id, JsonPatchDocument updateStaffReq)
        {
            return Ok(await _staffService.updateStaffPatch(id, updateStaffReq));
        }

        [AuthorizeMultiplePolicy(Policies.Administrator, true)]
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateStaff([FromRoute] Guid id, Staff updateStaffReq)
        {
            return Ok(await _staffService.UpdateStaff(id, updateStaffReq));
        }

        [AuthorizeMultiplePolicy(Policies.Administrator, true)]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deletePlayer([FromRoute] Guid id)
        {
            return Ok(await _staffService.DeleteStaff(id));
        }
    }
}
