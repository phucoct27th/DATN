using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutiShopDataContext.Heper
{
    public class JsonGioHang
    {
        public int IdSanPham { set; get; }
        public string Ten { set; get; }
        public string Anh { set; get; }
        public int Gia { set; get; }
        public JsonGioHang(int _IdSanPham,string _Ten ,string _Anh, int _Gia)
        {
            this.IdSanPham = _IdSanPham;
            this.Ten = _Ten;
            this.Anh = _Anh;
            this.Gia = _Gia;
        }
    }
}
