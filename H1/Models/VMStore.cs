using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using H1.Models;

namespace H1.Models
{
    public class VMStore
    {
        [DisplayName("商店菜單")]
        public IEnumerable<StorePicture> StorePicture { get; set; }
        public DateTime StorePictureNumberTime { get; set; }
        public string StorePictureNumberSID { get; set; }
        public string StorePictureNumberType { get; set; }
        public decimal StorePictureNumberPicture { get; set; }

        [DisplayName("圖片類型")]
        public IEnumerable<StorePicture> PictureType { get; set; }
        public int PictureTypeNumber { get; set; }
        public string PictureTypeType { get; set; }
        [DisplayName("商店資料")]
        public IEnumerable<Store> Store { get; set; }
        public string StoreIdentify { get; set; }
        public string StoreAddress { get; set; }
        public decimal StoreLocationX { get; set; }
        public decimal StoreLocationY { get; set; }
        public string StoreLocation { get; set; }
        public string StoreTelephone { get; set; }
        public bool StorePay { get; set; }
        public string StoreText { get; set; }
        public string StoreIdentifyChain { get; set; }
        public string StoreIdentifyChineseName { get; set; }
    }
 
}