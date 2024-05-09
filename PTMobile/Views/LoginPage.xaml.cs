using CommunityToolkit.Maui.Views;
using Newtonsoft.Json;
using Plugin.Fingerprint.Abstractions;
using PTMobile.Models;
using PTMobile.Resources.Languages;

namespace PTMobile;

public partial class LoginPage : ContentPage
{
    string Usuario = string.Empty;
    string Clave = string.Empty;
    string LoginMessage = string.Empty;

    private readonly IFingerprint fingerprint;
    public LoginPage(IFingerprint fingerprint)
	{
		InitializeComponent();
        MainPage.CurrentUser = new();
        this.fingerprint = fingerprint;

    }

    private void User_Completed(object sender, EventArgs e)
    {
        Usuario = ((Entry)sender).Text;
        Password.Focus();
    }
    private void Password_Completed(object sender, EventArgs e)
	{
        Clave = ((Entry)sender).Text;
        Aceptar.Focus();
    }

    public void Aceptar_Clicked(object sender, EventArgs e)
    {
        Usuario = User.Text;
        Clave = Password.Text;
        //App.CashierData.AddNewUser(new User() { Id = 1, UserName = "hola", Password = "123" });
        //App.CashierData.AddNewUser(new User() { Id = 2, UserName = "chao", Password = "321" });
        //App.CashierData.AddNewUser(new User() { Id = 3, UserName = "prueba", Password = "456" });
        //App.CashierData.AddNewAuthorization(new Authorization() { Date = new DateTime(2023, 3, 1), Description = "Autorizacion 4", Code = "04", IdAuth = Guid.NewGuid().ToString(),Status = 0,UserId = "01", UserName = "User1",
        //    DetailDescription= "Solicitud de permiso para anular una factura: Lorem ipsum dolor sit amet, " +
        //    "consectetur adipiscing elit. Donec ultrices mauris sagittis venenatis condimentum. Etiam vel interdum ipsum, " +
        //    "vel facilisis arcu. Donec leo eros, finibus in augue eu, laoreet porta dui. Suspendisse malesuada tempus tellus, " +
        //    "sit amet venenatis ante bibendum sed. Nulla mollis ex non velit maximus, " +
        //    "at gravida ante viverra. Interdum et malesuada fames ac ante ipsum primis in faucibus. " +
        //    "Nunc vitae imperdiet quam. Nulla sed dui ligula. Nunc sit amet euismod lacus." +
        //    " Fusce a vehicula mi. Vestibulum at laoreet massa. Ut cursus enim eu ultrices molestie. Duis congue pretium sem eu hendrerit. " +
        //    "Phasellus aliquam faucibus dui, vitae imperdiet arcu scelerisque et. In sit amet ante ornare, tincidunt tellus non, rutrum turpis." +
        //    " Pellentesque nibh risus, imperdiet in egestas sit amet, venenatis in eros."
        //});
        //App.CashierData.AddNewAuthorization(new Authorization() { Date = new DateTime(2023, 4, 2), Description = "Autorizacion 2", Code = "02", IdAuth = Guid.NewGuid().ToString(),Status = 1,UserId = "02", UserName = "User2",CheckDate= new DateTime(2023, 5, 9) });
        //App.CashierData.AddNewAuthorization(new Authorization() { Date = new DateTime(2023, 4, 3), Description = "Autorizacion 5", Code = "05", IdAuth = Guid.NewGuid().ToString(),Status = 2,UserId = "05", UserName = "User5",CheckDate= new DateTime(2023, 5, 11),CapturePath= "/data/user/0/com.posandtouch.ptmobile/files/IMG-20230514-WA0049.jpg" });

        if (CheckUser(new Models.User() { UserName = Usuario, Password = Clave }))
        {
            
            if (Preferences.ContainsKey("UserInfo"))
            {
                Preferences.Remove("UserInfo");
            }
            if (RememberCheckBox.IsChecked)
            {
                string userDetails = JsonConvert.SerializeObject(new Models.User() { UserName = Usuario, Password = Clave });
                Preferences.Set("UserInfo", userDetails);
            }
            


            MainPage.CurrentUser = App.CashierData.GetAllUser().Where(u=>u.UserName == Usuario).FirstOrDefault();
            Application.Current.MainPage = new AppShell();
            User.Text = "";
            Password.Text = "";

        }
        else
        {
            if (!Result.IsVisible)
            {
                Result.IsVisible = true;
            }
            Result.Text = LoginMessage;

        }
        Creado.Text = "";
    }


    public bool CheckUser(User user)
    {

            foreach (var item in App.CashierData.GetAllUser())
            {
                if (item.UserName == user.UserName)
                {

                    if (item.Password == user.Password)
                    {

                        return true;
                    }
                    
                }
            }

        LoginMessage = AppResources.IncorrectUserPassword;
        return false;
        }

    private void User_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (Result.IsVisible)
        {
            Result.IsVisible = false;
        }

    }

    private void Password_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (Result.IsVisible)
        {
            Result.IsVisible = false;
        }
    }

    private void Crear_Clicked(object sender, EventArgs e)
    {

        if (User.Text != null && User.Text.Length > 0 )
        {
            if (Password.Text != null && Password.Text.Length > 0)
            {
                App.CashierData.AddNewUser(new User() { Id = App.CashierData.GetAllUser().Count() + 1, UserName = User.Text, Password = Password.Text });
                Creado.Text = App.CashierData.StatusMessage;
            }
            else
            {
                Creado.Text = "Ingrese contraseña";
            }
           
        }
        else
        {
            Creado.Text = "Ingrese nombre de usuario";
        }

      
    }

    private async void HuellaBtn_Clicked(object sender, EventArgs e)
    {
        var request = new AuthenticationRequestConfiguration("Validacion de huella", "Acceso al sistema");
        var result = await fingerprint.AuthenticateAsync(request);

        if (result.Authenticated)
        {
            if (Preferences.ContainsKey("UserInfo"))
            {
                Preferences.Remove("UserInfo");
            }
            if (RememberCheckBox.IsChecked)
                Preferences.Set("UserInfo", "Finger");

            Application.Current.MainPage = new AppShell();
            User.Text = "";
            Password.Text = "";
        }
        else
        {
            await DisplayAlert("No Auntenticado", "Acceso denegado", "OK");
        }
    }
}