# Alterar tarefa hora final

Altera a hora final de uma tarefa

**URL** : `/tarefa/:id/`

**URL Parametros** : `id=[int]` id da tarefa

**Método** : `PATCH`

**Auth requerida** : YES

**Permisssão requerida** : Administrador ou Colaborador

**Dados**

Todos os campos devem ser preenchidos

```json
{
  "horaFinal": "2022-11-26T00:50:21.198Z"
}
```

## Resposta Sucesso

**Condição** : Hora final seja posterior a hora de ínicio da tarefa e o usuário tenha permissão para alterar a tarefa.

**Código** : `200 OK`

**Conteúdo** :

```json
{}
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

**Condição** : Tarefa não esteja cadastrada

**Código** : `404 NOT FOUND`

### Ou

**Condição** : Usuário não possui permissão.

**Code** : `401 Unauthorized`
