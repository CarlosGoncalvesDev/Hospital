# HospitalAPI

HospitalAPI é uma aplicação ASP.NET Core que fornece uma API RESTful para gerenciar pacientes, médicos e consultas em um hospital.

## Recursos

- **CRUD de Pacientes, Médicos e Consultas**: A API permite que você crie, leia, atualize e exclua pacientes, médicos e consultas.
- **Autenticação JWT**: A API usa a autenticação JWT para proteger os endpoints. O nome de usuário e a senha para acessar a API são `admin` e `1234`, respectivamente.
- **Entity Framework**: A API usa o Entity Framework como um ORM para abstrair o acesso ao banco de dados.
- **Dapper**: A API também usa o Dapper para realizar operações de banco de dados mais complexas e otimizadas.
- **Testes unitários com xUnit**: O projeto inclui testes unitários escritos com o framework de testes xUnit.

## Como usar

Para usar a API, primeiro autentique-se usando o endpoint de login com o nome de usuário e a senha fornecidos. Isso retornará um token JWT que você deve incluir no cabeçalho `Authorization` de todas as solicitações subsequentes.

Aqui estão alguns exemplos de como usar a API:

- **Obter todos os pacientes**: `GET /api/paciente`
- **Obter um paciente específico**: `GET /api/paciente/{id}`
- **Criar um novo paciente**: `POST /api/paciente`
- **Atualizar um paciente existente**: `PUT /api/paciente/{id}`
- **Excluir um paciente**: `DELETE /api/paciente/{id}`

Substitua `paciente` por `medico` ou `consulta` para realizar operações semelhantes em médicos ou consultas.

## Contribuindo

Contribuições são bem-vindas! Por favor, leia as diretrizes de contribuição antes de enviar um pull request.
