# Altera Senha

Através de um hash enviado pela rota, é concedida a permissão para alterar a senha cadastrada

**URL** : `/conta/senha/:hash`

**URL Parameters** : `hash=[string]` permite a busca do usuario e a autorização para alterar senha.

**Metodo** : `PUT`

**Auth requerido** : Não

**Permissão requerida** : Não

**Dados Rquisição**

```json
{
  "novaSenha": "string",
  "novaSenhaConfirmacao": "string"
}
```

**Dados Exemplo**

- Todos os campos precisam ser preenchidos;

```json
{
  "novaSenha": "senhaSS",
  "novaSenhaConfirmacao": "senhaSS"
}
```

## Resposta Sucesso

**Condição** : Atualização da senha se o hash da requisição for identico ao hash salvo no banco de dados e se a senha de confirmação for idêntica a nova senha

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

**Condição** : Não for encontrado nenhuma conta relacionada ao hash recebido

**Code** : `404 NOT FOUND`

**Condição** : Senhas da requisição não forem idênticas.

**Code** : `400 BAD REQUEST`
