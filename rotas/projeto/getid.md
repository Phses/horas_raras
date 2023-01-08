# Mostra Projeto Ativo

Mostra um projeto caso o usuário tenha permissão.

**URL** : `/projeto/:id/`

**URL Parametros** : `id=[int]` id do projeto

**Método** : `GET`

**Auth requerida** : SIM

**Permissão requerida** :

- Colaboradores : Podem acessar apenas projetos em que estão alocados;
- Administradores: Podem acessar todos os projetos Ativos;

## Resposta Sucesso

**Condição** : Se o projeto existir e o usuário tiver permissão para acessá-lo.

**Código** : `200 OK`

**Exemplo**

```json
{
  "id": 1,
  "nome": "Horas Raras",
  "custoPorHora": 200,
  "dataFinal": "2022-12-24T17:36:35.333Z",
  "clienteId": 1
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

**Condição** : Se o projeto não existir.

**Código** : `404 NOT FOUND`

**Content** : `{}`

### Ou

**Condição** : Se o projeto existir, mas o usuário não tiver permissão para acessá-lo.

**Código** : `401 UNAUTHORIZED`
