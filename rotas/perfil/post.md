# Cria Perfil

Cria um perfil

**URL** : `/perfil/`

**Method** : `POST`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Dados requisição**

```json
{
  "nome": "string"
}
```

## Resposta Sucesso

**Condição** : Se não houver um perfil com o mesmo nome ja cadastrado.

**Código** : `201 CREATED`

**Conteúdo**

```json
{
  "id": 1
}
```

## Resposta erro

**Exemplo** :

```json
{
  "codigo": "Nenhum",
  "descricao": "string",
  "mensagens": ["string"],
  "detalhe": "string"
}
```

**Condição** : Se o perfil já existir.

**Código** : `400 BAD REQUEST`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`
