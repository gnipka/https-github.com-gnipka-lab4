using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class CheckedItem<T> where T: class
    {
        public T Data { get; set; }
        public bool IsChecked { get; set; }
    }
}
