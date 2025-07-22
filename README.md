# DIO - Trilha .NET - API e Entity Framework
www.dio.me

## Desafio de projeto
Para este desafio, você precisará usar seus conhecimentos adquiridos no módulo de API e Entity Framework, da trilha .NET da DIO.

## Contexto
Você precisa construir um sistema gerenciador de tarefas, onde você poderá cadastrar uma lista de tarefas que permitirá organizar melhor a sua rotina.

Essa lista de tarefas precisa ter um CRUD, ou seja, deverá permitir a você obter os registros, criar, salvar e deletar esses registros.

A sua aplicação deverá ser do tipo Web API ou MVC, fique a vontade para implementar a solução que achar mais adequado.

A sua classe principal, a classe de tarefa, deve ser a seguinte:

![Diagrama da classe Tarefa](diagrama.png)

Não se esqueça de gerar a sua migration para atualização no banco de dados.

## Métodos esperados
É esperado que você crie o seus métodos conforme a seguir:


**Swagger**


![Métodos Swagger](swagger.png)


**Endpoints**


| Verbo  | Endpoint                | Parâmetro | Body          |
|--------|-------------------------|-----------|---------------|
| GET    | /Tarefa/{id}            | id        | N/A           |
| PUT    | /Tarefa/{id}            | id        | Schema Tarefa |
| DELETE | /Tarefa/{id}            | id        | N/A           |
| GET    | /Tarefa/ObterTodos      | N/A       | N/A           |
| GET    | /Tarefa/ObterPorTitulo  | titulo    | N/A           |
| GET    | /Tarefa/ObterPorData    | data      | N/A           |
| GET    | /Tarefa/ObterPorStatus  | status    | N/A           |
| POST   | /Tarefa                 | N/A       | Schema Tarefa |

Esse é o schema (model) de Tarefa, utilizado para passar para os métodos que exigirem

```json
{
  "id": 0,
  "titulo": "string",
  "descricao": "string",
  "data": "2022-06-08T01:31:07.056Z",
  "status": "Pendente"
}
```


## Solução
A implementação do sistema de gerenciamento de tarefas foi concluída com sucesso! Todos os endpoints necessários foram implementados e testados.

### Endpoints Implementados

1. **Obter Tarefa por ID**
   - **GET** `/Tarefa/{id}`
   - Retorna os detalhes de uma tarefa específica
   - Retorna 404 se a tarefa não for encontrada

2. **Listar Todas as Tarefas**
   - **GET** `/Tarefa/ObterTodos`
   - Retorna uma lista com todas as tarefas cadastradas
   - Retorna uma lista vazia se não houver tarefas

3. **Buscar Tarefas por Título**
   - **GET** `/Tarefa/ObterPorTitulo?titulo={titulo}`
   - Busca tarefas que contenham o termo no título (case insensitive)
   - Retorna uma lista de tarefas que correspondam ao critério

4. **Buscar Tarefas por Data**
   - **GET** `/Tarefa/ObterPorData?data={data}`
   - Busca tarefas pela data especificada
   - A data deve estar no formato ISO 8601 (ex: 2023-07-22)

5. **Buscar Tarefas por Status**
   - **GET** `/Tarefa/ObterPorStatus?status={status}`
   - Busca tarefas pelo status (Pendente ou Finalizado)
   - O status é case sensitive e deve corresponder ao enum

6. **Criar Nova Tarefa**
   - **POST** `/Tarefa`
   - Cria uma nova tarefa
   - O status padrão é definido como "Pendente" se não for informado
   - Campos obrigatórios: título e data

7. **Atualizar Tarefa Existente**
   - **PUT** `/Tarefa/{id}`
   - Atualiza uma tarefa existente
   - Retorna 404 se a tarefa não for encontrada
   - Valida os dados de entrada

8. **Excluir Tarefa**
   - **DELETE** `/Tarefa/{id}`
   - Remove uma tarefa do sistema
   - Retorna 204 No Content em caso de sucesso
   - Retorna 404 se a tarefa não for encontrada

### Testes

Um arquivo `test-api.http` foi criado na raiz do projeto para facilitar os testes dos endpoints. Você pode usar este arquivo com o Visual Studio Code e a extensão REST Client para testar a API localmente.

### Próximos Passos

- Configurar autenticação e autorização, se necessário
- Implementar paginação para listagens grandes
- Adicionar mais filtros de busca, se necessário
- Implementar documentação mais detalhada com Swagger/OpenAPI