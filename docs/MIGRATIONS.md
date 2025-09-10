# Guia de Migrations (Entity Framework Core)

Este documento orienta como criar e aplicar migrations no projeto utilizando o Entity Framework Core com PostgreSQL.

## Pré-requisito

Antes de executar os comandos de migrations, certifique-se de que a ferramenta `dotnet-ef` está instalada:

```sh
 dotnet tool install --global dotnet-ef
```

## Principais Comandos

### 1. Criar uma nova migration

```sh
 dotnet ef migrations add MigrationName --project CareGuide.Data --startup-project CareGuide.API
```

- `MigrationName`: Nome descritivo para a migration (ex: `AddPatientTable`).
- O parâmetro `--project` indica onde estão as classes de contexto e migrations.
- O parâmetro `--startup-project` indica o projeto de inicialização da aplicação.

### 2. Aplicar uma migration ao banco de dados

```sh
 dotnet ef database update --project CareGuide.Data --startup-project CareGuide.API
```

- Aplica todas migrações as pendentes.

## Documentação Oficial

Para mais comandos e opções, consulte a documentação oficial do Entity Framework Core:

- [EF Core CLI Reference](https://learn.microsoft.com/pt-br/ef/core/cli/dotnet)
- [Migrations Overview](https://learn.microsoft.com/pt-br/ef/core/managing-schemas/migrations/)
