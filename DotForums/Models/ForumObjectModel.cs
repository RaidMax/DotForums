using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DotForums.Models
{

    public class ForumObjectModel
    {
        [Key]
        public ulong ID { get; set; }
        public string Name { get; set; }

        public static string ForumObjectNavigationAsJSON<T>(T Object)
        {
            var x = new DataContractJsonSerializer(typeof(T));
            using (var Stream = new MemoryStream())
            {
                x.WriteObject(Stream, Object);
                return System.Text.Encoding.ASCII.GetString(Stream.ToArray());
            }
        }

        public static T JSONAsForumObjectNavigation<T>(string Object)
        {
            var x = new DataContractJsonSerializer(typeof(T));
            using (var Stream = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(Object)))
            {
                return (T)x.ReadObject(Stream);
            }
        }
    }
}
