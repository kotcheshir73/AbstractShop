using AbstractShopService.BindingModels;
using AbstractShopService.Interfaces;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace AbstractShopView
{
    public partial class FormPutOnStock : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStockService serviceS;

        private readonly IComponentService serviceC;

        private readonly IMainService serviceM;

        public FormPutOnStock(IStockService serviceS, IComponentService serviceC, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
        }

        private void FormPutOnStock_Load(object sender, EventArgs e)
        {
            try
            {
                List<ComponentViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxComponent.DisplayMember = "ComponentName";
                    comboBoxComponent.ValueMember = "Id";
                    comboBoxComponent.DataSource = listC;
                    comboBoxComponent.SelectedItem = null;
                }
                List<StockViewModel> listS = serviceS.GetList();
                if (listS != null)
                {
                    comboBoxStock.DisplayMember = "StockName";
                    comboBoxStock.ValueMember = "Id";
                    comboBoxStock.DataSource = listS;
                    comboBoxStock.SelectedItem = null;
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
                serviceM.PutComponentOnStock(new StockComponentBindingModel
                {
                    ComponentId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                    StockId = Convert.ToInt32(comboBoxStock.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
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
