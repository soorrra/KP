using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Input;

namespace _3Racha.ViewModel
{
    public class MenuViewModel:ViewModelBase

    { 
    private ViewModelBase _currentChildView;


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
   
    //--> Commands

    public ICommand ShowDishesViewCommand { get; }
    public MenuViewModel()
    {
         ShowDishesViewCommand = new ViewModelCommand(ExecuteShowDishesViewCommand);
    }

  
    private void ExecuteShowDishesViewCommand(object obj)
    {
        CurrentChildView = new DishesViewModel();
    
    }
  }
}