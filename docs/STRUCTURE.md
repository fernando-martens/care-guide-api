# Estrutura da Solução

Abaixo está a descrição dos principais projetos e pastas que compõem a solução do Care Guide.

## Projetos

### CareGuide.API

Responsável pelos controllers que recebem e respondem às requisições HTTP, além de gerenciar o fluxo da API.

### CareGuide.Core

Contém a lógica de negócios da aplicação, incluindo regras, validações e serviços centrais.

### CareGuide.Data

Gerencia o acesso ao banco de dados, persistência dos dados e implementação dos repositórios.

### CareGuide.Infra

Centraliza a configuração de infraestrutura, como injeção de dependências, middlewares e integrações externas.

### CareGuide.Models

Define os modelos de dados (entities), DTOs (Data Transfer Objects), enums e demais modelos utilizados em toda a solução.

### CareGuide.Security

Gerencia funcionalidades de segurança, como autenticação, autorização, hashing e validação de senhas dos usuários.

### CareGuide.Tests

Projeto responsável pelos testes unitários e de integração da solução. Garante a qualidade e o correto funcionamento das funcionalidades implementadas, utilizando boas práticas de automação de testes.
