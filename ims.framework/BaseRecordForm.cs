using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors.Controls;

namespace ims.framework
{
    public class BaseRecordForm : XtraForm
    {
        protected BindingSource BindingSource;

        protected PanelControl SearchPanel;
        protected PanelControl NavigationPanel;
        protected PanelControl ContentPanel;
        protected PanelControl BottomButtonPanel;

        protected SimpleButton AddButton;
        protected SimpleButton EditButton;
        protected SimpleButton SaveButton;
        protected SimpleButton CancelButton;
        protected SimpleButton DeleteButton;

        protected TextEdit SearchTextEdit;
        protected SimpleButton FindButton;

        protected SimpleButton FirstButton;
        protected SimpleButton PreviousButton;
        protected LabelControl RecordLabel;
        protected SimpleButton NextButton;
        protected SimpleButton LastButton;

        protected bool IsAdding;
        protected bool IsEditing;

        //private const int SmallButtonWidth = 48;
        //private const int SmallButtonHeight = 32;
        private const int SmallButtonWidth = 36;
        private const int SmallButtonHeight = 28;
        //private const int ButtonGap = 6;

        private const int CommandButtonWidth = 72;
        private const int CommandButtonHeight = 28;

        private const int NavButtonWidth = 48;
        private const int NavButtonHeight = 28;

        private const int ButtonGap = 6;
        protected SimpleButton ExitButton;

        public BaseRecordForm()
        {
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            InitializeBaseRecordForm();
            SetViewMode();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            PositionBottomButtons();
            PositionSearchControls();
            PositionNavigationControls();
        }

