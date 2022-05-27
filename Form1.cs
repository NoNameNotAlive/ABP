using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//hola
using System.Windows.Forms;
using System.Collections;

namespace ABP
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog2.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(openFileDialog2.CheckFileExists)
            {
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                ReadChilds();

            }
        }

        private void ReadChilds()
        {
            FileStream fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(textBox1.Text);

            string linea;
            linea = sr.ReadLine();
            List<string> llista1 = new List<string>();
            while (linea != null)
            {
                
                if (GetElementName(linea).Equals("Hosts") || GetElementName(linea).Equals("Foods") || GetElementName(linea).Equals("Refugees") || GetElementName(linea).Equals("FoodsDelivered"))
                {
                    if (llista1.IndexOf(GetElementName(linea)) == -1)
                    {

                        llista1.Add(GetElementName(linea));

                    }
                }         
         

                linea = sr.ReadLine();

            }

            foreach (Object item in llista1)
            {
                comboBox1.Items.Add(item);
            }


            sr.Close();
            fs.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            if (comboBox1.SelectedItem.ToString().Equals("Hosts"))
            {
                GetFullNameHost();
            }
            if (comboBox1.SelectedItem.ToString().Equals("Refugees"))
            {
                GetFullNameRefugees();
            }
            if (comboBox1.SelectedItem.ToString().Equals("Foods"))
            {
                GetDescFood();
            }
            if (comboBox1.SelectedItem.ToString().Equals("FoodsDelivered"))
            {
                GetDeliveryNote();
            }



        }
        private void GetFullNameRefugees()
        {
            FileStream fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(textBox1.Text);

            string linea;
            linea = sr.ReadLine();
            List<string> llista2 = new List<string>();
            while (linea != null)
            {
                while (!GetElementName(linea).Equals("Refugees"))
                {

                    linea = sr.ReadLine();

                }
                while (!GetElementName(linea).Equals("/Refugees"))
                {
                    if (GetElementName(linea).Equals("FullName"))
                    {
                        llista2.Add(GetElementData(linea));
                        while (!(GetElementName(linea).Equals("/Refugee")) && linea != null)
                        {
                            linea = sr.ReadLine();
                        }

                    }
                    linea = sr.ReadLine();
                }

                linea = null;

            }
            sr.Close();
            fs.Close();
            foreach (Object item in llista2)
            {
                comboBox2.Items.Add(item);
            }
        }
        private void GetFullNameHost()
        {
            FileStream fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(textBox1.Text);

            string linea;
            linea = sr.ReadLine();
            List<string> llista2 = new List<string>();
            while (linea != null)
            {
                while (!GetElementName(linea).Equals("Hosts"))
                {

                    linea = sr.ReadLine();

                }
                while (!GetElementName(linea).Equals("/Hosts"))
                {
                    if (GetElementName(linea).Equals("FullName"))
                    {
                        llista2.Add(GetElementData(linea));
                        while (!(GetElementName(linea).Equals("/Host")) && linea != null)
                        {
                            linea = sr.ReadLine();
                        }

                    }
                    linea = sr.ReadLine();
                }


                
                linea = null;
               
            }
            sr.Close();
            fs.Close();
            foreach (Object item in llista2)
            {
                comboBox2.Items.Add(item);
            }
        }
        private void GetDescFood()
        {

            FileStream fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(textBox1.Text);

            string linea;
            linea = sr.ReadLine();
            List<string> llista2 = new List<string>();
            while (linea != null)
            {
                while (!GetElementName(linea).Equals("Foods"))
                {

                    linea = sr.ReadLine();

                }
                while (!GetElementName(linea).Equals("/Foods"))
                {
                    if (GetElementName(linea).Equals("DescFood"))
                    {
                        llista2.Add(GetElementData(linea));

                    }
                    linea = sr.ReadLine();
                }



                linea = null;

            }
            sr.Close();
            fs.Close();
            foreach (Object item in llista2)
            {
                comboBox2.Items.Add(item);
            }

        }
        private void GetDeliveryNote()
        {

            FileStream fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(textBox1.Text);

            string linea;
            linea = sr.ReadLine();
            List<string> llista2 = new List<string>();
            while (linea != null)
            {
                while (!GetElementName(linea).Equals("FoodsDelivered"))
                {

                    linea = sr.ReadLine();

                }
                while (!GetElementName(linea).Equals("/FoodsDelivered"))
                {
                    if (GetElementName(linea).Equals("DeliveryNote"))
                    {
                        llista2.Add(GetElementData(linea));

                    }
                    linea = sr.ReadLine();
                }



                linea = null;

            }
            sr.Close();
            fs.Close();
            foreach (Object item in llista2)
            {
                comboBox2.Items.Add(item);
            }

        }
        private string GetElementName(string linea)
        {
            string elementname = "";
            if(linea != null)
            {
                string[] split = linea.Split('<', '>');

                elementname = split[1];
            }
            return elementname;
        }
        private string GetElementData(string linea)
        {
            string data= "";

            string[] split = linea.Split('<', '>');

            data = split[2];
            return data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Selecione las listas porfavor");
            }
            
        }
        private void search()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
