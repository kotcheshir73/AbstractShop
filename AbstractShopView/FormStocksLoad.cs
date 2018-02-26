using AbstractShopService;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormStocksLoad : Form
    {
        private InterfacesName type = InterfacesName.IReportService;

        public FormStocksLoad()
        {
            InitializeComponent();
        }

        private void FormStocksLoad_Load(object sender, EventArgs e)
        {
            try
            {
                RequestModel model = new RequestModel
                {
                    InterfaceName = type,
                    MethodName = MethodsName.GetStocksLoad
                };
                var response = TSPClient<StocksLoadViewModel>.SendRequest(model);
                if (response.Success)
                {
                    dataGridView.Rows.Clear();
                    foreach (var elem in response.ResponseList)
                    {
                        dataGridView.Rows.Add(new object[] { elem.StockName, "", "" });
                        foreach (var listElem in elem.Components)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.ComponentName, listElem.Count });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalCount });
                        dataGridView.Rows.Add(new object[] { });
                    }
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

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.SaveStocksLoad,
                        Request = new ReportBindingModel
                        {
                            FileName = sfd.FileName
                        }
                    };
                    var response = TSPClient<OrderViewModel>.SendRequest(model);
                    if (response.Success)
                    {
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
