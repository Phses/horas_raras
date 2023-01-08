# Vincular colaborador a um projeto

Vincula um colaborador a um projeto existente

**URL** : `/usuario/`

**Método** : `PATCH`

**Auth requerida** : SIM

**Permisssão requerida** : Administrador

**Dados**

```json
{
  "usuarioId": 0,
  "projetoId": 0
}
```

**Exemplo**

- Todos os campos devem ser preenchidos

```json
{
  "usuarioId": 1,
  "projetoId": 1
}
```

## Resposta Sucesso

**Condição** : Colaborador e projeto existam e o usuário da requisição tenha perfil de Administrador.

**Código** : `200 OK`

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

**Condição** : Usuário ou Projeto não estejam cadastrados

**Código** : `404 NOT FOUND`

### Ou

**Condition** : Usuário não possui permissão.

**Code** : `401 Unauthorized`
