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

### Etapa 1 — **API + Infra (DbContext no Controller)**
- Estrutura inicial simples, para aprendizado.
- Controllers acessam o `DbContext` diretamente.
- Migrations geradas direto no projeto de Infra.

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
* Enterprise: refresh tokens, roles (Admin ONG, Volunteer, Buyer), rate limiting, proteções antifraude.

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
