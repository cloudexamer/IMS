using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ims.framework
{
    public class BaseListForm : XtraForm
    {
        protected PanelControl BottomButtonPanel;
        protected GridControl Grid;
        protected GridView GridView;

        protected SimpleButton AddButton;
        protected SimpleButton EditButton;
        protected SimpleButton DeleteButton;
        protected SimpleButton RefreshButton;

        protected BindingSource BindingSource;

        private const int SmallButtonWidth = 48;
        private const int SmallButtonHeight = 32;
        private const int ButtonGap = 6;
        protected SimpleButton ExitButton;

        public BaseListForm()
        {
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            InitializeBaseListForm();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            PositionBottomButtons();
        }

        private void InitializeBaseListForm()
        {
            Text = "Base List Form";
            Width = 700;
            Height = 500;

            BindingSource = new BindingSource();

            BottomButtonPanel = new PanelControl
            {
                Dock = DockStyle.Bottom,
                Height = 48
            };

            AddButton = CreateIconButton("Add", CreateAddIcon());
            EditButton = CreateIconButton("Edit", CreateEditIcon());
            DeleteButton = CreateIconButton("Delete", CreateDeleteIcon());
            RefreshButton = CreateIconButton("Refresh", CreateRefreshIcon());


            ExitButton = CreateExitButton();
            BottomButtonPanel.Controls.Add(AddButton);
            BottomButtonPanel.Controls.Add(EditButton);
            BottomButtonPanel.Controls.Add(DeleteButton);
            BottomButtonPanel.Controls.Add(RefreshButton);
            BottomButtonPanel.Controls.Add(ExitButton);

            ExitButton.Click += (s, e) => Close();

            Grid = new GridControl
            {
                Dock = DockStyle.Fill,
                DataSource = BindingSource
            };

            GridView = new GridView(Grid);
            Grid.MainView = GridView;
            Grid.ViewCollection.Add(GridView);

            GridView.OptionsBehavior.Editable = false;
            GridView.OptionsView.ShowGroupPanel = false;
            GridView.OptionsView.ShowAutoFilterRow = true;

            Controls.Add(Grid);
            Controls.Add(BottomButtonPanel);

            BottomButtonPanel.Resize += (s, e) => PositionBottomButtons();

            AddButton.Click += (s, e) => AddRecord();
            EditButton.Click += (s, e) => EditRecord();
            DeleteButton.Click += (s, e) => DeleteRecord();
            RefreshButton.Click += (s, e) => RefreshRecords();

            GridView.DoubleClick += (s, e) => EditRecord();
        }

        private SimpleButton CreateExitButton()
        {
            var button = new SimpleButton
            {
                Text = "",
                Width = 48,
                Height = 28,
                ToolTip = "Exit",
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            button.ImageOptions.Image = CreateExitIcon();
            button.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;

            return button;
        }

        private Image CreateExitIcon()
        {
            var bitmap = new Bitmap(32, 32);

            using var g = Graphics.FromImage(bitmap);
            g.Clear(Color.Transparent);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using var darkGreenBrush = new SolidBrush(Color.DarkGreen);
            using var lightGreenBrush = new SolidBrush(Color.FromArgb(120, 180, 120));
            using var pen = new Pen(Color.DarkGreen, 2);

            // Door frame
            g.DrawRectangle(pen, 8, 6, 14, 20);

            // Door
            g.FillRectangle(lightGreenBrush, 10, 8, 10, 16);
            g.DrawRectangle(pen, 10, 8, 10, 16);

            // Arrow
            g.FillPolygon(darkGreenBrush, new[]
            {
                new Point(18, 12),
                new Point(27, 16),
                new Point(18, 20)
            });

            g.FillRectangle(darkGreenBrush, 14, 14, 8, 4);

            return bitmap;
        }

        private SimpleButton CreateIconButton(string tooltip, Image image)
        {
            return new SimpleButton
            {
                Width = SmallButtonWidth,
                Height = SmallButtonHeight,
                ImageOptions =
                {
                    Image = image,
                    ImageToTextAlignment = ImageAlignToText.LeftCenter
                },
                ToolTip = tooltip,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
        }

        //private void PositionBottomButtons()
        //{
        //    int rightMargin = 10;
        //    int top = 8;

        //    RefreshButton.Left = BottomButtonPanel.Width - rightMargin - SmallButtonWidth;
        //    RefreshButton.Top = top;

        //    DeleteButton.Left = RefreshButton.Left - ButtonGap - SmallButtonWidth;
        //    DeleteButton.Top = top;

        //    EditButton.Left = DeleteButton.Left - ButtonGap - SmallButtonWidth;
        //    EditButton.Top = top;

        //    AddButton.Left = EditButton.Left - ButtonGap - SmallButtonWidth;
        //    AddButton.Top = top;
        //}
        private void PositionBottomButtons()
        {
            int rightMargin = 10;
            int top = 8;

            ExitButton.Left = BottomButtonPanel.Width - rightMargin - ExitButton.Width;
            ExitButton.Top = top;

            RefreshButton.Left = ExitButton.Left - ButtonGap - SmallButtonWidth;
            RefreshButton.Top = top;

            DeleteButton.Left = RefreshButton.Left - ButtonGap - SmallButtonWidth;
            DeleteButton.Top = top;

            EditButton.Left = DeleteButton.Left - ButtonGap - SmallButtonWidth;
            EditButton.Top = top;

            AddButton.Left = EditButton.Left - ButtonGap - SmallButtonWidth;
            AddButton.Top = top;
        }

        protected object? GetCurrentRecord()
        {
            return BindingSource.Current;
        }

        protected void SetDataSource(object dataSource)
        {
            BindingSource.DataSource = dataSource;
            Grid.RefreshDataSource();
            GridView.BestFitColumns();
        }

        protected virtual void AddRecord()
        {
            OnAdd();
        }

        protected virtual void EditRecord()
        {
            if (GetCurrentRecord() == null)
            {
                XtraMessageBox.Show("Please select a record first.");
                return;
            }

            OnEdit(GetCurrentRecord());
        }

        protected virtual void DeleteRecord()
        {
            if (GetCurrentRecord() == null)
            {
                XtraMessageBox.Show("Please select a record first.");
                return;
            }

            var result = XtraMessageBox.Show(
                "Delete selected record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            OnDelete(GetCurrentRecord());
            RefreshRecords();
        }

        protected virtual void RefreshRecords()
        {
            OnRefresh();
        }

        protected virtual void OnAdd() { }
        protected virtual void OnEdit(object record) { }
        protected virtual void OnDelete(object record) { }
        protected virtual void OnRefresh() { }

        private Image CreateAddIcon() => DrawIcon(g =>
        {
            using var pen = new Pen(Color.ForestGreen, 3);
            g.DrawLine(pen, 8, 16, 24, 16);
            g.DrawLine(pen, 16, 8, 16, 24);
        });

        private Image CreateEditIcon() => DrawIcon(g =>
        {
            using var pen = new Pen(Color.RoyalBlue, 3);
            g.DrawLine(pen, 9, 23, 23, 9);
            g.DrawRectangle(Pens.Gray, 7, 22, 6, 3);
        });

        private Image CreateDeleteIcon() => DrawIcon(g =>
        {
            using var pen = new Pen(Color.DarkRed, 2);
            g.DrawRectangle(pen, 9, 11, 14, 15);
            g.DrawLine(pen, 7, 10, 25, 10);
            g.DrawLine(pen, 13, 7, 19, 7);
        });

        private Image CreateRefreshIcon() => DrawIcon(g =>
        {
            using var pen = new Pen(Color.DarkGreen, 2);
            g.DrawArc(pen, 8, 8, 16, 16, 30, 280);
            g.DrawLine(pen, 21, 7, 25, 7);
            g.DrawLine(pen, 25, 7, 25, 11);
        });

        private void InitializeComponent()
        {

        }

        private Image DrawIcon(Action<Graphics> drawAction)
        {
            var bitmap = new Bitmap(32, 32);

            using var g = Graphics.FromImage(bitmap);
            g.Clear(Color.Transparent);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            drawAction(g);

            return bitmap;
        }
    }
}