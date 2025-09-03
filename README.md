👤 FIAP Cloud Users (FCU)

📚 Sobre o Projeto

Fiap Cloud Users (FCU) é uma API de microsserviço desenvolvida dentro do ecossistema educacional da FIAP 
(Faculdade de Informática e Administração Paulista).
O objetivo é fornecer uma base sólida para gestão de usuários e autenticação/autorização via JWT, 
aplicada em cenários de estudo e práticas de cloud computing, microsserviços e DevOps.

⚙️ Tecnologias e Plataformas utilizadas
	- .NET 8
	- Visual Studio
	- Azure
	- EF Core
	- ASP.NET Core
	- Swagger
	- Docker
	- New Relic

🛠️ Como Executar
Usando Docker

Certifique-se de ter o Docker
 instalado em sua máquina.

No terminal, navegue até a raiz do projeto.

Execute o comando abaixo para construir e iniciar os containers:

docker-compose up -d --build


O serviço estará disponível em:

http://localhost:5001/


Para se autenticar, vá para o endpoint /api/auth/login e use as credenciais abaixo:

{
  "email": "admin@fiap.com.br",
  "password": "Admin1234!"
}


Obs: Essas credenciais são criadas automaticamente para fins acadêmicos.

📄 Licença

Este projeto está licenciado sob a licença MIT.

Feito com ❤️ para aprendizado em Cloud & Microsserviços! 🚀