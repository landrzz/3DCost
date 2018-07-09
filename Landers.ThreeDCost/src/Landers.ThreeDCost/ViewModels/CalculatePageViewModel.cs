using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Acr.UserDialogs;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using System.Collections.Generic;
using Landers.ThreeDCost.Models;
using System.Collections;
using System.Diagnostics;
using Landers.ThreeDCost.Helpers;
using Xamarin.Forms;
using FormsToolkit;

namespace Landers.ThreeDCost.ViewModels
{
	[AddINotifyPropertyChangedInterface]
	public class CalculatePageViewModel : ViewModelBase, INotifyPropertyChanged
    {
		private IUserDialogs _userDialogs { get; }
        private ILoggerFacade _loggerFacade { get; }
        new INavigationService _navigationService { get; }
		public event PropertyChangedEventHandler PropertyChangedd;

        public string WeightOfSpoolAmount { get; set; } = "";

        public string CalcVariableType { get; set; } = "";
        public string CalcVariableUnit { get; set; } = "";  
        public string MeasurementVarAmount { get; set; } = "";
        public string CalcVariableUnitTypePlaceholder { get; set; } = "";
        public string SelectedMaterial { get; set; }
		public string CostOfSpoolAmount { get; set; }
		public string CurrentSelectedFilamentThick { get; set; }
       
		public Color btn175TextColor { get; set; } = StyleKit.GreyChateau;
		public Color btn175BorderColor { get; set; } = StyleKit.Lynch;
		public Color btn175BackgroundColor { get; set; } = StyleKit.Arsenic;
		public Color btn300TextColor { get; set; } = StyleKit.GreyChateau;
		public Color btn300BorderColor { get; set; } = StyleKit.Lynch;
		public Color btn300BackgroundColor { get; set; } = StyleKit.Arsenic;
              
		private object _selected;
		public object Selected
        {         
            get { return _selected; }         
			set { _selected = value; RaisePropertyChanged("Selected"); }
        }

		public ObservableCollection<string> MaterialOptions { get; set; }
        //the header for picker columns
		public ObservableCollection<string> PickerHeader { get; set; }
        //the area
		public ObservableCollection<object> Area { get; set; }
        //country is the collection of country names
        private ObservableCollection<object> MeasurementVariables { get; set; }      
        //state is the collection of state names
        private ObservableCollection<object> Units { get; set; }

        public CalculatePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService, IUserDialogs userDialogs, Prism.Logging.ILoggerFacade loggerFacade)
            : base(navigationService, pageDialogService, deviceService)
        {
            _userDialogs = userDialogs;
            _loggerFacade = loggerFacade;
            _navigationService = navigationService;
            
            try
            {
                BuildPickers();
				MessagingService.Current.Subscribe<string>("CurrentSelectedFilamentThick", SetValues);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
           
        }

		private void BuildPickers()
		{
			Area = new ObservableCollection<object>();
            PickerHeader = new ObservableCollection<string>();
            MeasurementVariables = new ObservableCollection<object>();
            Units = new ObservableCollection<object>();
   
            MeasurementVariables.Add("Weight");
            MeasurementVariables.Add("Length");

			Units.Add("g");
			Units.Add("Kg");

			Area.Add(MeasurementVariables);
			Area.Add(Units);

			PickerHeader.Add("Measure by ");
			PickerHeader.Add("Unit");


			Selected = new ObservableCollection<object>() { "Weight", "g" };
            SetupDataForm();

			MaterialOptions = new ObservableCollection<string>();
			MaterialOptions.Add("PLA - 1.24g/cm3");
			MaterialOptions.Add("ABS - 1.03g/cm3");
		}

        public void SetupDataForm()
        {
            CalcVariableType = (Selected as IList)[0].ToString();
            RaisePropertyChanged("CalcVariableType");
            CalcVariableUnit = (Selected as IList)[1].ToString();
            RaisePropertyChanged("CalcVariableUnit");

            switch (CalcVariableUnit)
            {
                case "mm":
                    CalcVariableUnitTypePlaceholder = "700";
                    break;
                case "M":
                    CalcVariableUnitTypePlaceholder = "7.24";
                    break;
                case "Kg":
                    CalcVariableUnitTypePlaceholder = "1.2";
                    break;
                case "g":
                    CalcVariableUnitTypePlaceholder = "170";
                    break;
                default:
                    CalcVariableUnitTypePlaceholder = "";
                    break;
            }
        }

		public void SetValues(IMessagingService service, string t)
		{
			try
            {
				CurrentSelectedFilamentThick = t;
			}
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException);
            }

		}

		public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);         
        }      

		public void RaisePropertyChanged(string name)      
        {         
            if (PropertyChangedd != null)
                PropertyChangedd(this, new PropertyChangedEventArgs(name));         
        }
    }
}
