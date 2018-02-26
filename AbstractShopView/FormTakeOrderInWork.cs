using AbstractShopService;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormTakeOrderInWork : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormTakeOrderInWork()
        {
            InitializeComponent();
        }

        private void FormTakeOrderInWork_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                RequestModel modelI = new RequestModel { InterfaceName = InterfacesName.IImplementerService, MethodName = MethodsName.GetList };
                var responseI = TSPClient<ImplementerViewModel>.SendRequest(modelI);
                if (responseI.Success)
                {
                    List<ImplementerViewModel> list = responseI.ResponseList;
                    if (list != null)
                    {
                        comboBoxImplementer.DisplayMember = "ImplementerFIO";
                        comboBoxImplementer.ValueMember = "Id";
                        comboBoxImplementer.DataSource = list;
                        comboBoxImplementer.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(responseI.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxImplementer.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                RequestModel model = new RequestModel
                {
                    InterfaceName = InterfacesName.IMainService,
                    MethodName = MethodsName.TakeOrderInWork,
                    Request = new OrderBindingModel
                    {
                        Id = id.Value,
                        ImplementerId = Convert.ToInt32(comboBoxImplementer.SelectedValue)
                    }
                };
                ResponseModel<OrderViewModel> response = TSPClient<OrderViewModel>.SendRequest(model);
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
