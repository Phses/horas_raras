# Delete Cliente

Deleta um cliente pelo Id

**URL** : `/cliente/:id/`

**URL Parametros** : `id=[int]` id do cliente que se deseja deletar.

**Método** : `DELETE`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Dados** : `{}`

## Resposta Sucesso

**Condição** : Se o cliente existe e o usuário autenticado é um administrador.

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

**Condição** : Se não há um cliente com id da requisição.

**Código** : `404 NOT FOUND`

**Conteúdo** : `{}`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`

**Content** : `{}`
