# Mostra usuário Ativo

Mostra uma usuário caso o usuário da requisição tenha permissão.

**URL** : `/usuario/:id/`

**URL Parametros** : `id=[int]` id do usuário

**Método** : `GET`

**Auth requerida** : SIM

**Permissão requerida** :

- Colaboradores : Podem acessar apenas os próprios dados;
- Administradores: Podem acessar todao os usuários Ativos;

**Data**: `{}`

## Resposta Sucesso

**Condição** : Se a usuário com o id da requisição existir e o usuário tiver permissão para acessá-lo.

**Código** : `200 OK`

**Exemplo** Para Id 1

```json
{
  "id": 0,
  "nome": "string",
  "email": "string",
  "perfilId": 0
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

**Condição** : Se o usuário não existir.

**Código** : `404 NOT FOUND`

### Ou

**Condição** : Se a usuário existir, mas o usuário da requisição não tiver permissão para acessá-lo.

**Código** : `401 UNAUTHORIZED`
