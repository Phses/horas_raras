# Delete Perfil

Deleta um perfil se o usuario autenticado for um administrador.

**URL** : `/perfil/:id/`

**URL Parametros** : `id=[int]` id do perfil que se deseja apagar.

**Método** : `DELETE`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Dados** : `{}`

## Resposta Sucesso

**Condição** : Se o perfil existe.

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

**Condição** : Se não há um perfil com id da requisição.

**Código** : `404 NOT FOUND`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`
