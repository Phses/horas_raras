# Atualizar uma tarefa

Faz a atualização de um tarefa existente.

**URL** : `/tarefa/:id/`

**Método** : `PUT`

**Auth requerida** : SIM

**Permissão requerida** : Administrador ou Colaborador

**Dados**

Os campos que não se desejam alterar deverão ser preenchidos com os dados atuais

```json
{
  "projetoId": 0,
  "usuarioId": 0,
  "descricao": "string",
  "horaInicio": "2022-11-26T01:10:45.021Z",
  "horaFinal": "2022-11-26T01:10:45.021Z"
}
```

**Exemplo**

```json
{
  "projetoId": 1,
  "usuarioId": 2,
  "descricao": "Alteração de dados Tarefa",
  "horaInicio": "2022-11-26T01:10:45.021Z",
  "horaFinal": "2022-11-26T01:10:45.021Z"
}
```

## Resposta sucesso

**Condição** : Atualização de uma tarefa existente e feita por um perfil administrador ou colaborador responsável pela criação da tarefa. Alterações em tarefa serão permitidas até 48h da data de criação da tarefa.

**Código** : `204 NO CONTENT`

## Resposta erro

**Conteúdo** :

```json
{
  "codigo": "Nenhum",
  "descricao": "string",
  "mensagens": ["string"],
  "detalhe": "string"
}
```

**Condição** : tarefa não existe

**Código** : `404 NOT FOUND`

**Content** : `{}`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`

### Ou

**Condição** : Tempo para edição excedido.

**Code** : ``
