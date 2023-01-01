using System;
using System.Collections.Generic;

using System.Text;
using Xamarin.Forms;

namespace fourbot.ViewModels.Classes
{
    public class Orders
    {
        public string symbol { get; set; }
        public int column { get; set; }
             
        public string side { get; set; }
        public int price { get; set; }
        
    }
}
