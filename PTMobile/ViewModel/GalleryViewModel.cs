using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PTMobile.Models;
using PTMobile;
using System.Net;

namespace PTMobile.ViewModel;

public partial class GalleryViewModel : ObservableObject
{
	// the captured files will be stored inside this folder
	private static string GalleryFolder
	{
		get
		{
			
            string folder = "";
			#if WINDOWS
				folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			#else
				folder = FileSystem.AppDataDirectory;
#endif
            return folder;
        }
    } 

	public readonly IMediaPicker _mediaPicker;
	List<ProductImage> DataImages = new();

    public GalleryViewModel(IMediaPicker mediaPicker)
	{

        _mediaPicker = mediaPicker;

		// load the existing files on startup
		Files.Clear();
		if (!string.IsNullOrEmpty(MainPage.Product.Code))
		{
			DataImages = App.CashierData.GetProductImages(MainPage.Product.Code);
			if (DataImages.Count() > 0)
			{
				foreach (var item in DataImages)
				{
                 Files.Add(new CaptureDetails() { FileName = item.Name, Path = item.Path });

				}
			}
		}
        //Directory.GetFiles(GalleryFolder).ToList().ForEach(f => Files.Add(new CaptureDetails() { FileName = Path.GetFileName(f), Path = f }));
	}

	[ObservableProperty]
	private bool _isBusy;

	// contains references to the captured photos and videos
	public ObservableCollection<CaptureDetails> Files { get; } = new();

	[RelayCommand]
	private Task CapturePhoto()
	{
		return Capture(false);
	}

	[RelayCommand]
	private Task CaptureVideo()
	{
		return Capture(true);
	}

    [RelayCommand]
    private Task SearchPhoto()
    {
        return Search(false);
    }

    private async Task Capture(bool isVideo)
	{
		// return if the app is already performing another operation
		if (IsBusy)
		{
			return;
		}

		// show an error message if the camera is not available on the device
		if (!_mediaPicker.IsCaptureSupported)
		{
			await Shell.Current.DisplayAlert("Error", "Camera not available", "OK");
			return;
		}

		try
		{
			// mark as busy while using the camera
			IsBusy = true;

			// open the camera and capture a photo or video
			FileResult file = isVideo ? await _mediaPicker.CaptureVideoAsync() : await _mediaPicker.CapturePhotoAsync();

			// file is null if the user cancels the operation
			if (file != null)
			{
				string localFilePath = Path.Combine(GalleryFolder, file.FileName);

				// save the file into the gallery folder
				

				// save into database
				App.CashierData.AddImage(new ProductImage() { Name = file.FileName, Path = localFilePath,ProductCode=MainPage.Product.Code });



#if WINDOWS
				// on Windows file.OpenReadAsync() throws an exception
				//using Stream sourceStream = File.OpenRead(file.FullPath);
#else
                using FileStream localFileStream = File.OpenWrite(localFilePath);
                using Stream sourceStream = await file.OpenReadAsync();
                await sourceStream.CopyToAsync(localFileStream);
#endif



                // add the file path to the list to display the picture on the page
                Files.Add(new CaptureDetails()
				{
					FileName = file.FileName,
					Path = localFilePath,
				});
			}
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
		}
		finally
		{
			IsBusy = false;
		}
	}

    private async Task Search(bool isVideo)
    {
        // return if the app is already performing another operation
        if (IsBusy)
        {
            return;
        }

        try
        {
            // mark as busy while using the camera
            IsBusy = true;

            // Searching picture on gallery
            FileResult file = isVideo ? await _mediaPicker.CaptureVideoAsync() : await _mediaPicker.PickPhotoAsync();

            // file is null if the user cancels the operation
            if (file != null)
            {
                string localFilePath = Path.Combine(GalleryFolder, file.FileName);

                // save the file into the gallery folder
                using FileStream localFileStream = File.OpenWrite(localFilePath);


                // save into database
                App.CashierData.AddImage(new ProductImage() { Name = file.FileName, Path = localFilePath,ProductCode=MainPage.Product.Code });

			
				//ftpService.UploadLocalFile(file.FileName);


                


                ///Prueba FTP
                //#region FTP
                //if (File.Exists(file.FullPath))
                //{
                //    string fileName = file.FullPath;
                //    Stream fs = File.OpenRead(fileName);
                //    BinaryReader br = new BinaryReader(fs);
                //    byte[] bytes = br.ReadBytes((Int32)fs.Length);

                //    string filePath = fileName;
                //    fs.Dispose();
                //    File.WriteAllBytes(filePath, bytes);

                //    FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://192.168.0.108/" + file.FileName);
                //    request.Method = WebRequestMethods.Ftp.UploadFile;

                //    request.Credentials = new NetworkCredential("usuarioftp", "123ftp456*");

                //    //byte[] fileContents;
                //    //using (StreamReader sourceStream = new StreamReader(filePath))
                //    //{
                //    //    fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                //    //}

                //    request.ContentLength = bytes.Length;

                //    using (Stream requestStream = request.GetRequestStream())
                //    {
                //        requestStream.Write(bytes, 0, bytes.Length);
                //    }


                //}

                //#endregion


#if WINDOWS
				// on Windows file.OpenReadAsync() throws an exception
				using Stream sourceStream = File.OpenRead(file.FullPath);
#else
                using Stream sourceStream = await file.OpenReadAsync();
#endif

                await sourceStream.CopyToAsync(localFileStream);

                // add the file path to the list to display the picture on the main page
                Files.Add(new CaptureDetails()
                {
                    FileName = file.FileName,
                    Path = localFilePath,
                });
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
