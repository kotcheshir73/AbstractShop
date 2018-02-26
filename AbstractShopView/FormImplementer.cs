using AbstractShopService;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormImplementer : Form
    {
        public int Id { set { id = value; } }

        private InterfacesName type = InterfacesName.IImplementerService;

        private int? id;

        public FormImplementer()
        {
            InitializeComponent();
        }

        private void FormImplementer_Load(object sender, EventArgs e)
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
                    var response = TSPClient<ImplementerViewModel>.SendRequest(model);
                    if (response.Success)
                    {
                        textBoxFIO.Text = response.Response.ImplementerFIO;
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
                ResponseModel<ImplementerViewModel> response;
                if (id.HasValue)
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.UpdElement,
                        Request = new ImplementerBindingModel
                        {
                            Id = id.Value,
                            ImplementerFIO = textBoxFIO.Text
                        }
                    };
                    response = TSPClient<ImplementerViewModel>.SendRequest(model);
                }
                else
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.AddElement,
                        Request = new ImplementerBindingModel
                        {
                            ImplementerFIO = textBoxFIO.Text
                        }
                    };
                    response = TSPClient<ImplementerViewModel>.SendRequest(model);
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
