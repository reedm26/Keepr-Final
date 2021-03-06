using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpGet("{id}/keeps")]
    [Authorize]
    public ActionResult<Keep> GetKeepsByVaultId(int id)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vks.GetKeepsByVaultId(id, userId));
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public ActionResult<VaultKeep> Post([FromBody] VaultKeep vaultKeepData)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        vaultKeepData.UserId = userId;

        return Ok(_vks.Create(vaultKeepData));
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{vaultId}/keeps/{keepId}")]
    [Authorize]
    public ActionResult<string> Delete(int vaultId, int keepId)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vks.Delete(vaultId, keepId, userId));
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }
  }
}