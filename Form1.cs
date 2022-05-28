using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ABP
{
    public partial class Form1 : Form
    {
        List<string> llistaChild = new List<string>();
        List<string> llistaNameHost = new List<string>();
        List<string> llistaNameRefugee = new List<string>();
        List<string> llistaFoodDesc = new List<string>();
        List<string> llistaDeliveryNote = new List<string>();
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

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            ReadChilds();
        }

        private string CloseChild(string linea)
        {
            string elementname = "";
            if (linea != null)
            {
                string[] split = linea.Split('/', '>');

                elementname = split[1];
            }
            return elementname;
        }
        private void ReadChilds()
        {
            FileStream fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(textBox1.Text);

            string linea, closelinea, pare;
            linea = sr.ReadLine();
            pare = linea;
            linea = sr.ReadLine();

            while (linea != null)
            {
                if (CloseChild(linea).Equals(GetElementName(pare)))
                {
                    linea = null;
                }
                else
                {
                    closelinea = GetElementName(linea);
                    while (linea != null && (!CloseChild(linea).Equals(closelinea)) && (!CloseChild(linea).Equals(GetElementName(pare))))
                    {

                        linea = sr.ReadLine();

                    }
                    if (llistaChild.IndexOf(closelinea) == -1)
                    {
                        llistaChild.Add(closelinea);
                    }


                    linea = sr.ReadLine();
                }



            }

            foreach (Object item in llistaChild)
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
            llistaNameRefugee = new List<string>();
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
                        llistaNameRefugee.Add(GetElementData(linea));
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
            foreach (Object item in llistaNameRefugee)
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
            llistaNameHost = new List<string>();
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
                        llistaNameHost.Add(GetElementData(linea));
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
            foreach (Object item in llistaNameHost)
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
            llistaFoodDesc = new List<string>();
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
                        llistaFoodDesc.Add(GetElementData(linea));

                    }
                    linea = sr.ReadLine();
                }



                linea = null;

            }
            sr.Close();
            fs.Close();
            foreach (Object item in llistaFoodDesc)
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
            llistaDeliveryNote = new List<string>();
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
                        llistaDeliveryNote.Add(GetElementData(linea));

                    }
                    linea = sr.ReadLine();
                }



                linea = null;

            }
            sr.Close();
            fs.Close();
            foreach (Object item in llistaDeliveryNote)
            {
                comboBox2.Items.Add(item);
            }

        }
        private string GetElementName(string linea)
        {
             string elementname = "";
            if (linea != null)
            {
                string[] split = linea.Split('<', '>');

                elementname = split[1].Trim();
            }
            return elementname;
        }
        private string GetElementData(string linea)
        {
            string data = "";
            if (linea != null)
            {
                string[] split = linea.Split('<', '>');
                data = split[2].Trim();
            }
            
            return data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Selecione las listas porfavor");
            }
            else
            {
                if (comboBox1.SelectedItem.ToString().Equals("Hosts"))
                {

                    richTextBox1.Text = SearchHost();

                }
                if (comboBox1.SelectedItem.ToString().Equals("Foods"))
                {

                    richTextBox1.Text = SearchFood();

                }
                if (comboBox1.SelectedItem.ToString().Equals("FoodsDelivered"))
                {

                    richTextBox1.Text = SearchFoodsDelivered();

                }
                if (comboBox1.SelectedItem.ToString().Equals("Refugees"))
                {

                    richTextBox1.Text = SearchHostRefugee();

                }
            }
            
        }
        private string SearchFoodsDelivered()
        {
            StreamReader sr = new StreamReader(textBox1.Text);


            string linea, texto = "";
            Boolean exists;
            exists = false;

            linea = sr.ReadLine();
            List<string> text = new List<string>();

            while (!GetElementName(linea).Equals("FoodsDelivered"))
            {

                linea = sr.ReadLine();

            }
            while (!GetElementName(linea).Equals("/FoodDelivered") && linea != null)
            {
                if (GetElementName(linea).Equals("DeliveryNote"))
                {


                    text.Add("DELIVERY NOTE: " + GetElementData(linea));
                    if (GetElementData(linea).Equals(comboBox2.SelectedItem.ToString()))
                    {
                        exists = true;
                    }
                    else
                    {
                        while (!CloseChild(linea).Equals("FoodDelivered") && linea != null)
                        {
                            linea = sr.ReadLine();
                            text.Clear();

                        }
                    }

                }
                if (GetElementName(linea).Equals("DeliveryDate"))
                {

                    text.Add("DELIVERY DATE: " + GetElementData(linea));

                }
                if (GetElementName(linea).Equals("TotalPrice"))
                {

                    text.Add("TOTAL COST: " + GetElementData(linea));

                    if (exists == true)
                    {
                        texto = texto + "\n" + "-----------------------------------------------------";
                        foreach (Object item in text)
                        {
                            texto = texto + "\n" + item;

                        }
                        texto = texto + "\n" + "-----------------------------------------------------";

                    }


                }

                linea = sr.ReadLine();
            }


            return texto;

            sr.Close();
        }
        private string SearchFood()
        {
            StreamReader sr = new StreamReader(textBox1.Text);
            

                string linea, texto = "";

            linea = sr.ReadLine();
            List<string> text = new List<string>();

            while (!GetElementName(linea).Equals("FoodsDelivered"))
            {

                linea = sr.ReadLine();

            }
            while (!GetElementName(linea).Equals("/FoodDelivered") && linea != null)
            {

                if (GetElementName(linea).Equals("DeliveryNote"))
                {

                    text.Add("DELIVERY NOTE: " + GetElementData(linea));

                }
                if (GetElementName(linea).Equals("DeliveryDate"))
                {

                    text.Add("DELIVERY DATE: " + GetElementData(linea));

                }
                if (GetElementName(linea).Equals("TotalPrice"))
                {

                    text.Add("TOTAL COST: " + GetElementData(linea));


                }
                if (GetElementName(linea).Equals("Items"))
                {
                    while (!CloseChild(linea).Equals("Items") && linea != null)
                    {
                        if (GetElementData(linea).Equals(comboBox2.SelectedItem.ToString()))
                        {

                            texto = texto + "\n" + "-----------------------------------------------------";
                            foreach (Object item in text)
                            {
                                texto = texto + "\n" + item;

                            }
                            texto = texto + "\n" + "-----------------------------------------------------";

                        }
                        linea = sr.ReadLine();
                    }
                    text.Clear();


                }

                linea = sr.ReadLine();

            }
            
            
            return texto;
            
            sr.Close();
        }
        private string SearchHost()
        {
            StreamReader sr = new StreamReader(textBox1.Text);

            string linea, texto = "";
            Boolean exists;
            exists = false;
            linea = sr.ReadLine();
            List<string> text = new List<string>();

            while (!GetElementName(linea).Equals("FoodsDelivered"))
            {

                linea = sr.ReadLine();

            }
            while (!GetElementName(linea).Equals("/FoodsDelivered") && linea != null)
            {

                if (GetElementName(linea).Equals("DeliveryNote"))
                {

                    text.Add("DELIVERY NOTE: " + GetElementData(linea));

                }
                if (GetElementName(linea).Equals("DeliveryDate"))
                {

                    text.Add("DELIVERY DATE: " + GetElementData(linea));

                }
                if (GetElementName(linea).Equals("TotalPrice"))
                {

                    text.Add("TOTAL COST: " + GetElementData(linea));

                    if (exists == true)
                    {
                        texto = texto + "\n" + "-----------------------------------------------------";
                        foreach (Object item in text)
                        {
                            texto = texto + "\n" + item;

                        }
                        texto = texto + "\n" + "-----------------------------------------------------";

                    }


                }
                if (GetElementName(linea).Equals("HostFullName"))
                {
                    if (GetElementData(linea).Equals(comboBox2.SelectedItem.ToString()))
                    {
                        exists = true;
                    }
                    else
                    {
                        while (!CloseChild(linea).Equals("FoodDelivered") && linea != null)
                        {
                            linea = sr.ReadLine();
                            text.Clear();

                        }
                    }
                }

                linea = sr.ReadLine();

            }
            return texto;

            sr.Close();
        }
        private string SearchHostRefugee()
        {
            StreamReader sr = new StreamReader(textBox1.Text);

            string linea, texto = "", Hostrefugee = "";
            Boolean exists,existsrefugee;
            exists = false;
            existsrefugee = false;
            linea = sr.ReadLine();
            List<string> text = new List<string>();

            while (!GetElementName(linea).Equals("Hosts"))
            {

                linea = sr.ReadLine();

            }
            while (!CloseChild(linea).Equals("Hosts") || existsrefugee == false)
            {
                if(existsrefugee == false)
                {
                    Hostrefugee = null;
                }
                while (!CloseChild(linea).Equals("Host"))
                {
                    if (GetElementName(linea).Equals("FullName") && (Hostrefugee == null))
                    {
                        Hostrefugee = GetElementData(linea);
                    }
                    linea = sr.ReadLine();
                    if (GetElementData(linea).Equals(comboBox2.SelectedItem.ToString()))
                    {
                        existsrefugee = true;
                    }
                }
                linea = sr.ReadLine();
            }

            while (!GetElementName(linea).Equals("FoodsDelivered"))
            {

                linea = sr.ReadLine();

            }
            while (!GetElementName(linea).Equals("/FoodsDelivered") && linea != null)
            {

                if (GetElementName(linea).Equals("DeliveryNote"))
                {

                    text.Add("DELIVERY NOTE: " + GetElementData(linea));

                }
                if (GetElementName(linea).Equals("DeliveryDate"))
                {

                    text.Add("DELIVERY DATE: " + GetElementData(linea));

                }
                if (GetElementName(linea).Equals("TotalPrice"))
                {

                    text.Add("TOTAL COST: " + GetElementData(linea));

                    if (exists == true)
                    {
                        texto = texto + "\n" + "-----------------------------------------------------";
                        foreach (Object item in text)
                        {
                            texto = texto + "\n" + item;

                        }
                        texto = texto + "\n" + "-----------------------------------------------------";

                    }


                }
                if (GetElementName(linea).Equals("HostFullName"))
                {
                    if (GetElementData(linea).Equals(Hostrefugee))
                    {
                        exists = true;
                    }
                    else
                    {
                        while (!CloseChild(linea).Equals("FoodDelivered") && linea != null)
                        {
                            linea = sr.ReadLine();
                            text.Clear();

                        }
                    }
                }

                linea = sr.ReadLine();

            }
            return texto;

            sr.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string rutaCompleta = @"D:\S1AM\TREBALL ABF\ABP\dades.txt";
            using (StreamWriter sw = File.AppendText(rutaCompleta))
            {

                if (comboBox1.SelectedItem.ToString().Equals("Hosts"))
                {

                    sw.WriteLine(SearchHost());

                }
                if (comboBox1.SelectedItem.ToString().Equals("Foods"))
                {

                    sw.WriteLine(SearchFood());

                }
                if (comboBox1.SelectedItem.ToString().Equals("FoodsDelivered"))
                {

                    sw.WriteLine(SearchFoodsDelivered());

                }

            }
            
        }
    }
}
