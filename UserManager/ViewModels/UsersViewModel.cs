﻿using AutoMapper;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DataAccess;
using MvvmTools.Components;
using UserManager.DTOs;
using MvvmTools.Bindings;
using MvvmTools.Core;
using UserManager.Resources;
using Easy.MessageHub;
using UserManager.Events;

namespace UserManager.ViewModels
{
    public class UsersViewModel : BindableObject, IViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageDialog _messageDialog;
        private readonly IMapper _mapper;
        private readonly IMessageHub _eventAggregator;

        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            private set { SetProperty(ref _loading, value); }
        }

        public BindingList<UserListItemDto> Users { get; }

        public UsersViewModel(
            IUserRepository userRepository,
            IMessageDialog messageDialog,
            IMapper mapper,
            IMessageHub eventAggregator)
        {
            _userRepository = userRepository;
            _messageDialog = messageDialog;
            _mapper = mapper;
            this.Users = new BindingList<UserListItemDto>();
            _eventAggregator = eventAggregator;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _eventAggregator.Subscribe<UserCreatedEvent>(OnUserCreated);
        }

        public async Task Load()
        {
            try
            {
                this.Loading = true;
                var users = await _userRepository.GetAll();
                var mappedUsers = users.Select(_mapper.Map<UserListItemDto>).ToList();
                ApplicationDispatcher.Invoke(() => Users.AddRange(mappedUsers));
            }
            catch (Exception ex)
            {
                _messageDialog.ShowError(title:General.ErrorLoadingUsersTitle, message: ex.Message);
            }
            finally
            {
                this.Loading = false;
            }
        }

        public void OnUserCreated(UserCreatedEvent evt)
        {
            Users.Add(_mapper.Map<UserListItemDto>(evt.CreatedUser));
        }
    }
}
