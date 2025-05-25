using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNotes.Models
{
    public partial class Task : ObservableObject
    {
        [ObservableProperty]
        public string name = "";

        [ObservableProperty]
        public bool isChecked = false;
    }
}
