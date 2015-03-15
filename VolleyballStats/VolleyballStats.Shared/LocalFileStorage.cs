using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows;

namespace VolleyballStats
{
    public class LocalFileStorage
    {
        public static async Task<string> Open()
        {
            // define picker
            var picker = new FileOpenPicker() { 
                ViewMode =  PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add( ".csv");

            //let user pick file
            var file = await picker.PickSingleFileAsync();
            if (file == null)
            { 
                await new Windows.UI.Popups.MessageDialog("No file").ShowAsync();
                return null;
            }

            return await Windows.Storage.FileIO.ReadTextAsync(file);
        }

        public static async Task Save(string FileName, string content)
        { 
            var picker = new FileSavePicker() { 
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeChoices.Add("csv", new List<string>() { ".csv" });
            picker.SuggestedFileName = FileName + ".csv";

            var file = await picker.PickSaveFileAsync();
            if (file == null)
            {
                return;
            }
            Windows.Storage.FileIO.WriteTextAsync(file, content);
        }
    }

}
