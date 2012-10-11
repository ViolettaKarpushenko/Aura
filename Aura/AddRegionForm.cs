using System;
using System.Windows.Forms;

namespace Aura
{
    public partial class AddRegionForm : Form
    {
        private Func<string, string> Validator;

        public AddRegionForm(string name, string caption, Func<string, string> validator)
        {
            Validator = validator;
            InitializeComponent();
            Text = name;
            _lbCaption.Text = caption;
        }

        public string Value
        {
            get
            {
                var message = Validator(_tbValue.Text);
                if (string.IsNullOrEmpty(message))
                {
                    return _tbValue.Text;
                }

                return null;
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OnNext(object sender, EventArgs e)
        {
            var message = Validator(_tbValue.Text);
            if (string.IsNullOrEmpty(message))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(message);
            }
        }
    }
}
