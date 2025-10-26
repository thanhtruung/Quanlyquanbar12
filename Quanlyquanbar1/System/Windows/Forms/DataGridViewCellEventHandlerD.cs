
namespace System.Windows.Forms
{
    internal class DataGridViewCellEventHandlerD
    {
        private Action<object, DataGridViewCellEventArgs> dgvKhachHang_CellClick;

        public DataGridViewCellEventHandlerD(Action<object, DataGridViewCellEventArgs> dgvKhachHang_CellClick)
        {
            this.dgvKhachHang_CellClick=dgvKhachHang_CellClick;
        }
    }
}