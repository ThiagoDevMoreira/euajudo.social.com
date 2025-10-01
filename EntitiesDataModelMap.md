# EuAjudo — Mapa de Modelagem de Dados

Este documento resume as entidades centrais do domínio conforme implementadas no MVP (Etapa 1) e serve como referência para backend e para o protótipo Angular minimalista. Os nomes das entidades permanecem em inglês para manter consistência com a base de código.

## Visão Geral
- Todas as entidades utilizam `Guid` como chave primária.
- Campos de auditoria (`CreatedAt`, `UpdatedAt`, `DeletedAt`, `IsActive`) são mantidos em UTC.
- Relações são expressas por coleções (`List<T>`) ou navegações obrigatórias (`required`).
- `Member` é a entidade pivô do domínio — todo relacionamento relevante se conecta a ela.

## Entidades

### Organization
- **Campos principais**: `Description`, `Document` (value object), `Email`, `WhatsAppNumber`, `Settings` (JSONB).
- **Relacionamentos**: `Campaigns`, `Sales`, `OrganizationMembers`, `OrganizationContributors`.
- **Observações**: representa a ONG; configurações dinâmicas ficam em `Settings`.

### Campaign
- **Campos principais**: `Name`, `Description`, `Website`, `CheckoutSite`, `Status`.
- **Relacionamentos**: pertence a uma `Organization`; possui `Sales`, `VoucherTemplates`, `CampaignMembers`, `CampaignContributors`.
- **Observações**: controla a jornada de venda dos vouchers.

### Member
- **Campos principais**: `FirstName`, `LastName`, `Email`, `WhatsAppNumber`, `IsActive`.
- **Relacionamentos**: `Sales`, `OrganizationMembers`, `CampaignMembers`.
- **Observações**: associado ao `AppUser` (Identity) e centraliza o acesso ao domínio.

### Role
- **Campos principais**: `Name`, `Description`.
- **Relacionamentos**: `OrganizationMembers`.
- **Observações**: implementa RBAC para membros em organizações.

### OrganizationMember
- **Chave composta**: `OrganizationId`, `MemberId`.
- **Campos principais**: `RoleId`, `CreatedAt`, `DeletedAt`.
- **Observações**: liga membros às organizações com o respectivo papel.

### Contributor
- **Campos principais**: `FirstName`, `LastName`, `Email`, `WhatsAppNumber`, `IsActive`.
- **Relacionamentos**: `Sales`, `OrganizationContributors`, `CampaignContributors`.
- **Observações**: compradores ou doadores; podem ser recorrentes.

### OrganizationContributor
- **Chave composta**: `OrganizationId`, `ContributorId`.
- **Campos principais**: `LastContributeAt`, `ContributeSum`.
- **Observações**: histórico agregado de contribuições por ONG.

### CampaignContributor
- **Chave composta**: `CampaignId`, `ContributorId`.
- **Observações**: representa a participação do contribuidor em campanhas específicas.

### CampaignMember
- **Chave composta**: `CampaignId`, `MemberId`.
- **Observações**: vincula membros às campanhas com responsabilidades específicas.

### Sale
- **Campos principais**: `PaymentStatus`, `TotalAmount`, `PaymentReceived`, `Currency`, `PaymentMethod`, `Notes`, `PaymentAt`.
- **Relacionamentos**: `Organization`, `Member` (vendedor), `Campaign`, `Contributor`, `VoucherInstances`.
- **Observações**: agrega itens vendidos e facilita conciliação financeira.

### VoucherTemplate
- **Campos principais**: `Name`, `Description`, `UnitPrice`, `TotalAvailable`, `TotalReserved`, `ValidUntil`.
- **Relacionamentos**: `Campaign`, `VoucherInstances`.
- **Observações**: define o produto/experiência comercializada.

### VoucherInstance
- **Campos principais**: `Code` (gerado por `VoucherCode`), `Status`, `CreatedAt`, `Canceled`, `RedeemedAt`.
- **Relacionamentos**: `VoucherTemplate`, `Sale`.
- **Observações**: cupom individual emitido após a venda.

### Auxiliares
- **`VoucherCode`**: helper para gerar códigos amigáveis (alfabeto customizado via NanoID).
- **`Document`**: value object representando CNPJ/CPF.

## Próximos Passos na Etapa 1
- Garantir que `VoucherInstance` seja carregado no endpoint `GET /member/me`.
- Validar modelagem ao criar o endpoint `POST /auth/register`, vinculando `AppUser` ↔ `Member`.
- Expor essas entidades no protótipo Angular para validar o fluxo completo do MVP.
