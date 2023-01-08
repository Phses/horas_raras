# Gera Hash para alteração de senha

Gera um hash e o envia para o email da requisição, habilitando o usuario para alterar a senha cadastrada.

**URL** : `/conta/hash-senha`

**Metodo** : `PUT`

**Auth requerido** : Não

**Permissão requerida** : Não

**Dados Rquisição**

```json
{
  "email": "string"
}
```

**Dados Exemplo**

- Todos os campos precisam ser preenchidos;

```json
{
  "email": "envia-hash@exemplo.com"
}
```

## Resposta Sucesso

**Condição** : Que o email de requisição esteja cadastrado e o email do usuario esteja confirmada

**Code** : `200 OK`

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

**Condição** : Não for encontrado nenhuma conta relacionada ao email recebido

**Codigo** : `404 NOT FOUND`
