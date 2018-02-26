using AbstractShopService;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormComponent : Form
    {
        public int Id { set { id = value; } }

        private InterfacesName type = InterfacesName.IComponentService;

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
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.GetElement,
                        Request = id.Value
                    };
                    var response = TSPClient<ComponentViewModel>.SendRequest(model);
                    if (response.Success)
                    {
                        textBoxName.Text = response.Response.ComponentName;
                    }
                    else
                    {
                        throw new Exception(response.ErrorMessage);
                    }
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
                ResponseModel<ComponentViewModel> response;
                if (id.HasValue)
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.UpdElement,
                        Request = new ComponentBindingModel
                        {
                            Id = id.Value,
                            ComponentName = textBoxName.Text
                        }
                    };
                    response = TSPClient<ComponentViewModel>.SendRequest(model);
                }
                else
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.AddElement,
                        Request = new ComponentBindingModel
                        {
                            ComponentName = textBoxName.Text
                        }
                    };
                    response = TSPClient<ComponentViewModel>.SendRequest(model);
                }
                if (response.Success)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    throw new Exception(response.ErrorMessage);
                }
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
