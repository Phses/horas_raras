# Delete Projeto

Deleta um projeto se o usuario autenticado for uma administrador.

**URL** : `/projeto/:id/`

**URL Parametros** : `id=[int]` id do projeto que se deseja apagar.

**Método** : `DELETE`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Dados** : `{}`

## Resposta Sucesso

**Condição** : Se o projeto existe.

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

**Condição** : Se não há um projeto com id da requisição.

**Código** : `404 NOT FOUND`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`
