using DevExpress.Xpo;
using DevExpress.XtraEditors;
using ims.data;
using ims.domain;
using ims.framework;

namespace ims.desktop
{
    public class InventoryForm : BaseListForm
    {
        private UnitOfWork unitOfWork;

        public InventoryForm()
        {
            Text = "Inventory";

            unitOfWork = XpoDataService.CreateUnitOfWork();

            LoadInventory();
        }

        private void LoadInventory()
        {
            var records = new XPCollection<InventoryItem>(unitOfWork);

            SetDataSource(records);

            GridView.Columns["Oid"].Visible = false;

            GridView.BestFitColumns();
        }

        protected override void OnAdd()
        {
            using var form = new InventoryEditForm();

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                LoadInventory();
            }
        }

        protected override void OnEdit(object record)
        {
            if (record is InventoryItem item)
            {
                using var form = new InventoryEditForm(item.Oid);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadInventory();
                }
            }
        }

        protected override void OnDelete(object record)
        {
            if (record is InventoryItem item)
            {
                item.Delete();
                unitOfWork.CommitChanges();
            }
        }

        protected override void OnRefresh()
        {
            LoadInventory();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}