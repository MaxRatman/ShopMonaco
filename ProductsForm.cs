using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopMonaco
{
    public partial class CaFormForTable : Form
    {
        DataGridView dataGridView;
        DataBaseContext dataBaseContext;
        Button buttonClearTable;
        FormChoice fromChoice;
        public CaFormForTable(DataBaseContext dataBaseContext1, FormChoice fromChoice, Form1 form1)
        {
            InitializeComponent();
            this.Load += CaFormForTable_Load;
            this.MdiParent = form1;
            this.dataBaseContext = dataBaseContext1;
            this.fromChoice = fromChoice;
            this.FormClosed += CaFormForTable_FormClosed;
        }

        private void CaFormForTable_FormClosed(object? sender, FormClosedEventArgs e)
        {
            try
            {
                dataBaseContext.SaveChanges();
            }
            catch (Exception ex) { Debug.WriteLine(ex.ToString()); }
        }

        private void CaFormForTable_Load(object? sender, EventArgs e)
        {
            buttonClearTable = new Button();
            buttonClearTable.Dock = DockStyle.Right;
            buttonClearTable.Text = "О\rч\rи\rс\rт\rи\rт\rь";
            buttonClearTable.Font = new Font("", 12);
            buttonClearTable.Click += ButtonClearTable_Click;

            dataBaseContext = new DataBaseContext();
            dataGridView = new DataGridView();
            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.AllowUserToAddRows = true;
            dataGridView.AllowUserToDeleteRows = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.DataError += (s, e) => MessageBox.Show(e.ToString());
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
            dataGridViewCellStyle.Font = new Font("", 16);
            dataGridView.DefaultCellStyle = dataGridViewCellStyle;
            switch (fromChoice)
            {
                case FormChoice.Products:
                    dataBaseContext.Products.Load();
                    dataGridView.DataSource = dataBaseContext.Products.Local.ToBindingList();
                    this.Text = "Продукты";
                    break;
                case FormChoice.Employees:
                    dataBaseContext.Employees.Load();
                    dataGridView.DataSource = dataBaseContext.Employees.Local.ToBindingList();
                    this.Text = "Работники";
                    break;
                case FormChoice.Sales:
                    dataBaseContext.Sales.Load();
                    dataGridView.DataSource = dataBaseContext.Sales.Local.ToBindingList();
                    this.Text = "Продажи";
                    break;
                case FormChoice.ProductInWarehouse:
                    dataBaseContext.productInWarehouses.Load();
                    dataGridView.DataSource = dataBaseContext.productInWarehouses.Local.ToBindingList();
                    this.Text = "Продукты на складе";
                    break;
            }
            Controls.AddRange(new Control[] { buttonClearTable, dataGridView });
        }

        private void ButtonClearTable_Click(object? sender, EventArgs e)
        {
            dataGridView.ClearSelection();
            switch (fromChoice)
            {
                case FormChoice.Products:
                    dataBaseContext.Products.Local.Clear();
                    break;
                case FormChoice.Employees:
                    dataBaseContext.Employees.Local.Clear();
                    break;
                case FormChoice.Sales:
                    dataBaseContext.Employees.Local.Clear();
                    break;
                case FormChoice.ProductInWarehouse:
                    dataBaseContext.productInWarehouses.Local.Clear();
                    break;
            }
            dataBaseContext.SaveChanges();
            dataGridView.Update();
        }
    }
}
