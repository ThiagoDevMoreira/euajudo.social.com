using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Infra;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly EuAjudoDbContext _db;

    public OrganizationsController(EuAjudoDbContext context)
    {
        _db = context;
    }

    // GET: api/organizations?page=1&pageSize=20
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        // implementação depois
        throw new NotImplementedException();
    }

    // GET: api/organizations/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Organization>> GetOrganization(Guid id)
    {
        // implementação depois
        throw new NotImplementedException();
    }

    // POST: api/organizations
    [HttpPost]
    public async Task<ActionResult<Organization>> CreateOrganization(Organization organization)
    {
        // implementação depois
        throw new NotImplementedException();
    }

    // PUT: api/organizations/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrganization(Guid id, Organization organization)
    {
        // implementação depois
        throw new NotImplementedException();
    }

    // DELETE: api/organizations/{id} (soft delete)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(Guid id)
    {
        // implementação depois
        throw new NotImplementedException();
    }
}
