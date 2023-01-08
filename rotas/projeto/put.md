# Atualizar um Projeto

Faz a atualização de um projeto existente e ativo.

**URL** : `/projeto/:id/`

**Método** : `PUT`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Dados**

- CustoPorHora : Opcional,
- DataFinal : Opcional

```json
{
  "nome": "string",
  "custoPorHora": double,
  "dataFinal": "2022-11-24T22:53:20.338Z",
  "clienteId": int,
  "usuariosEmail": [
    "Este campo é ignorado"
  ]
}
```

**Exemplo**

```json
{
  {
  "nome": "Horas Raras",
  "custoPorHora": 150.0,
  "dataFinal": "2022-11-24T22:41:18.960Z",
  "clienteId": 1,
  "usuariosEmail": ["Este campo é ignorado"]
}
}
```

## Resposta sucesso

**Condição** : Atualização de um projeto existente e feita por um perfil administrador.

**Code** : `204 NO CONTENT`

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

**Condição** : Projeto não existe

**Código** : `404 NOT FOUND`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`

## Notas

### Dados Ignorados

- A lista de usuários será ignorada pelo mapping, fazendo com que esta rota não tenha como finalidade atualizar os usuários do projeto.
