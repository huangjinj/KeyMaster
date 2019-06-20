using KeyMaster.Models;
using KeyMaster.Services;
using KeyMaster.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Xamarin.Forms;

namespace KeyMaster
{
    public partial class MainPage : ContentPage
    {
        ItemsViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Load items only when app start
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemPage
            {
                BindingContext = new Item()
            });
        }

        void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            MessagingCenter.Send(this, "DeleteItem", mi.CommandParameter as Item);
        }

        public void OnCopy(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Item item = mi.CommandParameter as Item;
            string str = item.Account + "," + item.Password;
            DataPackage dp = new DataPackage();
            dp.SetText(str);
            Clipboard.SetContent(dp);
        }

        async void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            await Navigation.PushAsync(new ItemPage
            {
                BindingContext = mi.CommandParameter as Item
            });
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var item = e.SelectedItem as Item;
            //if (item == null)
            //    return;
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemPage
            {
                BindingContext = item
            });

            //    string title = item.Name;
            //    if (string.IsNullOrEmpty(title))
            //    {
            //        title = "Action";
            //    }
            //    var action = await DisplayActionSheet(title, "Cancel", null, "Edit", "Delete", "Copy");
            //    switch (action)
            //    {
            //        case "Cancel":
            //            break;
            //        case "Edit":
            //            break;
            //        case "Delete":
            //            break;
            //        case "Copy":
            //            break;
            //        default:
            //            break;
            //    }
        }
    }
}
