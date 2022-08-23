using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetDeneme1
{
    public class ProductDAL
    {
        SqlConnection connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB; database=UrunYonetimiAdoNet; integrated security=true");
        void ConnectionKontrol()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public List<Product> GetAll() 
        {
            ConnectionKontrol(); 
            List<Product> urunListesi = new List<Product>(); 
            SqlCommand command = new SqlCommand("select * from Products", connection); 
            SqlDataReader reader = command.ExecuteReader(); 

            while (reader.Read()) 
            {
                Product product = new Product() 
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    UrunAdi = reader["UrunAdi"].ToString(),
                    StokMiktari = Convert.ToInt32(reader["StokMiktari"]),
                    UrunFiyati = Convert.ToDecimal(reader["UrunFiyati"])
                };
                urunListesi.Add(product);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return urunListesi;
        }
        public DataTable GetAllDataTable()
        {
            ConnectionKontrol(); 
            DataTable dt = new DataTable(); 
            SqlCommand command = new SqlCommand("select * from Products", connection);
            SqlDataReader reader = command.ExecuteReader();
            dt.Load(reader); 
            reader.Close(); 
            command.Dispose(); 
            connection.Close();
            return dt; 
        }
        public int Add(Product product)
        {
            ConnectionKontrol();
            SqlCommand command = new SqlCommand("Insert into Products (UrunAdi, UrunFiyati, StokMiktari) values (@UrunAdi, @UrunFiyati, @Stok)", connection); 
            command.Parameters.AddWithValue("@UrunAdi", product.UrunAdi);
            command.Parameters.AddWithValue("@UrunFiyati", product.UrunFiyati);
            command.Parameters.AddWithValue("@Stok", product.StokMiktari);
            int islemSonucu = command.ExecuteNonQuery(); 
            command.Dispose(); 
            connection.Close();
            return islemSonucu; 
        }
    }
}
