using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Interface
{
    public static class URIManager
    {
        public static readonly Uri BaseUri = new Uri("pack://application:,,,/");
        public static readonly  Uri StringResources = new Uri("pack://application:,,,/Resources/StringResources.xaml");

        public static readonly string MontserratBlack = "/Resources/Fonts/#Montserrat Black";
    }
}
