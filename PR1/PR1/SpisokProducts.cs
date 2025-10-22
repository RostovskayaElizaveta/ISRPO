using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PR1
{
    public partial class SpisokProducts : Form
    {
        private List<Product> selectedProducts = new List<Product>();
        private DatabaseHelper dbHelper = new DatabaseHelper();

        public SpisokProducts()
        {
            InitializeComponent();
            loadProducts();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxProducts.SelectedItem != null)
            {
                Product selectedProduct = (Product)comboBoxProducts.SelectedItem;
                selectedProducts.Add(selectedProduct);
                listBoxSelected.Items.Add(selectedProduct.ToString());
            }
        }
        private void loadProducts()
        {
            try
            {
                var products = dbHelper.GetProducts();
                comboBoxProducts.DataSource = products;
                comboBoxProducts.DisplayMember = "Name";
                comboBoxProducts.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки продуктов: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            selectedProducts.Clear();
            listBoxSelected.Items.Clear();
            textBoxTotal.Clear();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            decimal total = selectedProducts.Sum(p => p.Price);
            textBoxTotal.Text = total.ToString("C");
        }
    }
}
