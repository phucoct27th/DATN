using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutiShopDataContext.Heper
{
    public class CookieAdmin
    {
        public string Id { set; get; }
        public List<PhanQuyenAdmin> Phanquyens { set; get; }
    }
    public class PhanQuyenAdmin
    {
        public string IdQuyen { set; get; }
    }
}
