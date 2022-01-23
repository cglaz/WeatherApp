using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WeatherApp.Helpers;
using WeatherApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        protected string Email { get; set; }
        protected string Password { get; set; }

        public LoginView()
        {
            InitializeComponent();
            //GetUserDetails();

        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if(txtUserEmail.Text == "cezar@gmail.com" && txtUserPassword.Text == "123")
            {
                Navigation.PushAsync(new CurrentHomePage());
            } else
            {
                DisplayAlert("Ups..", "Nieprawidłowy email lub hasło", "Ok");
            }


            
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new RegisterView());
        }



        private async void GetUserDetails()
        {
            var url = $"https://weatherapp-cdv.azurewebsites.net/users";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var resultResponse = result.Response;
                    var userDetails = JsonConvert.DeserializeObject<User>(resultResponse.Substring(1, resultResponse.Length - 2));
                    Console.WriteLine(userDetails);

                    Email = userDetails.email;
                    Password = userDetails.password;

                }
                catch (Exception ex)
                {
                    await DisplayAlert("User details", ex.Message, "OK");
                }
            }
        }
    }
}
