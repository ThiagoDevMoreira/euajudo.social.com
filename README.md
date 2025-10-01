# 🌐 EuAjudo — Tecnologia para ampliar impacto social

EuAjudo é uma plataforma SaaS pensada para ONGs e coletivos que vendem produtos solidários (pizzas, rifas, artesanato) para financiar suas ações. O objetivo é digitalizar o ciclo completo de campanhas, da venda do voucher à retirada do item, oferecendo transparência e autonomia para equipes voluntárias.

Este repositório consolida o MVP inicial e evoluirá seguindo um roadmap didático que demonstra como um desenvolvedor .NET/Angular pode estruturar um produto real do zero até padrões enterprise.

---

## 🚀 Por que este projeto importa
- **Impacto social real**: ajuda instituições a organizar arrecadações, controlar estoques e dar visibilidade aos resultados.
- **Experiência prática**: une backend .NET 9, frontend Angular e PostgreSQL, refletindo stacks exigidas em projetos corporativos.
- **Portfólio profissional**: demonstra domínio de arquitetura em camadas, testes e boas práticas — ideal para talentos em transição de júnior para pleno.

---

## 🛠️ Stack atual do MVP (Etapa 1)
- **Backend**: ASP.NET Core (.NET 9), Entity Framework Core, Identity + JWT.
- **Frontend de referência**: Angular (PWA minimalista) consumindo os endpoints do MVP.
- **Banco de dados**: PostgreSQL.
- **Infra local**: Docker Compose (em planejamento) e migrations controladas via `src/Infra`.

---

## 📈 Roadmap evolutivo
O roadmap segue o blueprint revisado, com entregas incrementais:

### 1. API + Frontend Angular minimalista + Infra (em andamento)
- Endpoints de autenticação (`login` e `register`).
- Endpoint autenticado `GET /member/me` retornando organizações, campanhas, templates e vouchers.
- Protótipo Angular minimalista para validar contratos e UX básica.
- Testes de integração cobrindo fluxos críticos.

### 2. API + Application + Infra
- Introdução de uma camada Application intermediando controllers e DbContext.
- Refinamento de validações e regras de negócio fora dos controllers.

### 3. API + Infra como serviços
- Serviços específicos para orquestrar consultas/escritas no DbContext.
- Preparação para desacoplar domínio das entidades de persistência.

### 4. Minimal API + Application simplificado
- Controllers enxutos, orquestração em services.
- Regras mais ricas com princípios SOLID.

### 5. Arquitetura Enterprise (DDD completo)
- Camadas Domain/Application/Infra bem definidas.
- Multi-tenancy, observabilidade, pipelines CI/CD e automações de qualidade.

---

## 📂 Estrutura atual do repositório
```
euajudo.social.com/
├── blueprint.md                     # Roadmap e diretrizes estratégicas
├── README.md                        # Este documento
├── EntitiesDataModelMap.md          # Resumo das entidades do domínio
├── UseCasesToEndpointsDefinition.md # Contratos MVP entre frontend e API
├── TestsReferences.md               # Orientações de testes automatizados
├── EuAjudo.sln
└── src/
    ├── Api/                         # Projeto ASP.NET Core (controllers, Program.cs)
    ├── Core/                        # Entidades de domínio e regras básicas
    ├── Infra/                       # DbContext, migrations, seeds, Identity
    ├── InfraTests/                  # Testes de integração/infra
    └── Utils/                       # Utilitários compartilhados
```

O frontend Angular minimalista será versionado em `src/Frontend` durante a conclusão da Etapa 1.

---

## 💼 Para recrutadores e talent hunters
- **Foco em entregas incrementais**: cada etapa possui documentação, backlog técnico e métricas de qualidade.
- **Boas práticas presentes**: uso de DTOs, testes de integração, autenticação segura e planejamento de PWA.
- **Capacidade de comunicação**: documentação alinhada (`blueprint.md`, contratos e mapa de dados) facilita onboarding de novos colaboradores.

Interessado em acompanhar a evolução? Abra uma issue ou entre em contato para conversar sobre o roadmap e oportunidades de colaboração.

---

## ▶️ Como executar (prévia)
1. Clonar o repositório e abrir a solução `EuAjudo.sln`.
2. Restaurar dependências (`dotnet restore`).
3. Configurar a string de conexão PostgreSQL no `appsettings.Development.json`.
4. Executar `dotnet ef database update` no projeto `src/Infra` para aplicar migrations.
5. Rodar a API (`dotnet run --project src/Api`).
6. (Em breve) Executar `npm install && ng serve` dentro de `src/Frontend` para o protótipo Angular.

---

## 🤝 Contribuindo
Contribuições são bem-vindas! Utilize Pull Requests descrevendo claramente o objetivo e referencie o blueprint quando abrir novas features. Testes automatizados são obrigatórios para endpoints novos.

---

## 📄 Licença
Projeto em desenvolvimento; a licença definitiva será definida ao final do MVP.
