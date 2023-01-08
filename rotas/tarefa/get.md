# Mostra Tarefas Ativas

Mostra todas as tarefas ativas caso o nível de acesso do responsável pela requisição for de Administrador, para colaboradores é retornado somente as tarefas que ele é o responsável.

**URL** : `/tarefa/`

**URL** : `/tarefa/mes/` Tarefas do mês atual

**URL** : `/tarefa/semana/` Tarefas até 7 dias anteriores a data atual

**URL** : `/tarefa/dia/` Tarafas do dia atual

**URL** : `/tarefa/projeto/:id/` Tarafas relacionadas a um projeto

**URL Parametros** : `id=[int]` id do projeto

**Method** : `GET`

**Auth requerida** : SIM

**Permissão requerida** : Administrador ou Colaborador

**Dados** : `{}`

## Resposta Sucesso

**Condição** : Auth requerida para acesso as tarefa

**Code** : `200 OK`

**Conteúdo** :

```json
[
  {
    "id": 0,
    "descricao": "string",
    "horaInicio": "2022-11-26T00:45:53.974Z",
    "horaFinal": "2022-11-26T00:45:53.974Z",
    "totalHoras": 0,
    "avisoHoraFinal": "string",
    "projetoId": 0,
    "usuarioId": 0
  },
  {
    "id": 0,
    "descricao": "string",
    "horaInicio": "2022-11-26T00:45:53.974Z",
    "horaFinal": "2022-11-26T00:45:53.974Z",
    "totalHoras": 0,
    "avisoHoraFinal": "string",
    "projetoId": 0,
    "usuarioId": 0
  }
]
```

## Resposta Erro

**Conteúdo** :

```json
{
  "codigo": "Nenhum",
  "descricao": "string",
  "mensagens": ["string"],
  "detalhe": "string"
}
```

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`

**Content** : `{}`
