using KeyMaster.Models;
using KeyMaster.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeyMaster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemPage : ContentPage
    {
        bool _isEdit;

        public ItemPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Item item = (Item)BindingContext;
            if(item.ID == 0)
            {
                _isEdit = false;
                btnAction.Text = "Add";
            }
            else
            {
                _isEdit = true;
                btnAction.Text = "Save";
            }
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var item = (Item)BindingContext;
            MessagingCenter.Send(this, _isEdit? "EditItem" : "AddItem", item);
            await Navigation.PopAsync();
        }
    }
}