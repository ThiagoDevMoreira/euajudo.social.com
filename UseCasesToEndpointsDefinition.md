# Necessidade de dados.

## Página de Login

* Recolhe dados de login:
    - usuario
    - senha

Montar mecanismo de login de forma independente das demais regras de negócio. Inclusive para que possa ser mantido/substituído facilmente nas próximas etapas.

### Caso a 'Autenticação' do login seja aceita o endpoint deve retornar:

Esta resposta deve ser montada um DTO entregue pela API para o cliente frontend, com esses dados o frontend tem o necessário para solicitar novas requests para a API conforme necessário.

Apesar de que a entidade `Organization` seja a "maior" em hierarquia de relações entre entidades, no sistema a entidade mais importante é `Member`, pois a partir dele se desdobram todas as outras atividades no sistema.

Aqui a resposta do endpoint:

* Flag de autenticação aceita. (mecanismo de login)

Resolvido o login, dispara a resposta com estes dados:

* Datos de `Member` com todos os dados.
* Lista de `Organizations` com dados carregados conforme que ele faça parte, com a adição do campo `Organization.Role` para cada organização da lista.
* Lista de `Campaigns` com dados carregados conforme que ele faça parte.
* Lista de `VoucherTemplate` com dados carregados conforme sejam de campanhas que ele é coordenador ou membro.
* Lista de `VoucherInstance` com dados carregados conforme ele seja o vendedor (`VoucherInstance.Member`)

### Caso a 'Autenticação' seja aceita o end point deve retornar:

* Flag de autenticação rejeitada.

## Estratégia

Simular o login (ou manter o mais simples possível) e mockar o banco para desenvolver o endpoint.
