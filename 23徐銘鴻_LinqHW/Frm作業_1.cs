using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            var q = from p in this.nwDataSet1.Orders
                    group p by p.OrderDate.Year into ody
                    select ody.Key;
            this.comboBox1.DataSource = q.ToList();
            this.comboBox1.SelectedIndex = 0;

            

            var pp = (from p in this.nwDataSet1.Products
                      select p).Take(Convert.ToInt32(this.textBox1.Text));
            this.dataGridView1.DataSource = pp.ToList();

        }
        int Topp = 0;
        int skipp = 0;
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Topp += Convert.ToInt32(this.textBox1.Text);
            skipp = Topp-Convert.ToInt32(this.textBox1.Text);
            var pp = (from p in this.nwDataSet1.Products
                      select p).Take(Topp).Skip(skipp);
            this.dataGridView1.DataSource = pp.ToList();
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.DataSource = this.nwDataSet1.Orders;
            var q = from p in this.nwDataSet1.Orders                    
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.DataSource = this.nwDataSet1.Orders;
            var q = from o in this.nwDataSet1.Orders
                    where o.OrderDate.Year == Convert.ToInt32(comboBox1.SelectedItem)
                    select o;
            var qd = from od in this.nwDataSet1.Order_Details
                     join order in this.nwDataSet1.Orders
                    on od.OrderID equals order.OrderID
                     where order.OrderDate.Year == Convert.ToInt32(comboBox1.SelectedItem)
                     select od;
            this.dataGridView1.DataSource = q.ToList();
            this.dataGridView2.DataSource = qd.ToList();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            this.dataGridView1.DataSource = files;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
           
            System.IO.FileInfo[] filelist = dir.GetFiles();

            var ffile = from ff in filelist
                        where ff.CreationTime.Year == 2022
                        orderby ff.CreationTime
                        select ff;
            this.dataGridView1.DataSource = ffile.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] filelist = dir.GetFiles();

            var ffile = from ff in dir.GetFiles()
                        where ff.Length>8000
                        select ff;

            this.dataGridView1.DataSource = ffile.ToList();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if (Topp > Convert.ToInt32(this.textBox1.Text)) 
            {
                Topp -= Convert.ToInt32(this.textBox1.Text);
                skipp = Topp-Convert.ToInt32(this.textBox1.Text);
                var pp = (from p in this.nwDataSet1.Products
                          select p).Take(Topp).Skip(skipp);
                this.dataGridView1.DataSource = pp.ToList();
            }
            else
            {
                MessageBox.Show("已經到底了");
            }
            
        }
    }
}
