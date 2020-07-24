﻿using DeliveryAppWhiterocks.Models;
using DeliveryAppWhiterocks.Models.XeroAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace DeliveryAppWhiterocks.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeliveryInfo : ContentPage
    {
        ObservableCollection<Stock> _stockInfo = new ObservableCollection<Stock>();

        public DeliveryInfo()
        {
            InitializeComponent();
            Init();
            MapItemList();
        }

        private void Init()
        {
            App.CheckInternetIfConnected(noInternetLbl, this);

            if(Device.Idiom == TargetIdiom.Tablet)
            {
                QuantityHeaderLabel.FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label));
            } else
            {
                QuantityHeaderLabel.FontSize = Device.GetNamedSize(NamedSize.Subtitle, typeof(Label));
            }
        }

        private void MapItemList()
        {
            double totalWeight = 0;
            foreach(KeyValuePair<string,Stock> stock in XeroAPI._ItemDictionary)
            {
                Stock stockX = stock.Value;
                _stockInfo.Add(stockX);
                totalWeight += stockX.Weight;
            }
            
            WeightTotalLabel.Text = string.Format($"{totalWeight:F2} Kg");
            DeliveryItemListView.ItemsSource = _stockInfo;
        }

        private async void  TapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}