        private void InitializeBaseRecordForm()
        {
            Text = "Base Record Form";
            Width = 470;
            Height = 560;
            MinimumSize = new Size(470, 560);

            BindingSource = new BindingSource();

            SearchPanel = new PanelControl
            {
                Dock = DockStyle.Top,
                Height = 45
            };

            NavigationPanel = new PanelControl
            {
                Dock = DockStyle.Top,
                Height = 45
            };

            ContentPanel = new PanelControl
            {
                Dock = DockStyle.Fill
            };

            BottomButtonPanel = new PanelControl
            {
                Dock = DockStyle.Bottom,
                Height = 48
            };

            SearchTextEdit = new TextEdit
            {
                Left = 10,
                Top = 8,
                Width = 300
            };

            FindButton = CreateIconButton("Find", CreateFindIcon());
            FindButton.Left = SearchTextEdit.Right + 8;
            FindButton.Top = 7;
            //FindButton = CreateNavButton("Find");
            //FindButton.Width = 55;

            SearchPanel.Controls.Add(SearchTextEdit);
            SearchPanel.Controls.Add(FindButton);

            //FirstButton = CreateIconButton("Beg", CreateFirstIcon());
            //PreviousButton = CreateIconButton("Prev", CreatePreviousIcon());
            //NextButton = CreateIconButton("Next", CreateNextIcon());
            //LastButton = CreateIconButton("End", CreateLastIcon());

            FirstButton = CreateNavButton("|<");
            PreviousButton = CreateNavButton("<");
            NextButton = CreateNavButton(">");
            LastButton = CreateNavButton(">|");

            FirstButton.Left = 10;
            FirstButton.Top = 7;

            PreviousButton.Left = FirstButton.Right + ButtonGap;
            PreviousButton.Top = 7;

            RecordLabel = new LabelControl
            {
                Text = "Record 0 of 0",
                Width = 120,
                AutoSizeMode = LabelAutoSizeMode.None,
                Appearance =
                {
                    TextOptions =
                    {
                        HAlignment = DevExpress.Utils.HorzAlignment.Center
                    }
                }
            };

            NextButton.Left = 325;
            NextButton.Top = 7;

            LastButton.Left = NextButton.Right + ButtonGap;
            LastButton.Top = 7;

            NavigationPanel.Controls.Add(FirstButton);
            NavigationPanel.Controls.Add(PreviousButton);
            NavigationPanel.Controls.Add(RecordLabel);
            NavigationPanel.Controls.Add(NextButton);
            NavigationPanel.Controls.Add(LastButton);

            //AddButton = CreateIconButton("Add", CreateAddIcon(), true);
            //EditButton = CreateIconButton("Edit", CreateEditIcon(), true);
            //SaveButton = CreateIconButton("Save", CreateSaveIcon(), true);
            //CancelButton = CreateIconButton("Cancel", CreateCancelIcon(), true);
            //DeleteButton = CreateIconButton("Delete", CreateDeleteIcon(), true);

            AddButton = CreateCommandButton("Add", true);
            EditButton = CreateCommandButton("Edit", true);
            SaveButton = CreateCommandButton("Save", true);
            CancelButton = CreateCommandButton("Cancel", true);
            DeleteButton = CreateCommandButton("Delete", true);
            ExitButton = CreateExitButton();
            ExitButton.Click += (s, e) => Close();

            BottomButtonPanel.Controls.Add(AddButton);
            BottomButtonPanel.Controls.Add(EditButton);
            BottomButtonPanel.Controls.Add(SaveButton);
            BottomButtonPanel.Controls.Add(CancelButton);
            BottomButtonPanel.Controls.Add(DeleteButton);
            BottomButtonPanel.Controls.Add(ExitButton);

            PositionBottomButtons();

            Controls.Add(ContentPanel);
            Controls.Add(BottomButtonPanel);
            Controls.Add(NavigationPanel);
            Controls.Add(SearchPanel);

            BottomButtonPanel.Resize += (s, e) => PositionBottomButtons();
            SearchPanel.Resize += (s, e) => PositionSearchControls();
            NavigationPanel.Resize += (s, e) => PositionNavigationControls();

            AddButton.Click += (s, e) => AddRecord();
            EditButton.Click += (s, e) => EditRecord();
            SaveButton.Click += (s, e) => SaveRecord();
            CancelButton.Click += (s, e) => CancelRecord();
            DeleteButton.Click += (s, e) => DeleteRecord();

            FindButton.Click += (s, e) => FindRecord();

            FirstButton.Click += (s, e) => MoveFirst();
            PreviousButton.Click += (s, e) => MovePrevious();
            NextButton.Click += (s, e) => MoveNext();
            LastButton.Click += (s, e) => MoveLast();

            BindingSource.PositionChanged += (s, e) => UpdateRecordLabel();
            BindingSource.ListChanged += (s, e) => UpdateRecordLabel();
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

        private SimpleButton CreateCommandButton(string text, bool anchorRight = false)
        {
            var button = new SimpleButton
            {
                Text = text,
                Width = CommandButtonWidth,
                Height = CommandButtonHeight,
                Anchor = anchorRight
                    ? AnchorStyles.Top | AnchorStyles.Right
                    : AnchorStyles.Top | AnchorStyles.Left
            };

            return button;
        }

        private SimpleButton CreateNavButton(string text)
        {
            var button = new SimpleButton
            {
                Text = text,
                Width = NavButtonWidth,
                Height = NavButtonHeight,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            return button;
        }

        private SimpleButton CreateIconButton(string tooltip, Image image, bool anchorRight = false)
        {
            var button = new SimpleButton
            {
                Width = SmallButtonWidth,
                Height = SmallButtonHeight,
                ImageOptions =
        {
            Image = image,
            ImageToTextAlignment = ImageAlignToText.LeftCenter
        },
                ToolTip = tooltip,
                Anchor = anchorRight
                    ? AnchorStyles.Top | AnchorStyles.Right
                    : AnchorStyles.Top | AnchorStyles.Left
            };

            return button;
        }

        private void PositionNavigationControls()
        {
            int gap = 6;
            int labelGap = 18;

            int totalWidth =
                FirstButton.Width +
                gap +
                PreviousButton.Width +
                labelGap +
                RecordLabel.Width +
                labelGap +
                NextButton.Width +
                gap +
                LastButton.Width;

            int left = (NavigationPanel.Width - totalWidth) / 2;
            left = Math.Max(10, left);

            FirstButton.Left = left;
            FirstButton.Top = 7;

            PreviousButton.Left = FirstButton.Right + gap;
            PreviousButton.Top = 7;

            RecordLabel.Left = PreviousButton.Right + labelGap;
            RecordLabel.Top = 13;

            NextButton.Left = RecordLabel.Right + labelGap;
            NextButton.Top = 7;

            LastButton.Left = NextButton.Right + gap;
            LastButton.Top = 7;
        }

        private void PositionSearchControls()
        {
            int gap = 8;
            int searchWidth = 300;
            int findWidth = 48;

            int totalWidth = searchWidth + gap + findWidth;
            int left = (SearchPanel.Width - totalWidth) / 2;

            SearchTextEdit.Width = searchWidth;
            SearchTextEdit.Left = Math.Max(10, left);
            SearchTextEdit.Top = 8;

            FindButton.Width = findWidth;
            FindButton.Left = SearchTextEdit.Right + gap;
            FindButton.Top = 7;
        }

        private void PositionBottomButtons()
        {
            int rightMargin = 10;
            int top = 8;

            ExitButton.Left = BottomButtonPanel.Width - rightMargin - ExitButton.Width;
            ExitButton.Top = top;

            DeleteButton.Left = ExitButton.Left - ButtonGap - CommandButtonWidth;
            DeleteButton.Top = top;

            CancelButton.Left = DeleteButton.Left - ButtonGap - CommandButtonWidth;
            CancelButton.Top = top;

            SaveButton.Left = CancelButton.Left - ButtonGap - CommandButtonWidth;
            SaveButton.Top = top;

            EditButton.Left = SaveButton.Left - ButtonGap - CommandButtonWidth;
            EditButton.Top = top;

            AddButton.Left = EditButton.Left - ButtonGap - CommandButtonWidth;
            AddButton.Top = top;
            //int rightMargin = 10;
            //int top = 8;

            //DeleteButton.Left = BottomButtonPanel.Width - rightMargin - SmallButtonWidth;
            //DeleteButton.Top = top;

            //CancelButton.Left = DeleteButton.Left - ButtonGap - SmallButtonWidth;
            //CancelButton.Top = top;

            //SaveButton.Left = CancelButton.Left - ButtonGap - SmallButtonWidth;
            //SaveButton.Top = top;

            //EditButton.Left = SaveButton.Left - ButtonGap - SmallButtonWidth;
            //EditButton.Top = top;

            //AddButton.Left = EditButton.Left - ButtonGap - SmallButtonWidth;
            //AddButton.Top = top;
        }

        protected void SetDataSource(object dataSource)
        {
            BindingSource.DataSource = dataSource;
            UpdateRecordLabel();
        }

        protected object? GetCurrentRecord()
        {
            return BindingSource.Current;
        }

        protected virtual void AddRecord()
        {
            IsAdding = true;
            IsEditing = false;

            BindingSource.DataSource = CreateNewRecord();

            SetEditMode();
            OnAdd();
            UpdateRecordLabel();
        }

        protected virtual void EditRecord()
        {
            if (GetCurrentRecord() == null)
            {
                XtraMessageBox.Show("No record selected.");
                return;
            }

            IsAdding = false;
            IsEditing = true;

            SetEditMode();
            OnEdit();
        }

        protected virtual void SaveRecord()
        {
            BindingSource.EndEdit();

            OnSave();

            IsAdding = false;
            IsEditing = false;

            SetViewMode();
            ReloadRecords();
            UpdateRecordLabel();
        }

        protected virtual void CancelRecord()
        {
            BindingSource.CancelEdit();

            OnCancel();

            IsAdding = false;
            IsEditing = false;

            SetViewMode();
            ReloadRecords();
            UpdateRecordLabel();
        }

        protected virtual void DeleteRecord()
        {
            if (GetCurrentRecord() == null)
            {
                XtraMessageBox.Show("No record selected.");
                return;
            }

            var result = XtraMessageBox.Show(
                "Delete current record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            OnDelete(GetCurrentRecord());

            ReloadRecords();
            SetViewMode();
            UpdateRecordLabel();
        }

        protected virtual void FindRecord()
        {
            OnFind(SearchTextEdit.Text);
            UpdateRecordLabel();
        }

        protected virtual void MoveFirst()
        {
            BindingSource.MoveFirst();
            UpdateRecordLabel();
        }

        protected virtual void MovePrevious()
        {
            BindingSource.MovePrevious();
            UpdateRecordLabel();
        }

        protected virtual void MoveNext()
        {
            BindingSource.MoveNext();
            UpdateRecordLabel();
        }

        protected virtual void MoveLast()
        {
            BindingSource.MoveLast();
            UpdateRecordLabel();
        }

        protected virtual void ReloadRecords()
        {
            OnLoadRecords();
        }

        protected virtual object CreateNewRecord()
        {
            throw new NotImplementedException("Child form must override CreateNewRecord().");
        }

        protected virtual void OnLoadRecords() { }
        protected virtual void OnAdd() { }
        protected virtual void OnEdit() { }
        protected virtual void OnSave() { }
        protected virtual void OnCancel() { }
        protected virtual void OnDelete(object record) { }
        protected virtual void OnFind(string searchText) { }

        protected virtual void SetEditMode()
        {
            AddButton.Enabled = false;
            EditButton.Enabled = false;
            DeleteButton.Enabled = false;
            SaveButton.Enabled = true;
            CancelButton.Enabled = true;

            SearchTextEdit.Enabled = false;
            FindButton.Enabled = false;

            FirstButton.Enabled = false;
            PreviousButton.Enabled = false;
            NextButton.Enabled = false;
            LastButton.Enabled = false;

            SetDataControlsEnabled(true);
        }

        protected virtual void SetViewMode()
        {
            AddButton.Enabled = true;
            EditButton.Enabled = true;
            DeleteButton.Enabled = true;
            SaveButton.Enabled = false;
            CancelButton.Enabled = false;

            SearchTextEdit.Enabled = true;
            FindButton.Enabled = true;

            FirstButton.Enabled = true;
            PreviousButton.Enabled = true;
            NextButton.Enabled = true;
            LastButton.Enabled = true;

            SetDataControlsEnabled(false);
        }

        protected virtual void SetDataControlsEnabled(bool enabled)
        {
            SetControlsEditableRecursive(ContentPanel, enabled);
        }

        private void SetControlsEditableRecursive(Control parent, bool editable)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is BaseEdit editor)
                {
                    editor.Properties.ReadOnly = !editable;
                    editor.TabStop = editable;

                    if (editable)
                        editor.BackColor = SystemColors.Window;
                    else
                        editor.BackColor = SystemColors.Control;
                }

                if (control.HasChildren)
                {
                    SetControlsEditableRecursive(control, editable);
                }
            }
        }

