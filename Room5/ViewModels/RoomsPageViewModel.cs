﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Controls;

using Room5.Core.Models;
using Room5.Core.Services;
using Room5.Helpers;
using Windows.UI.Xaml.Controls;

namespace Room5.ViewModels
{
    public class RoomsPageViewModel  : INotifyPropertyChanged
    {
       // private MasterDetailsViewState viewState;
        private ObservableCollection<RoomsViewModel> _rooms = new ObservableCollection<RoomsViewModel>();

        public ObservableCollection<RoomsViewModel> Rooms { get => _rooms; }
        public RoomsPageViewModel()
        {
         //   Task.Run(GetRoomListAsync);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private bool _showRoomEditPanel = false;
        public bool ShowRoomEditPanel
        {
            get => _showRoomEditPanel;
            set
            {
                if (_showRoomEditPanel != value)
                {
                    _showRoomEditPanel = value;
                    OnPropertyChanged();
                }
            }
        }
       
        private bool _addingNewRoom = false;

        public bool AddingNewRoom
        {
            get => _addingNewRoom;
            set
            {
                if (_addingNewRoom != value)
                {
                    _addingNewRoom = value;
                    ShowRoomEditPanel = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _editingRoom = false;

        public bool EditingRoom
        {
            get => _editingRoom;
            set
            {
                if (_editingRoom != value)
                {
                    _editingRoom = value;
                    ShowRoomEditPanel = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool EnableCommandBar => !AddingNewRoom;

        private RoomsViewModel _newRoom;

        public RoomsViewModel NewRoom
        {
            get => _newRoom;
            set
            {
                if (_newRoom != value)
                {
                    _newRoom = value;
                    OnPropertyChanged();
                }
            }
        }

      

        private string _roomName;
        public string RoomName
        {
            get => _roomName;
            set
            {
                if (_roomName != value)
                {
                    _roomName = value;
                    
                    OnPropertyChanged();
                }
            }
        }

        private RoomsViewModel _selectedRoom;

        public RoomsViewModel SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                if (_selectedRoom != value)
                {
                    _selectedRoom = value;
                    /*
                     * hide the form after selecting another room
                     */
                  //  CancelEditRoom();
                   
                    OnPropertyChanged();
                }
            }
        }

        public async Task CreateNewRoomAsync()
        {
            RoomsViewModel newRoom = new RoomsViewModel(new Models.Room());
            NewRoom = newRoom;
          //  await App.Repository.Rooms.UpsertAsync(NewRoom.Model);
            RoomName = default;
            AddingNewRoom = true;
        }

        public void EditRoomAsync()
        {
            RoomName = SelectedRoom.RoomName;
            EditingRoom = true;
            
            
        }

        public async void CancelEditRoom()
        {
            if (NewRoom != null)
            {
                await App.Repository.Rooms.DeleteAsync(_newRoom.Model.Id);
                AddingNewRoom = false;
            }
            if (EditingRoom)
            {
                EditingRoom = false;
            }
        }


        public async Task DeleteRoomAsync()
        {
            // Update this method
        }

        public async void DeleteAndUpdateAsync()
        {
            if (SelectedRoom != null)
            {
                await App.Repository.Rooms.DeleteAsync(_selectedRoom.Model.Id);
                await UpdateRoomsAsync();
            }
        }

       

        public async Task GetRoomListAsync()
        {
            var R = await App.Repository.Rooms.GetAsync();
            if (R == null)
            {
                return;
            }
            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Rooms.Clear();
                foreach (var c in R)
                {
                    Rooms.Add(new RoomsViewModel(c));
                }
                
            });
           
        }

        public async Task SaveChangesAsync()
        {
            if (String.IsNullOrEmpty(RoomName))
            {
                ContentDialog noRoomNameDialog = new ContentDialog
                {
                    Title = "Raumname",
                    Content = "Bitte geben Sie einen Raumnamen ein.",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await noRoomNameDialog.ShowAsync();

            }
            else
            {
                if (EditingRoom == true)
                {
                    SelectedRoom.RoomName = RoomName;
                    await App.Repository.Rooms.UpsertAsync(SelectedRoom.Model);
                    await UpdateRoomsAsync();
                    SelectedRoom = Rooms.First();
                    EditingRoom = false;
                }
                else if (AddingNewRoom == true)
                {
                    NewRoom.RoomName = RoomName;
                    await App.Repository.Rooms.UpsertAsync(NewRoom.Model);
                    NewRoom.RoomName = RoomName;
                    await UpdateRoomsAsync();
                    AddingNewRoom = false;
                }
            }
        }

        public async Task UpdateRoomsAsync()
        {
            foreach (var modifiedRoom in Rooms
                // TODO: dont't save whene name is empty

            .Where(x => x.IsModified).Select(x => x.Model))
            {
                await App.Repository.Rooms.UpsertAsync(modifiedRoom);
            }
            await GetRoomListAsync();
        }

        public async Task LoadDataAsync(MasterDetailsViewState viewState)
        {
            await GetRoomListAsync();

            if ((viewState == MasterDetailsViewState.Both) && (Rooms.Count > 0))
            {

                SelectedRoom = Rooms.First();
            }
        }
    }
}
