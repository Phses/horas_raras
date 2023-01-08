# Delete tarefa

Deleta um tarefa pelo Id

**URL** : `/tarefa/:id/`

**URL Parameters** : `id=[int]` id da tarefa que se deseja apagar.

**Método** : `DELETE`

**Auth requerida** : SIM

**Permissão requerida** : Administrador ou Colaborador

## Resposta Sucesso

**Condição** : Se a tarefa existe e o usuário autenticado é um administrador ou colaborador (Caso seja um colaborador, é necessário que seja o responsável pela tarefa)

**Código** : `204 NO CONTENT`

**Conteúdo** : `{}`

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

**Condição** : Se não há um tarefa com id da requisição.

**Código** : `404 NOT FOUND`

**Conteúdo** : `{}`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`

**Content** : `{}`
