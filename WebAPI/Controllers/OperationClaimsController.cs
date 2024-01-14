using Business.Repositories.OperationClaimRepository;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [HttpPost("add")]
        public IActionResult Add(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Add(operationClaim);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _operationClaimService.GetById(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            var result = _operationClaimService.GetList();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Update(operationClaim);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Delete(operationClaim);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
