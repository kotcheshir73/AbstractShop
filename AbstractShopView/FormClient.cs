using AbstractShopService;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormClient : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        private InterfacesName type = InterfacesName.IClientService;

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
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.GetElement,
                        Request = id.Value
                    };
                    var response = TSPClient<ClientViewModel>.SendRequest(model);
                    if (response.Success)
                    {
                        textBoxFIO.Text = response.Response.ClientFIO;
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
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ResponseModel<ClientViewModel> response;
                if (id.HasValue)
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.UpdElement,
                        Request = new ClientBindingModel
                        {
                            Id = id.Value,
                            ClientFIO = textBoxFIO.Text
                        }
                    };
                    response = TSPClient<ClientViewModel>.SendRequest(model);
                }
                else
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.AddElement,
                        Request = new ClientBindingModel
                        {
                            ClientFIO = textBoxFIO.Text
                        }
                    };
                    response = TSPClient<ClientViewModel>.SendRequest(model);
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
