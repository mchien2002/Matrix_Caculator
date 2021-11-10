using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20110262_NguyenMinhChien
{
    class matrix
    {
        const int MaxN = 10;
        private int row;
        private int col;
        public double[,] mt = new double[MaxN, MaxN];
   
        public matrix() { }
        public int ROW
        {
            get { return row; }
            set { row = value; }
        }
        public int COL
        {
            get { return col; }
            set { col = value; }
        }
 
        public matrix(int a, int b, double[,] mt_temp)
        {
            row = a;
            col = b;
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    mt[i, j] = mt_temp[i, j];
        }
        public static matrix operator +(matrix a, matrix b)
        {
            int row = a.ROW;
            int col = a.COL;
            matrix c = new matrix();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    c.mt[i, j] = a.mt[i, j] + b.mt[i, j];
                }
            }
            return c;
        }
        public static matrix operator -(matrix a, matrix b)
        {
            int row = a.ROW;
            int col = a.COL;
            matrix c = new matrix();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    c.mt[i, j] = a.mt[i, j] - b.mt[i, j];
                }
            }
            return c;
        }
        public static matrix operator *(matrix a, matrix b)
        {
            int row = a.ROW;
            int col = b.COL;
            matrix c = new matrix();
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    c.mt[j, i] = 0;
                    for (int k = 0; k < col; k++)
                    {
                        c.mt[j, i] += a.mt[k, i] * b.mt[j, k];
                    }
                }
            return c;
        }
    }
}
