using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace VolleyballStats
{
    public class FileStorage
    {
        StorageFolder folder = ApplicationData.Current.LocalFolder;
        CreationCollisionOption option = CreationCollisionOption.ReplaceExisting;

        public async Task<StorageFile> CreateFile(string name)
        {
            return await folder.CreateFileAsync(name, option);
        }

        public async void Write(StorageFile file, string content)
        {
            await FileIO.WriteTextAsync(file, content);
        }

        public async Task<StorageFile> Get(string name)
        {
            return await folder.GetFileAsync(name);
        }

        public async Task<string> GetContent(StorageFile file)
        {
            return await FileIO.ReadTextAsync(file);
        }
    }
}
