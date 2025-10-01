# Eu Ajudo — Documentação inicial (Blueprint Revisado)

**Domínio:** `euajudo.social.br`  
**Propósito:** Plataforma SaaS para digitalizar e automatizar vendas de cupons/fichas por ONGs, facilitando arrecadação, distribuição e controle de retirada.

---

## 1. Resumo executivo

Plataforma PWA multi-tenant destinada a ONGs e grupos sociais que vendem itens por meio de cupons (pizzas, salgados, artesanato etc.). O sistema cobre criação de campanhas, venda de cupons com pagamento integrado, emissão de cupons digitais (QR Codes/links), validação de retirada via PWA/Aplicativo leve e dashboards de transparência.

Objetivos desta documentação:

* Servir como referência inicial para desenvolvimento (backend C# .NET 9 + EF Core, frontend Angular PWA).
* Organizar roadmap técnico e arquitetural em **etapas evolutivas** (de MVP até nível Enterprise).

---

## 2. Escopo do MVP

**Funcionalidades mínimas obrigatórias:**

* Cadastro de ONG e usuários (administradores e voluntários).
* Criação de campanhas (nome, data, local de retirada, itens e quantidades).
* Venda de cupons online (integração inicial com Pix via provedor único).
* Emissão de cupom digital (link + QR Code com identificador único).
* Validação de retirada no ponto (scan QR/inspeção PWA offline-capable).
* Dashboard básico de vendas e arrecadação.

**Funcionalidades adiáveis (post-MVP):**

* Integração com múltiplos gateways de pagamento.
* Personalização white-label por ONG.
* API pública para integrações de terceiros.
* Integração com WhatsApp para envio de cupons.

---

## 3. Roadmap técnico e arquitetural

O desenvolvimento seguirá uma trilha evolutiva de aprendizado e robustez, partindo de implementações mais simples até a estrutura Enterprise. Essa abordagem reflete o roadmap discutido em **"Abordagem didática"**.

### Etapa 1 — **API + Frontend Angular minimalista + Infra (DbContext no Controller)**

#### Objetivo da etapa

Entregar o núcleo autenticado do sistema com um cliente de referência que valide o fluxo ponta a ponta:

1. **Autenticação** (login + token JWT).
2. **Consulta dos dados centrais do membro autenticado** (organizações, campanhas, templates e vouchers).
3. **Protótipo Angular minimalista** que consome os endpoints do MVP e representa os fluxos de cadastro/login e leitura básica.

#### Estado atual

- Estrutura inicial simples para aprendizado, com controllers acessando diretamente o `EuAjudoDbContext`.
- Migrations geradas diretamente no projeto `Infra`, com `EuAjudoDbContext` e Identity configurados em `src/Infra/EuAjudoDbContext.cs` e `src/Api/Program.cs`.
- Autenticação já disponível via `POST /auth/login` em `src/Api/Controllers/AuthController.cs`, retornando `{ token, expiresAt }`.
- Endpoint autenticado `GET /member/me` em `src/Api/Controllers/MemberController.cs`, trazendo o `Member` associado ao token, suas organizações, campanhas e templates de vouchers.
- Seeds iniciais configurados para provisionar um usuário administrador e artefatos mínimos (ver `src/Infra/DevSeedData.cs`).

#### Pendências para concluir a etapa

1. **Cadastro de usuário (backend)** — implementar `POST /auth/register` que crie `AppUser` + `Member`, valide e-mail único e retorne confirmação ou token imediato (`AuthController.cs`).
2. **Ampliação do contrato de `member/me`** — incluir a lista de `VoucherInstance` relacionados ao membro autenticado, conforme especificado em `UseCasesToEndpointsDefinition.md` (`MemberController.cs`).
3. **Protótipo Angular minimalista** — iniciar o projeto em `src/Frontend` (Angular), com telas para registro/login e dashboard básico que consome `member/me`, servindo como cliente de validação de contratos.
4. **Atualização de `Api.http` e testes** — adicionar exemplos de requisições para registro e member/vouchers, além de testes de integração cobrindo os novos fluxos (`src/Api/Api.http`, `src/InfraTests`).

#### Referências de código relevantes

- `src/Api/Controllers/AuthController.cs`
- `src/Api/Controllers/MemberController.cs`
- `src/Api/Program.cs`
- `src/Api/Api.http`
- `src/Core/Models/*`
- `src/Infra/EuAjudoDbContext.cs`
- `src/Infra/DevSeedData.cs`
- `src/Infra/Auth/AppUserModel.cs`
- `src/InfraTests/Entities/*`

> **Centralidade de Member**
> O `AppUser` é apenas a credencial de login.
> `Member` é a entidade central do domínio: todo bootstrap e todas as atividades (Organizações, Campanhas, Vouchers, Vendas) partem dele.
> Por isso, o login não retorna dados de domínio — esse papel é do `MemberController` (que usa o `MemberId` resolvido do token).

#### Referencia da organização do código

> mapa de referencia da estrutura de pastas do projeto euajudo.social.br

```
\euajudo.social.com
\euajudo.social.com\Blueprint.md // documento atual com roadmap e diretrizes do projeto.
\euajudo.social.com\UseCasesToEndpointsDefinition.md // mapeia casos de uso para contratos de API.
\euajudo.social.com\EntitiesDataModelMap.md // referência da modelagem de dados das entidades.
\euajudo.social.com\TestsReferences.md // visão geral dos cenários de testes planejados.
\euajudo.social.com\README.md // instruções gerais do repositório.
\euajudo.social.com\EuAjudo.sln
\euajudo.social.com\src
\euajudo.social.com\src\Api // projeto ASP.NET Core.
\euajudo.social.com\src\Api\Controllers\AuthController.cs // login implementado, cadastro pendente.
\euajudo.social.com\src\Api\Controllers\MemberController.cs // expõe o endpoint member/me.
\euajudo.social.com\src\Api\Program.cs // configuração de serviços, Identity, JWT e EF Core.
\euajudo.social.com\src\Api\Api.http // coleção de requisições de referência para a API.
\euajudo.social.com\src\Core // regras de negócio e modelagem das entidades.
\euajudo.social.com\src\Core\Models // definições das entidades do domínio.
\euajudo.social.com\src\Infra // acesso a dados, DbContext e seeds.
\euajudo.social.com\src\Infra\EuAjudoDbContext.cs
\euajudo.social.com\src\Infra\DevSeedData.cs // popula dados iniciais para desenvolvimento.
\euajudo.social.com\src\Infra\Auth\AppUserModel.cs // configuração Identity/AppUser.
\euajudo.social.com\src\InfraTests // testes de infraestrutura (PostgreSQL, entidades, relacionamentos).
\euajudo.social.com\src\InfraTests\Entities // testes de validação das entidades.
\euajudo.social.com\src\Utils // classes utilitárias compartilhadas.
```

### Etapa 2 — **API + Application + Infra (sem Domain explícito)**
- Introdução da camada Application para organizar regras de negócio fora dos Controllers.
- `Infra` fornece acesso ao banco, mas ainda sem separação completa de entidades de domínio.

### Etapa 3 — **API + Infra (DbContext como Service)**
- Criação de serviços intermediários que abstraem o uso do `DbContext`.
- Preparação para introdução de entidades de domínio e repositórios.

### Etapa 4 — **Minimal API + Application simplificado**
- Introdução de repositórios e entidades de domínio.
- Uso de Application Services para centralizar casos de uso.
- Melhor organização e aplicação de princípios SOLID.

### Etapa 5 — **Arquitetura Enterprise (DDD + Camadas)**
- Estrutura final:
  - **Domain**: entidades, value objects, agregados.
  - **Application**: casos de uso e orquestração.
  - **Infra**: EF Core + Repositórios.
  - **API**: Web API (controllers minimalistas).
- Implementação de multi-tenancy, segurança avançada e padrões de qualidade.

---

## 4. Stack tecnológica

* **Backend:** C# .NET 9, Web API, EF Core (migrations), Identity + JWT.
* **Frontend:** Angular (última versão), Angular PWA (`ng add @angular/pwa`), Material Design.
* **Banco de Dados:** PostgreSQL (primário).
* **Infra:** Deploy inicial em Azure App Service (ou Docker local para desenvolvimento).
* **Observabilidade:** Logs estruturados (Serilog), métricas (Application Insights / Prometheus).

---

## 5. Domain Model (Enterprise Vision)

The domain model defines the main entities and their relationships.  
All entity and field names are standardized in **English** to ensure consistency across codebase and documentation.

### General Principles
* **Identifiers**: all entities use `Guid/UUID` as primary key to support distributed and offline-first scenarios.
* **Audit Fields**: entities include `CreatedAt`, `UpdatedAt`, and soft-delete markers (`IsDeleted`, `DeletedAt`).
* **Dynamic Configuration**: certain settings (e.g., UI preferences, voucher design) are stored as **JSONB** fields in PostgreSQL for flexibility and evolutivity.
* **Voucher Design**: separated into `VoucherTemplate` (definition of what can be sold) and `VoucherInstance` (unique voucher issued after purchase).
* **Sales and Payments**: modeled with `Sale` and `SaleItem`, allowing multiple vouchers per transaction, plus an optional `Payment` entity for gateway integrations.
* **Access Control**: members are linked to organizations and associated with roles through an RBAC model (`Member`, `Role`, `MemberRole`).
* **Contributors**: buyers/donors can be persisted as `Contributor` profiles or treated as guests at the sale level.

### Entity Overview (high-level)
- **Organization**: represents NGOs or social groups. Stores metadata, contacts, and dynamic settings.
- **Member / Role / MemberRole**: represent people linked to organizations with RBAC permissions.
- **Campaign**: fundraising initiatives managed by organizations, linked to voucher templates.
- **VoucherTemplate**: defines a product/coupon (category, subtype, price, content, sales limit).
- **VoucherInstance**: individual coupons generated and redeemed, ensuring traceability.
- **Contributor**: optional persistent profiles for buyers/donors.
- **OrganizationContributor**: aggregates contributions per contributor/organization.
- **Sale / SaleItem**: commercial transactions and their items.
- **Payment** (optional): detailed integration logs with external providers.

> ℹ️ Detailed technical specification of each entity (fields, types, purposes) is documented separately in `entities.md`.

---

## 6. Etapas de banco de dados

* MVP: migrations únicas agrupando entidades básicas.
* Evolução: migrations por feature (`Migrations/Campaigns`, `Migrations/Orders`).
* Uso de GUIDs para suportar offline-first.
* Filtro global de tenant via `OrganizationId`.

---

## 7. Segurança

* MVP: Identity + JWT simples.

### Autenticação no MVP

Implementada com ASP.NET Core Identity (AppUser com Guid como PK e vínculo obrigatório com MemberId).
JWT (HS256) gerado pela própria API no login.
O endpoint /auth/login será minimalista: valida usuário/senha e retorna apenas { token, expiresAt }.
Nenhum payload de domínio é retornado pelo login. Isso garante independência total entre o mecanismo de autenticação e o domínio principal.

* Enterprise: refresh tokens, roles (Admin ONG, Volunteer, Buyer), rate limiting, proteções antifraude.

### Autorização no MVP

Usaremos policies baseadas em OrganizationMember.
Cada request autenticado deve incluir X-Org-Id (ou rota com orgId).
A policy MustBeMemberOfOrg valida que o MemberId presente no token está vinculado àquela organização via OrganizationMember + Role.
O Identity não é usado para RBAC de negócio. Roles do Identity ficam para controle técnico (se necessário), enquanto Role do domínio é usado para permissões de organização.

---

## 8. Convenções de desenvolvimento

* **Commits:** Conventional Commits.
* **Branches:** `main` (prod), `develop`, `feature/*`.
* **Code style:** EditorConfig + Roslyn Analyzers (C#), ESLint + Prettier (Angular).
* **Testes:**
  - xUnit para Domain e Application.
  - Integration tests para API.
  - Cypress para frontend E2E.

---

## 9. Roadmap de entregas

1. Etapa 1 — Scaffold inicial + migrations + endpoints básicos.
2. Etapa 2 — Camada Application + primeiros casos de uso.
3. Etapa 3 — Serviços de Infra para abstração do `DbContext`.
4. Etapa 4 — Introdução de Domain Entities e Repositórios.
5. Etapa 5 — Arquitetura Enterprise (DDD completo, segurança, multi-tenant).

---
