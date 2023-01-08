# Login

Gera um token para um usuario registrado.

**URL** : `/conta/login/`

**Metodo** : `POST`

**Auth requerido** : NÃO

**Dados Rquisição**

```json
{
  "senha": "string",
  "email": "string"
}
```

**Dados Exemplo**

- Todos os campos precisam ser preenchidos;

```json
{
  "senha": "senhaSS",
  "email": "autenticacao@exemplo.com"
}
```

## Resposta Sucesso

**Condição** : Se existir um usuario com o email de requisição cadastrado e se a senha cadastrada conferir com a senha de requisição.

**Codigo** : `200 OK`

**Exemplo**

```json
{
  "token": "string"
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

**Condição** : Se não existir um usuario cadastrado com o email da requisição ou se as senhas não forem iguais.

**Codigo** : `400 BAD REQUEST`
