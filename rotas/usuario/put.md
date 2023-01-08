# Atualizar uma Usuário

Faz a atualização de um usuário existente.

**URL** : `/usuario/:id/`

**Método** : `PUT`

**Auth requerida** : SIM

**Permissão requerida** : Administrador ou Colaborador

**Dados**

Os campos que não se desejam alterar deverão ser preenchidos com os dados atuais. O perfilId não pode ser alterado.

```json
{
  "id": 0,
  "nome": "string",
  "email": "string",
  "perfilId": 0
}
```

**Exemplo**

```json
{
  "id": 1,
  "nome": "Pedro Henrique",
  "email": "pedro@gmail.com",
  "perfilId": 1
}
```

## Resposta sucesso

**Condição** : Atualização de uma usuário existente e feita por um perfil administrador ou colaborador.

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

**Condição** : usuário não existe

**Código** : `404 NOT FOUND`

**Content** : `{}`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`

## Notas

- Essa rota não permite que um usuario altere seu perfil.
