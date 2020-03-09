using System;
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

namespace Room5.ViewModels
{
    public class RoomsPageViewModel  : INotifyPropertyChanged
    {
        private MasterDetailsViewState viewState;
        private ObservableCollection<RoomsViewModel> _rooms = new ObservableCollection<RoomsViewModel>();

        public ObservableCollection<RoomsViewModel> Rooms { get => _rooms; }
        public RoomsPageViewModel()
        {
         //   Task.Run(GetRoomListAsync);
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

       


        private RoomsViewModel _selectedRoom;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public RoomsViewModel SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                if (_selectedRoom != value)
                {
                    _selectedRoom = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task CreateNewRoomAsync()
        {
            RoomsViewModel newRoom = new RoomsViewModel(new Models.Room());
            NewRoom = newRoom;
            await App.Repository.Rooms.UpsertAsync(NewRoom.Model);
            AddingNewRoom = true;
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

        public async Task DeleteNewRoomAsync()
        {
            if (NewRoom != null)
            {
                await App.Repository.Rooms.DeleteAsync(_newRoom.Model.Id);
                AddingNewRoom = false;
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

        public async Task SaveInitialChangesAsync()
        {
            await App.Repository.Rooms.UpsertAsync(NewRoom.Model);
            await UpdateRoomsAsync();
            AddingNewRoom = false;
        }

        public async Task UpdateRoomsAsync()
        {
            foreach (var modifiedRoom in Rooms
            .Where(x => x.IsModified).Select(x => x.Model))
            {
                await App.Repository.Rooms.UpsertAsync(modifiedRoom);
            }
            await GetRoomListAsync();
        }

        public async Task LoadDataAsync(MasterDetailsViewState viewState)
        {
            await GetRoomListAsync();

            if (viewState == MasterDetailsViewState.Both)
            {
                SelectedRoom = Rooms.First();
            }
        }
    }
}
