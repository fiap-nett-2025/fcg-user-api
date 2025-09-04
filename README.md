<h2>﻿👤 FIAP Cloud Users (FCU)</h2>

<u>📚 Sobre o Projeto</u>

Fiap Cloud Users (FCU) é uma API de microsserviço desenvolvida dentro do ecossistema educacional da FIAP 
(Faclidade de Informática e Administração Paliista).
O objetivo é fornecer uma base sólida para gestão de usuários e autenticação/autorização via JWT, 
aplicada em cenários de estudo e práticas de cloud computing, microsserviços e DevOps.

<ul>
	⚙️ Tecnologias e Plataformas utilizadas
	<li>.NET 8</li>
	<li>Visual Studio</li>
	<li>Azure</li>
	<li>EF Core</li>
	<li>ASP.NET Core</li>
	<li>Swagger</li>
	<li>Docker</li>
	<li>New Relic</li>
</ul>

🛠️ Como Executar
Usando Docker

Certifique-se de ter o Docker
 instalado em sua máquina.

No terminal, navegue até a raiz do projeto.

Execute o comando abaixo para construir e iniciar os containers:

<code>
	docker-compose up -d --build
</code>

O serviço estará disponível em:

<code>
	http://localhost:5001/
</code>

Para se autenticar, vá para o endpoint /api/auth/login e use as credenciais abaixo:

<code>
{
  "email": "admin@fiap.com.br",
  "password": "Admin1234!"
}
</code>

Obs: Essas credenciais são criadas automaticamente para fins acadêmicos.

<hr/>
📄 Licença

Este projeto está licenciado sob a licença MIT.

<hr/>
Feito com ❤️ para aprendizado em Cloud & Microsserviços! 🚀
