# Confirma Email

Através de um token, enviado por email no momento do cadasto, é concedida a permissão para atualizar o status do email do usuario para confirmado

**URL** : `/conta/email/:hash`

**URL Parameters** : `hash=[guid]` permite a busca do usuario e a confirmacão do email de forma segura.

**Metodo** : `PATCH`

**Auth requerido** : Não

**Permissão requerida** : Não

## Resposta Sucesso

**Condição** : Atualização do status do email depende da existência de uma conta relacionada ao hash da requisição

**Codigo** : `200 OK`

## Error Response

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

## Notas

- O botão de confirmar email, locaizado no corpo do email enviado, ainda não é funcional. Após clicar no botão é aberta uma página em uma rota do localhost, o hash localizado nessa rota que é necessário para a modificação do status do email para confirmado.
