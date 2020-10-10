using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NikshitPanchal_WebMobi.Models
{
    public class ProductModel
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int brand_id { get; set; }
        public string brand_name { get; set; }
        public int category_id { get; set; }
        public string category_name { get; set; }
        public byte[] product_image { get; set; }
        public short model_year { get; set; }
        public decimal list_price { get; set; }
    }

    public class brand
    {
        public List<int> brand_id { get; set; }
        public List<string> brand_name { get; set; }
    }
    public class category
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
    }


}