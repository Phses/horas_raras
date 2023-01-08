# Mostra Tarefa Ativa

Mostra uma tarefa caso o usuário tenha permissão.

**URL** : `/tarefa/:id/`

**URL Parametros** : `id=[int]` id da tarefa

**Método** : `GET`

**Auth requerida** : SIM

**Permissão requerida** :

- Colaboradores : Podem acessar apenas tarefas em que estão alocados;
- Administradores: Podem acessar todas as tarefas Ativas;

**Data**: `{}`

## Resposta Sucesso

**Condição** : Se a tarefa com o id da requisição existir e o usuário tiver permissão para acessá-lo.

**Código** : `200 OK`

**Exemplo** Para Id 1

```json
{
  "id": 1,
  "descricao": "string",
  "horaInicio": "2022-11-26T00:45:53.974Z",
  "horaFinal": "2022-11-26T00:45:53.974Z",
  "totalHoras": 0,
  "avisoHoraFinal": "string",
  "projetoId": 0,
  "usuarioId": 0
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

**Condição** : Se a tarefa não existir.

**Código** : `404 NOT FOUND`

### Ou

**Condição** : Se a tarefa existir, mas o usuário não tiver permissão para acessá-la.

**Código** : `401 UNAUTHORIZED`
