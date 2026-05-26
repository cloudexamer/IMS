using DevExpress.Xpo;
using DevExpress.XtraEditors;
using ims.data;
using ims.domain;
using ims.framework;

namespace ims.desktop.MenuToolBar
{
    public class RolodexForm : BaseRecordForm
    {
        private UnitOfWork unitOfWork;

        private TextEdit firstNameTextEdit;
        private TextEdit lastNameTextEdit;
        private TextEdit phoneTextEdit;
        private TextEdit emailTextEdit;

        public RolodexForm()
        {
            Text = "Rolodex";
            unitOfWork = XpoDataService.CreateUnitOfWork();

            BuildFields();
            BindFields();

            LoadRecords();
            SetViewMode();
        }

        private void BuildFields()
        {
            AddLabel("First Name:", 30, 35);
            firstNameTextEdit = AddTextEdit(130, 30);

            AddLabel("Last Name:", 30, 75);
            lastNameTextEdit = AddTextEdit(130, 70);

            AddLabel("Phone:", 30, 115);
            phoneTextEdit = AddTextEdit(130, 110);

            AddLabel("Email:", 30, 155);
            emailTextEdit = AddTextEdit(130, 150);
        }

        private void BindFields()
        {
            firstNameTextEdit.DataBindings.Add("EditValue", BindingSource, "FirstName", true, DataSourceUpdateMode.OnPropertyChanged);
            lastNameTextEdit.DataBindings.Add("EditValue", BindingSource, "LastName", true, DataSourceUpdateMode.OnPropertyChanged);
            phoneTextEdit.DataBindings.Add("EditValue", BindingSource, "Phone", true, DataSourceUpdateMode.OnPropertyChanged);
            emailTextEdit.DataBindings.Add("EditValue", BindingSource, "Email", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void AddLabel(string text, int left, int top)
        {
            var label = new LabelControl
            {
                Text = text,
                Left = left,
                Top = top,
                Width = 90
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

        private void LoadRecords()
        {
            var records = new XPCollection<Rolodex>(unitOfWork);

            SetDataSource(records);
        }

        protected override object CreateNewRecord()
        {
            return new Rolodex(unitOfWork);
        }

        protected override void OnSave()
        {
            unitOfWork.CommitChanges();
            XtraMessageBox.Show("Record saved.");
        }

        protected override void OnCancel()
        {
            unitOfWork.RollbackTransaction();
        }

        protected override void OnDelete(object record)
        {
            if (record is Rolodex rolodex)
            {
                rolodex.Delete();
                unitOfWork.CommitChanges();
            }
        }

        protected override void OnLoadRecords()
        {
            LoadRecords();
        }

        protected override void OnFind(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                BindingSource.MoveFirst();
                return;
            }

            for (int i = 0; i < BindingSource.Count; i++)
            {
                var record = BindingSource[i] as Rolodex;

                if (record == null)
                    continue;

                var fullName = $"{record.FirstName} {record.LastName}";

                if (fullName.Contains(searchText, System.StringComparison.OrdinalIgnoreCase)
                    || (record.Phone?.Contains(searchText) ?? false)
                    || (record.Email?.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ?? false))
                {
                    BindingSource.Position = i;
                    return;
                }
            }

            XtraMessageBox.Show("No matching record found.");
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