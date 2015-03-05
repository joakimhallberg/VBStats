using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace VolleyballStats
{
    public class FileStorage
    {
        StorageFolder folder = ApplicationData.Current.LocalFolder;
        CreationCollisionOption option = CreationCollisionOption.ReplaceExisting;

        public StorageFile CreateFile(string name)
        {
            var file = await folder.CreateFileAsync(name, option);
            return file;
        }

        public void Write(StorageFile file, string content)
        {
            await FileIO.Write(file, content);
        }

        public StorageFile Get(string name)
        {
            return await folder.GetFileAsync(name);
        }

        public string GetContent(StorageFile file)
        {
            return await FileIO.ReadTextAsync(file);
        }
    }
}
