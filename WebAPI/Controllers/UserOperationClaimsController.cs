using Business.Repositories.UserOperationClaimRepository;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private readonly IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        [HttpPost("add")]
        public IActionResult Add(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimService.Add(userOperationClaim);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimService.Delete(userOperationClaim);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            var result = _userOperationClaimService.GetList();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int userOperationClaimId)
        {
            var result = _userOperationClaimService.GetById(userOperationClaimId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimService.Update(userOperationClaim);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("deleteById")]
        public IActionResult DeleteById(int userOperationClaimId)
        {
            var result = _userOperationClaimService.DeleteById(userOperationClaimId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
