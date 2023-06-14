Tarefas API
Este repositório contém o código-fonte de uma API de gerenciamento de tarefas. A API permite criar, atualizar e consultar tarefas, além de anexar arquivos PDF a cada tarefa.

Tecnologias Utilizadas
A API foi desenvolvida utilizando as seguintes tecnologias:

ASP.NET Core: framework web utilizado para criar a API.
Entity Framework Core: ORM (Object-Relational Mapping) utilizado para interagir com o banco de dados.
C#: linguagem de programação utilizada para implementar a lógica da API.
Microsoft SQL Server: banco de dados utilizado para armazenar as informações das tarefas.
Swagger: ferramenta de documentação e teste de APIs utilizada para documentar a API.
Funcionalidades
A API possui as seguintes funcionalidades:

CRUD (Create, Read, Update, Delete) de tarefas: permite criar, consultar, atualizar e excluir tarefas.
Anexar arquivos PDF às tarefas: permite anexar arquivos PDF a cada tarefa.
Consulta de duração em andamento: permite calcular o período de tempo em que uma tarefa esteve com o status "Em Andamento".
Atualização de status para "Finalizada": permite atualizar o status de uma tarefa para "Finalizada".
Estrutura do Projeto
O projeto está organizado da seguinte forma:

Controllers: contém as classes de controladores responsáveis por receber as requisições e fornecer as respostas.
Models: contém as classes de modelos utilizadas para representar as entidades do sistema.
Repositories: contém as classes de repositórios responsáveis por interagir com o banco de dados.
Services: contém as classes de serviços responsáveis por implementar a lógica de negócio.
Data: contém as classes de contexto do Entity Framework Core e as configurações das entidades.
Migrations: contém as migrações do Entity Framework Core para criação do banco de dados.
Configuração
Antes de executar a API, é necessário realizar algumas configurações:

Configurar a string de conexão com o banco de dados no arquivo appsettings.json.
Executar as migrações do Entity Framework Core para criar o banco de dados. Para isso, execute o seguinte comando no terminal:
bash
Copy code
dotnet ef database update
Executando a API
Para executar a API, execute o seguinte comando no terminal:

bash
Copy code
dotnet run
A API estará disponível no seguinte endereço: https://localhost:5001.

ATENÇÃO:
Para realizar os metodos Post e Put siga as recomendações abaixo:
ESTE É O CORPO DO PEDIDO:
{
  "id": 0,
  "nome": "string",
  "descricao": "string",
  "dataDeInicio": "2023-06-14T20:28:07.892Z",
  "dataDeFinalizacao": "2023-06-14T20:28:07.892Z",
  "estimativaEmDias": 0,
  "status": 1,
  "usuarioId": 0,
  "usuario": {
    "id": 0,
    "nome": "string"
  },
  "arquivoPDF": "string"
}

ALTERE DESTA FORMA PARA FUNCIONAR:
INSIRA OU ALTERE OS DADOS CONFORME DESEJAR.
{
  "id": 0,
  "nome": "string",
  "descricao": "string",
  "dataDeInicio": "2023-06-14T20:28:07.892Z",
  "dataDeFinalizacao": "2023-06-14T20:28:07.892Z",
  "estimativaEmDias": 0,
  "status": 1,
  "usuarioId": 0  
}

Documentação da API
A documentação da API pode ser acessada através da rota /swagger. Nela, você encontrará informações sobre os endpoints disponíveis, seus parâmetros e respostas esperadas.

Contribuição
Contribuições para melhorias e correções são bem-vindas! Se você encontrar algum problema ou tiver alguma sugestão, sinta-se à vontade para abrir uma issue ou enviar um pull request.

Licença
Este projeto está licenciado sob a licença MIT.
