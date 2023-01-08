# Delete usuário

Deleta um usuário pelo Id

**URL** : `/usuario/:id/`

**URL Parametros** : `id=[int]` id do usuário que se deseja deletar.

**Método** : `DELETE`

**Auth requerida** : SIM

**Permissão requerida** : Administrador ou Colaborador

**Dados** : `{}`

## Resposta Sucesso

**Condição** : Se a usuário existe e o usuário autenticado é um administrador ou colaborador (Caso seja um colaborador, é necessário que seja o usuário deletado)

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

**Condição** : Se não há um usuário com id da requisição.

**Código** : `404 NOT FOUND`

**Conteúdo** : `{}`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`

**Content** : `{}`
