using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using VolleyballStats.ViewModel;

namespace VolleyballStats
{
    public class ViewModelLocator
    {
        static  ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            //SimpleIoc.Default.Register<INavigationService, NavigationService>();
            //SimpleIoc.Default.Register<INavigationService>(() =>
            //{
            //    return new NavigationService(App.Current);
            //});

            SimpleIoc.Default.Register<VolleyballGameViewModel>();
            SimpleIoc.Default.Register<GameViewModel>();
            SimpleIoc.Default.Register<LandingViewModel>();
            //SimpleIoc.Default.Register();
        }

        public VolleyballGameViewModel VolleyballGame
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VolleyballGameViewModel>();
            }
        }

        public GameViewModel GameViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GameViewModel>();
            }
        }

        public LandingViewModel LandingViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LandingViewModel>();
            }
        }
    }

}
