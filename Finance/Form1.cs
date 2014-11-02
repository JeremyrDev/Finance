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
        string sy;
        int counter = 0;
        int item = 0;
        int panelPosition = 0;

        TextBox[] textbox;
        Button[] eButton;

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
      
        //on hovering over the panel, show a subtle X button all the way to the right in the middle of it, click it to delete the item
        private void Form1_Load(object sender, EventArgs e)
        {
            sy = "f";
            textbox = new TextBox[500];
            eButton = new Button[500];

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
                        Client.DownloadFile("http://dev.markitondemand.com/Api/v2/Quote/xmlp?symbol=" + sy, @"E:\Users\Jeremy-SSDOS\Desktop\" + sy + ".xml");
                    }

                    XmlDocument doc = new XmlDocument();
                    doc.Load(@"E:\Users\Jeremy-SSDOS\Desktop\" + sy + ".xml");

                    XmlNodeList prop = doc.SelectNodes("/StockQuote");

                    foreach (XmlNode n in prop)
                    {
                        names[item] = n.SelectSingleNode("Name").InnerText;
                        symbols[item] = n.SelectSingleNode("Symbol").InnerText;
                        prices[item] = n.SelectSingleNode("LastPrice").InnerText;
                        changes[item] = n.SelectSingleNode("Change").InnerText + " : " + n.SelectSingleNode("ChangePercent").InnerText + "%";
                    }
                }
                catch
                {

                }

                addItem();
                textBox1.Focus();
            }
        }
        void PanelClickHandler(object sender, EventArgs e)
        {
            //Button button = sender as Button;
            Panel selectedPanel = sender as Panel;
            selectedPanel.Parent.Focus();
            selectedPanel.BackColor = Color.DarkGray;
        }
        void panelLostFocusHandler(object sender, EventArgs e)
        {
            Panel selectedPanel = sender as Panel;
            selectedPanel.BackColor = Color.FromArgb(80, 80, 80);
            eButton[int.Parse(selectedPanel.Name)].Visible = false;
        }
        void panelMouseHoverHandler(object sender, EventArgs e)
        {
            Panel selectedPanel = sender as Panel;
            selectedPanel.BackColor = Color.FromArgb(100, 100, 100);
            eButton[int.Parse(selectedPanel.Name)].Visible = true;
        }
        void addItem()
        {
            panel[item] = new Panel();
            textbox[item] = new TextBox();
            eButton[item] = new Button();

            name[item] = new Label();
            symbol[item] = new Label();
            price[item] = new Label();
            change[item] = new Label();

            panel[item].Click += PanelClickHandler;
            panel[item].LostFocus += panelLostFocusHandler;
            panel[item].MouseHover += panelMouseHoverHandler;
            //panel[i].Click += PanelClickHandler;
            //panel[i].LostFocus += panelLostFocusHandler;

            panel[item].BackColor = Color.FromArgb(60, 60, 60);
            panel[item].BackgroundImage = new Bitmap(@"e:\users\jeremy-ssdos\documents\visual studio 2013\Projects\Finance\Finance\bg.bmp");
            panel[item].Dock = DockStyle.Top;
            panel[item].Name = item.ToString();

            eButton[item].Location = new Point(panel3.Width-60, 13);
            eButton[item].Text = "X";
            eButton[item].Width = 30;
            eButton[item].FlatStyle = FlatStyle.Flat;

            eButton[item].Visible = false;

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

            textbox[item].Location = new Point(575, 13);
            textbox[item].Width = 60;
            textbox[item].Text = "Shares";
            textbox[item].TextAlign = HorizontalAlignment.Center;
            textbox[item].Font = new Font("Arial", 12);
            textbox[item].BorderStyle = BorderStyle.None;
            //textbox[i].BackColor = Color.FromArgb(203, 222, 231);
            textbox[item].BackColor = Color.White;

            panel[item].Width = panel3.Width - 10;
            panel[item].Height = 45;

            panel[item].Controls.Add(name[item]);
            panel[item].Controls.Add(symbol[item]);
            panel[item].Controls.Add(price[item]);
            panel[item].Controls.Add(change[item]);
            panel[item].Controls.Add(eButton[item]);
            panel[item].Controls.Add(textbox[item]);

            panel3.Controls.Add(panel[item]);
            panelPosition += 45;

            textbox[item] = new TextBox();
            textbox[item].Click += textBoxClickHandler;
            textbox[item].LostFocus += textBoxLostFocusHandler;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //buildString = "";
            listBox1.Items.Clear();
            listBox1.Height = 0;
            listBox1.Visible = true;
            string s = textBox1.Text;

            try
            {
                using (WebClient Client = new WebClient())
                {
                    Client.DownloadFile("http://dev.markitondemand.com/Api/v2/Lookup/xmlp?input=" + s + "&callback=myFunction",@"E:\Users\Jeremy-SSDOS\Desktop\"+ s + ".xml");
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(@"E:\Users\Jeremy-SSDOS\Desktop\" + s + ".xml");
                //label1.Text = doc.SelectNodes(;
                //XmlNode node = doc.SelectSingleNode("LookUpResult");

                XmlNodeList prop = doc.SelectNodes("LookupResultList/LookupResult");

                foreach (XmlNode n in prop)
                {
                    //name[i].Text = "*" + node.SelectSingleNode("Name").InnerText;
                    //label1.Text = n["Name"].InnerText;
                    listBox1.Items.Add(n.SelectSingleNode("Symbol").InnerText);
                    listBox1.Height += 13;
                    //price[i].Text = node.SelectSingleNode("LastPrice").InnerText;
                    //change[i].Text = node.SelectSingleNode("Change").InnerText + " : " + node.SelectSingleNode("ChangePercent").InnerText + "%";
                }
            }
            catch
            {

            }
        }

        private void ListBox1_Click(object sender, EventArgs e)
        {
            sy = listBox1.SelectedItem.ToString();
            textBox1.Text = "";
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
            for (int i = 0; i < item; i++)
            {
                //names[i] = "";
                //symbols[i] = "";
                changes[i] = "";
                prices[i] = "";
                using (WebClient Client = new WebClient())
                {
                    Client.DownloadFile("http://dev.markitondemand.com/Api/v2/Quote/xmlp?symbol=" + symbols[i], symbols[i]+".xml");
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(symbols[i]+".xml");

                XmlNode node = doc.SelectSingleNode("StockQuote");

                //XmlNodeList prop = node.SelectNodes("Items");

                //foreach (XmlNode n in prop)
                //{
                name[i].Text = "*" + node.SelectSingleNode("Name").InnerText;
                symbol[i].Text = node.SelectSingleNode("Symbol").InnerText;
                price[i].Text = node.SelectSingleNode("LastPrice").InnerText;
                change[i].Text = node.SelectSingleNode("Change").InnerText + " : " + node.SelectSingleNode("ChangePercent").InnerText+"%";
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
