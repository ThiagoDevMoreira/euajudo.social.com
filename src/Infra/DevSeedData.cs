// src\Infra\SeedData.cs
using Core.Models;
using Core.Models.MockFactory;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infra.Auth;

namespace Infra;

public static class SeedData
{
    public static async Task InitializeAsync(
        EuAjudoDbContext context,
        UserManager<AppUser> userManager)
    {
        await context.Database.MigrateAsync();

        // 👉 Se já existe algum usuário, não roda o seed
        if (await userManager.Users.AnyAsync())
            return;

        // 1. Criar Member usando a EntityMockFactory
        var member = EntitiesMockFactory.Create<Member>();
        member.Email = "admin@euajudo.org";    // e-mail fixo para login
        member.FirstName = "Admin";
        member.LastName = "Seed";
        context.Member.Add(member);

        // 2. Criar AppUser vinculado ao Member
        var appUser = new AppUser
        {
            Id = Guid.NewGuid(),
            UserName = member.Email,
            Email = member.Email,
            MemberId = member.Id,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(appUser, "Admin@123");
        if (!result.Succeeded)
        {
            throw new Exception("Falha ao criar usuário seed: " 
                + string.Join(", ", result.Errors));
        }

        var organization = EntitiesMockFactory.Create<Organization>();
        context.Organization.Add(organization);

        var role = EntitiesMockFactory.Create<Role>();
        role.Name = "Admin"; // força o papel inicial
        context.Role.Add(role);

        var orgMember = EntitiesMockFactory.Create<OrganizationMember>();
        // substitui todos os valores padrão pelas entidades instanciadas aqui.
        orgMember.Organization = organization;
        orgMember.OrganizationId = organization.Id;
        orgMember.Member = member;
        orgMember.MemberId = member.Id;
        orgMember.Role = role;
        orgMember.RoleId = role.Id; 
        context.OrganizationMember.Add(orgMember);

        // 6. Campaign
        var campaign = EntitiesMockFactory.Create<Campaign>();
        campaign.Organization = organization;
        campaign.OrganizationId = organization.Id;
        context.Campaign.Add(campaign);

        // 7. VoucherTemplate
        var template = EntitiesMockFactory.Create<VoucherTemplate>();
        template.Organization = organization;
        template.OrganizationId = organization.Id;
        template.Campaign = campaign;
        template.CampaignId = campaign.Id;
        context.VoucherTemplate.Add(template);

        // 8. Sale
        var sale = EntitiesMockFactory.Create<Sale>();
        sale.Member = member;
        sale.MemberId = member.Id;
        sale.Campaign = campaign;
        sale.CampaignId = campaign.Id;
        context.Sale.Add(sale);

        // 9. VoucherInstance
        var cupom_001 = EntitiesMockFactory.Create<VoucherInstance>();
        cupom_001.VoucherTemplate = template;
        cupom_001.VoucherTemplateId = template.Id;
        context.VoucherInstance.Add(cupom_001);

        await context.SaveChangesAsync();
    }
}
