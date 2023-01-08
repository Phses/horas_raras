# Atualizar um Cliente

Faz a atualização de um cliente existente.

**URL** : `/cliente/:id/`

**Método** : `PUT`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Dados**

Os campos que não se desejam alterar deverão ser preenchidos com os dados atuais

```json
{
  "nome": "string",
  "email": "string"
}
```

**Exemplo**

```json
{
  "nome": "Raro Labs",
  "email": "raro@gmail.com"
}
```

## Resposta sucesso

**Condição** : Atualização de um cliente existente e feita por um perfil administrador.

**Código** : `204 NO CONTENT`

## Resposta erro

**Conteúdo** :

```json
{
  "codigo": "Nenhum",
  "descricao": "string",
  "mensagens": ["string"],
  "detalhe": "string"
}
```

**Condição** : cliente não existe

**Código** : `404 NOT FOUND`

**Content** : `{}`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`
