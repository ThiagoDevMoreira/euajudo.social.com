# Regras de relacionamento entre entidades:

---

## Relações N:N

Sintaxa da definição de chaves compostas em FluentAPI

```
modelBuilder.Entity<AB>()
	.HasKey(ab => new {ab.AID, ab.BID}).IsUnique();
```

###  Organizations <N:N> Member

	* Usa tabela de junção "OrganizationMember".
		- que por sua vez usa chave composta única (OrganizationsId + MemberId).
	
### Organization <N:N> Contributor

	* Usa tabela de junção "OrganizationContributor".
		- que por sua vez usa chave composta única (OrganizationsId + ContributorrId).
	
### Campaign <N:N> Member

	* Usa tabela de junção "CampaignMember".
		- que por sua vez usa chave composta única (CampaignMemberId + MemberId).

### Campaign <N:N> Contributor

	* Usa tabela de junção "CampaignContributor".
		- que por sua vez usa chave composta única(CampaignId + ContributorId).

---

## Relações 1:1

Sintaxa da definição de relações 1:1 explicitas

```
\\ para relação A <1:1> B <1:1> A
modelBuilder.Entity<OrganizationMember>()
    .HasOne(om => om.Role)
    .WithOne(r => r.OrganizationMember)
    .HasForeignKey<Role>(r => r.OrganizationMemberId);
	.IsRequired();

\\ para relação A <1:1> B <1:N> A	
modelBuilder.Entity<OrganizationMember>()
    .HasOne(om => om.Role)                  // OrganizationMember → Role
    .WithMany(r => r.OrganizationMembers)   // Role → muitos OrganizationMembers
    .HasForeignKey(om => om.RoleId)         // FK em OrganizationMember
    .IsRequired();                          // caso B seja obrigatório
```

### OrganizationMember <1:1> Role <1:N> OrganizationMember
	
### Campaign <1:1> Organization(required) <1:N> Campaign
	
### Sale <1:1> Organization(required) <1:N> Sale
	
### Sale <1:1> Member(required) <1:N> Sale
	
### Sale <1:1> Campaign(required) <1:N> Sale
	
### Sale <1:1> Contributor(required) <1:N> Sales
	
### VoucherInstance <1:1> VoucherTemplate(required) <1:N> VoucherInstance
	
### VoucheInstance <1:1> Sale? <1:N> VoucherInstance

### VoucherTemplate <1:1> Organization(required) <1:N> VoucherTemplate
	
### VoucherTemplate <1:1> Campaign(required) <1:N> VoucherTemplate
	
---

## Relações 1:N

> Sem definições no `OnModelCreating`, apenas definição no modelo de dados das Entidades.

* Organization <1:N> Campaign
* Organization <1:N> Sale
* Member <1:N> Sales
* Campaign <1:N> Sales
* Campaign <1:N> VoucherTemplate
* Contributor <1:N> Sale, sem tabela de junção
* Sale <1:N> VoucheInstance
* VoucherTemplate <1:N> VoucherInstance
	
---
