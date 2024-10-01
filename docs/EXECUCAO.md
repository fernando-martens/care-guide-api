## Como Iniciar a Aplicação

### Pré-requisitos

- [WSL](https://learn.microsoft.com/pt-br/windows/wsl/install) (Para Windows)
- [Docker](https://docs.docker.com/get-docker/)

### Passo a passo

1.  Clone o repositório:

```sh
  git clone https://github.com/fernando-martens/care-guide-api.git
```

2.  Certifique-se de que os arquivos Dockerfile e docker-compose.yml estão presentes na raiz do projeto.

3.  Inicie a aplicação com Docker Compose:

```sh
  docker-compose up
```

4.  A aplicação será iniciada e todos os serviços necessários serão configurados automaticamente.

<br>
<br>

**IMPORTANTE:**
Esta API está programada para ser acessada a partir de `http://localhost:8080` e o banco de dados utiliza a porta `5434`.
Certifique-se de que não existam outros recursos ocupando as portas `8080` e `5434` antes de subir o projeto.

Para buildar novamente a aplicação (após alterações no código ou no Dockerfile), execute o comando:

```sh
  docker-compose up -d --build
```

Para parar a execução dos containers, execute o comando:

```sh
  docker-compose stop
```

Para parar a execução dos containers e removê-los, execute o comando:

```sh
  docker-compose down
```
