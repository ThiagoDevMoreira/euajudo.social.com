# Referências de Testes Automatizados — Etapa 1

A suíte de testes vive no projeto `src/InfraTests` e usa fixtures que levantam um banco PostgreSQL temporário para validações de integração.

## Objetivos de cobertura para o MVP
1. **Autenticação e cadastro**
   - Testes de integração garantindo que `POST /auth/login` retorne token válido para credenciais seeded.
   - Novos testes cobrindo `POST /auth/register`, validando criação de `AppUser` + `Member` e rejeição de e-mails duplicados.
2. **Member profile**
   - Testar `GET /member/me` carregando organizações, campanhas, templates e **voucher instances** associados ao membro autenticado.
3. **Modelagem de entidades**
   - Manter testes por entidade validando regras de `Core.Models` (ex.: obrigatoriedade de campos, relacionamentos).

## Arquivos de apoio
- `EntitiesDataModelMap.md` — mapa resumido das entidades e relacionamentos.
- `ValidationEntityFields.cs` — helpers para validar regras de domínio.
- `EntityMockFactory.cs` — fabrica mocks coerentes com o domínio para cenários de teste.

## Boas práticas
- Use `AsNoTracking()` nos queries de leitura para evitar side-effects nos testes.
- Adote nomenclatura `Feature_Scenario_ExpectedBehavior` nos métodos.
- Priorize cenários ponta a ponta que reflitam o consumo real pelo frontend Angular minimalista.
