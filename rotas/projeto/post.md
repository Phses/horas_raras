# Cria Projeto

Cria um projeto e vincula colaboradores a ele. Somente Administradores possuem permissão de acesso para criar projetos.

**URL** : `/projeto/`

**Method** : `POST`

**Auth requerida** : SIM

**Permissão requerida** : Administradores

**Dados requisição**

- CustoPorHora : Opcional,
- DataFinal : Opcional

```json
{
  "nome": "string",
  "custoPorHora": double,
  "dataFinal": "DateTime",
  "clienteId": int,
  "usuariosEmail": [
    "string"
  ]
}
```

**Exemplo**

```json
{
  "nome": "Horas Raras",
  "custoPorHora": 150.0,
  "dataFinal": "2022-11-24T22:41:18.960Z",
  "clienteId": 1,
  "usuariosEmail": ["pedro@gmail.com", "luigi@gmail.com", "lili@gmail.com"]
}
```

## Resposta Sucesso

**Condição** : Se não houver um projeto ja cadastrado. Para que seja a vinculação dos colaboradores ao projeto é necessário que os usuários estejam cadastrados, caso não estejam será enviado um email para o usuário notificando sobre o erro.

**Código** : `201 CREATED`

**Content example**

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

**Condição** : Se o projeto já existir.

**Código** : `400 BAD REQUEST`

### Ou

**Condição** : Se houver campos faltando.

**Código** : `400 BAD REQUEST`

### Ou

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`
