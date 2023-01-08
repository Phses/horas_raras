# Criar conta de usuario

Cria uma conta de Administrador ou Colaborador com permissão de admin.

**URL** : `/usuario/`

**Metodo** : `POST`

**Auth requerido** : Sim

**Permissão requerida** : Administrador

**Dados Requisição**

```json
{
  "nome": "string",
  "email": "string",
  "senha": "string",
  "perfilId": 1
}
```

**Exemplo**

- Todos os campos precisam ser preenchidos;
- Email precisa ser em um formato válido;
- Senha precisa conter letra minúscula, letra maiúscula e mínimo de seis caracteres;
- Perfil Administrador: 1
- Perfil Colaborador: 1

```json
{
  "nome": "Pedro",
  "email": "pedro@gmail.com",
  "senha": "senhaSS",
  "perfilId": 1
}
```

## Resposta sucesso

**Codigo** : `201 CREATED`

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

**Condição** : Se uma conta já existir.

**Código** : `400 BAD REQUEST`

### Ou

**Condição** : Se algum item estiver faltando.

**Código** : `400 BAD REQUEST`

### Notas

- Por questões de segurança para cadastro um usuário admin é necessário que seja feito por um usuário com permissão de admin.
