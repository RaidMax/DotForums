using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Models
{
    public class PermissionModel : ForumObjectModel
    {
        [Flags]
        public enum PermissionEnum
        {
            NONE = 0x0,
            READ = 0x1,
            CREATE = 0x2,
            MODIFY = 0x4,
            DELETE = 0x8
        }

        [ForeignKey("Group")]
        public ulong? GroupID { get; set; }
        public GroupModel Group { get; set; }

        [ForeignKey("Thread")]
        public ulong? ThreadID { get; set; }
        public ThreadModel Thread { get; set; }
        public PermissionEnum Permission { get; set; }

        public const PermissionEnum ALL_PERMISSIONS = PermissionEnum.READ | PermissionEnum.CREATE | PermissionEnum.MODIFY | PermissionEnum.DELETE;

        public static bool CanRead(PermissionEnum Permissions)
        {
            return (Permissions & PermissionEnum.READ) == PermissionEnum.READ;
        }

        public static bool CanCreate(PermissionEnum Permissions)
        {
            return (Permissions & PermissionEnum.CREATE) == PermissionEnum.CREATE;
        }

        public static bool CanModify(PermissionEnum Permissions)
        {
            return (Permissions & PermissionEnum.MODIFY) == PermissionEnum.MODIFY;
        }

        public static bool CanDelete(PermissionEnum Permissions)
        {
            return (Permissions & PermissionEnum.DELETE) == PermissionEnum.DELETE;
        }

        public PermissionModel()
        {
            Name = "PermissionModel";
        }
    }
}
