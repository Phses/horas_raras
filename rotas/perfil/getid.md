# Mostra Projeto Ativo

Mostra um perfil caso o usuário tenha permissão.

**URL** : `/projeto/:id/`

**URL Parameters** : `id=[int]` id do projeto

**Método** : `GET`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Data**: `{}`

## Resposta Sucesso

**Condição** : Se o perfil existir e o usuário tiver permissão para acessá-lo.

**Código** : `200 OK`

**Exemplo**

```json
{
  "id": 0,
  "nome": "string"
}
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

**Condição** : Se o projeto não existir.

**Código** : `404 NOT FOUND`

**Content** : `{}`

### Ou

**Condição** : Se o projeto existir, mas o usuário não tiver permissão para acessá-lo.

**Código** : `401 UNAUTHORIZED`
