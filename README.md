# 🌐 EuAjudo - Plataforma de Apoio Social

O **EuAjudo** é uma plataforma digital criada para conectar pessoas e instituições em ações de solidariedade, voluntariado e doações.  
O objetivo principal é **facilitar a colaboração social** através de tecnologia, oferecendo um espaço simples e seguro para integrar quem precisa de ajuda e quem deseja ajudar.

---

## 🚀 Propósito da Plataforma

- **Conectar** pessoas dispostas a ajudar com instituições e causas sociais.  
- **Organizar** ações de voluntariado, campanhas e doações de forma eficiente.  
- **Escalar** o impacto social usando uma arquitetura robusta, moderna e sustentável.  

---

## 🛠️ Stack Tecnológica

A plataforma será construída sobre uma stack moderna e de longo prazo:

- **Backend:** C# .NET 9 (ASP.NET Core)  
- **Frontend:** Angular (TypeScript)  
- **Banco de Dados:** PostgreSQL  
- **Infraestrutura:** Docker + Kubernetes (futuro)  
- **ORM:** Entity Framework Core  
- **Versionamento:** GitHub  

---

## 📈 Roadmap de Desenvolvimento

O desenvolvimento será evolutivo, dividido em etapas claras:  

### ✅ Etapa 1 - MVP (atual)
- Estrutura inicial em **ASP.NET MVC**.  
- Configuração do **DbContext** e conexão com PostgreSQL.  
- API básica de autenticação e cadastro de usuários.  

### 🔜 Etapa 2 - API + Infra
- Reorganização da solução em **camadas (API + Infra + Domain + Application)**.  
- Controllers utilizando **DbContext** via camada de infraestrutura.  
- Estrutura de testes unitários básicos.  

### 📌 Etapa 3 - Frontend Angular
- Criação do frontend em Angular.  
- Integração com a API (cadastro, login, listagem inicial de entidades).  
- Layout inicial responsivo.  

### 📌 Etapa 4 - Domínio Expandido
- Implementação de casos de uso principais (doações, campanhas, voluntariado).  
- Regras de negócio organizadas em **camada de domínio**.  
- Ampliação da cobertura de testes automatizados.  

### 📌 Etapa 5 - Enterprise Ready
- Arquitetura completa com todas as camadas:  
  - **API** (controllers, endpoints REST).  
  - **Application** (casos de uso, DTOs).  
  - **Domain** (entidades, agregados, eventos de domínio).  
  - **Infra** (persistência, repositórios, integração externa).  
- Autenticação e autorização robustas (Identity + JWT).  
- Monitoramento, observabilidade e logs estruturados.  

### 📌 Etapa 6 - Escalabilidade
- Preparação para ambientes de nuvem.  
- Containerização com Docker.  
- Orquestração com Kubernetes.  
- CI/CD configurado com GitHub Actions.  

---

## 📂 Estrutura do Projeto (prevista)

/EuAjudo
├── Api # Endpoints da aplicação
├── App # Casos de uso e regras de aplicação
├── Domain # Entidades e lógica de domínio
├── Infra # Infraestrutura e persistência
├── Front # Aplicação Angular
└── docs # Documentação do projeto

---

### 💡 Sobre o Projeto

O **EuAjudo** nasceu com a missão de **ampliar o alcance de ações sociais** através da tecnologia.  
Queremos transformar a solidariedade em algo mais acessível, escalável e organizado.  