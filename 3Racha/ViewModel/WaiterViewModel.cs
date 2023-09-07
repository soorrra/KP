using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _3Racha.ViewModel
{
    public class WaiterViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        //--> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }
        public ICommand ShowMenuViewCommand { get; }
        public ICommand ShowDishesViewCommand { get; }
        public ICommand ShowCreateOrderViewCommand { get; }
        public ICommand ShowRoomViewCommand { get; }
        public ICommand ShowOrderGiveViewCommand { get; }
        public ICommand ShowMainWaiterViewCommand { get; }


        public WaiterViewModel()
        {

            //Initialize commands
            //ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
            ShowMenuViewCommand = new ViewModelCommand(ExecuteShowMenuViewCommand);
            ShowDishesViewCommand = new ViewModelCommand(ExecuteShowDishesViewCommand);
            ShowCreateOrderViewCommand = new ViewModelCommand(ExecuteShowCreateOrderViewCommand);
            ShowRoomViewCommand = new ViewModelCommand(ExecuteShowRoomViewCommand);
            ShowOrderGiveViewCommand = new ViewModelCommand(ExecuteShowOrderGiveViewCommand);
            ShowMainWaiterViewCommand = new ViewModelCommand(ExecuteMainWaiterViewCommand);


            //Default view
            //ExecuteShowHomeViewCommand(null);

        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new StaffViewModel();
            Caption = "Customers";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowMenuViewCommand(object obj)
        {
            CurrentChildView = new MenuViewModel();
            Caption = "Меню";
        }
        private void ExecuteShowDishesViewCommand(object obj)
        {
            CurrentChildView = new DishesViewModel();
            Caption = "Оформление закза";
        }
        private void ExecuteShowCreateOrderViewCommand(object obj)
        {
            CurrentChildView = new CreateOrderViewModel();
            Caption = "Заказы";
        }
        private void ExecuteShowRoomViewCommand(object obj)
        {
            CurrentChildView = new RoomViewModel();
            Caption = "Комната отдыха";
        }
        private void ExecuteShowOrderGiveViewCommand(object obj)
        {
            CurrentChildView = new OrderGiveViewModel();
            Caption = "Выдача заказов";
        }
        private void ExecuteMainWaiterViewCommand(object obj)
        {
            CurrentChildView = new MainViewWaiterModel();
            Caption = "Главная";
        }
        //private void ExecuteShowHomeViewCommand(object obj)
        //{
        //    CurrentChildView = new HomeViewModel();
        //    Caption = "Dashboard";
        //    Icon = IconChar.Home;
        //}




    }
}
