# Mostra o total de horas

Mostra o total de horas trabalhadas em um projeto.

**URL** : `/projeto/horas/:id/`

**URL Parametros** : `id=[int]` id do projeto

**Method** : `GET`

**Auth requerida** : SIM

**Permissão requerida** : Administrador

**Dados** : `{}`

## Resposta Sucesso

**Condição** : Auth requerida para acesso ao total de horas

**Code** : `200 OK`

**Conteúdo** :

```json
{
  "horasTotais": 0
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

**Condição** : Usuário não tem permissão.

**Code** : `401 Unauthorized`

**Condição** : Projeto não existe .

**Code** : `404 NOT FOUND`
