## Guia de Execução da Aplicação

### Pré-requisitos

- [Docker Desktop](https://docs.docker.com/get-docker/) instalado e em execução.
- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/) ou superior instalado (para execução local via IIS Express).

### Passo a Passo

1. **Clone o repositório:**

   ```sh
   git clone https://github.com/fernando-martens/care-guide-api.git
   ```

2. **Verifique os arquivos de configuração:**  
   Certifique-se de que os arquivos `Dockerfile` e `docker-compose.yml` estão presentes na raiz do projeto.

---

### Executando via Docker Compose

3. **Inicie a aplicação com Docker Compose:**

   ```sh
   docker-compose up
   ```

   Todos os serviços necessários serão configurados e iniciados automaticamente.

---

### Executando Localmente pelo IIS Express

1. Abra a solução `CareGuide.sln` no Visual Studio.
2. Selecione o projeto de inicialização `CareGuide.API`.
3. No menu superior, selecione o perfil **IIS Express**.
4. Pressione `F5` ou clique em **Iniciar Depuração** para executar a API localmente.
5. A API estará disponível em `http://localhost:5000` (ou na porta configurada em `launchSettings.json`).

   > Certifique-se de que o banco de dados esteja acessível e configurado corretamente em `appsettings.Development.json`.

---

### Informações Importantes

- A API estará disponível em: [http://localhost:8080](http://localhost:8080)
- O banco de dados PostgreSQL utilizará a porta `5434`.
- Certifique-se de que as portas `8080` e `5434` estejam livres antes de iniciar o projeto.

---

### Comandos Úteis

- **Recriar containers após alterações:**

  ```sh
  docker-compose up -d --build
  ```

- **Parar a execução dos containers:**

  ```sh
  docker-compose stop
  ```

- **Parar e remover containers:**
  ```sh
  docker-compose down
  ```

---
