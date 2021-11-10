using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _20110262_NguyenMinhChien
{
    public partial class Form1 : Form
    {
        const int MaxN = 10; // the maximum allowable dimension of the matrix
        int n = 3; // The current dimension of the matrix
        TextBox[,] MatrText = null; // The matrix of TextBox type elements
        double[,] Matr1 = new double[MaxN, MaxN]; // The matrix 1 of floating point numbers
        double[,] Matr2 = new double[MaxN, MaxN]; // The matrix 1 of floating point numbers
        double[,] Matr3 = new double[MaxN, MaxN]; // The matrix of results
        bool f1; // flag, which indicates about that the data were entered into the matrix Matr1
        bool f2; // flag, which indicates about that the data were entered into the matrixMatr1
        int dx = 40, dy = 20; // width and height of cells in MatrText [,]
        Form2 form2 = null; // an instance (object) of the class form "Form2
        int check = 0;
        private matrix result = new matrix();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // І. Initializing of controls and internal variables
            textBox1.Text = "";
            f1 = f2 = false; // matrices are not yet filled
            label2.Text = "false";
            label3.Text = "false";
            // ІІ. Memory allocation and configure MatrText
            int i, j;
            // 1. Memory allocation for Form2
            form2 = new Form2();
            // 2. Memory allocation for the whole matrix (not for cells)
            MatrText = new TextBox[MaxN, MaxN];
            // 3. Memory allocation for each cell of the matrix and its setting
            for (i = 0; i < MaxN; i++)
                for (j = 0; j < MaxN; j++)
                {
                    // 3.1. Allocate memory
                    MatrText[i, j] = new TextBox();
                    // 3.2. Set the value to zero
                    MatrText[i, j].Text = "0";
                    // 3.3. Set the position of cell in the Form2
                    MatrText[i, j].Location = new System.Drawing.Point(10 + i * dx, 10 + j * dy);
                    // 3.4. Set the size of cell
                    MatrText[i, j].Size = new System.Drawing.Size(dx, dy);
                    // 3.5. Hide the cell
                    MatrText[i, j].Visible = false;
                    // 3.6. Add MatrText[i,j] into the form2
                    form2.Controls.Add(MatrText[i, j]);
                }
        }
        private void Clear_MatrText()
        {
            // Setting the cells of MatrText to zero
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    MatrText[i, j].Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Reading of the matrix dimension
            if (textBox1.Text == "") return;
            n = int.Parse(textBox1.Text);
            // 2. Zeroing of cell MatrText
            Clear_MatrText();
            // 3. Setting the properties of the matrix cells
            // with binding to the value of n and the form Form2
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Tab order
                    MatrText[i, j].TabIndex = i * n + j + 1;
                    // 3.2. Set the cell as visible
                    MatrText[i, j].Visible = true;
                }
            // 4. Correcting of form size
            form2.Width = 10 + n * dx + 20;
            form2.Height = 10 + n * dy + form2.button1.Height + 50;
            // 5. Correcting of the position and size of the button on the Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + n * dy + 10;
            form2.button1.Width = form2.Width - 30;
            // 6. Calling the form Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Moving lines from the Form2 form into the matrix Matr1
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (MatrText[i, j].Text != "")
                            Matr1[i, j] = Double.Parse(MatrText[i, j].Text);
                        else
                            Matr1[i, j] = 0;
                // 8. Data were entered into matrix
                f1 = true;
                label2.Text = "true";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 1. Reading of the matrix dimension
            if (textBox1.Text == "") return;
            n = int.Parse(textBox1.Text);
            // 2. Zeroing of cell MatrText
            Clear_MatrText();
            // 3. Setting the properties of the matrix cells
            // with binding to the value of n and the form Form2
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Tab order
                    MatrText[i, j].TabIndex = i * n + j + 1;
                    // 3.2. Set the cell as visible
                    MatrText[i, j].Visible = true;
                }
            // 4. Correcting of form size
            form2.Width = 10 + n * dx + 20;
            form2.Height = 10 + n * dy + form2.button1.Height + 50;
            // 5. Correcting of the position and size of the button on the Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + n * dy + 10;
            form2.button1.Width = form2.Width - 30;
            // 6. Calling the form Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Moving lines from the Form2 form into the matrix Matr1
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        Matr2[i, j] = Double.Parse(MatrText[i, j].Text);
                // 8. Matrix Matr2 is formed
                f2 = true;
                label3.Text = "true";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            int nn;
            nn = Int16.Parse(textBox1.Text);
            if (nn != n)
            {
                f1 = f2 = false;
                label2.Text = "false";
                label3.Text = "false";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileStream fw = null;
            string msg;
            byte[] msgByte = null; // array of bytes
                                   // 1. Open file for writing
            fw = new FileStream("Res_Matr.txt", FileMode.Create);
            // 2. Saving the matrix of result in file
            // 2.1. Save the number of elements of the matrix Matr3
            msg = n.ToString() + "\r\n";
            // Converting the string msg into a byte array msgByte
            msgByte = Encoding.Default.GetBytes(msg);
            // save of array msgByte into the file
            fw.Write(msgByte, 0, msgByte.Length);
            // 2.2. Now saving of the matrix
            msg = "";
            for (int i = 0; i < n; i++)
            {
                // forming of a string based on the matrix
                for (int j = 0; j < n; j++)
                    msg = msg + Matr3[i, j].ToString() + " ";
                msg = msg + "\r\n"; // new line
            }
            // 3. Converting the strings into a byte array
            msgByte = Encoding.Default.GetBytes(msg);
            // 4. Saving the strings into the file
            fw.Write(msgByte, 0, msgByte.Length);
            // 5. Close the file
            if (fw != null) fw.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            check = 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            check = 2;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            check = 3;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 1. Checking, were inputted data in the both matrices?
            if (!((f1 == true) && (f2 == true)) || check == 0) return;
            // 2. Calculating of the product of matrices. Result is in Matr3
            if (check == 1)
            {
                matrix a = new matrix(n, n, Matr1);
                matrix b = new matrix(n, n, Matr2);
                result = a + b;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        Matr3[i, j] = result.mt[i, j];
                    }
                check = 0;
            }
            if (check == 2)
            {
                matrix a = new matrix(n, n, Matr1);
                matrix b = new matrix(n, n, Matr2);
                result = a - b;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        Matr3[i, j] = result.mt[i, j];
                    }
                check = 0;
            }
            if (check == 3)
            {
                matrix a = new matrix(n, n, Matr1);
                matrix b = new matrix(n, n, Matr2);
                result = a * b;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        Matr3[i, j] = result.mt[i, j];
                    }
                check = 0;
            }
            // 3. Inputting data into MatrText
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Tab order
                    MatrText[i, j].TabIndex = i * n + j + 1;
                    // 3.2. Converting the number to a string
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                }
            // 4. Show the form
            form2.ShowDialog();
        }
    }
}
