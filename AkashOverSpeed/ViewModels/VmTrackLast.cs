using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkashOverSpeed.ViewModels
{
    public class VmTrackLast
    {
        public string strUser { get; set; }
        public string strTEID { get; set; }
        public string strCarNum { get; set; }
        public int? nOverSpeed { get; set; }
        public int? nSpeed { get; set; }
        public int? nCarState { get; set; }
        public int? nDirection { get; set; }
        public int? nGPSSignal { get; set; }
        public int? nGSMSignal { get; set; }
        public int? nMileage { get; set; }
        public int? nTEState { get; set; }
        public int? nTime { get; set; }
        public decimal? dbLat { get; set; }
        public decimal? dbLon { get; set; }
        public string strTime { get; set; }
        public string StatusSpeed { get; set; }
    }
}