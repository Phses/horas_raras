using System.Linq.Expressions;
using HorasRaras.Domain.Interfaces.Repository;
using HorasRaras.Domain.Interfaces.Service;
using HorasRaras.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using HorasRaras.Domain.Utils;
using HorasRaras.Domain.Exceptions;

namespace HorasRaras.Service
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> _repository;
        public readonly int? UserId;
        public readonly string UserPerfil;
        public readonly string UserEmail;

        public BaseService(IBaseRepository<T> repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            UserId = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.SerialNumber).ToInt();
            UserPerfil = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.Role);
            UserEmail = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.Email);
        }

        public virtual async Task<List<T>> ObterTodosAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.ListAsync(expression);
        }

        public virtual async Task<T> ObterAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await _repository.FindAsync(expression);
            if (entity == null)
                throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado");

            return entity;
        }

        public virtual async Task<List<T>> ObterTodosAsync()
        {
            return await _repository.ListAsync(x => x.Ativo);
        }

        public virtual async Task<T> ObterPorIdAsync(int id)
        {
            var entity = await _repository.FindAsync(x => x.Id == id && x.Ativo);
            if (entity == null)
                throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {id}");

            return entity;
        }

        public virtual async Task AdicionarAsync(T entity)
        {
            entity.DataInclusao = DateTime.Now;
            entity.UsuarioInclusao = UserId;
            await _repository.AddAsync(entity);
        }

        public virtual async Task DeletarAsync(int id)
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null)
                throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {id}");
            
            entity.UsuarioAlteracao = UserId;
            entity.DataAlteracao = DateTime.Now;
            entity.Ativo = false;
            await _repository.EditAsync(entity);
        }

        public virtual  async Task AlterarAsync(T entity)
        {
            var find = await _repository.FindAsNoTrackingAsync(x => x.Id == entity.Id && x.Ativo);
            if (entity == null)
                throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {entity}");

            entity.DataInclusao = find.DataInclusao;
            entity.DataAlteracao = DateTime.Now;
            entity.UsuarioAlteracao = UserId;
            await _repository.EditAsync(entity);
        }
    }

}

