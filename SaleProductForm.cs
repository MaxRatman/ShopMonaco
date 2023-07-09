using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopMonaco
{
    public partial class SaleProductForm : Form
    {
        DataGridView dataGridView;
        Label labelCodProduct;
        TextBox textBoxCodProduct;
        Label labelAmountProduct;
        TextBox textBoxAmountProduct;
        Label SumProductsInt;
        Button buttonAddProduct;
        Button buttonRemoveProduct;
        Button buttonCancel;
        Button buttonOk;
        DataBaseContext dataBaseContext1;
        PictureBox pictureBox;
        BindingList<Sales>salesNow;
        double SumProducts;
        int listBoxIndex;
        public SaleProductForm(DataBaseContext dataBaseContext,Form1 form1)
        {
            InitializeComponent();
            dataBaseContext1= dataBaseContext;
            this.Load += SaleProductForm_Load;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Size = new Size(850,900);
            this.MdiParent = form1;
        }

        private void SaleProductForm_Load(object? sender, EventArgs e)
        {
            salesNow = new BindingList<Sales>();
            listBoxIndex = 0;
            SumProducts = 0;
            dataGridView=new DataGridView();
            dataGridView.Size = new Size(600, 800);
            dataGridView.Font = new Font("", 10);
            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.AllowUserToAddRows = true;
            dataGridView.AllowUserToDeleteRows = true;

            labelAmountProduct = new Label();
            labelAmountProduct.Text = "Количество продукта";
            labelAmountProduct.Location = new Point(700, 0);

            textBoxAmountProduct = new TextBox();
            textBoxAmountProduct.Location = new Point(700, 30);

            labelCodProduct = new Label();
            labelCodProduct.Text = "Код продукта";
            labelCodProduct.Location = new Point(700,60);
            textBoxCodProduct=new TextBox();
            textBoxCodProduct.Location = new Point(700, 90);
 
            buttonAddProduct = new Button();
            buttonAddProduct.Click += ButtonAddProduct_Click;
            buttonAddProduct.Location = new Point(700, 130);
            buttonAddProduct.Size = new Size(100, 50);
            buttonAddProduct.Text = "Добавить продукт";

            buttonRemoveProduct = new Button();
            buttonRemoveProduct.Location = new Point(700, 190);
            buttonRemoveProduct.Size = new Size(100, 50);
            buttonRemoveProduct.Click += ButtonRemoveProduct_Click;
            buttonRemoveProduct.Text = "Удалить продукт";

            buttonCancel =new Button();
            buttonCancel.Dock = DockStyle.Bottom;
            buttonCancel.Text = "Отмена";
            buttonOk=new Button();
            buttonOk.Dock = DockStyle.Bottom;
            buttonOk.Text = "Готово";
            buttonOk.Click += ButtonOk_Click;

            SumProductsInt = new Label();
            SumProductsInt.Text = "";
            SumProductsInt.Location = new Point(650, 250);
            SumProductsInt.Size = new Size(150, 50);
            SumProductsInt.Font = new Font("", 14);

            pictureBox = new PictureBox();
            pictureBox.Size = new Size(150, 150);
            pictureBox.Location = new Point(620, 400);
            pictureBox.Image = Properties.Resources.Monaco_150x150jpg;

            Controls.AddRange(new Control[] {dataGridView,labelCodProduct,textBoxCodProduct, labelAmountProduct,textBoxAmountProduct,buttonOk,buttonCancel,buttonAddProduct,buttonRemoveProduct,SumProductsInt,pictureBox});
        }

        private void ButtonOk_Click(object? sender, EventArgs e)
        {
            if (salesNow.Count != 0)
            {
                for (int i = 0; i < salesNow.Count; i++)
                {
                    dataBaseContext1.Sales.Local.Add(salesNow[i]);
                }
                for(int i = 0;i < salesNow.Count; i++)
                {
                    
                }
            }
            dataBaseContext1.SaveChanges();
            dataBaseContext1.Sales.Reverse();
        }

        private void ButtonRemoveProduct_Click(object? sender, EventArgs e)
        {

        }

        private void ButtonAddProduct_Click(object? sender, EventArgs e)
        {
            SumProductsInt.Text = null;
            for(int i=0;i<dataBaseContext1.Products.ToList().Count();i++)
            {
                if (int.Parse(textBoxCodProduct.Text.ToString()) == dataBaseContext1.Products.ToList()[i].Id)
                {
                    for (int j = 0; j < dataBaseContext1.productInWarehouses.ToList().Count(); j++)
                    {
                        if (dataBaseContext1.Products.ToList()[i].Id == dataBaseContext1.productInWarehouses.ToList()[j].ProductId)
                        {
                            if (dataBaseContext1.productInWarehouses.ToList()[j].Amount !<int.Parse(textBoxAmountProduct.Text))
                            {
                                salesNow.Add(new Sales()
                                {
                                    Amount = int.Parse(textBoxAmountProduct.Text),
                                    NameProduct = dataBaseContext1.Products.ToList()[i].Name,
                                    dateTime = DateAndTime.Now,
                                    Price = dataBaseContext1.Products.ToList()[i].PriceForSell * int.Parse(textBoxAmountProduct.Text)
                                });
                                dataGridView.DataSource=salesNow;
                                dataGridView.Update();
                                //dataGridView.Columns[dataGridView.Columns.Count-1].=(dataBaseContext1.Products.ToList()[i].Name + " | " +
                                //int.Parse(textBoxAmountProduct.Text + " | "+
                                //dataBaseContext1.Products.ToList()[i].PriceForSell*int.Parse(textBoxAmountProduct.Text)));
                                //SumProducts += dataBaseContext1.Products.ToList()[i].PriceForSell * int.Parse(textBoxAmountProduct.Text);
                            }
                            else
                            {
                                listBoxIndex++;
                                //dataGridView.Items.Add($"Имя:"+dataBaseContext1.Products.ToList()[i].Name 
                                //    +$" | "+ $"Код:"+int.Parse(textBoxCodProduct.Text)+$" | " + $"Цена:"+
                                //dataBaseContext1.Products.ToList()[i].PriceForSell *int.Parse(textBoxAmountProduct.Text)+$" | "+$"Номер: "+ listBoxIndex.ToString());
                                //SumProducts += dataBaseContext1.Products.ToList()[i].PriceForSell * int.Parse(textBoxAmountProduct.Text);
                                salesNow.Add(new Sales()
                                {
                                    Amount = int.Parse(textBoxAmountProduct.Text),
                                    NameProduct = dataBaseContext1.Products.ToList()[i].Name,
                                    dateTime = DateAndTime.Now,
                                    Price = dataBaseContext1.Products.ToList()[i].PriceForSell * int.Parse(textBoxAmountProduct.Text)
                                });
                                salesNow.Reverse<Sales>();
                                dataGridView.DataSource = salesNow;
                                dataGridView.Update();
                            }
                        }
                    }
                }
            }
            SumProductsInt.Text += SumProducts.ToString();
        }
    }
}
