
#if ANDROID
using Android.Graphics;
using Android.Media;
using AndroidX.Lifecycle;
#endif
using PTMobile.Functions;
using PTMobile.Models;
using PTMobile.ViewModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Stream = System.IO.Stream;

namespace PTMobile;

public partial class ProductDetail : ContentPage
{
    Stream PhotoStream;
    string PhotoPath;
    string FileName;
    bool Camera = false;
    GalleryViewModel ViewModel;

    
    public ProductDetail(GalleryViewModel viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel;
        InitBinding(true);
    }

    void InitBinding(bool dontInitGallery)
    {
        if (!dontInitGallery)
            ViewModel = new(ViewModel._mediaPicker);

        if(MainPage.Product != null)
        {
            ProductData.BindingContext = MainPage.Product;
        }        
        
        NewPictureBtn.BindingContext = ViewModel;
        SearchPictureBtn.BindingContext = ViewModel;
        CarouselView.BindingContext = ViewModel;
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (MainPage.Product.Name.Length > 0)
        {
            this.Title = MainPage.Product.Name;
        }
        else
        {
            this.Title = "Producto Nuevo";
        }

        //base.OnNavigatedTo(args);

        InitBinding(false);
        

    }
    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        ViewModel.Files.Clear();
        base.OnNavigatedFrom(args); 
    }




    async void Save_Clicked(object sender, EventArgs e)
	{
        if(!string.IsNullOrEmpty(Code.Text) && !string.IsNullOrEmpty(Name.Text))
        {
            if (App.CashierData.GetProduct(Code.Text) == null)
            {
                var response = await Shell.Current.DisplayActionSheet("¿Desea guardar los cambios?", "", null, "Guardar", "Cancelar");

                if (response == "Guardar")
                {
                    App.CashierData.AddNewProduct(new Models.Product() { Id = Convert.ToInt32(Id.Text), Code = Code.Text, Name = Name.Text, Price = Convert.ToDouble(Price.Text) });
                    //SavePhotoAsync();
                    await Shell.Current.DisplayAlert("", App.CashierData.StatusMessage, "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
            else
            {
                var response = await Shell.Current.DisplayActionSheet("¿Desea guardar los cambios?", "", null, "Guardar", "Cancelar");

                if (response == "Guardar")
                {
                    App.CashierData.UpdateProduct(new Models.Product() { Id = Convert.ToInt32(Id.Text), Code = Code.Text, Name = Name.Text, Price = Convert.ToDouble(Price.Text) });
                    //SavePhotoAsync();
                    await Shell.Current.DisplayAlert("", App.CashierData.StatusMessage, "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
        }
		
		

		

        
    }

    async void DeleteBtn_Clicked(object sender, EventArgs e)
    {
        var response = await Shell.Current.DisplayActionSheet("¿Desea eliminar el registro?", "", null, "Confirmar", "Cancelar");
        if (response == "Confirmar")
        {
            App.CashierData.DeleteProduct(new Models.Product() { Id=Convert.ToInt32(Id.Text),Code=Code.Text,Name=Name.Text,Price=Convert.ToDouble(Price.Text)});
                App.CashierData.DeleteAllProductImage(Code.Text);
            await Shell.Current.DisplayAlert("", App.CashierData.StatusMessage, "OK");
            await Shell.Current.GoToAsync("..");
        }
        
    }

    async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var parameter = e.Parameter;
        var response = await Shell.Current.DisplayActionSheet("¿Desea eliminar imagen?", "", null, "Eliminar", "Cancelar");
        if (response == "Eliminar")
        {
            App.CashierData.DeleteProductImage(((Models.CaptureDetails)parameter).Path);
            
        }
    }



    //async void SearchPictureBtn_Clicked(object sender, EventArgs e)
    //{
    //    await SearchPhotoAsync();
    //}

    //async void NewPictureBtn_Clicked(object sender, EventArgs e)
    //{
    //    await TakePhotoAsync();
    //}



    //async Task TakePhotoAsync()
    //{
    //    try
    //    {


    //        //if (photo != null)
    //        //{
    //        //    Camera = true;
    //        //    FileName = photo.FileName;
    //        //    await LoadPhotoAsync(photo);
    //        //}

    //    }
    //    catch (FeatureNotSupportedException fnsEx)
    //    {
    //        // Feature is not supported on the device
    //    }
    //    catch (PermissionException pEx)
    //    {
    //        // Permissions not granted
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    //async Task SearchPhotoAsync()
    //{
    //    try
    //    {

    //        var photo = await MediaPicker.PickPhotoAsync();
    //        if( photo != null)
    //        {
    //            Camera = false;
    //            FileName = photo.FileName;
    //            await LoadPhotoAsync(photo);
    //        }

    //    }
    //    catch (FeatureNotSupportedException fnsEx)
    //    {
    //        // Feature is not supported on the device
    //    }
    //    catch (PermissionException pEx)
    //    {

    //        // Permissions not granted
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
    //    }
    //}

    //async Task LoadPhotoAsync(FileResult photo)
    //{
    //    // canceled  
    //    if (photo == null)
    //    {
    //        PhotoPath = null;
    //        return;
    //    }
    //    Stream stream = await photo.OpenReadAsync();

    //    PhotoStream = stream;

    //    //await File.WriteAllBytesAsync(filePath, AddIn.GetImageStreamAsBytes(stream));

    //    //PhotoPath = filePath;



    //}

    //    async void SavePhotoAsync()
    //    {
    //        using var memoryStream = new MemoryStream();
    //        PhotoStream.CopyTo(memoryStream);

    //        PhotoStream.Position = 0;
    //        memoryStream.Position = 0;

    //#if WINDOWS
    //		await System.IO.File.WriteAllBytesAsync(
    //			@"C:\Users\joverslu\Desktop\DrawingView\Test.png", memoryStream.ToArray());
    //#elif ANDROID
    //        var context = Platform.CurrentActivity;
    //        string format = "";
    //        Android.Net.Uri uri = Android.Net.Uri.Parse("");
    //        if (OperatingSystem.IsAndroidVersionAtLeast(29))
    //        {
    //            Android.Content.ContentResolver resolver = context.ContentResolver;
    //            Android.Content.ContentValues contentValues = new();
    //            contentValues.Put(Android.Provider.MediaStore.IMediaColumns.DisplayName, FileName);
    //            if (FileName.Contains(".png"))
    //            {
    //                format = "png";
    //                contentValues.Put(Android.Provider.MediaStore.IMediaColumns.MimeType, "image/png");
    //                contentValues.Put(Android.Provider.MediaStore.IMediaColumns.RelativePath, "DCIM/" + "PTMobile");
    //                Android.Net.Uri imageUri = resolver.Insert(Android.Provider.MediaStore.Images.Media.ExternalContentUri, contentValues);
    //                PhotoPath = imageUri.Path;
    //                uri = imageUri;
    //                var os = resolver.OpenOutputStream(imageUri);
    //                Android.Graphics.BitmapFactory.Options options = new();
    //                options.InJustDecodeBounds = true;
    //                var bitmap = Android.Graphics.BitmapFactory.DecodeStream(PhotoStream);

    //                if (Camera)
    //                {
    //                    //// create new matrix
    //                    Matrix matrix = new Matrix();
    //                    // setup rotation degree
    //                    matrix.PostRotate(90);
    //                    Bitmap bmp = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
    //                    bmp.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, os);
    //                }
    //                else
    //                {
    //                    bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, os);
    //                }

    //                os.Flush();
    //                os.Close();
    //            }
    //            if (FileName.Contains(".jpg")){
    //                format = "jpg";
    //                contentValues.Put(Android.Provider.MediaStore.IMediaColumns.MimeType, "image/jpg");
    //                contentValues.Put(Android.Provider.MediaStore.IMediaColumns.RelativePath, "DCIM/" + "PTMobile");
    //                Android.Net.Uri imageUri = resolver.Insert(Android.Provider.MediaStore.Images.Media.ExternalContentUri, contentValues);
    //                PhotoPath = imageUri.Path;
    //                uri = imageUri;
    //                var os = resolver.OpenOutputStream(imageUri);
    //                Android.Graphics.BitmapFactory.Options options = new();

    //                options.InJustDecodeBounds = true;
    //                var bitmap = Android.Graphics.BitmapFactory.DecodeStream(PhotoStream);

    //                if (Camera)
    //                {
    //                    //// create new matrix
    //                    Matrix matrix = new Matrix();
    //                    // setup rotation degree
    //                    matrix.PostRotate(90);
    //                    Bitmap bmp = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
    //                    bmp.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, os);
    //                }
    //                else
    //                {
    //                    bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, os);
    //                }





    //                os.Flush();
    //                os.Close();

    //            }
    //            //uri = Android.Net.Uri.Parse("file://" + PhotoPath + "." + format);
    //            string uriString = uri.Path;
    //            Android.Content.ContentValues contentValuesnew = new();
    //            contentValues.Put(Android.Provider.MediaStore.IMediaColumns.DisplayName, FileName);
    //            contentValuesnew.Put(Android.Provider.MediaStore.IMediaColumns.RelativePath, "DCIM/" + "PTMobile");
    //            Android.Net.Uri urinew = resolver.Insert(Android.Provider.MediaStore.Images.Media.ExternalContentUri, contentValues);
    //            urinew = Android.Net.Uri.Parse("content://media" + uriString);
    //            try
    //            {
    //                Bitmap readBitmap = ImageDecoder.DecodeBitmap(ImageDecoder.CreateSource(context.ContentResolver, urinew));
    //            }
    //            catch (Exception ex)
    //            {
    //            }

    //            try
    //            {
    //                var stream = resolver.OpenInputStream(uri);
    //                var bitmapStream = BitmapFactory.DecodeStream(stream);
    //            }
    //            catch (Exception ex)
    //            {
    //            }

    //        }
    //        else
    //        {
    //            Java.IO.File storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
    //            string path = System.IO.Path.Combine(storagePath.ToString(), "test.png");
    //            System.IO.File.WriteAllBytes(path, memoryStream.ToArray());
    //            var mediaScanIntent = new Android.Content.Intent(Android.Content.Intent.ActionMediaScannerScanFile);
    //            mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(path)));
    //            context.SendBroadcast(mediaScanIntent);
    //        }
    //#elif IOS || MACCATALYST
    //        var image = new UIKit.UIImage(Foundation.NSData.FromArray(memoryStream.ToArray()));
    //        image.SaveToPhotosAlbum((image, error) =>
    //        {
    //        });
    //#endif
    //        ProductImage imagenFinal = new ProductImage();
    //        imagenFinal.Name = FileName;
    //        imagenFinal.ProductCode = Code.Text;
    //        imagenFinal.Path = PhotoPath;
    //        App.CashierData.AddImage(imagenFinal);
    //    }
}