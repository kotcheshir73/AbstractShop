using AbstractShopService;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormStock : Form
    {
        public int Id { set { id = value; } }

        private InterfacesName type = InterfacesName.IStockService;

        private int? id;

        public FormStock()
        {
            InitializeComponent();
        }

        private void FormStock_Load(object sender, EventArgs e)
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
                    var response = TSPClient<StockViewModel>.SendRequest(model);
                    if (response.Success)
                    {
                        textBoxName.Text = response.Response.StockName;
                        dataGridView.DataSource = response.Response.StockComponents;
                        dataGridView.Columns[0].Visible = false;
                        dataGridView.Columns[1].Visible = false;
                        dataGridView.Columns[2].Visible = false;
                        dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                ResponseModel<StockViewModel> response;
                if (id.HasValue)
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.UpdElement,
                        Request = new StockBindingModel
                        {
                            Id = id.Value,
                            StockName = textBoxName.Text
                        }
                    };
                    response = TSPClient<StockViewModel>.SendRequest(model);
                }
                else
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.AddElement,
                        Request = new StockBindingModel
                        {
                            StockName = textBoxName.Text
                        }
                    };
                    response = TSPClient<StockViewModel>.SendRequest(model);
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
