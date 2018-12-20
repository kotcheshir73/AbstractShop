using AbstractShopServiceDAL.BindingModels;
using AbstractShopServiceDAL.ViewModels;
using System;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormClient : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormClient()
        {
            InitializeComponent();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ClientViewModel client = APIClient.GetRequest<ClientViewModel>("api/Client/Get/" + id.Value);
                    textBoxFIO.Text = client.ClientFIO;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<ClientBindingModel, bool>("api/Client/UpdElement", new ClientBindingModel
                    {
                        Id = id.Value,
                        ClientFIO = textBoxFIO.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<ClientBindingModel, bool>("api/Client/AddElement", new ClientBindingModel
                    {
                        ClientFIO = textBoxFIO.Text
                    });
                }

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}