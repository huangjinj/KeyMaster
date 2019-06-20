using KeyMaster.Models;
using KeyMaster.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KeyMaster.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private bool IsBusy;
        IDataStore<Item> DataStore;


        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Items = new ObservableCollection<Item>();
            DataStore = LocalDBDataStore.Instance;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                newItem.CreateDate = DateTime.Now.ToShortDateString();
                if(await DataStore.AddItemAsync(newItem))
                    Items.Add(newItem);
                else
                    await obj.DisplayAlert("Error", "An error occured when adding the this item.", "OK");
            });

            MessagingCenter.Subscribe<ItemPage, Item>(this, "EditItem", async (obj, item) =>
            {
                var newItem = item as Item;
                newItem.CreateDate = DateTime.Now.ToShortDateString();
                if(await DataStore.UpdateItemAsync(newItem))
                    LoadItemsCommand.Execute(null);
                else
                    await obj.DisplayAlert("Error", "An error occured when editing this item.", "OK");
            });

            MessagingCenter.Subscribe<MainPage, Item>(this, "DeleteItem", async (obj, item) =>
            {
                var itemToDelete = item as Item;
                if(await DataStore.DeleteItemAsync(itemToDelete))
                    Items.Remove(itemToDelete);
                else
                    await obj.DisplayAlert("Error", "An error occured when deleting this item.", "OK");
            });
        }

        /// <summary>
        /// Reload all items from local DB
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
