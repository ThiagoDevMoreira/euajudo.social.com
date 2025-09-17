
# Eu Ajudo — Entities Technical Specification

This document describes the **technical structure** of the main entities of the platform, including
field names, purposes, PostgreSQL data types and .NET (C#) types.

---

## Organization

| Field           | Purpose                                 | PostgreSQL Type | .NET Type         |
|-----------------|-----------------------------------------|-----------------|-------------------|
| Id              | Unique identifier                       | uuid            | Guid              |
| Name            | Organization’s official name            | varchar(200)    | string            |
| Description     | Short description                       | text            | string            |
| Country         | Country                                 | varchar(100)    | string            |
| State           | State or region                         | varchar(100)    | string            |
| City            | City                                    | varchar(100)    | string            |
| Website         | Website URL                             | varchar(255)    | string            |
| Email           | Contact email                           | varchar(255)    | string            |
| WhatsappNumber  | Contact WhatsApp number                 | varchar(50)     | string            |
| DocumentNumber  | Registration number (e.g. CNPJ/CPF)     | varchar(50)     | string            |
| DocumentType    | Type of document                        | varchar(50)     | string            |
| Settings        | Dynamic settings (UI/configurations)    | jsonb           | JsonDocument      |
| IsActive        | Whether the organization is active      | boolean         | bool              |
| CreatedAt       | Record creation timestamp               | timestamptz     | DateTimeOffset    |
| UpdatedAt       | Record update timestamp                 | timestamptz     | DateTimeOffset?   |
| IsDeleted       | Soft delete flag                        | boolean         | bool              |
| DeletedAt       | Timestamp of deletion                   | timestamptz     | DateTimeOffset?   |

---

## Member

| Field          | Purpose                        | PostgreSQL Type | .NET Type       |
|----------------|--------------------------------|-----------------|-----------------|
| Id             | Unique identifier              | uuid            | Guid            |
| OrganizationId | Link to organization           | uuid            | Guid            |
| FirstName      | Member’s first name            | varchar(100)    | string          |
| LastName       | Member’s last name             | varchar(100)    | string          |
| Email          | Contact email                  | varchar(255)    | string          |
| WhatsappNumber | Contact WhatsApp number        | varchar(50)     | string          |
| IsActive       | Whether the member is active   | boolean         | bool            |
| CreatedAt      | Record creation timestamp      | timestamptz     | DateTimeOffset  |
| UpdatedAt      | Record update timestamp        | timestamptz     | DateTimeOffset? |
| DeletedAt      | Timestamp of deletion          | timestamptz     | DateTimeOffset? |

---

## Role

| Field       | Purpose             | PostgreSQL Type | .NET Type |
|-------------|---------------------|-----------------|-----------|
| Id          | Unique identifier   | uuid            | Guid      |
| Name        | Role name           | varchar(100)    | string    |
| Description | Description of role | text            | string    |

---

## MemberRole

| Field      | Purpose                         | PostgreSQL Type | .NET Type       |
|------------|---------------------------------|-----------------|-----------------|
| MemberId   | Link to member                  | uuid            | Guid            |
| RoleId     | Link to role                    | uuid            | Guid            |
| CreatedAt  | Record creation timestamp       | timestamptz     | DateTimeOffset  |
| LastUpdate | `<roleAnterior> -> <roleAtual>` | varchar(100)    | string          |
| UpdatedAt  | Record update timestamp         | timestamptz     | DateTimeOffset? |

---

## Campaign

| Field          | Purpose                        | PostgreSQL Type | .NET Type       |
|----------------|--------------------------------|-----------------|-----------------|
| Id             | Unique identifier              | uuid            | Guid            |
| OrganizationId | Link to organization           | uuid            | Guid            |
| Name           | Campaign title                 | varchar(200)    | string          |
| Description    | Campaign description           | text            | string          |
| Website        | Campaign public page URL       | varchar(255)    | string          |
| CheckoutSite   | Campaign checkout page URL     | varchar(255)    | string          |
| IsActive       | Whether the campaign is active | boolean         | bool            |
| CreatedAt      | Record creation timestamp      | timestamptz     | DateTimeOffset  |
| UpdatedAt      | Record update timestamp        | timestamptz     | DateTimeOffset? |
| DeletedAt      | Timestamp of deletion          | timestamptz     | DateTimeOffset? |

---

## VoucherTemplate

| Field          | Purpose                               | PostgreSQL Type | .NET Type       |
|----------------|---------------------------------------|-----------------|-----------------|
| Id             | Unique identifier                     | uuid            | Guid            |
| OrganizationId | Link to organization                  | uuid            | Guid            |
| CampaignId     | Link to campaign                      | uuid            | Guid            |
| Category       | Product category (e.g. Pizza)         | varchar(100)    | string          |
| SubType        | Product subtype (e.g. Mozzarella)     | varchar(100)    | string          |
| Content        | Voucher design/content (JSON)         | jsonb           | JsonDocument    |
| SalesLimit     | Max number of units                   | int             | int             |
| SalesCount     | Current number of units sold          | int             | int             |
| Price          | Unit price                            | numeric(12,2)   | decimal         |
| Currency       | Currency code (e.g. BRL, USD)         | varchar(10)     | string          |
| CheckoutUrl    | Specific checkout URL for voucher     | varchar(255)    | string          |
| IsActive       | Whether the voucher is active         | boolean         | bool            |
| CreatedAt      | Record creation timestamp             | timestamptz     | DateTimeOffset  |
| UpdatedAt      | Record update timestamp               | timestamptz     | DateTimeOffset? |
| DeletedAt      | Timestamp of deletion                 | timestamptz     | DateTimeOffset? |

---

## VoucherInstance

| Field            | Purpose                          | PostgreSQL Type | .NET Type       |
|------------------|----------------------------------|-----------------|-----------------|
| Id               | Unique identifier                | uuid            | Guid            |
| VoucherTemplateId| Link to voucher template         | uuid            | Guid            |
| Code             | Unique voucher code (nanoid)     | varchar(100)    | string          |
| Status           | Voucher status                   | varchar(50)     | string          |
| CreatedAt        | Timestamp when voucher was issued| timestamptz     | DateTimeOffset  |
| RedeemedAt       | When voucher was redeemed        | timestamptz     | DateTimeOffset? |
| RedeemedBySaleId | Sale that redeemed voucher       | uuid            | Guid?           |
| CanceledAt       | When voucher was canceled        | timestamptz     | DateTimeOffset? |

---

## Contributor

| Field          | Purpose                         | PostgreSQL Type | .NET Type       |
|----------------|---------------------------------|-----------------|-----------------|
| Id             | Unique identifier               | uuid            | Guid            |
| FirstName      | Contributor’s first name        | varchar(100)    | string          |
| LastName       | Contributor’s last name         | varchar(100)    | string          |
| Email          | Contributor’s email             | varchar(255)    | string          |
| WhatsappNumber | Contributor’s WhatsApp number   | varchar(50)     | string          |
| IsActive       | Whether profile is active       | boolean         | bool            |
| CreatedAt      | Record creation timestamp       | timestamptz     | DateTimeOffset  |
| UpdatedAt      | Record update timestamp         | timestamptz     | DateTimeOffset? |
| DeletedAt      | Timestamp of deletion           | timestamptz     | DateTimeOffset? |

---

## OrganizationContributor

| Field          | Purpose                       | PostgreSQL Type | .NET Type       |
|----------------|-------------------------------|-----------------|-----------------|
| OrganizationId | Link to organization          | uuid            | Guid            |
| ContributorId  | Link to contributor           | uuid            | Guid            |
| LastContribute | Date of last contribution     | timestamptz     | DateTimeOffset? |
| ContributeSum  | Total amount contributed      | numeric(12,2)   | decimal         |

---

## Sale

| Field          | Purpose                          | PostgreSQL Type | .NET Type       |
|----------------|----------------------------------|-----------------|-----------------|
| Id             | Unique identifier                | uuid            | Guid            |
| OrganizationId | Link to organization             | uuid            | Guid            |
| CampaignId     | Link to campaign (optional)      | uuid            | Guid?           |
| ContributorId  | Link to contributor (optional)   | uuid            | Guid?           |
| PurchaserName  | Buyer’s name                     | varchar(200)    | string          |
| PurchaserEmail | Buyer’s email                    | varchar(255)    | string          |
| PurchaserPhone | Buyer’s phone/WhatsApp           | varchar(50)     | string          |
| TotalAmount    | Total sale amount                | numeric(12,2)   | decimal         |
| Currency       | Currency code                    | varchar(10)     | string          |
| PaymentStatus  | Status (Pending, Paid, etc.)     | varchar(50)     | string          |
| PaymentAt      | Timestamp of payment confirmation| timestamptz     | DateTimeOffset? |
| CreatedAt      | Record creation timestamp        | timestamptz     | DateTimeOffset  |
| UpdatedAt      | Record update timestamp          | timestamptz     | DateTimeOffset? |
| CanceledAt     | Record canceled timestamp        | timestamptz     | DateTimeOffset? |

---

## SaleItem

| Field             | Purpose                        | PostgreSQL Type   | .NET Type |
|-------------------|--------------------------------|-------------------|-----------|
| SaleId            | Link to sale                   | uuid              | Guid      |
| VoucherTemplateId | Link to voucher template       | uuid              | Guid      |
| VoucherCode       | Link to voucher instance       | varchar(8) nanoid | string    |

---

## Payment (optional): não implementado na etapa 1 (MVP)

| Field                 | Purpose                              | PostgreSQL Type | .NET Type       |
|-----------------------|--------------------------------------|-----------------|-----------------|
| Id                    | Unique identifier                    | uuid            | Guid            |
| SaleId                | Link to sale                         | uuid            | Guid            |
| Provider              | Payment provider                     | varchar(100)    | string          |
| ProviderTransactionId | Provider’s transaction reference     | varchar(100)    | string          |
| Status                | Payment status                       | varchar(50)     | string          |
| Amount                | Amount processed                     | numeric(12,2)   | decimal         |
| ProcessedAt           | When payment was processed           | timestamptz     | DateTimeOffset  |
| RawResponse           | Provider raw response (audit/tracing)| jsonb           | JsonDocument    |
