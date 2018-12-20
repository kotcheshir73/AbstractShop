using AbstractShopServiceDAL.BindingModels;
using AbstractShopServiceDAL.ViewModels;
using System;
using System.Windows.Forms;

namespace AbstractShopView
{
    public partial class FormStock : Form
    {
        public int Id { set { id = value; } }

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
                    StockViewModel view = APIClient.GetRequest<StockViewModel>("api/Stock/Get/" + id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.StockName;
                        dataGridView.DataSource = view.StockComponents;
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
                if (id.HasValue)
                {
                    APIClient.PostRequest<StockBindingModel, bool>("api/Stock/UpdElement", new StockBindingModel
                    {
                        Id = id.Value,
                        StockName = textBoxName.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<StockBindingModel, bool>("api/Stock/AddElement", new StockBindingModel
                    {
                        StockName = textBoxName.Text
                    });
                }
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