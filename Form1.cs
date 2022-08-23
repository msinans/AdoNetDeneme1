using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetDeneme1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDAL productDAL = new ProductDAL();

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvUrunler.DataSource = productDAL.GetAllDataTable();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Product product = new Product(); 
            product.StokMiktari = Convert.ToInt32(txtStokMiktari.Text);
            product.UrunAdi = txtUrunAdi.Text;
            product.UrunFiyati = Convert.ToDecimal(txtUrunFiyati.Text);
            var islemSonucu = productDAL.Add(product); 

            if (islemSonucu > 0)
            {
                dgvUrunler.DataSource = productDAL.GetAllDataTable(); 
                MessageBox.Show("Kayıt Başarılı");
            }
            else MessageBox.Show("Kayıt Başarısız!");
        }
    }
}
