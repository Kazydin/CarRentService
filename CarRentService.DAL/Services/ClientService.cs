using System.Collections.ObjectModel;
using AutoMapper;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Validators;
using GuardNet;

namespace CarRentService.DAL.Services;

public class ClientService : BaseCrudService<Client>, IClientService
{
    public override ObservableCollection<Client> Table { get; set; }

    private readonly AppState _appState;

    private static bool _subscribedToUserChanged;

    public ClientService(IDataStoreContext store,
        ClientValidator validator,
        IMapper mapper,
        AppState appState) : base(store, validator, mapper)
    {
        _appState = appState;

        if (!_subscribedToUserChanged)
        {
            appState.OnUserChanged += SetTable;
            _subscribedToUserChanged = true;
        }

    }

    private void SetTable(object? sender, EventArgs? eventArgs)
    {
        if (_appState.CurrentUser != null)
        {
            if (_appState.CurrentUser.Role == ManagerRoleEnum.Admin)
            {
                Table = _store.Client;
            }
            else
            {
                Table = _store.Client.Where(p => _appState.CurrentUser.BranchIds.Contains(p.Id)).ToObservableCollection();
            }
        }
    }

    public override Client? TryFindById(int id)
    {
        return Table.FirstOrDefault(p => p.Id == id);
    }

    public ObservableCollection<ClientDto> GetDtos()
    {
        return Table.Select(p => GetDto(p.Id)).ToObservableCollection();
    }

    public ClientDto GetDto(int clientId)
    {
        var entity = _store.Client.FirstOrDefault(p => p.Id == clientId);

        Guard.NotNull(entity, nameof(entity), $"Клиент с ID {entity} не найден");

        entity = _mapper.Map<Client>(entity);

        var dto = _mapper.Map<ClientDto>(entity);

        IncludeBranch(dto);
        IncludeRentals(dto);

        return dto;
    }

    public void IncludeBranch(ClientDto dto)
    {
        dto.Branch = _mapper.Map<BranchDto>(_store.Branch.FirstOrDefault(p => p.Id == dto.BranchId));
    }

    public void IncludeRentals(ClientDto dto)
    {
        dto.Rentals =
            _mapper.Map<ObservableCollection<RentalDto>>(_store.Rental.Where(p => p.ClientId == dto.Id));
    }

    protected override void CleanEntity(Client entity)
    {
    }
}