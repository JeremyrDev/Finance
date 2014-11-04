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
using System.IO;
namespace Finance
{
    public partial class Form1 : Form
    {
        bool overButton = false;
        string sy;
        string path = @"C:\Users\Jeremy\Desktop\finance\";
        int counter = 0;
        int item = 0;
        int panelPosition = 0;
        int previousHeight = 0;
        //int[] deleted;

        TextBox[] sharesTextBox;
        TextBox[] priceTextBox;
        Button[] eButton;

        Label[] stockValue;
        Label[] name;
        Label[] symbol;
        Label[] price;
        Label[] change;

        Panel[] panel;

        string[] names;
        string[] symbols;
        string[] prices;
        string[] changes;

        Image bmp;

        public Form1()
        {
            InitializeComponent();
        }
      
        //on hovering over the panel, show a subtle X button all the way to the right in the middle of it, click it to delete the item
        private void Form1_Load(object sender, EventArgs e)
        {
            sy = "f";
            sharesTextBox = new TextBox[500];
            priceTextBox = new TextBox[500];
            eButton = new Button[500];

            stockValue = new Label[500];
            name = new Label[500];
            symbol = new Label[500];
            price = new Label[500];
            change = new Label[500];

            panel = new Panel[500];

            names = new String[500];
            symbols = new String[500];
            changes = new String[500];
            prices = new String[500];
            bmp = new Bitmap(Finance.Properties.Resources.bg);
            textBox1.Focus();
        }
        public void showData()
        {

            if (sy == "")
            {
                this.Text = "Enter a value to start";
            }
            else
            {
                try
                {
                    this.Text = "Finance";
                    using (WebClient Client = new WebClient())
                    {
                        Client.DownloadFile("http://dev.markitondemand.com/Api/v2/Quote/xmlp?symbol=" + sy, path + sy + ".xml");
                    }

                    XmlDocument doc = new XmlDocument();
                    doc.Load(path + sy + ".xml");

                    XmlNodeList prop = doc.SelectNodes("/StockQuote");

                    foreach (XmlNode n in prop)
                    {
                        names[item] = n.SelectSingleNode("Name").InnerText;
                        symbols[item] = n.SelectSingleNode("Symbol").InnerText;
                        prices[item] = n.SelectSingleNode("LastPrice").InnerText;
                        changes[item] = n.SelectSingleNode("Change").InnerText + " (" + float.Parse(n.SelectSingleNode("ChangePercent").InnerText, CultureInfo.CurrentCulture).ToString("f2") + "%)";
                        
                    }
                    float temp = float.Parse(prices[item])*100;
                    label1.Text = temp.ToString();
                }
                catch
                {
                    label1.Text = "Failed! Retry...";
                    return;
                }
                addItem();
                textBox1.Focus();
            }
        }

