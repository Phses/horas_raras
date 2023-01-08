# Mostra Usuários Ativos

Mostra todos os usuários ativos caso o nível de acesso do responsável pela requisição for de Administrador.

**URL** : `/usuario/`

**Method** : `GET`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Dados** : `{}`

## Resposta Sucesso

**Condição** : Auth requerida para acesso aos usuários

**Code** : `200 OK`

**Conteúdo** :

```json
[
  {
    "id": 0,
    "nome": "string",
    "email": "string",
    "perfilId": 0
  },
  {
    "id": 0,
    "nome": "string",
    "email": "string",
    "perfilId": 0
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
