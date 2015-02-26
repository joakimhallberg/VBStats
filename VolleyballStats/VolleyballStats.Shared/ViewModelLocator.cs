using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;


namespace VolleyballStats
{
    public class ViewModelLocator
    {
        static  ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<VolleyballGame>();
        }

        public VolleyballGame VolleyballGame
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VolleyballGame>();
            }
        }
    }

}
