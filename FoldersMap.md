# Sistem folders map

> mapa de referencia da estrutura de pastas do projeto euajudo.social.br

```
\euajudo
\euajudo\src
\euajudo\src\Api //-> Projeto que disponibiliza o sistema para cliente frontend.
\euajudo\src\Api\appsettings.json // contém string de conexão
\euajudo\src\Core //-> Projeto que reune todas as regras de negócio e a modelagem das entidades.
\euajudo\src\Core\Models // definição das entidades.
\euajudo\src\Infra //-> Projeto que gerencia repositório, dbcontext
\euajudo\src\Infra\EuAjudoDbContext.cs
\euajudo\src\InfraTests //-> Projeto para testar a infraestrutura model-dbcontext-postgres e a relação entre entidades.
\euajudo\src\InfraTests\Entities // uma arquivo de teste por entidade e um arquivo de testes de relações entre entidades.
\Blueprint.md //-> referências técnicas e funcionais para o desenvolvimento do projeto euajudo.social.
\EntitiesDataModelMap.md //-> referência da modelagem  de dados das entidades.
\EntitiesRelationsMap.md //-> referência técnicas do relacionamento entre entidades.
\FolderMap.md //-> este arquivo, mapeamento das pastas que compõem o projeto euajudo.social
\EuAjudo.sln
```