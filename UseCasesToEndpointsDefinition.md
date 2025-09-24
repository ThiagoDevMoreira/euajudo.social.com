# Necessidade de dados.

## Página de Login

* Recolhe dados de login:
    - usuario
    - senha

### Caso a 'Autenticação' do login seja aceita o endpoint deve retornar:

* Flag de autenticação aceita.
* Datos de `Member` com todos os dados.
* Lista de `Organizations` com dados carregados conforme que ele faça parte, com a adição do campo `Organization.Role` para cada organização da lista.
* Lista de `Campaigns` com dados carregados conforme que ele faça parte.
* Lista de `VoucherTemplate` com dados carregados conforme sejam de campanhas que ele é coordenador ou membro.
* Lista de `VoucherInstance` com dados carregados conforme ele seja o vendedor (`VoucherInstance.Member`)

### Caso a 'Autenticação' seja aceita o end point deve retornar:

* Flag de autenticação rejeitada.

## Estratégia

Simular o login sempre aceito e mockar o banco para desenvolver o endpoint.
