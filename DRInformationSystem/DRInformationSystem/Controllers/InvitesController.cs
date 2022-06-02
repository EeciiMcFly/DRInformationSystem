using BusinessLogicLayer.Auth;
using BusinessLogicLayer.Services;
using DRInformationSystem.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DRInformationSystem.Controllers;

[ApiController]
public class InvitesController : Controller
{
	private readonly IInvitesService _invitesService;

	public InvitesController(IInvitesService invitesService)
	{
		_invitesService = invitesService;
	}

	[HttpPost("api/v1/invites")]
	[Authorize(Roles = AuthOptions.AggregatorRole)]
	[SwaggerResponse(200, "Invite created")]
	public async Task<IActionResult> CreateInvite([FromBody] CreateInviteDto createInviteDto)
	{
		var inviteResult = await _invitesService.InviteConsumerAsync(createInviteDto.Email);

		if (inviteResult)
			return Ok();

		return BadRequest(new {errorText = "Create during invite."});
	}
}