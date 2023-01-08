# Mostra Clientes Ativos

Mostra todos os clientes ativos caso o nível de acesso do responsável pela requisição for de Administrador.

**URL** : `/cliente/`

**Method** : `GET`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Dados** : `{}`

## Resposta Sucesso

**Condição** : Auth requerida para acesso aos clientes

**Code** : `200 OK`

**Conteúdo** :

```json
[
  {
    "id": 0,
    "nome": "string",
    "email": "string"
  },
  {
    "id": 0,
    "nome": "string",
    "email": "string"
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
