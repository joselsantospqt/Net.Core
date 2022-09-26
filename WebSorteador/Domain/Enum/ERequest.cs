using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum ERequest
    {
        [Description("Put")]
        Put,
        [Description("Post")]
        Post,
        [Description("GetById")]
        GetById,
        [Description("GetAll")]
        GetAll,
        [Description("Delete")]
        Delete,

    }
}
