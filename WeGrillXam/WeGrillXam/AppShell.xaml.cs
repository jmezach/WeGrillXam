using System;
using System.Collections.Generic;
using WeGrillXam.ViewModels;
using WeGrillXam.Views;
using Xamarin.Forms;

namespace WeGrillXam
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
        }

    }
}

