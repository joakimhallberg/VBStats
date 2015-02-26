using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
//using Microsoft.Bcl.Async;
using System.Runtime.CompilerServices;

namespace VolleyballStats
{
    public abstract class ObservableObjectOld : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged([CallerMemberName] string name = null)
        //{
        //    var pc = PropertyChanged;
        //    if (pc != null)
        //        pc(this, new PropertyChangedEventArgs(name));
        //}

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null) 
        {
            if(!EqualityComparer<T>.Default.Equals(field, value)) 
            {
                field = value;
                var pc = PropertyChanged;
                if(pc != null)
                    pc(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }

        protected bool ForceChangeEvent<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
                //field = value;
                var pc = PropertyChanged;
                if (pc != null)
                    pc(this, new PropertyChangedEventArgs(propertyName));
                return true;
            //if (!EqualityComparer<T>.Default.Equals(field, value))
            //{
            //}
            //return false;
        }


    }

    //public static class ObjectHelper
    //{
    //    public static string GetMemberName(Expression action)
    //    {
    //        MemberExpression memberExpression = null;
    //        Expression expression = action.Body;
    //        // in case of casting to object
    //        UnaryExpression unaryExpression = expression as UnaryExpression;
    //        if (unaryExpression != null)
    //        {
    //            memberExpression = (MemberExpression)unaryExpression.Operand;
    //        }
    //        else
    //        {
    //            memberExpression = (MemberExpression)expression;
    //        }

    //        return memberExpression.Member.Name;
    //    }
    //}

}
