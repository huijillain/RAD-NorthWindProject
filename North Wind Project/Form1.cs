using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace North_Wind_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // save function
        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.productsBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.northwndDataSet);
            }
            catch (DBConcurrencyException)
            {
                MessageBox.Show("A concurrency error occurred. " + "Some rows were not updated.", "Concurrency Exception");
                this.productsTableAdapter.Fill(this.northwndDataSet.Products);
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString()); productsBindingSource.CancelEdit();
            }
        }

        // when we open the form, this function will fill up date showing on form page
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwndDataSet.Suppliers' table. You can move, or remove it, as needed.
            this.suppliersTableAdapter.Fill(this.northwndDataSet.Suppliers);
            // TODO: This line of code loads data into the 'northwndDataSet.Order_Details' table. You can move, or remove it, as needed.
            this.order_DetailsTableAdapter.Fill(this.northwndDataSet.Order_Details);
            // TODO: This line of code loads data into the 'northwndDataSet.Shippers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.northwndDataSet.Customers);
            // TODO: This line of code loads data into the 'northwndDataSet.Categories' table. You can move, or remove it, as needed.
            this.categoriesTableAdapter.Fill(this.northwndDataSet.Categories);
            // TODO: This line of code loads data into the 'northwndDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.northwndDataSet.Products);

            // codes catches a SQL EXCEPTION
            try { this.productsTableAdapter.Fill(this.northwndDataSet.Products); }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number + ": " + ex.Message, ex.GetType().ToString());

            }
        }

        private void ordersDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            int row = e.RowIndex + 1; string errorMessage = "A data error occurred.\n" + "Row: " + 
                row + "\n" + "Error: " + e.Exception.Message; MessageBox.Show(errorMessage, "Data Error");
        }
    }
}