        protected virtual void UpdateRecordLabel()
        {
            if (BindingSource.Count == 0)
            {
                RecordLabel.Text = "Record 0 of 0";
                return;
            }

            RecordLabel.Text = $"Record {BindingSource.Position + 1} of {BindingSource.Count}";
        }

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

        private Image CreateSaveIcon() => DrawIcon(g =>
        {
            using var brush = new SolidBrush(Color.SteelBlue);
            g.FillRectangle(brush, 7, 6, 18, 20);
            g.FillRectangle(Brushes.White, 10, 8, 10, 5);
            g.FillRectangle(Brushes.LightGray, 11, 18, 10, 6);
        });

        private Image CreateCancelIcon() => DrawIcon(g =>
        {
            using var pen = new Pen(Color.Firebrick, 3);
            g.DrawLine(pen, 8, 8, 24, 24);
            g.DrawLine(pen, 24, 8, 8, 24);
        });

        private Image CreateDeleteIcon() => DrawIcon(g =>
        {
            using var pen = new Pen(Color.DarkRed, 2);
            g.DrawRectangle(pen, 9, 11, 14, 15);
            g.DrawLine(pen, 7, 10, 25, 10);
            g.DrawLine(pen, 13, 7, 19, 7);
        });

        private Image CreateFindIcon() => DrawIcon(g =>
        {
            using var pen = new Pen(Color.DarkSlateGray, 3);
            g.DrawEllipse(pen, 7, 7, 12, 12);
            g.DrawLine(pen, 18, 18, 25, 25);
        });

        private Image CreateFirstIcon() => DrawIcon(g =>
        {
            using var pen = new Pen(Color.Black, 2);
            g.DrawLine(pen, 8, 8, 8, 24);
            g.DrawLine(pen, 22, 8, 12, 16);
            g.DrawLine(pen, 12, 16, 22, 24);
        });

        private Image CreatePreviousIcon() => DrawIcon(g =>
        {
            using var pen = new Pen(Color.Black, 2);
            g.DrawLine(pen, 21, 8, 11, 16);
            g.DrawLine(pen, 11, 16, 21, 24);
        });

        private Image CreateNextIcon() => DrawIcon(g =>
        {
            using var pen = new Pen(Color.Black, 2);
            g.DrawLine(pen, 11, 8, 21, 16);
            g.DrawLine(pen, 21, 16, 11, 24);
        });

        private Image CreateLastIcon() => DrawIcon(g =>
        {
            using var pen = new Pen(Color.Black, 2);
            g.DrawLine(pen, 24, 8, 24, 24);
            g.DrawLine(pen, 10, 8, 20, 16);
            g.DrawLine(pen, 20, 16, 10, 24);
        });

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