using AbstractShopService;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormPutOnStock : Form
    {
        public FormPutOnStock()
        {
            InitializeComponent();
        }

        private void FormPutOnStock_Load(object sender, EventArgs e)
        {
            try
            {
                RequestModel modelC = new RequestModel { InterfaceName = InterfacesName.IComponentService, MethodName = MethodsName.GetList };
                var responseC = TSPClient<ComponentViewModel>.SendRequest(modelC);
                if (responseC.Success)
                {
                    List<ComponentViewModel> list = responseC.ResponseList;
                    if (list != null)
                    {
                        comboBoxComponent.DisplayMember = "ComponentName";
                        comboBoxComponent.ValueMember = "Id";
                        comboBoxComponent.DataSource = list;
                        comboBoxComponent.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(responseC.ErrorMessage);
                }
                RequestModel modelS = new RequestModel { InterfaceName = InterfacesName.IStockService, MethodName = MethodsName.GetList };
                var responseS = TSPClient<StockViewModel>.SendRequest(modelS);
                if (responseS.Success)
                {
                    List<StockViewModel> list = responseS.ResponseList;
                    if (list != null)
                    {
                        comboBoxStock.DisplayMember = "StockName";
                        comboBoxStock.ValueMember = "Id";
                        comboBoxStock.DataSource = list;
                        comboBoxStock.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(responseS.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStock.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                RequestModel model = new RequestModel
                {
                    InterfaceName = InterfacesName.IMainService,
                    MethodName = MethodsName.PutComponentOnStock,
                    Request = new StockComponentBindingModel
                    {
                        ComponentId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                        StockId = Convert.ToInt32(comboBoxStock.SelectedValue),
                        Count = Convert.ToInt32(textBoxCount.Text)
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
