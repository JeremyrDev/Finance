using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using System.Net;
using System.Globalization;
namespace Finance
{
    public partial class Form1 : Form
    {
        string sy;
        string buildString;
        string information;
        string temp;
        int counter = 0;
        int item = 0;
        int panelPosition = 0;
        XmlReader reader;

        TextBox[] textbox;

        Label[] name;
        Label[] symbol;
        Label[] price;
        Label[] change;

        Panel[] panel;

        string[] names;
        string[] symbols;
        string[] prices;
        string[] changes;

        public Form1()
        {
            InitializeComponent();
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
            sy = "f";
            textbox = new TextBox[500];

            name = new Label[500];
            symbol = new Label[500];
            price = new Label[500];
            change = new Label[500];

            panel = new Panel[500];

            names = new String[500];
            symbols = new String[500];
            changes = new String[500];
            prices = new String[500];
        }
        public void showData()
        {
            reader.Close();
            reader.Dispose();
            if (sy == "")
            {
                //label1.Text = "Enter a value to start";
            }
            else
            {
                information = "";
                using (reader = XmlReader.Create("http://dev.markitondemand.com/Api/v2/Quote/xmlp?symbol=" + sy))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name.ToString())
                            {
                                case "Name":
                                    temp = reader.ReadString();
                                    information += temp + Environment.NewLine;
                                    names[item] += temp;
                                    break;
                                case "Symbol":
                                    temp = reader.ReadString();
                                    information += temp + Environment.NewLine;
                                    symbols[item] += temp;
                                    break;
                                case "LastPrice":
                                    temp = reader.ReadString();
                                    information += temp + Environment.NewLine;
                                    prices[item] += temp;
                                    break;
                                case "High":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                                case "Low":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                                case "Change":
                                    temp = reader.ReadString();
                                    information += temp + Environment.NewLine;
                                    changes[item] += temp + " : ";
                                    break;
                                case "ChangePercent":
                                    temp = reader.ReadString();
                                    information += temp + Environment.NewLine;
                                    changes[item] += temp + "% ";
                                    break;
                                case "Timestamp":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                                case "MarketCap":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                                case "Volume":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                                case "Open":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                            }
                        }
                    }
                }

                addItem();
            }
        }
        void addItem()
        {
            panel[item] = new Panel();

            name[item] = new Label();
            symbol[item] = new Label();
            price[item] = new Label();
            change[item] = new Label();
            //panel[i].Click += PanelClickHandler;
            //panel[i].LostFocus += panelLostFocusHandler;

            panel[item].BackColor = Color.FromArgb(60, 60, 60);
            panel[item].BackgroundImage = new Bitmap(@"e:\users\jeremy-ssdos\documents\visual studio 2013\Projects\Finance\Finance\bg.bmp");
            panel[item].Dock = DockStyle.Top;
            panel[item].Name = "Panel" + item;

            name[item].Location = new Point(15, 13);
            name[item].Text = "test";
            name[item].Font = new Font("Arial", 10);
            name[item].AutoSize = true;
            name[item].ForeColor = Color.White;
            name[item].BackColor = Color.FromArgb(80, 80, 80);
            name[item].Text = names[item];

            symbol[item].Location = new Point(205, 13);
            symbol[item].Text = "test";
            symbol[item].Font = new Font("Arial", 10);
            symbol[item].AutoSize = true;
            symbol[item].ForeColor = Color.White;
            symbol[item].BackColor = Color.FromArgb(80, 80, 80);
            symbol[item].Text = symbols[item];

            price[item].Location = new Point(305, 13);
            price[item].Text = "test";
            price[item].Font = new Font("Arial", 10);
            price[item].AutoSize = true;
            price[item].ForeColor = Color.White;
            price[item].BackColor = Color.FromArgb(80, 80, 80);
            price[item].Text = prices[item];

            change[item].Location = new Point(405, 13);
            change[item].Text = "test";
            change[item].Font = new Font("Arial", 10);
            change[item].AutoSize = true;
            change[item].ForeColor = Color.White;
            change[item].BackColor = Color.FromArgb(80, 80, 80);
            change[item].Text = changes[item];

            panel[item].Width = panel2.Width - 10;
            panel[item].Height = 45;

            panel[item].Controls.Add(name[item]);
            panel[item].Controls.Add(symbol[item]);
            panel[item].Controls.Add(price[item]);
            panel[item].Controls.Add(change[item]);

            panel2.Controls.Add(panel[item]);
            panelPosition += 45;

            textbox[item] = new TextBox();
            textbox[item].Click += textBoxClickHandler;
            textbox[item].LostFocus += textBoxLostFocusHandler;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buildString = "";
            listBox1.Items.Clear();
            listBox1.Height = 0;
            listBox1.Visible = true;
            string s = textBox1.Text;

            try
            {
                using (reader = XmlReader.Create("http://dev.markitondemand.com/Api/v2/Lookup/xmlp?input=" + s + "&callback=myFunction"))
                {
                    while (reader.Read())
                    {

                        if (reader.IsStartElement())
                        {
                            switch (reader.Name.ToString())
                            {
                                case "Symbol":
                                    buildString += reader.ReadString();
                                    listBox1.Items.Add(buildString);
                                    buildString = "";
                                    listBox1.Height += 13;
                                    break;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void ListBox1_Click(object sender, EventArgs e)
        {
            sy = listBox1.SelectedItem.ToString();
            textBox1.Text = sy;
            listBox1.Visible = false;
            showData();
            item++;
        }
        protected void textBoxClickHandler(object sender, EventArgs e)
        {
            TextBox valueBox = sender as TextBox;
            valueBox.BackColor = Color.White;
            valueBox.Text = valueBox.Text.ToString();
        }
        protected void textBoxLostFocusHandler(object sender, EventArgs e)
        {
            TextBox valueBox = sender as TextBox;
            valueBox.BorderStyle = BorderStyle.None;
            //valueBox.BackColor = Color.FromArgb(203, 222, 231);
            valueBox.BackColor = Color.White;
            int tempInt = Convert.ToInt32(valueBox.Text);
            valueBox.Text = tempInt.ToString("n0");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            

            counter++;

            information = "";
            for (int i = 0; i < item; i++)
            {
                names[i] = "";
                symbols[i] = "";
                changes[i] = "";
                prices[i] = "";
                using (reader = XmlReader.Create("http://dev.markitondemand.com/Api/v2/Quote/xmlp?symbol=" + sy))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name.ToString())
                            {
                                case "Name":
                                    temp = reader.ReadString();
                                    information += temp + Environment.NewLine;
                                    names[i] += temp;
                                    name[i].Text = counter + "  " + names[i];
                                    break;
                                case "Symbol":
                                    temp = reader.ReadString();
                                    information += temp + Environment.NewLine;
                                    symbols[i] += temp;
                                    symbol[i].Text = symbols[i];
                                    break;
                                case "LastPrice":
                                    temp = reader.ReadString();
                                    information += temp + Environment.NewLine;
                                    prices[i] += temp;
                                    price[i].Text = prices[i];
                                    break;
                                case "High":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                                case "Low":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                                case "Change":
                                    temp = reader.ReadString();
                                    information += temp + Environment.NewLine;
                                    changes[i] += temp + " : ";
                                    break;
                                case "ChangePercent":
                                    temp = reader.ReadString();
                                    information += temp + Environment.NewLine;
                                    changes[i] += temp + "% ";
                                    change[i].Text = changes[i];
                                    break;
                                case "Timestamp":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                                case "MarketCap":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                                case "Volume":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                                case "Open":
                                    information += reader.ReadString() + Environment.NewLine;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (item > 0)
            {
                if (checkBox1.Checked)
                    timer1.Enabled = true;
                else
                    timer1.Enabled = false;
            }
        }
    }
}
