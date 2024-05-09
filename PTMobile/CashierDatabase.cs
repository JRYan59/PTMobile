
using PTDocs;
using PTMobile.Models;
using PTMobile.Resources.Languages;
using PTMobile.ViewModel;
using SQLite;
using System.ComponentModel;
using System.Linq;

namespace PTMobile
{
    public class CashierDatabase
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteConnection conn;

        private void Init()
        {
            if (conn is not null)
                return;

            conn = new(_dbPath);

            conn.CreateTable<Cashier>();
            conn.CreateTable<ReportZ>();
            conn.CreateTable<User>();
            conn.CreateTable<Product>();
            conn.CreateTable<ProductImage>();
            conn.CreateTable<Product_Count>();
            conn.CreateTable<Product_Count_Det>();
            conn.CreateTable<Authorization>();
        }

        public CashierDatabase(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewCashier(Cashier cashier)
        {
            int result = 0;
            try
            {
                Init();
                if (cashier == null)
                    throw new Exception("Cashier is null");

                result = conn.Insert(cashier);
                StatusMessage = String.Format("{0} record(s) added ({1})", result, cashier.Name);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add {0}. Error: {1}",cashier.Name, ex.Message);
            }
        }
        public void AddNewReport(ReportZ rep)
        {
            int result = 0;
            try
            {
                Init();
                if (rep == null)
                    throw new Exception("Cashier is null");

                result = conn.Insert(rep);
                StatusMessage = String.Format("{0} record(s) added ({1})", result, rep.Number);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add {0}. Error: {1}", rep.Number, ex.Message);
            }
        }

        public void AddNewUser(User user)
        {
            int result = 0;
            try
            {
                Init();
                if (user == null)
                    throw new Exception("User is null");

                result = conn.Insert(user);
                StatusMessage = AppResources.UserCreated;
            }
            catch (Exception ex)
            {
                StatusMessage = AppResources.UserDontCreated+": "+ ex.Message;
            }
        }
        public void AddNewProduct(Product product)
        {
            int result = 0;
            try
            {
                Init();
                if (product == null)
                    throw new Exception("Product is null");

                result = conn.Insert(product);
                StatusMessage = "Producto Creado";
            }
            catch (Exception ex)
            {
                StatusMessage = "No se pudo crear el Producto: " + ex.Message;
            }
        }

        public void AddNewAuthorization(Authorization authorization)
        {
            int result = 0;
            try
            {
                Init();
                if (authorization == null)
                    throw new Exception("Authorization is null");

                result = conn.Insert(authorization);
                StatusMessage = String.Format("record added");
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add: " + ex);
            }
        }

        public void DeleteProduct(Product product)
        {
            int result = 0;
            try
            {
                Init();
                if (product == null)
                    throw new Exception("Product is null");

                result = conn.Delete(product);
                StatusMessage = "Producto Eliminado";
            }
            catch (Exception ex)
            {
                StatusMessage = "No se pudo eliminar el Producto: " + ex.Message;
            }
        }
        public void DeleteAllProductImage(string productCode)
        {
            List<ProductImage> images = GetProductImages(productCode);
            int result = 0;
            try
            {
                Init();
                if (images == null || images.Count() == 0)
                    return;

                foreach (var item in images)
                {
                   result = DeleteImage(item);
                }                
                
            }
            catch (Exception ex)
            {
                StatusMessage = "Error: " + ex.Message;
            }
        }
        public void DeleteProductImage(string path)
        {
            ProductImage image = conn.Table<ProductImage>().Where(r => r.Path == path).FirstOrDefault();
            int result = 0;
            try
            {
                Init();
                if (image != null)
                {
                    result = DeleteImage(image);

                }
                


            }
            catch (Exception ex)
            {
                StatusMessage = "Error: " + ex.Message;
            }
        }

        public int DeleteImage(ProductImage image)
        {
            int result = conn.Delete(image);

            if (result > 0)
            {
                File.Delete(image.Path);
                conn.Insert(new ProductImageLocal() { Name = image.Name, Date = DateTime.Now, Path = image.Path, Status = 2 });
                
            }
            return result;
        }


        public void ClearCashiers()
        {
            int result = 0;

            try
            {
                Init();
                result = conn.DeleteAll<Cashier>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format(ex.Message);
            }
        }
        public void ClearReports()
        {
            int result = 0;

            try
            {
                Init();
                result = conn.DeleteAll<ReportZ>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format(ex.Message);
            }
        }

        public void ClearUsers()
        {
            int result = 0;

            try
            {
                Init();
                result = conn.DeleteAll<User>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format(ex.Message);
            }
        }
        public void ClearProducts()
        {
            int result = 0;

            try
            {
                Init();
                result = conn.DeleteAll<Product>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format(ex.Message);
            }
        }
        public void ClearProductCounts()
        {
            int result = 0;

            try
            {
                Init();
                result = conn.DeleteAll<Product_Count>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format(ex.Message);
            }
        }
        public void ClearProductCountDets()
        {
            int result = 0;

            try
            {
                Init();
                result = conn.DeleteAll<Product_Count_Det>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format(ex.Message);
            }
        }
        public void ClearProductImages()
        {
            int result = 0;

            try
            {
                Init();
                result = conn.DeleteAll<ProductImage>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format(ex.Message);
            }
        }

        public void ClearAuthorizations()
        {
            int result = 0;

            try
            {
                Init();
                result = conn.DeleteAll<Authorization>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format(ex.Message);
            }
        }

        public void ClearDatabase()
        {

            try
            {
                Init();
                ClearProductCountDets();
                ClearProductCounts();
                ClearProducts();
                ClearCashiers();
                ClearProductImages();
                ClearReports();
                ClearAuthorizations();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format(ex.Message);
            }
        }

        public List<Cashier> GetAllCashiers()
        {
            try
            {
                Init();
                
                return conn.Table<Cashier>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Cashier>();
        }

        public List<Product_Count> GetAllProductCounts()
        {
            try
            {
                Init();

                return conn.Table<Product_Count>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Product_Count>();
        }

        public List<ReportZ> GetAllReports()
        {
            try
            {
                Init();
                return conn.Table<ReportZ>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<ReportZ>();
        }

        public List<User> GetAllUser()
        {
            try
            {
                Init();
                return conn.Table<User>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<User>();
        }

        public List<Product> GetProducts(string product)
        {
            try
            {
                Init();

                if (MainPage.forDescr)
                {
                    return conn.Table<Product>().Where(r => r.Name.ToLower().Contains(product.ToLower())).ToList();
                }
                if (MainPage.forCode)
                {
                    return conn.Table<Product>().Where(r => r.Code.ToLower().Contains(product.ToLower())).ToList();
                }
                if (MainPage.All)
                {
                    return conn.Table<Product>().Where(r => r.Code.ToLower().Contains(product.ToLower()) || r.Name.ToLower().Contains(product.ToLower())).ToList();
                }
                

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Product>();
        }
        public List<Authorization> GetAllAuthorizations()
        {
            try
            {
                Init();

                return conn.Table<Authorization>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Authorization>();
        }

        public Models.Product GetProduct(string code)
        {

            try
            {
                Init();
                return conn.Table<Product>().Where(r => r.Code == code).FirstOrDefault();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format(ex.Message);
            }
            return null;
        }

        public List<ProductImage> GetAllProductImages()
        {
            try
            {
                Init();
                return conn.Table<ProductImage>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<ProductImage>();
        }
        public void ClearAllProductImages()
        {
            try
            {
                Init();
                conn.DropTable<ProductImage>();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete data. {0}", ex.Message);
            }
        }
        public void UpdateProduct(Product product)
        {
            int result = 0;
            try
            {
                Init();

                if (product == null)
                    throw new Exception("Product is null");

                result = conn.Update(product);
                StatusMessage = "Producto actualizado";
            }
            catch (Exception ex)
            {
                StatusMessage = "No se pudo actualizar el usuario: " + ex.Message;
            }
        }
        public List<Product> GetProducts()
        {
            try
            {
                Init();
                return conn.Table<Product>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Product>();
        }

        public void AddImage(ProductImage img)
        {
                        int result = 0;
            try
            {
                Init();
                if (img == null)
                    throw new Exception("img is null");

                List<ProductImage> productImages = conn.Table<ProductImage>().ToList();
                result = conn.Insert(img);
            }
            catch (Exception ex)
            {
                StatusMessage = "No se pudo añadir la imagen: " + ex.Message;
            }
        }
        public void AddNewProductCount(Product_Count product_Count)
        {
            int result = 0;
            try
            {
                Init();
                if (product_Count == null)
                    throw new Exception("pc is null");
                result = conn.Insert(product_Count);
            }
            catch (Exception ex)
            {
                StatusMessage = "No se pudo añadir el pc " + ex.Message;
            }
        }

        public void UpdateAuthorization(Authorization authorization)
        {
            int result = 0;
            try
            {
                Init();
                if (authorization == null)
                    throw new Exception("Authorization is null");

                result = conn.Update(authorization);
                StatusMessage = String.Format("record added");
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add: " + ex);
            }
        }

        public int StartProductCount(string warehouse,string Descr)
        {
            int result = 0;
            Product_Count pc = new Product_Count();
            try
            {
                pc.Descr = Descr;
                pc.StartDate = DateTime.Now;
                pc.Status = 0;
                pc.UserId = MainPage.CurrentUser.Id;
                pc.Warehouse= warehouse;
                Init();
                if (pc == null)
                    throw new Exception("productCount is null");
                 conn.Insert(pc);
                result = pc.Id;
                return result;
            }
            catch (Exception ex)
            {
                StatusMessage = "No se pudo añadir el conteo de productos: " + ex.Message;
            }
            return 0;
        }
        public Product_Count GetProductCount(int pcId)
        {
            try
            {
                Init();
                return conn.Find<Product_Count>(pcId);

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new Product_Count();
        }
        public void EditProductCount(Product_Count pc, string newDescr,string newWareHouse)
        {
            int result = 0;

            try
            {
                pc.Descr = newDescr;
                pc.Warehouse = newWareHouse;
                pc.Status= 0;
                Init();
                if (pc == null)
                    throw new Exception("productCount is null");
                result = conn.Update(pc);

            }
            catch (Exception ex)
            {
                StatusMessage = "No se pudo añadir el conteo de productos: " + ex.Message;
            }
        }

        public void UpdateProductCountStatus(Product_Count pc,int newStatus)
        {
            int result = 0;
            
            try
            {
                pc.Status = newStatus;
                if(newStatus == 1 || newStatus ==2)
                pc.EndDate = DateTime.Now;
                Init();
                if (pc == null)
                    throw new Exception("productCount is null");
                result = conn.Update(pc);
            }
            catch (Exception ex)
            {
                StatusMessage = "No se pudo añadir el conteo de productos: " + ex.Message;
            }
        }

        public List<Product_Count_Det> GetProduct_Count_Dets(int productCountId, string productCode)
        {

            try
            {
                Init();
                return conn.Table<Product_Count_Det>().Where(r=>r.Id_Pc == productCountId && r.ProductCode == productCode).ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Product_Count_Det>();

        }
        public List<Product_Count_Det_Display> GetProduct_Count_Dets(int productCountId)
        {

            try
            {
                Init();


                var query1 = (from Product_Count_Det in conn.Table<Product_Count_Det>()
                            join Product in conn.Table<Product>()
                            on Product_Count_Det.ProductCode equals Product.Code into g from Product in g.DefaultIfEmpty()
                            where Product_Count_Det.Id_Pc == productCountId
                            select new Product_Count_Det_Display  
                            { Qty = Product_Count_Det.Qty, Location = Product_Count_Det.Location, ProductCode = Product_Count_Det.ProductCode, Description = Product.Name }).ToList();
                
                

                return query1;
               // conn.Table<Product_Count_Det>().Join(x=>x.)
                
               //return conn.Table<Product_Count_Det>().Where(r => r.Id_Pc == productCountId).ToList();
               // conn.Table<Product_Count_Det>()
               // conn.Table<Product_Count_Det>()

               // select Product_Count_Det.*,Products.Descripcion from Product_Count_Det 
               //     left join Products on Product_Count_Det.Codigo=Products.Codigo
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Product_Count_Det_Display>();

        }

        public List<ProductImage> GetProductImages(string ProductCode)
        {
            try
            {
                Init();
                return conn.Table<ProductImage>().Where(r=>r.ProductCode==ProductCode).ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<ProductImage>();
        }
        public int SaveProductCountDet(Product_Count_Det pc)
        {
            int result = 0;
            try
            {
                Init();
                if (pc == null)
                    throw new Exception("ProductCountDet is null");
                result = conn.Insert(pc);
                return result;

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to insert data. {0}", ex.Message);
            }
            return result;
        }
        public int UpdateProductCountDet(Product_Count_Det pc)
        {
            int result = 0;
            try
            {
                Init();
                if (pc == null)
                    throw new Exception("ProductCountDet is null");
                result = conn.Update(pc);
                return result;

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to insert data. {0}", ex.Message);
            }
            return result;
        }

    }
}
