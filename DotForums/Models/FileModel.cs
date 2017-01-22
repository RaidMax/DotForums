using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Models
{
    public class FileModel : ForumObjectModel
    {
        private static readonly string UPLOAD_PATH = "./UserUploads/";
        public enum FileType
        {
            INVALID,
            TEXT,
            IMAGE,
            BINARY,
            ARCHIVE,
        }

        public string Title { get; set; }
        public DateTime Created { get; set; }
        private string _filename;
        public virtual string FileName
        {
            get
            {
                return _filename;
            }

            set
            {
                if (Created == DateTime.MinValue)
                {
                    string[] split = value.Split('\\');
                    int index = split.Count() - 1;
                    _filename = DateTime.Now.ToFileTimeUtc() + "_" + split[index];
                }

                else
                    _filename = value;
            }
        }
        public virtual string Path
        {
            get
            {
                return UPLOAD_PATH + FileName;
            }
        }
        public FileType Type { get; set; }
        [Required]
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public string Link { get; set; }

        public virtual int Size
        {
            get
            {
                return (Data == null) ? 0 : Data.Length;
            }
        }

        public FileModel()
        {
            Name = "FileModel";
            ContentType = "text/plain";
        }

        public static FileType GetFileType(string filename)
        {
            string extension = filename?.ToLower().Split('.')?[1];
            if (extension != null)
            {
                switch(extension)
                {
                    case "txt":
                    case "rtf":
                        return FileType.TEXT;
                    case "png":
                    case "jpg":
                    case "jpeg":
                    case "gif":
                    case "bmp":
                        return FileType.IMAGE;
                    case "zip":
                    case "rar":
                    case "7z":
                    case "tar":
                        return FileType.ARCHIVE;
                    case "exe":
                    case "dll":
                        return FileType.BINARY;
                    default:
                        return FileType.INVALID;
                }
            }
            return FileType.INVALID;
        }
    }
}
