using AbstractShopService;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormProduct : Form
    {
        public int Id { set { id = value; } }

        private InterfacesName type = InterfacesName.IProductService;

        private int? id;

        private List<ProductComponentViewModel> productComponents;

        public FormProduct()
        {
            InitializeComponent();
        }

        private void FormProduct_Load(object sender, EventArgs e)
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
                    var response = TSPClient<ProductViewModel>.SendRequest(model);
                    if (response.Success)
                    {
                        textBoxName.Text = response.Response.ProductName;
                        textBoxPrice.Text = response.Response.Price.ToString();
                        productComponents = response.Response.ProductComponents;
                        LoadData();
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
            else
            {
                productComponents = new List<ProductComponentViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (productComponents != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = productComponents;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormProductComponent();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if(form.Model != null)
                {
                    if(id.HasValue)
                    {
                        form.Model.ProductId = id.Value;
                    }
                    productComponents.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new FormProductComponent();
                form.Model = productComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    productComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        productComponents.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (productComponents == null || productComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ResponseModel<ProductViewModel> response;
                List<ProductComponentBindingModel> productComponentBM = new List<ProductComponentBindingModel>();
                for (int i = 0; i < productComponents.Count; ++i)
                {
                    productComponentBM.Add(new ProductComponentBindingModel
                    {
                        Id = productComponents[i].Id,
                        ProductId = productComponents[i].ProductId,
                        ComponentId = productComponents[i].ComponentId,
                        Count = productComponents[i].Count
                    });
                }
                if (id.HasValue)
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.UpdElement,
                        Request = new ProductBindingModel
                        {
                            Id = id.Value,
                            ProductName = textBoxName.Text,
                            Price = Convert.ToInt32(textBoxPrice.Text),
                            ProductComponents = productComponentBM
                        }
                    };
                    response = TSPClient<ProductViewModel>.SendRequest(model);
                }
                else
                {
                    RequestModel model = new RequestModel
                    {
                        InterfaceName = type,
                        MethodName = MethodsName.AddElement,
                        Request = new ProductBindingModel
                        {
                            ProductName = textBoxName.Text,
                            Price = Convert.ToInt32(textBoxPrice.Text),
                            ProductComponents = productComponentBM
                        }
                    };
                    response = TSPClient<ProductViewModel>.SendRequest(model);
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
