# Mostra projetos Ativos

Mostra todos os perfis cadastrados

**URL** : `/perfil/`

**Method** : `GET`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Dados** : `{}`

## Resposta Sucesso

**Condição** : Auth requerida para acesso aos perfis

**Code** : `200 OK`

**Conteúdo** :

```json
[
  {
    "id": 0,
    "nome": "string"
  },
  {
    "id": 0,
    "nome": "string"
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
