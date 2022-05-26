using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                ReadFile();

            }
        }
        private void ReadHost()
        {
           
        }
        private void ReadFile()
        {
            FileStream fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(textBox1.Text);

            string linea;
            linea = sr.ReadLine();
            List<string> llista1 = new List<string>();
            List<string> llista2 = new List<string>();
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
            foreach (Object item in llista2)
            {
                comboBox2.Items.Add(item);
            }


            sr.Close();
            fs.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            
        }
        private string[] GetValues()
        {



        }
        private string GetElementName(string linea)
        {
            string elementname = "";
            string[] split = linea.Split('<', '>');

            elementname = split[1];
            return elementname;
        }
        private string GetElementData(string linea)
        {
            string data= "";

            string[] split = linea.Split('<', '>');

            data = split[2];
            return data;
        }
    }
}
