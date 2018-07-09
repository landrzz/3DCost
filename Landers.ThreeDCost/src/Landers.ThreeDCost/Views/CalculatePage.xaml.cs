using System.Collections;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Syncfusion.XForms.DataForm;
using System;
using System.Diagnostics;
using Landers.ThreeDCost.Helpers;
using FormsToolkit;

namespace Landers.ThreeDCost.Views
{
    public partial class CalculatePage : ContentPage
    {
		string CurrentMeasurementItem;
		string CurrentMeasurementUnit;
        string CurrentCalcVar;
		string CurrentSelectedFilamentThick;

        public CalculatePage()
        {
            InitializeComponent();

			if (Device.RuntimePlatform == Device.iOS)
            {
				spoolweightEntry.Style = Application.Current.Resources["entryLineStyle"] as Style;            
				spoolCostEntry.Style = Application.Current.Resources["entryLineStyle"] as Style;
				calcVarAmountEntry.Style = Application.Current.Resources["entryLineStyle"] as Style;
            }

        }

		private void picker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)      
        {         
            try
            {
                if (picker.ItemsSource != null && CurrentMeasurementItem != (e.NewValue as IList)[0].ToString())            
                {
					//Updated the second column collection based on first column selected value.
					(picker.ItemsSource as ObservableCollection<object>).RemoveAt(1);
                    
					(picker.ItemsSource as ObservableCollection<object>).Add(GetTypes((e.NewValue as IList)[0].ToString(), (e.NewValue as IList)[1].ToString()));
					SetCorrectIndex((e.NewValue as IList)[0].ToString(), (e.NewValue as IList)[1].ToString() );
  				}
            
				var currentCalcVal = (picker.SelectedItem as IList)[0].ToString();
				var currentUnitVal = (picker.SelectedItem as IList)[1].ToString();
				Debug.WriteLine($"{currentCalcVal} - - - {currentUnitVal}");
				CalcVariableUnitLabel.Text = currentUnitVal;

				SetEntryCell(currentCalcVal, currentUnitVal);            
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
              
		public ObservableCollection<object> GetTypes(string CalcVarType, string CalcVarUnit)
        {
            try
            {
                CurrentMeasurementItem = CalcVarType;
				CurrentMeasurementUnit = CalcVarUnit;
                ObservableCollection<object> selectedUnits = new ObservableCollection<object>();
                if (CalcVarType == "Weight")
                {
                    selectedUnits.Add("g");
                    selectedUnits.Add("Kg");
                                  
                }
                else if (CalcVarType == "Length")
                {
                    selectedUnits.Add("M");
                    selectedUnits.Add("mm");
               
                }
                return selectedUnits;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public void SetCorrectIndex(string CalcVarType, string CalcVarUnit)
		{         
			if (CalcVarType == "Weight")
            {
                picker.SelectedItem = new ObservableCollection<object>() { "Weight", "g" };
            }
            else if (CalcVarType == "Length")
            {
                picker.SelectedItem = new ObservableCollection<object>() { "Length", "M" };          
            }         
		}

        public void SetEntryCell(string CalcVarType, string CalcVarUnit)
		{

            CurrentCalcVar = CalcVarType;
			CurrentUnitItemLabel.Text = CurrentCalcVar;    

            switch (CalcVarUnit)
            {
                case "mm":
					calcVarAmountEntry.Placeholder = "700";
                    break;
                case "M":
					calcVarAmountEntry.Placeholder = "7.24";
                    break;
                case "Kg":
					calcVarAmountEntry.Placeholder = "1.2";
                    break;
                case "g":
					calcVarAmountEntry.Placeholder = "170";
                    break;
                default:
					calcVarAmountEntry.Placeholder = "";
                    break;
            }  
		}
       
        public void btn175_Clicked(object sender, EventArgs e)
		{
			btn300.BorderColor = StyleKit.Lynch;
			btn300.TextColor = StyleKit.Lynch;

			btn175.BorderColor = StyleKit.CaribbeanGreen;
			btn175.TextColor = StyleKit.CaribbeanGreen;
			CurrentSelectedFilamentThick = "175";

			UpdateSelectedValues();
		}

		public void btn300_Clicked(object sender, EventArgs e)
        {
			btn175.BorderColor = StyleKit.Lynch;
			btn175.TextColor = StyleKit.Lynch;

			btn300.BorderColor = StyleKit.CaribbeanGreen;
            btn300.TextColor = StyleKit.CaribbeanGreen;
			CurrentSelectedFilamentThick = "300";

			UpdateSelectedValues();
        }

        public void UpdateSelectedValues()
		{
			MessagingService.Current.SendMessage("CurrentSelectedFilamentThick", CurrentSelectedFilamentThick);
		}
	}
}
