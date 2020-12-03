using AutoMapper;
using WinformsTools.MVVM.Bindings;
using WinformsTools.MVVM.Components;
using WinformsTools.MVVM.Core;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DataAccess.Repositories;
using UserManager.Common.Extensions;
using UserManager.DTOs;
using UserManager.Resources;

namespace UserManager.ViewModels
{
    public class RolesViewModel : BindableObject, IViewModel
    {
        public BindingList<RoleListItemDto> Roles { get; }

        private bool _loading;
        private readonly IRoleRepository _roleRepository;
        private readonly IMessageDialog _messageDialog;
        private readonly IMapper _mapper;

        public bool Loading
        {
            get { return _loading; }
            private set { SetProperty(ref _loading, value); }
        }

        public RolesViewModel(IRoleRepository roleRepository, IMessageDialog messageDialog, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _messageDialog = messageDialog;
            _mapper = mapper;
            this.Roles = new BindingList<RoleListItemDto>();
        }

        public async Task Load()
        {
            try
            {
                this.Loading = true;
                var users = await _roleRepository.GetAll();
                var mappedRoles = users.Select(_mapper.Map<RoleListItemDto>).ToList();
                ApplicationDispatcher.Invoke(() => Roles.AddRange(mappedRoles));
            }
            catch (Exception ex)
            {
                _messageDialog.ShowError(title: General.ErrorLoadingRolesTitle, message: ex.Message);
            }
            finally
            {
                this.Loading = false;
            }
        }
    }
}