        void addItem()
        {
            panel[item] = new Panel();
            sharesTextBox[item] = new TextBox();
            priceTextBox[item] = new TextBox();
            eButton[item] = new Button();

            stockValue[item] = new Label();
            name[item] = new Label();
            symbol[item] = new Label();
            price[item] = new Label();
            change[item] = new Label();

            panel[item].BackColor = Color.FromArgb(60, 60, 60);
            panel[item].BackgroundImage = bmp;
            panel[item].Dock = DockStyle.Top;
            panel[item].Name = item.ToString();

            eButton[item].Location = new Point(panel3.Width-60, 13);
            eButton[item].Text = "X";
            eButton[item].Width = 30;
            eButton[item].FlatStyle = FlatStyle.Flat;
            eButton[item].Name = item.ToString();
            eButton[item].Visible = true;

            stockValue[item].Location = new Point(725, 13);
            stockValue[item].Font = new Font("Arial", 10);
            stockValue[item].AutoSize = true;
            stockValue[item].ForeColor = Color.White;
            stockValue[item].BackColor = Color.FromArgb(80, 80, 80);
            stockValue[item].Text = "+/- $0.00";

            name[item].Location = new Point(15, 13);
            name[item].Font = new Font("Arial", 10);
            name[item].AutoSize = true;
            name[item].ForeColor = Color.White;
            name[item].BackColor = Color.FromArgb(80, 80, 80);
            name[item].Text = names[item];

            symbol[item].Location = new Point(205, 13);
            symbol[item].Font = new Font("Arial", 10);
            symbol[item].AutoSize = true;
            symbol[item].ForeColor = Color.White;
            symbol[item].BackColor = Color.FromArgb(80, 80, 80);
            symbol[item].Text = symbols[item];

            price[item].Location = new Point(305, 13);
            price[item].Font = new Font("Arial", 10);
            price[item].AutoSize = true;
            price[item].ForeColor = Color.White;
            price[item].BackColor = Color.FromArgb(80, 80, 80);
            price[item].Text = prices[item];

            change[item].Location = new Point(405, 13);
            change[item].Font = new Font("Arial", 10);
            change[item].AutoSize = true;
            change[item].ForeColor = Color.White;
            change[item].BackColor = Color.FromArgb(80, 80, 80);    
            change[item].Text = changes[item];

            sharesTextBox[item].Location = new Point(575, 13);
            sharesTextBox[item].Width = 60;
            sharesTextBox[item].Text = "100";
            sharesTextBox[item].TextAlign = HorizontalAlignment.Center;
            sharesTextBox[item].Font = new Font("Arial", 12);
            sharesTextBox[item].BorderStyle = BorderStyle.None;
            //textbox[i].BackColor = Color.FromArgb(203, 222, 231);
            sharesTextBox[item].BackColor = Color.White;
            sharesTextBox[item].Name = item.ToString();

            priceTextBox[item].Location = new Point(645, 13);
            priceTextBox[item].Width = 60;
            priceTextBox[item].Text = prices[item];
            priceTextBox[item].TextAlign = HorizontalAlignment.Center;
            priceTextBox[item].Font = new Font("Arial", 12);
            priceTextBox[item].BorderStyle = BorderStyle.None;
            //textbox[i].BackColor = Color.FromArgb(203, 222, 231);
            priceTextBox[item].BackColor = Color.White;
            priceTextBox[item].Name = item.ToString();

            panel[item].Width = panel3.Width - 10;
            panel[item].Height = 45;

            panel[item].Controls.Add(stockValue[item]);
            panel[item].Controls.Add(name[item]);
            panel[item].Controls.Add(symbol[item]);
            panel[item].Controls.Add(price[item]);
            panel[item].Controls.Add(change[item]);
            panel[item].Controls.Add(eButton[item]);
            panel[item].Controls.Add(sharesTextBox[item]);
            panel[item].Controls.Add(priceTextBox[item]);

            panel3.Controls.Add(panel[item]);
            panelPosition += 45;

            panel[item].Click += PanelClickHandler;
            panel[item].MouseLeave += panelMouseLeaveHandler;
            panel[item].MouseHover += panelMouseHoverHandler;

            eButton[item].MouseEnter += eButtonMouseOverHandler;
            eButton[item].Click += eButtonClickHandler;
            sharesTextBox[item].Click += sharesTextBoxClickHandler;
            sharesTextBox[item].LostFocus += sharesTextBoxLostFocusHandler;
            sharesTextBox[item].TextChanged += sharesTextBoxTextChangedHandler;
            priceTextBox[item].TextChanged += priceTextBoxTextChangedHandler;

            calculatePrice(item, prices[item]);
        }
        public void calculatePrice(int n, string price)
        {
            try
            {
                float temp1 = float.Parse(priceTextBox[n].Text, CultureInfo.CurrentCulture);
                float temp2 = float.Parse(sharesTextBox[n].Text, CultureInfo.CurrentCulture);
                float temp3;
                bool test;
                test = float.TryParse(price, NumberStyles.Any, CultureInfo.InvariantCulture, out temp3);
                stockValue[n].Text = "$" + ((temp2 * temp3) - (temp1 * temp2)).ToString("n0");
            }
            catch(Exception e)
            {
                if (priceTextBox[n].Text == null || priceTextBox[n].Text == "")
                    label1.Text = "null";
                else
                {
                    label1.Text = "Not null";
                    MessageBox.Show(e.ToString());
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            label1.Text = counter.ToString();
            for (int i = 0; i < item; i++)
            {
                changes[i] = "";
                prices[i] = "";
                try
                {
                    using (WebClient Client = new WebClient())
                    {
                        Client.DownloadFile("http://dev.markitondemand.com/Api/v2/Quote/xmlp?symbol=" + symbols[i], path + symbols[i] + ".xml");
                    }
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path + symbols[i] + ".xml");

                    XmlNode node = doc.SelectSingleNode("StockQuote");

                    name[i].Text = "*" + node.SelectSingleNode("Name").InnerText;
                    symbol[i].Text = node.SelectSingleNode("Symbol").InnerText;
                    price[i].Text = node.SelectSingleNode("LastPrice").InnerText;
                    change[i].Text = node.SelectSingleNode("Change").InnerText + " (" + float.Parse(node.SelectSingleNode("ChangePercent").InnerText, CultureInfo.CurrentCulture).ToString("f2") + "%)";

                    calculatePrice(i, price[i].Text);
                }
                catch { }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Height = previousHeight;
            listBox1.Visible = true;
            string s = textBox1.Text;

            try
            {
                using (WebClient Client = new WebClient())
                {
                    Client.DownloadFile("http://dev.markitondemand.com/Api/v2/Lookup/xmlp?input=" + s + "&callback=myFunction", path + s + ".xml");
                }
                XmlDocument doc = new XmlDocument();
                doc.Load(path + s + ".xml");
                XmlNodeList prop = doc.SelectNodes("LookupResultList/LookupResult");

                foreach (XmlNode n in prop)
                {
                    listBox1.Items.Add(n.SelectSingleNode("Symbol").InnerText);
                    listBox1.Height += 13;
                }
            }
            catch
            {

            }
        }
        void PanelClickHandler(object sender, EventArgs e)
        {
            Panel selectedPanel = sender as Panel;
            selectedPanel.Parent.Focus();
        }
        void panelMouseHoverHandler(object sender, EventArgs e)
        {
            Panel selectedPanel = sender as Panel;
            eButton[int.Parse(selectedPanel.Name)].Visible = true;
        }
        void panelMouseLeaveHandler(object sender, EventArgs e)
        {
            if (overButton)
            {

            }
            else
            {
                Panel selectedPanel = sender as Panel;
                overButton = false;
            }
        }
        protected void eButtonMouseOverHandler(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int tempInt = Convert.ToInt32(b.Name);
            eButton[tempInt].Visible = true;
            overButton = true;
        }
        protected void eButtonClickHandler(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int tempInt = Convert.ToInt32(b.Name);
            panel3.Controls.Remove(panel[tempInt]);
        }
        private void ListBox1_Click(object sender, EventArgs e)
        {
            sy = listBox1.SelectedItem.ToString();
            textBox1.Text = "";
            listBox1.Visible = false;
            showData();
            item++;
        }
        protected void sharesTextBoxClickHandler(object sender, EventArgs e)
        {
            TextBox valueBox = sender as TextBox;
            valueBox.BackColor = Color.White;
            valueBox.Text = valueBox.Text.ToString();
        }
        protected void sharesTextBoxLostFocusHandler(object sender, EventArgs e)
        {
            TextBox valueBox = sender as TextBox;
        }
        protected void sharesTextBoxTextChangedHandler(object sender, EventArgs e)
        {
            TextBox valueBox = sender as TextBox;
            valueBox.BackColor = Color.White;
            int tempInt = Convert.ToInt32(valueBox.Text);
            calculatePrice(tempInt, prices[tempInt]);
        }
        protected void priceTextBoxTextChangedHandler(object sender, EventArgs e)
        {
            TextBox valueBox = sender as TextBox;
            valueBox.BackColor = Color.White;
            int tempInt = Convert.ToInt32(valueBox.Name);
            calculatePrice(tempInt, prices[tempInt]);
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

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (listBox1.Items.Count == 0 || listBox1.SelectedItem == null)
                {
                    sy = textBox1.Text;
                    textBox1.Text = "";
                    listBox1.Visible = false;
                    showData();
                    item++;
                }
                else
                {
                    sy = listBox1.SelectedItem.ToString();
                    textBox1.Text = sy;
                    listBox1.Visible = false;
                    showData();
                    item++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
