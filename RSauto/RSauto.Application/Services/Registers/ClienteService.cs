﻿using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities.Cadastro.Cliente;
using RSauto.Domain.Entities.Cadastro.Cliente.Input;
using RSauto.Domain.Entities.Command;
using RSauto.Shared.Utilities;
using System;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Registers
{
    public class ClienteService : IClienteService
    {
        private readonly IBaseCrudRepository _crudRepository;
        private readonly IClienteRepository _repository;
        private readonly ClienteValidate _validate;

        public ClienteService(IBaseCrudRepository crudRepository, ClienteValidate validate, IClienteRepository repository)
        {
            _crudRepository = crudRepository;
            _validate = validate;
            _repository = repository;
        }

        public async Task<ICommandResult> Create(ClienteInput input)
        {
            var retorno = _validate.Validate(input);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));            

            await _crudRepository.Create(input.ToMapper());
            return new CommandResult(true, "Cadastro realizado com sucesso.");
        }

        public async Task<ICommandResult> Listar(ClienteListarInput input)
        {
            return new CommandResult(true, "Consulta realizado com sucesso", await _repository.Listar(input));
        }

        public async Task<ICommandResult> Update(int id, ClienteInput input)
        {
            var retorno = _validate.Validate(input);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (id == 0)
                return new CommandResult(false, "Informe o Id do Cliente");

            await _crudRepository.Update(input.ToMapper(id));
            return new CommandResult(true, "Cadastro realizado com sucesso.");
        }
    }
}
