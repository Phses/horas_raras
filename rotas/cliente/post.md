# Cria Cliente

Cria um cliente

**URL** : `/cliente/`

**Method** : `POST`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Dados requisição**

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

## Resposta Sucesso

**Condição** : Se não houver um cliente ja cadastrado com o email e nome da requisição

**Código** : `201 CREATED`

**Content example**

```json
{
  "id": 1
}
```

## Resposta erro

**Conteúdo**

```json
{
  "codigo": "Nenhum",
  "descricao": "string",
  "mensagens": ["string"],
  "detalhe": "string"
}
```

**Condição** : Se o cliente já existir ou houver campos faltando

**Código** : `400 BAD REQUEST`

**Content** : `{}`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`
