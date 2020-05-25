using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Простая_итерация
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        double test(double x, double y)
        {
            return Math.Exp(x * y);
        }
        double func(double x, double y)
        {
            return -1.0 * Math.Cosh(x * x * y);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int Nmax = Convert.ToInt32(textBox11.Text);
            int N = 0;//Количество итераций
            int a = -1, b = 0, c = 0, d = 1;//диапазон
            int n = Convert.ToInt32(textBox14.Text);//по икс
            int m = Convert.ToInt32(textBox13.Text);//по игрек
            this.dataGridView1.ColumnCount = n + 2;
            this.dataGridView1.RowCount = m + 2;
            double eps = Convert.ToDouble(textBox12.Text);//точность метода
            double eps_max = 0, eps_cur = 0;
            double[,] v = new double[200, 200];
            double[,] f = new double[200, 200];
            double v_old, v_new;
            bool ff = false;
            double h = (double)(b - a) / n;
            double k = (double)(d - c) / m;
            double h2 = -1.0 * Math.Pow(n / (b - a), 2);
            double k2 = -1.0 * Math.Pow(n / (d - c), 2);
            double a2 = -2.0 * (h2 + k2);
            double tau;
            double lambdaMin = 4.0 / (h * h) * Math.Sin(Math.PI / (2 * n)) * Math.Sin(Math.PI / (2 * n)) +
                               4.0 / (k * k) * Math.Sin(Math.PI / (2 * m)) * Math.Sin(Math.PI / (2 * m));
            double lambdaMax = 4.0 / (h * h) * Math.Sin(Math.PI * (n - 1) / (2.0 * n)) * Math.Sin(Math.PI * (n - 1) / (2 * n)) +
                               4.0 / (k * k) * Math.Sin(Math.PI * (m - 1) / (2.0 * m)) * Math.Sin(Math.PI * (m - 1) / (2 * m));
            tau = 2.0 / (lambdaMax + lambdaMin);
            for (int j = 0; j < m + 1; j++)
            {
                for (int i = 0; i < n + 1; i++)
                {
                    f[i, j] = ((j * k) * (j * k) + (i * h - 1) * (i * h - 1)) * Math.Exp((i * h - 1) * (j * k));
                }
            }
            for (int i = 0; i < n + 1; i++)
                for (int j = 0; j < m + 1; j++)
                    v[i, j] = 0;

            for (int j = 0; j < m + 1; j++)
            {
                v[0, j] = Math.Exp(-j * k);
                v[n, j] = 1;
            }

            //x = i * h - 1;
            //y = j * k ;
            for (int i = 0; i < n + 1; i++)
            {

                v[i, 0] = 1;
                v[i, m] = Math.Exp(i * h - 1);
            }

            while (!ff)
            {
                eps_max = 0;
                for (int i = 1; i < n; i++)
                {
                    for (int j = 1; j < m; j++)
                    {
                        v_old = v[i, j];            //Это Ах                                                                         Это b
                        v_new = v[i, j] - tau * (a2 * v[i, j] + h2 * (v[i - 1, j] + v[i + 1, j]) + k2 * (v[i, j - 1] + v[i, j + 1]) + f[i, j]);
                        eps_cur = Math.Abs(v_old - v_new);
                        if (eps_cur > eps_max)
                        {
                            eps_max = eps_cur;
                        }
                        v[i, j] = v_new;
                    }
                }
                N++;
                if (eps_max < eps || N >= Nmax)
                {
                    ff = true;
                }
            }
            double maxeps = 0;
            double cureps = 0;
            for (int j = 0; j < m + 1; j++)
            {
                for (int i = 0; i < n + 1; i++)
                {

                    cureps = Math.Abs(test(i * h - 1, j * k) - v[i, j]);
                    if (cureps >= maxeps)
                    {
                        maxeps = cureps;
                    }
                }
            }
            this.dataGridView1.ColumnCount = n + 2;
            this.dataGridView1.RowCount = m + 2;
            dataGridView1.Columns[0].HeaderText = "i";
            dataGridView1.Rows[0].HeaderCell.Value = "j";
            for (int i = 1; i <= n + 1; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = Math.Round(((i - 1) * h - 1) * 1000) / 1000;
                dataGridView1.Columns[i].HeaderText = Convert.ToString(i - 1);

            }
            dataGridView1.RowHeadersVisible = true;
            int p = 1;
            for (int j = m + 1; j >= 1; j--)
            {

                dataGridView1.Rows[p].Cells[0].Value = Math.Round(((j - 1) * k) * 1000) / 1000;
                dataGridView1.Rows[p].HeaderCell.Value = Convert.ToString(j - 1);
                p++;
            }

            dataGridView1.Rows[0].Cells[0].Value = "Y/X";



            for (int j = 1; j < m + 2; j++)
            {
                for (int i = 1; i < n + 2; i++)
                {

                    dataGridView1.Rows[j].Cells[i].Value = Math.Round(v[i - 1, m + 1 - j] * 1000) / 1000;
                }

            }

            this.textBox9.Text = Convert.ToString(N);
            this.textBox8.Text = Convert.ToString(eps_max);
            this.textBox15.Text = Convert.ToString(maxeps);
            this.textBox10.Text = Convert.ToString(tau);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Nmax = Convert.ToInt16(textBox4.Text);
            int N = 0;
            double eps = Convert.ToDouble(textBox3.Text);
            double eps_max = 0;
            double eps_cur = 0;
            int n = Convert.ToInt16(textBox1.Text);
            int m = Convert.ToInt16(textBox2.Text);
            this.dataGridView2.ColumnCount = n + 2;
            this.dataGridView2.RowCount = m + 2;
            double[,] v = new double[200, 200];
            double[,] f = new double[200, 200];
            double a = -1.0, b = 0, c = 0, d = 1.0;
            int p, r;
            double v_old;
            double v_new;
            bool ff = false;
            double eps_n = 0;
            double h = (double)(b - a) / n;
            double k = (double)(d - c) / m;
            double h2 = -1.0 * Math.Pow(n / (b - a), 2);
            double k2 = -1.0 * Math.Pow(n / (d - c), 2);
            double a2 = -2.0 * (h2 + k2);
            double tau;
            double lambdaMin = 4.0 / (h * h) * Math.Sin(Math.PI / (2 * n)) * Math.Sin(Math.PI / (2 * n)) +
                               4.0 / (k * k) * Math.Sin(Math.PI / (2 * m)) * Math.Sin(Math.PI / (2 * m));
            double lambdaMax = 4.0 / (h * h) * Math.Sin(Math.PI * (n - 1) / (2.0 * n)) * Math.Sin(Math.PI * (n - 1) / (2 * n)) +
                               4.0 / (k * k) * Math.Sin(Math.PI * (m - 1) / (2.0 * m)) * Math.Sin(Math.PI * (m - 1) / (2 * m));
            tau = 2.0 / (lambdaMax + lambdaMin);
            //заполнение границ
            for (int j = 0; j < m + 1; j++)
            {
                v[0, j] = Math.Sin(Math.PI * (j * k));
                v[n, j] = Math.Abs(Math.Sin(2 * Math.PI * (j * k)));
            }

            for (int i = 0; i < n + 1; i++)
            {
                v[i, 0] = -1 * (i * h - 1) * ((i * h - 1) + 1);
                v[i, m] = -1 * (i * h - 1) * ((i * h - 1) + 1);
            }

            while (!ff)
            {
                eps_max = 0;
                for (int j = 1; j < m; j++)
                {
                    for (int i = 1; i < n; i++)
                    {
                        v_old = v[i, j];
                        v_new = v[i, j] - tau * (a2 * v[i, j] + h2 * (v[i - 1, j] + v[i + 1, j]) + k2 * (v[i, j - 1] + v[i, j + 1]) + func(i * h - 1, j * k));
                        eps_cur = Math.Abs(v_old - v_new);
                        if (eps_cur > eps_max)
                        {
                            eps_max = eps_cur;
                        }
                        v[i, j] = v_new;
                    }
                }
                N++;
                if (eps_max < eps || N >= Nmax)
                {
                    ff = true;
                }
            }

            this.textBox6.Text = Convert.ToString(N);

            this.textBox7.Text = Convert.ToString(eps_max);

            double maxeps = 0;

            double cureps = 0;

            //Таблица
            this.dataGridView2.ColumnCount = n + 2;

            this.dataGridView2.RowCount = m + 2;

            dataGridView2.Columns[0].HeaderText = "i";

            dataGridView2.Rows[0].HeaderCell.Value = "j";
            for (int i = 1; i <= n + 1; i++)
            {
                dataGridView2.Rows[0].Cells[i].Value = Math.Round(((i - 1) * h - 1) * 1000) / 1000;
                dataGridView2.Columns[i].HeaderText = Convert.ToString(i - 1);
            }
            dataGridView2.RowHeadersVisible = true;
            p = 1;
            for (int j = m + 1; j >= 1; j--)

            {
                dataGridView2.Rows[p].Cells[0].Value = Math.Round(((j - 1) * h) * 1000) / 1000;
                dataGridView2.Rows[p].HeaderCell.Value = Convert.ToString(j - 1);
                p++;
            }

            dataGridView2.Rows[0].Cells[0].Value = "Y/X";


            for (int j = 1; j < m + 2; j++)

            {
                for (int i = 1; i < n + 2; i++)
                {
                    dataGridView2.Rows[j].Cells[i].Value = Math.Round(v[i - 1, m + 1 - j] * 1000) / 1000;
                }

            }

            //Половинный шаг

            N = 0;
            eps_max = 0;
            eps_cur = 0;
            double v_old2;
            double v_new2;
            ff = false;
            double[,] v2 = new double[200, 200];
            n *= 2;
            m *= 2;
            h = (double)(b - a) / n;
            k = (double)(d - c) / m;
            h2 = -1.0 * Math.Pow(n / (b - a), 2);
            k2 = -1.0 * Math.Pow(n / (d - c), 2);
            a2 = -2.0 * (h2 + k2);
            lambdaMin = 4.0 / (h * h) * Math.Sin(Math.PI / (2 * n)) * Math.Sin(Math.PI / (2 * n)) +
                               4.0 / (k * k) * Math.Sin(Math.PI / (2 * m)) * Math.Sin(Math.PI / (2 * m));
            lambdaMax = 4.0 / (h * h) * Math.Sin(Math.PI * (n - 1) / (2.0 * n)) * Math.Sin(Math.PI * (n - 1) / (2 * n)) +
                               4.0 / (k * k) * Math.Sin(Math.PI * (m - 1) / (2.0 * m)) * Math.Sin(Math.PI * (m - 1) / (2 * m));
            tau = 2.0 / (lambdaMax + lambdaMin);
            //заполнение границ
            for (int j = 0; j < m + 1; j++)

            {
                v2[0, j] = Math.Sin(Math.PI * (j * k));
                v2[n, j] = Math.Abs(Math.Sin(2 * Math.PI * (j * k)));
            }

            for (int i = 0; i < n + 1; i++)

            {
                v2[i, 0] = -1 * (i * h - 1) * ((i * h - 1) + 1);
                v2[i, m] = -1 * (i * h - 1) * ((i * h - 1) + 1);
            }
            eps_n = 0;
            double eps_max_abs = 0;
            while (!ff)

            {
                eps_max = 0;
                eps_max_abs = 0;
                for (int j = 1; j < m; j++)
                {
                    for (int i = 1; i < n; i++)
                    {
                        v_old2 = v2[i, j];
                        v_new2 = v2[i, j] - tau * (a2 * v2[i, j] + h2 * (v2[i - 1, j] + v2[i + 1, j]) + k2 * (v2[i, j - 1] + v2[i, j + 1]) + func(i * h - 1, j * k));
                        eps_cur = Math.Abs(v_old2 - v_new2);
                        if (eps_cur > eps_max_abs)
                            eps_max_abs = eps_cur;
                        if (eps_cur > eps_max)
                        {
                            eps_max = eps_cur;
                        }
                        v2[i, j] = v_new2;
                    }
                }
                N++;
                if (eps_max < eps || N >= Nmax)
                {
                    ff = true;
                }
            }
            //Вычисление максимального разности 2 приближений
            n /= 2;
            m /= 2;
            double eps_max_abs1 = 0;
            for (int i = 0; i < n + 1; i++)
                for (int j = 0; j < m + 1; j++)
                {
                    eps_cur = Math.Abs(v[i, j] - v2[i * 2, j * 2]);
                    if (eps_cur >= eps_max_abs1)
                        eps_max_abs1 = eps_cur;
                }
            //Таблица(половинный шаг)
            this.dataGridView3.ColumnCount = n * 2 + 2;

            this.dataGridView3.RowCount = m * 2 + 2;
            dataGridView3.Columns[0].HeaderText = "i";
            dataGridView3.Rows[0].HeaderCell.Value = "j";
            for (int i = 1; i <= n * 2 + 1; i++)

            {
                dataGridView3.Rows[0].Cells[i].Value = Math.Round(((i - 1) * h - 1) * 1000) / 1000;
                dataGridView3.Columns[i].HeaderText = Convert.ToString(i - 1);
            }
            dataGridView3.RowHeadersVisible = true;
            p = 1;
            for (int j = m * 2 + 1; j >= 1; j--)

            {
                dataGridView3.Rows[p].Cells[0].Value = Math.Round(((j - 1) * h) * 1000) / 1000;
                dataGridView3.Rows[p].HeaderCell.Value = Convert.ToString(j - 1);
                p++;
            }

            dataGridView3.Rows[0].Cells[0].Value = "Y/X";


            for (int j = 1; j < m * 2 + 2; j++)

            {
                for (int i = 1; i < n * 2 + 2; i++)
                {

                    dataGridView3.Rows[j].Cells[i].Value = Math.Round(v2[i - 1, m * 2 + 1 - j] * 1000) / 1000;
                }

            }

            this.textBox17.Text = Convert.ToString(N);
            this.textBox18.Text = Convert.ToString(eps_max_abs1);
            this.textBox16.Text = Convert.ToString(eps_max_abs);
            this.textBox5.Text = Convert.ToString(tau);
        }
    }
}
