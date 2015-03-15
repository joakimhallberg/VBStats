using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
//using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace VolleyballStats
{
    public class JSon
    {
        public static T Deserialize<T>(string json)
        {
            return  JsonConvert.DeserializeObject<T>(json);
            //var _Bytes = Encoding.Unicode.GetBytes(json);
            //using (MemoryStream _Stream = new MemoryStream(_Bytes))
            //{
            //    //var _Serializer = new JsonConvert(typeof(T));
            //    //return (T)_Serializer.ReadObject(_Stream);
            //}
        }

        public static async Task<string> Serialize(object instance)
        {
            return await JsonConvert.SerializeObjectAsync(instance);
            //using (MemoryStream _Stream = new MemoryStream())
            //{
            //    var _Serializer = new DataContractJsonSerializer(instance.GetType());
            //    _Serializer.WriteObject(_Stream, instance);
            //    _Stream.Position = 0;
            //    using (StreamReader _Reader = new StreamReader(_Stream))
            //    { return await  _Reader.ReadToEndAsync(); }

            //}
        }
    }
}
