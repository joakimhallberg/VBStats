using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using VolleyballStats.Model;

namespace VolleyballStats
{
    public class Point : ObservableObject
    {
        private bool? _serving;
        private Player _server;
        private Player _serveReturner;
        //private int _server;
        private ServeGrade _grade;
        private bool _returned;
        private bool? _won;
        private Reason _reason;
        //private LooseReason _loose;
        private PlayerFault _fault;
        private Player _credit;

        public bool? Serving
        {
            get { return this._serving; }
            set { Set(ref _serving, value); }
        }

        public Player Server
        {
            get { return this._server; }
            set {
                if (value != null)
                {
                    this.Serving = (value.Number > -1);
                }
                Set(ref _server, value); 
            }
        }

        public Player ServeReturner
        {
            get { return this._serveReturner; }
            set
            {
                //if (value != null)
                //{
                //    this.Serving = (value.Number > -1);
                //}
                Set(ref _serveReturner, value);
            }
        }

        //public int Server
        //{
        //    get { return this._server; }
        //    set { SetProperty(ref _server, value); }            
        //}

        public ServeGrade ServeGrade
        {
            get { return this._grade; }
            set { Set(ref _grade, value); }
        }

        public int? Grade
        {
            get {
                if (ServeGrade != null) 
                    return ServeGrade.Grade;
                else
                    return null;
            }
            //set { SetProperty(ref _grade, value); }
        }

        public bool Returned
        {
            get { return this._returned; }
            set { Set(ref _returned, value); }
        }

        public bool? Won
        {
            get { return this._won; }
            set { Set(ref _won, value); }
        }

        public Reason Reason
        {
            get { return this._reason; }
            set 
            {
                Set(ref _reason, value);
                if (value != null)
                {
                    if (value.ServeReturned.HasValue && !value.ServeReturned.Value && value.Win.HasValue && Serving.HasValue)
                    {
                        Won = this.Serving.HasValue && this.Serving.Value == value.Win.Value;
                        if (value.ReceiveError.HasValue && value.ReceiveError.Value)
                        {
                            Won = !Won;
                        }
                    }
                    if (value.ServeReturned.HasValue)
                    {
                        Returned = value.ServeReturned.Value;
                    }
                    if (value.Grade != null)
                    { 
                        //this.
                        this.ServeGrade= value.Grade;
                    }
                    //Won = true;
                    //Loose = null;
                }
            }
        }

        //public LooseReason Loose
        //{
        //    get { return this._loose; }
        //    set
        //    {
        //        SetProperty(ref _loose, value);
        //        if (value != null)
        //        {
        //            Win = null;
        //            Won = false;
        //        }
        //    }
        //}

        public PlayerFault Fault
        {
            get { return this._fault; }
            set { Set(ref _fault, value); }
        }

        public Player Credit
        {
            get { return this._credit; }
            set { Set(ref _credit, value); }
        }

        public string ExportCSV(int set)
        {
            string csv = string.Empty;
            if (this.Serving.HasValue && Serving.Value)
            {
                csv = "m,";
                if (Server != null)
                {
                    csv += Server.Number.ToString();
                }
            }
            else
            {
                csv = "t,";
            }
            csv += ",";
            if (ServeGrade != null)
            {
                csv += ServeGrade.Grade.ToString();
            }
            csv += ",";
            if (Returned)
            {
                csv += "1,";
            }
            else
            {
                csv += ",";
            }
            if (Won.HasValue && Won.Value)
            {
                csv += "m,";                
            }
            else if (Won.HasValue && Won.Value)
            {
                csv += "t,";
            }
            else
            {
                csv += ",";
            }
            csv += set.ToString() +",";
            if (Reason != null)
            {
                csv += Reason.Name + "," + Reason.Win.ToString() + ",";                
            }
            else
            {
                csv += ",,";                
            }
            //if (Loose != null)
            //{
            //    csv += Loose.Name +"," + Loose.Us.ToString() + ",";                
            //}
            //else
            //{
            //    csv += ",,";
            //}
            if (Fault != null)
            {
                csv += Fault.Name + ",";
            }
            else
            {
                csv += ",";
            }
            if (this._credit != null)
            {
                csv += _credit.Number.ToString() ;
            }
            //else
            //{
            //    csv += ",";
            //}
            return csv;
        }
    }   
}
