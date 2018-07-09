using System;
using System.Collections.Generic;
using System.Linq;
using MvvmHelpers;
using Acr.UserDialogs;
using Prism.AppModel;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using Landers.ThreeDCost.Models;
using Landers.ThreeDCost.Resources;

namespace Landers.ThreeDCost.ViewModels
{
    public class TodoItemDetailViewModel : ViewModelBase
    {
        private IUserDialogs _userDialogs { get; }
        public TodoItemDetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                       IDeviceService deviceService, IUserDialogs userDialogs)
            : base(navigationService, pageDialogService, deviceService)
        {
            _userDialogs = userDialogs;

            Title = AppResources.TodoItemDetailTitle;
            SaveCommand = new DelegateCommand(OnSaveCommandExecuted);
        }

        public TodoItem Model { get; set; }

        public DelegateCommand SaveCommand { get; }

        private bool _isNew;

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            _isNew = parameters.GetValue<bool>("new");
            Model = parameters.GetValue<TodoItem>("todoItem");
        }

        private async void OnSaveCommandExecuted()
        {
            if (_isNew)
            {
                Toast("New Item Saved");
                await _navigationService.GoBackAsync(new NavigationParameters { { "todoItem", Model } });
            }
            else
            {
                Toast("Item Updated");
                await _navigationService.GoBackAsync();
            }
        }
        private void Toast(string message)
        {
            _userDialogs.Toast(new ToastConfig(message)
            {
                Position = ToastPosition.Top
            });
        }
    }
}