# Mostra projetos Ativos

Mostra todos os projetos caso o nível de acesso for de Administrador, para colaboradores é retornado somente os projetos em que ele está alocado.

**URL** : `/projeto/` Todas tarefas

**Method** : `GET`

**Auth requerida** : SIM

**Permissão requerida** : Administrador ou Colaborador

**Dados** : `{}`

## Resposta Sucesso

**Condição** : Auth requerida para acesso aos projetos

**Code** : `200 OK`

**Conteúdo** :

```json
[
  {
    "id": 1,
    "nome": "Trabalho Final",
    "custoPorHora": 150,
    "dataFinal": "2022-12-24T17:25:42.372Z",
    "clienteId": 1
  },
  {
    "id": 2,
    "nome": "Horas Raras",
    "custoPorHora": 200,
    "dataFinal": "2023-01-24T17:25:42.372Z",
    "clienteId": 2
  }
]
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

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`
