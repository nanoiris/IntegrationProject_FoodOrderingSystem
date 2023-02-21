using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace RmsApp.Pages
{
    public class CategoryBase : ComponentBase
    {
        public List<TabPath> Paths = new List<TabPath>
        {
        new TabPath { Title = "Home", Path = "/" },
        new TabPath { Title = "Weather Forcast", Path = "/fetchdata" },
        };



    }
}