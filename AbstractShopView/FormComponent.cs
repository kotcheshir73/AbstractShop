using AbstractShopServiceDAL.BindingModels;
using AbstractShopServiceDAL.ViewModels;
using System;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormComponent : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormComponent()
        {
            InitializeComponent();
        }

        private void FormComponent_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ComponentViewModel component = APIClient.GetRequest<ComponentViewModel>("api/Component/Get/" + id.Value);
                    textBoxName.Text = component.ComponentName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<ComponentBindingModel, bool>("api/Component/UpdElement", new ComponentBindingModel
                    {
                        Id = id.Value,
                        ComponentName = textBoxName.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<ComponentBindingModel, bool>("api/Component/AddElement", new ComponentBindingModel
                    {
                        ComponentName = textBoxName.Text
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