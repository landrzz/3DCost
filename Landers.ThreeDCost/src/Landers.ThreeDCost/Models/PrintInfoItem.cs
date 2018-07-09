using System;
using Syncfusion.XForms.DataForm;
using Syncfusion.XForms.DataForm.Editors;

namespace Landers.ThreeDCost.Models
{
    public class PrintInfoItem
    {
		private double _filamentWeight;
		private double _costOfSpool;
        //private string lastName;

		[Display(GroupName = "Details", ShortName = "Spool Weight")]
        public double FilamentWeight
        {
			get { return this._filamentWeight; }
            set
            {
				this._filamentWeight = value;
            }
        }

		[Display(GroupName = "Details")]
        public double CostOfSpool
        {
			get { return this._costOfSpool; }
            set
            {
				this._costOfSpool = value;
            }
        }
        //public string LastName
        //{
        //    get { return this.lastName; }
        //    set
        //    {
        //        this.lastName = value;
        //    }
        //}

        

      




    }
}
