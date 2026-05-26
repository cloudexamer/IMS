using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using ims.desktop.MenuToolBar;
using ims.desktop.Modules.Orders;

namespace ims.desktop
{
    public partial class MainForm : XtraForm
    {
        //These fields are initialized later in InitializeMainShell().
        private BarManager barManager = null!;
        private Bar mainMenu = null!;
        private Bar toolBar = null!;

        public MainForm()
        {
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            InitializeMainShell();
        }

        private void InitializeMainShell()
        {
            Text = "Integrated Management System (IMS)";
            WindowState = FormWindowState.Maximized;
            IsMdiContainer = true;
            BackColor = Color.FromArgb(120, 120, 120);

            barManager = new BarManager();
            barManager.Form = this;

            mainMenu = new Bar(barManager, "Main Menu");
            mainMenu.DockStyle = BarDockStyle.Top;
            mainMenu.OptionsBar.MultiLine = true;
            barManager.MainMenu = mainMenu;

            toolBar = new Bar(barManager, "Shortcut Bar");
            toolBar.DockStyle = BarDockStyle.Top;

            barManager.Bars.Add(mainMenu);
            barManager.Bars.Add(toolBar);

            AddMenuItems();
            AddToolbarButtons();
        }

        private void AddMenuItems()
        {
            var fileMenu = new BarSubItem(barManager, "File");

            var exitItem = new BarButtonItem(barManager, "Exit");
            exitItem.ItemClick += (s, e) => ExitApplication();
            fileMenu.AddItem(exitItem);

            mainMenu.AddItem(fileMenu);

            mainMenu.AddItem(CreateMenuItem<RolodexForm>("Rolodex"));
            mainMenu.AddItem(CreateMenuItem<InventoryForm>("Inventory"));

            mainMenu.AddItem(CreateMenuItem<OrdersForm>("Orders"));
            //mainMenu.AddItem(CreateComingSoonMenuItem("Orders"));
            mainMenu.AddItem(CreateComingSoonMenuItem("Payroll"));
            mainMenu.AddItem(CreateComingSoonMenuItem("Employees"));
            //mainMenu.AddItem(new BarSubItem(barManager, "Orders"));
            //mainMenu.AddItem(new BarSubItem(barManager, "Payroll"));
            //mainMenu.AddItem(new BarSubItem(barManager, "Employees"));
        }

        private void AddToolbarButtons()
        {
            toolBar.AddItem(CreateToolbarButton<RolodexForm>("Rolodex", "rolodex.png"));
            toolBar.AddItem(CreateToolbarButton<InventoryForm>("Inventory", "inventory.png"));
            toolBar.AddItem(CreateExitToolbarButton());
            //toolBar.AddItem(CreateToolbarActionButton("Exit", "exit.png", (s, e) => ExitApplication()));

            //toolBar.AddItem(CreateToolbarButton<OrdersForm>("Orders", "orders.png"));
            //toolBar.AddItem(CreateToolbarButton<EmployeesForm>("Employees", "employees.png"));
            //toolBar.AddItem(CreateToolbarButton<TasksForm>("Tasks", "tasks.png"));
        }

        private BarButtonItem CreateToolbarButton<TForm>(string caption, string iconFileName, bool includeCaption = false)
            where TForm : XtraForm, new()
        {
            var button = new BarButtonItem(barManager, caption)
            {
                Hint = caption, // Shows text next to icon
                PaintStyle = BarItemPaintStyle.CaptionGlyph
            };

            button.Caption = includeCaption ? caption : "";

            string imagePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Assets",
                "MenuToolbar",
                iconFileName
            );

            if (File.Exists(imagePath))
            {
                button.ImageOptions.Image = Image.FromFile(imagePath);
            }

            button.ItemClick += (s, e) => OpenChildForm<TForm>();

            return button;
        }

        private BarButtonItem CreateComingSoonMenuItem(string caption)
        {
            var item = new BarButtonItem(barManager, caption);

            item.ItemClick += (s, e) =>
            {
                XtraMessageBox.Show(
                    "Coming soon",
                    caption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            };

            return item;
        }

        private BarButtonItem CreateToolbarActionButton(
            string caption,
            string iconFileName,
            ItemClickEventHandler clickHandler,
            bool includeCaption = false)
        {
            var button = new BarButtonItem(barManager, caption)
            {
                Hint = caption,
                PaintStyle = BarItemPaintStyle.CaptionGlyph
            };

            button.Caption = includeCaption ? caption : "";

            string imagePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Assets",
                "MenuToolbar",
                iconFileName
            );

            if (File.Exists(imagePath))
            {
                button.ImageOptions.Image = Image.FromFile(imagePath);
            }

            button.ItemClick += clickHandler;

            return button;
        }

        private Image CreateGreenExitIcon()
        {
            var bitmap = new Bitmap(32, 32);

            using var g = Graphics.FromImage(bitmap);
            g.Clear(Color.Transparent);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using var darkGreenBrush = new SolidBrush(Color.DarkGreen);
            using var lightGreenBrush = new SolidBrush(Color.FromArgb(130, 190, 130));
            using var pen = new Pen(Color.DarkGreen, 2);

            // Door frame
            g.DrawRectangle(pen, 8, 6, 14, 20);

            // Door
            g.FillRectangle(lightGreenBrush, 10, 8, 10, 16);
            g.DrawRectangle(pen, 10, 8, 10, 16);

            // Exit arrow
            g.FillRectangle(darkGreenBrush, 14, 14, 8, 4);

            g.FillPolygon(darkGreenBrush, new[]
            {
                new Point(19, 11),
                new Point(28, 16),
                new Point(19, 21)
            });

            return bitmap;
        }

        private BarButtonItem CreateExitToolbarButton()
        {
            var button = new BarButtonItem(barManager, "")
            {
                Hint = "Exit",
                PaintStyle = BarItemPaintStyle.Standard
            };

            button.ImageOptions.Image = CreateGreenExitIcon();

            button.ItemClick += (s, e) => ExitApplication();

            return button;
        }

        private void InitializeComponent()
        {

        }

        private void ExitApplication()
        {
            var result = XtraMessageBox.Show(
                "Exit IMS?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
                Close();
        }

        private void OpenChildForm<TForm>()
            where TForm : XtraForm, new()
        {
            var form = new TForm();
            form.MdiParent = this;
            form.Show();
        }

        private BarButtonItem CreateMenuItem<TForm>(string caption)
            where TForm : XtraForm, new()
        {
            var item = new BarButtonItem(barManager, caption);

            item.ItemClick += (s, e) => OpenChildForm<TForm>();

            return item;
        }
    }
}