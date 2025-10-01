// src\Api\Controllers\MemberController.cs
using System.Security.Claims;
using Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("member")]
[Authorize]
public sealed class MemberController : ControllerBase
{
    private readonly EuAjudoDbContext _context;

    public MemberController(EuAjudoDbContext context)
    {
        _context = context;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var memberIdClaim = User.Claims.FirstOrDefault(c => c.Type == "memberId");
        if (memberIdClaim is null)
            return Unauthorized(new { error = "MissingMemberIdClaim" });

        if (!Guid.TryParse(memberIdClaim.Value, out var memberId))
            return Unauthorized(new { error = "InvalidMemberIdClaim" });

        var member = await _context.Member
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == memberId);

        if (member is null)
            return NotFound(new { error = "MemberNotFound" });

        var organizations = await _context.OrganizationMember
            .Where(om => om.MemberId == memberId)
            .Select(om => new
            {
                om.Organization.Id,
                om.Organization.ShortName,
                om.Organization.Description,
                Role = new { om.Role.Id, om.Role.Name }
            })
            .ToListAsync();

        var campaigns = await _context.CampaignMember
            .Where(cm => cm.MemberId == memberId)
            .Select(cm => new
            {
                cm.Campaign.Id,
                cm.Campaign.Name
            })
            .ToListAsync();

        var campaignIds = campaigns.Select(c => c.Id).ToList();
        var voucherTemplates = await _context.VoucherTemplate
            .Where(vt => campaignIds.Contains(vt.CampaignId))
            .Select(vt => new
            {
                vt.Id,
                vt.OrganizationId,
                vt.CampaignId,
                vt.Category,
                vt.Subtype,
                vt.Price,
                vt.Currency,
                vt.IsActive
            })
            .ToListAsync();

        return Ok(new
        {
            member = new
            {
                member.Id,
                member.FirstName,
                member.LastName,
                member.Email,
                member.WhatsAppNumber,
                member.IsActive,
                member.CreatedAt
            },
            organizations,
            campaigns,
            voucherTemplates
        });
    }
}
