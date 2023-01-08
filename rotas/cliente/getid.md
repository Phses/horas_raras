# Mostra cliente Ativo

Mostra um cliente caso o usuário da requisição tenha permissão.

**URL** : `/cliente/:id/`

**URL Parametros** : `id=[int]` id do cliente

**Método** : `GET`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Data**: `{}`

## Resposta Sucesso

**Condição** : Se o cliente com o id da requisição existir e o usuário tiver permissão para acessá-lo.

**Código** : `200 OK`

**Exemplo**

```json
{
  "id": 0,
  "nome": "string",
  "email": "string"
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

**Condição** : Se o cliente não existir.

**Código** : `404 NOT FOUND`

### Ou

**Condição** : Se o cliente existir, mas o usuário da requisição não tiver permissão para acessá-lo.

**Código** : `401 UNAUTHORIZED`
