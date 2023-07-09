using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace ShopMonaco
{
    public enum FormChoice { Products,Employees,Sales,ProductInWarehouse,ReportForDay, ReportForMonth, ReportForYear}
    public partial class Form1 : Form
    {
        Panel panelEnterEmployees;
        CaFormForTable caFormForTable;
        SaleProductForm saleProductForm;
        FormChoiceReport formChoiceReport;
        DataBaseContext dataBaseContext;
        ToolStrip toolStrip;
        ToolStripButton toolStripDropDownMenuWorkWithProducts;
        ToolStripButton toolStripButtonSales;
        ToolStripButton toolStripButtonTableEmployees;
        ToolStripButton toolStripButtonTableProductInWarehouse;
        ToolStripSeparator ToolStripSeparator;
        ToolStripButton toolStripButtonSaleProduct;
        ToolStripButton toolStripButtonReportSales;
        PictureBox pictureBoxMonacoTextPanelEnterEmployees;
        Label labelNameEnterEmployees;
        TextBox textBoxNameEnterEmployees;
        Label labelSurnameEnterEmployees;
        TextBox textBoxSurnameEnterEmployees;
        Button buttonEnterEmployees;
        DateOnly dateOnlyFromReportSales;

        FormChoice formChoice;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Monaco";
            this.Load += Form1_Load; 
            this.IsMdiContainer = true;
            this.WindowState= FormWindowState.Maximized;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                dateOnlyFromReportSales = new DateOnly();

                dataBaseContext = new DataBaseContext();
                dataBaseContext.SaveChanges();

                labelNameEnterEmployees = new Label();
                labelNameEnterEmployees.Text = "Имя:";
                labelNameEnterEmployees.Size = new Size(50, 30);
                labelNameEnterEmployees.Font = new Font("", 10);
                labelNameEnterEmployees.Location = new Point(750, 700);

                textBoxNameEnterEmployees = new TextBox();
                textBoxNameEnterEmployees.Location = new Point(800, 700);

                labelSurnameEnterEmployees =new Label();
                labelSurnameEnterEmployees.Text = "Фамилия:";
                labelSurnameEnterEmployees.Location = new Point(900, 700);
                labelSurnameEnterEmployees.Size = new Size(75, 30);
                labelSurnameEnterEmployees.Font = new Font("", 10);

                textBoxSurnameEnterEmployees=new TextBox();
                textBoxSurnameEnterEmployees.Location = new Point(975, 700);

                buttonEnterEmployees = new Button();
                buttonEnterEmployees.Text = "Войти";
                buttonEnterEmployees.Size = new Size(300,30);
                buttonEnterEmployees.FlatStyle = FlatStyle.Popup;
                buttonEnterEmployees.Font = new Font("", 10);
                buttonEnterEmployees.Location = new Point(770, 740);
                buttonEnterEmployees.Click += ButtonEnterEmployees_Click;

                pictureBoxMonacoTextPanelEnterEmployees = new PictureBox();
                pictureBoxMonacoTextPanelEnterEmployees.Image = Properties.Resources.Monaco_design_amped_name;
                pictureBoxMonacoTextPanelEnterEmployees.Size = new Size(725, 350);
                pictureBoxMonacoTextPanelEnterEmployees.Location = new Point(400, 100);

                panelEnterEmployees = new Panel();
                panelEnterEmployees.Dock = DockStyle.Fill;
                panelEnterEmployees.BackColor = Color.White;
                panelEnterEmployees.Controls.AddRange(new Control[] { labelNameEnterEmployees,textBoxNameEnterEmployees,labelSurnameEnterEmployees,textBoxSurnameEnterEmployees,buttonEnterEmployees,pictureBoxMonacoTextPanelEnterEmployees });
                Controls.Add(panelEnterEmployees);

                saleProductForm = new SaleProductForm(dataBaseContext, this);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonEnterEmployees_Click(object? sender, EventArgs e)
        {
            for(int  i = 0;i<dataBaseContext.Employees.Count();i++)
            {
                if (dataBaseContext.Employees.ToList()[i].EmployeeName == textBoxNameEnterEmployees.Text && dataBaseContext.Employees.ToList()[i].EmployeeSurname==textBoxSurnameEnterEmployees.Text)
                {
                    panelEnterEmployees.Visible = false;
                    CreateToolStripMenu();
                    break;
                }
                else
                {
                    MessageBox.Show("Имя или Фамилия введены не правильно!");
                }
            }
        }

        void CreateToolStripMenu()
        {
             toolStripDropDownMenuWorkWithProducts = new ToolStripButton("Товары",null,WorkWithProducts_Click);
            toolStripButtonSales = new ToolStripButton("Продажи", null, SalesButton_Click);
            toolStripButtonTableEmployees = new ToolStripButton("Работники",null, EmployeesButton_Click);
            toolStripButtonTableProductInWarehouse = 
                new ToolStripButton("Продукты на складе", null, ProductInWarehouse_Click);
            ToolStripSeparator = new ToolStripSeparator();   
            toolStripButtonSaleProduct = new ToolStripButton("Продать", null, SaleProductButton_Click);
            toolStripButtonReportSales = new ToolStripButton("Отчёт", null, ReportSalesButton_Click);
            toolStrip = new ToolStrip(new ToolStripItem[] { toolStripDropDownMenuWorkWithProducts,
                toolStripButtonSales, toolStripButtonTableEmployees,toolStripButtonTableProductInWarehouse,ToolStripSeparator, toolStripButtonSaleProduct,toolStripButtonReportSales});
            Controls.Add(toolStrip);
        }
        private void WorkWithProducts_Click(object sender, EventArgs e) 
        {
            formChoice = FormChoice.Products;
            caFormForTable = new CaFormForTable(dataBaseContext,formChoice,this);
            caFormForTable.Show();
        }
        private void ReportSalesButton_Click(object sender, EventArgs e)
        {
            formChoiceReport = new FormChoiceReport(formChoice);
            formChoiceReport.ShowDialog();
        }
        private void SalesButton_Click(object sender, EventArgs e) 
        {
            formChoice = FormChoice.Sales;
            caFormForTable = new CaFormForTable(dataBaseContext, formChoice, this);
            caFormForTable.Show();
        }
        private void EmployeesButton_Click(object sender,EventArgs e)
        {
            formChoice = FormChoice.Employees;
            caFormForTable = new CaFormForTable(dataBaseContext, formChoice, this);
            caFormForTable.Show();
        }
        private void ProductInWarehouse_Click(object sender,EventArgs e)
        {
            formChoice = FormChoice.ProductInWarehouse;
            caFormForTable = new CaFormForTable(dataBaseContext, formChoice, this);
            caFormForTable.Show();
        }
        private void SaleProductButton_Click(object sender,EventArgs e)
        {
            saleProductForm.Show();
        }
    }
}