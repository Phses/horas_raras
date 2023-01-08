# Cria Tarefa

Cria um tarefa e vinculada à um projeto.

**URL** : `/tarefa/`

**Method** : `POST`

**Auth requerida** : SIM

**Permissão requerida** : Administrador ou Colaborador

**Dados requisição**

- HoraFinal : Opcional

```json
{
  "projetoId": 0,
  "usuarioId": 0,
  "descricao": "string",
  "horaInicio": "2022-11-26T00:56:36.470Z",
  "horaFinal": "2022-11-26T00:56:36.470Z"
}
```

**Exemplo**

```json
{
  "projetoId": 1,
  "usuarioId": 2,
  "descricao": "Documentação projeto final",
  "horaInicio": "2022-11-26T16:56:36.470Z",
  "horaFinal": "2022-11-26T20:56:36.470Z"
}
```

## Resposta Sucesso

**Condição** : Se todos os campos forem preenchidos corretamente

**Código** : `201 CREATED`

**Content example**

```json
{
  "id": 1
}
```

## Resposta erro

**Conteúdo**

```json
{
  "codigo": "Nenhum",
  "descricao": "string",
  "mensagens": ["string"],
  "detalhe": "string"
}
```

**Condição** : Se houver campos faltando.

**Code** : `400 BAD REQUEST`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`
