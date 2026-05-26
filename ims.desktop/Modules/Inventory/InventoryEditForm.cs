using DevExpress.Xpo;
using DevExpress.XtraEditors;
using ims.data;
using ims.domain;
using ims.framework;
using System.Windows.Forms;

namespace ims.desktop
{
    public class InventoryEditForm : BaseRecordForm
    {
        private UnitOfWork unitOfWork;
        private InventoryItem? currentItem;

        private TextEdit itemNumberTextEdit;
        private TextEdit itemNameTextEdit;
        private TextEdit categoryTextEdit;
        private SpinEdit quantitySpinEdit;
        private SpinEdit unitCostSpinEdit;

        public InventoryEditForm()
        {
            Text = "Add Inventory Item";

            unitOfWork = XpoDataService.CreateUnitOfWork();

            BuildFields();
            BindFields();

            AddRecord();
        }

        public InventoryEditForm(int oid)
        {
            Text = "Edit Inventory Item";

            unitOfWork = XpoDataService.CreateUnitOfWork();
            currentItem = unitOfWork.GetObjectByKey<InventoryItem>(oid);

            BuildFields();
            BindFields();

            SetDataSource(currentItem);
            SetViewMode();
            EditRecord();
        }

        private void BuildFields()
        {
            AddLabel("Item Number:", 30, 35);
            itemNumberTextEdit = AddTextEdit(140, 30);

            AddLabel("Item Name:", 30, 75);
            itemNameTextEdit = AddTextEdit(140, 70);

            AddLabel("Category:", 30, 115);
            categoryTextEdit = AddTextEdit(140, 110);

            AddLabel("Qty On Hand:", 30, 155);
            quantitySpinEdit = AddSpinEdit(140, 150);

            AddLabel("Unit Cost:", 30, 195);
            unitCostSpinEdit = AddMoneySpinEdit(140, 190);
        }

        private void BindFields()
        {
            itemNumberTextEdit.DataBindings.Add("EditValue", BindingSource, "ItemNumber", true, DataSourceUpdateMode.OnPropertyChanged);
            itemNameTextEdit.DataBindings.Add("EditValue", BindingSource, "ItemName", true, DataSourceUpdateMode.OnPropertyChanged);
            categoryTextEdit.DataBindings.Add("EditValue", BindingSource, "Category", true, DataSourceUpdateMode.OnPropertyChanged);
            quantitySpinEdit.DataBindings.Add("EditValue", BindingSource, "QuantityOnHand", true, DataSourceUpdateMode.OnPropertyChanged);
            unitCostSpinEdit.DataBindings.Add("EditValue", BindingSource, "UnitCost", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void AddLabel(string text, int left, int top)
        {
            var label = new LabelControl
            {
                Text = text,
                Left = left,
                Top = top,
                Width = 100
            };

            ContentPanel.Controls.Add(label);
        }

        private TextEdit AddTextEdit(int left, int top)
        {
            var textEdit = new TextEdit
            {
                Left = left,
                Top = top,
                Width = 250
            };

            ContentPanel.Controls.Add(textEdit);
            return textEdit;
        }

        private SpinEdit AddSpinEdit(int left, int top)
        {
            var spinEdit = new SpinEdit
            {
                Left = left,
                Top = top,
                Width = 120
            };

            spinEdit.Properties.IsFloatValue = false;
            spinEdit.Properties.MinValue = 0;
            spinEdit.Properties.MaxValue = 999999;

            ContentPanel.Controls.Add(spinEdit);
            return spinEdit;
        }

        private SpinEdit AddMoneySpinEdit(int left, int top)
        {
            var spinEdit = new SpinEdit
            {
                Left = left,
                Top = top,
                Width = 120
            };

            spinEdit.Properties.IsFloatValue = true;
            spinEdit.Properties.Mask.EditMask = "c2";
            spinEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            spinEdit.Properties.MinValue = 0;
            spinEdit.Properties.MaxValue = 999999;

            ContentPanel.Controls.Add(spinEdit);
            return spinEdit;
        }

        protected override object CreateNewRecord()
        {
            currentItem = new InventoryItem(unitOfWork);
            return currentItem;
        }

        protected override void OnSave()
        {
            unitOfWork.CommitChanges();
            DialogResult = DialogResult.OK;
            Close();
        }

        protected override void OnCancel()
        {
            unitOfWork.RollbackTransaction();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected override void OnDelete(object record)
        {
            if (record is InventoryItem item)
            {
                item.Delete();
                unitOfWork.CommitChanges();
                DialogResult = DialogResult.OK;
                Close();
            }
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