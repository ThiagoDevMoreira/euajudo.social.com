# Use Cases → API Contracts (Etapa 1)

Este guia descreve os contratos mínimos que o backend deve atender e como o protótipo Angular minimalista irá consumi-los. Serve como fonte única de verdade para alinhamento entre API e frontend.

## Login

### Dados coletados no frontend
- `email`
- `password`

O protótipo Angular deve apresentar um formulário simples com validação básica e acionar `POST /auth/login`.

### Resposta esperada do endpoint
Quando as credenciais forem válidas, a API deve retornar um DTO com:
- `token` (JWT)
- `expiresAt`

Após autenticar, o frontend deve solicitar `GET /member/me` para carregar o contexto do usuário.

Em caso de erro de autenticação, retornar mensagem amigável e `401 Unauthorized`.

## Cadastro de usuário

### Dados coletados no frontend
- `firstName`
- `lastName`
- `email`
- `password`
- `whatsAppNumber`

O protótipo Angular deve oferecer um fluxo simples de cadastro e, após sucesso, opcionalmente autenticar automaticamente o usuário reutilizando a resposta do backend.

### Resposta esperada do endpoint `POST /auth/register`
- Confirmação de criação (`201 Created`) com payload contendo o `memberId` e, se disponível, o token JWT para auto login.
- Mensagens de validação claras para campos faltantes ou e-mail duplicado (`400 BadRequest`).

## Perfil do membro autenticado (`GET /member/me`)

Após o login, o frontend consome este endpoint para renderizar o painel inicial.

### Payload esperado
- `member`: dados básicos (`id`, `firstName`, `lastName`, `email`, `whatsAppNumber`).
- `organizations`: lista das organizações com campo adicional `role`.
- `campaigns`: campanhas em que o membro atua.
- `voucherTemplates`: templates associados às campanhas relevantes.
- `voucherInstances`: vouchers emitidos cujo vendedor é o membro autenticado.

O frontend deve usar esse payload para montar cards de resumo e tabelas minimalistas, demonstrando domínio funcional.

## Estratégia de desenvolvimento
1. Formalizar esses contratos em testes de integração (Postman, HTTP files ou xUnit).
2. Implementar o protótipo Angular com serviços tipados (`HttpClient`) espelhando os DTOs.
3. Validar fluxos ponta a ponta usando usuários seeded e cenários de cadastro real.

Manter este documento sincronizado com qualquer ajuste no blueprint ou na API garante que backend e frontend evoluam em paralelo durante a Etapa 1.
