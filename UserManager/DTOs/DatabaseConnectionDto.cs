﻿using WinformsTools.MVVM.Bindings;

namespace UserManager.DTOs
{
    public class DatabaseConnectionDto : BindableObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _server;
        public string Server
        {
            get { return _server; }
            set { SetProperty(ref _server, value); }
        }

        private ConnectionStatus _connectionStatus;
        public ConnectionStatus ConnectionStatus
        {
            get { return _connectionStatus; }
            set { SetProperty(ref _connectionStatus, value); }
        }
    }

    public enum ConnectionStatus
    {
        Connecting,
        Connected,
        Disconnected
    }
}
