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
    public partial class FormChoiceReport : Form
    {
        Button buttonReportDay;
        Button buttonReportMonth;
        Button buttonReportYear;
        Button buttonCancel;
        Button buttonEnterDate;
        Panel panelChoiceDate;
        public FormChoice fromChoice;
        DateTime dateOnly1;
        DateTimePicker dateTimePicker;
        public FormChoiceReport(FormChoice fromChoice1)
        {
            InitializeComponent();
            dateOnly1 = new DateTime();
            this.Size = new Size(500, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Load += FormChoiceReport_Load;
            this.fromChoice = fromChoice1;
        }

        private void FormChoiceReport_Load(object? sender, EventArgs e)
        {
            dateTimePicker = new DateTimePicker();
            dateTimePicker.Visible= false;
            dateTimePicker.Dock = DockStyle.Top;
            dateTimePicker.MaxDate = DateTime.Today;

            panelChoiceDate = new Panel();
            panelChoiceDate.Dock = DockStyle.Fill;
            panelChoiceDate.BackColor = Color.Gray;

            buttonReportDay = new Button();
            buttonReportDay.Click += ButtonReportAll_Click;
            buttonReportDay.Name = "buttonReportDay";
            buttonReportDay.Text = "День";
            buttonReportDay.Dock = DockStyle.Bottom;

            buttonReportMonth = new Button();
            buttonReportMonth.Click += ButtonReportAll_Click;
            buttonReportMonth.Name = "buttonReportMonth";
            buttonReportMonth.Text = "Месяц";
            buttonReportMonth.Dock = DockStyle.Bottom;

            buttonReportYear = new Button();
            buttonReportYear.Click += ButtonReportAll_Click;
            buttonReportYear.Name = "buttonReportYear";
            buttonReportYear.Text = "Год";
            buttonReportYear.Dock = DockStyle.Bottom;

            buttonEnterDate = new Button();
            buttonEnterDate.Dock = DockStyle.Top;
            buttonEnterDate.Text = "Подтвердить";
            buttonEnterDate.Visible = false;
            buttonEnterDate.Click += ButtonEnterDate_Click;

            buttonCancel = new Button();
            buttonCancel.Click += ButtonCancel_Click;
            this.Controls.AddRange(new Control[] {dateTimePicker,buttonEnterDate, panelChoiceDate, buttonReportDay,buttonReportMonth,buttonReportYear ,buttonCancel});
        }

        private void ButtonEnterDate_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
        private void ButtonReportAll_Click(object? sender, EventArgs e)
        {
            switch((sender as Button).Name)
            {
                case "buttonReportDay":
                    fromChoice = FormChoice.ReportForDay;
                    dateOnly1 = dateTimePicker.Value;
                    break;
                case "buttonReportMonth":
                    fromChoice = FormChoice.ReportForMonth;
                    dateOnly1 = dateTimePicker.Value;
                    break;
                case "buttonReportYear":
                    fromChoice = FormChoice.ReportForYear;
                    dateOnly1 = dateTimePicker.Value;
                    break;
            }
            dateTimePicker.Visible = true;
            buttonEnterDate.Visible=true;
        }

        private void ButtonCancel_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
