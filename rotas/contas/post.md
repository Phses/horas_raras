# Criar conta de usuario

Cria uma conta de colaborador.

**URL** : `/conta/cadastro`

**Metodo** : `POST`

**Auth requerido** : Não

**Permissão requerida** : Não

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
- Perfil Colaborador: 2

```json
{
  "nome": "Pedro",
  "email": "pedro@gmail.com",
  "senha": "senhaSS",
  "perfilId": 2
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

- Por questões de segurança, já que esta rota não precisa de autenticação, não é possível fazer o cadastro de um Administrador por ela. Sendo necessário usar a rota POST de usuário para cadastrar um Administrador.
