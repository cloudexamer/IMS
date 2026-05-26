using DevExpress.XtraEditors;

namespace ims.desktop
{
    public class LoginForm : XtraForm
    {
        private TextEdit usernameTextEdit;
        private TextEdit passwordTextEdit;
        private SimpleButton loginButton;
        private SimpleButton cancelButton;

        public LoginForm()
        {
            Text = "IMS Login";
            Width = 360;
            Height = 220;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            BuildLoginControls();
        }

        private void BuildLoginControls()
        {
            var userLabel = new LabelControl
            {
                Text = "Login:",
                Left = 35,
                Top = 35,
                Width = 80
            };

            usernameTextEdit = new TextEdit
            {
                Left = 115,
                Top = 30,
                Width = 180
            };

            var passLabel = new LabelControl
            {
                Text = "Password:",
                Left = 35,
                Top = 75,
                Width = 80
            };

            passwordTextEdit = new TextEdit
            {
                Left = 115,
                Top = 70,
                Width = 180
            };

            passwordTextEdit.Properties.PasswordChar = '*';

            loginButton = new SimpleButton
            {
                Text = "Login",
                Left = 115,
                Top = 120,
                Width = 85
            };

            cancelButton = new SimpleButton
            {
                Text = "Cancel",
                Left = 210,
                Top = 120,
                Width = 85
            };

            loginButton.Click += (s, e) => TryLogin();
            cancelButton.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            AcceptButton = loginButton;
            CancelButton = cancelButton;

            Controls.Add(userLabel);
            Controls.Add(usernameTextEdit);
            Controls.Add(passLabel);
            Controls.Add(passwordTextEdit);
            Controls.Add(loginButton);
            Controls.Add(cancelButton);
        }

        private void TryLogin()
        {
            #if DEBUG
                        DialogResult = DialogResult.OK;
                        Close();
                        return;
            #endif

            string login = usernameTextEdit.Text.Trim();
            string password = passwordTextEdit.Text.Trim();

            if (login == "mss" && password == "mss")
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            XtraMessageBox.Show(
                "Invalid login or password.",
                "Login Failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            passwordTextEdit.SelectAll();
            passwordTextEdit.Focus();
        }
    }
}