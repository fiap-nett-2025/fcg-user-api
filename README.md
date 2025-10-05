﻿# 🎮 FIAP Cloud Games (FCG) - Game Service

## 📚 Sobre o Projeto

Fiap Cloud Users (FCU) é uma API de microsserviço desenvolvida dentro do ecossistema educacional da FIAP 
(Faclidade de Informática e Administração Paliista).
O objetivo é fornecer uma base sólida para gestão de usuários e autenticação/autorização via JWT, 
aplicada em cenários de estudo e práticas de cloud computing, microsserviços e DevOps.

[Documentação](https://www.notion.so/Fiap-Cloud-Games-FCG-1dea50ade75480e78653c05e2cca2193?pvs=4)

## 🎮 Sobre o Serviço de Usuários

O serviço de usuário é responsável por gerenciar os dados de usuários, niveis de acesso e biblioteca de jogos. Ele oferece funcionalidades para criar, ler, atualizar e deletar dados desses itens.

## ⚙️ Tecnologias e Plataformas utilizadas

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio](https://visualstudio.microsoft.com/pt-br/)
- [EF Core](https://learn.microsoft.com/pt-br/ef/core/)
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [XUnit](https://xunit.net/)
- [Swagger](https://swagger.io/)
- [Docker](https://www.docker.com/)

## 🛠️ Como Executar

### Usando Docker

1. Certifique-se de ter o [Docker](https://www.docker.com/get-started/) instalado em sua máquina.
2. No terminal, navegue até a raiz do projeto.
3. Execute o comando abaixo para construir e iniciar os containers:

```bash
docker-compose up -d --build
```

4. O serviço estará disponível em `http://localhost:5001/`.

Para se autenticar, vá para o endpoint /api/auth/login e use as credenciais abaixo:

<code>
{
  "email": "admin@fiap.com.br",
  "password": "Admin1234!"
}
</code>

Obs: Essas credenciais são criadas automaticamente para fins acadêmicos.

## 🧪 Testes

- Para rodar os testes, utilize o **Test Explorer** do Visual Studio.
- Ou execute via terminal:

```bash
dotnet test
```

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

## 📄 Licença

Este projeto está licenciado sob a licença MIT.

---

Feito com ❤️!

