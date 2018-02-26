using AbstractShopService;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormClientOrders : Form
    {
        private InterfacesName type = InterfacesName.IReportService;

        public FormClientOrders()
        {
            InitializeComponent();
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                                            "c " + dateTimePickerFrom.Value.ToShortDateString() +
                                            " по " + dateTimePickerTo.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);

                RequestModel model = new RequestModel
                {
                    InterfaceName = type,
                    MethodName = MethodsName.GetClientOrders,
                    Request = new ReportBindingModel
                    {
                        DateFrom = dateTimePickerFrom.Value,
                        DateTo = dateTimePickerTo.Value
                    }
                };
                var response = TSPClient<ClientOrdersModel>.SendRequest(model);
                if (response.Success)
                {
                    var dataSource = response.ResponseList;
                    ReportDataSource source = new ReportDataSource("DataSetOrders", dataSource);
                    reportViewer.LocalReport.DataSources.Add(source);
                }
                else
                {
                    throw new Exception(response.ErrorMessage);
                }

                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonToPdf_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.SaveClientOrders,
                        Request = new ReportBindingModel
                        {
                            FileName = sfd.FileName,
                            DateFrom = dateTimePickerFrom.Value,
                            DateTo = dateTimePickerTo.Value
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
