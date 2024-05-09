#if ANDROID
using Android.Content;
#endif
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;

using PTMobile.Models;

namespace PTMobile.Functions
{
    internal class AddIn
    {

        static PTDocsClasses.SalesReport.TotalClass totalClass;


        public static void AddLabel(Grid section, int column, int row, string line, TextAlignment textAlignment, FontAttributes fontAttributes)
        {
            if (column == 0)
            {
                section.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
            }

            Label label = new()
            {

                Text = line,
                HorizontalTextAlignment = textAlignment,
                FontAttributes = fontAttributes
            };


            section.Add(label, column, row);
        }
        public static void AddLabel(Grid section, int column, int row, string line, TextAlignment textAlignment, FontAttributes fontAttributes, int[] color)
        {
            if (column == 0)
            {
                section.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
            }

            Label label = new()
            {

                Text = line,
                HorizontalTextAlignment = textAlignment,
                FontAttributes = fontAttributes,
                BackgroundColor = new Color(color[0], color[1], color[2]),
                FontSize = 16
            };
            section.Add(label, column, row);
        }

        public static string GetImageStreamAsBase64String(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                input.Position = 0;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                string prueba = Convert.ToBase64String(ms.ToArray());
                return prueba;
            }
        }

        public static ImageSource GetImageSourceFromString(string Base64Image)
        {
            byte[] bytes = System.Convert.FromBase64String(Base64Image);
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        public static void FillDemoReports()
        {
           
            FillReport();
            string repjson = totalClass.GetJsonObject();
            App.CashierData.AddNewReport(new Models.ReportZ() { Id = "01", CashierId = "1", Date = totalClass.Date, TotalClass = repjson }); ;
            App.CashierData.AddNewReport(new Models.ReportZ() { Id = "02", CashierId = "2", Date = totalClass.Date.AddDays(1), TotalClass = repjson });
            App.CashierData.AddNewReport(new Models.ReportZ() { Id = "03", CashierId = "3", Date = totalClass.Date.AddDays(2), TotalClass = repjson });
            App.CashierData.AddNewReport(new Models.ReportZ() { Id = "04", CashierId = "4", Date = totalClass.Date.AddDays(3), TotalClass = repjson });
            App.CashierData.AddNewReport(new Models.ReportZ() { Id = "05", CashierId = "5", Date = totalClass.Date.AddDays(4), TotalClass = repjson });
            List<ReportZ> list = App.CashierData.GetAllReports();


        }


#if ANDROID
        public static void SetNavFromNotification(Intent intent)
        {
            if (intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                {
                    if (key == "NavigationID")
                    {
                        string idValue = intent.Extras.GetString(key);
                        if (Preferences.ContainsKey("NavigationID"))
                            Preferences.Remove("NavigationID");

                        Preferences.Set("NavigationID", idValue);

                        WeakReferenceMessenger.Default.Send(new PushNotificationReceived("test"));

                    }
                }
            }
        }
#endif


        static void FillReport()
        {
            string prueba = string.Empty;
            PTDocsClasses.Currencies currencies = new();

            currencies.List.Add(new PTDocsClasses.Currency()
            {
                CurrencyCode = "01",
                CurrencyId = 1,
                CurrencyName = "Dollar",
                CurrencySymbol = "$",
                Factor = (decimal)11.7,
                FactorType = PTDocsClasses.Enums.FactorTypeCalculation.Divide,
                RoundingType = PTDocsClasses.Enums.RoundingType.Simple,
                Decimals = 2
            });
            currencies.List.Add(
                new PTDocsClasses.Currency()
                {
                    CurrencyCode = "02",
                    CurrencyId = 2,
                    CurrencyName = "Euro",
                    CurrencySymbol = "E",
                    Factor = (decimal)11.7,
                    FactorType = PTDocsClasses.Enums.FactorTypeCalculation.Divide,
                    RoundingType = PTDocsClasses.Enums.RoundingType.Simple,
                    Decimals = 2
                });
            currencies.List.Add(
                new PTDocsClasses.Currency()
                {
                    CurrencyCode = "03",
                    CurrencyId = 3,
                    CurrencyName = "Bolivares",
                    CurrencySymbol = "Bs.",
                    Factor = (decimal)0,
                    FactorType = PTDocsClasses.Enums.FactorTypeCalculation.Divide,
                    IsLocal = true,
                    RoundingType = PTDocsClasses.Enums.RoundingType.Simple,
                    Decimals = 2
                });

            totalClass = new PTDocsClasses.SalesReport.TotalClass();
            totalClass.IsZ = false;
            totalClass.Number = 111;
            totalClass.Cashier = "01";
            totalClass.User = "Jose";
            totalClass.Date = new DateTime(2023,04,10);

            totalClass.Load(currencies, PTDocsClasses.Enums.RoundingType.Simple,2, true,true,true,true);


            //Ingresos
            totalClass.TotalPayments.Add(new PTDocsClasses.SalesReport.TotalPayment() { Amount = 1500, Type = PTDocsClasses.Enums.PaymentType.Cash, Code = "01", Name = "Efectivo Bs", Currency = currencies.List[2] });
            totalClass.TotalPayments.Add(new PTDocsClasses.SalesReport.TotalPayment() { Amount = 300, Type = PTDocsClasses.Enums.PaymentType.Cash, Code = "02", Name = "Efectivo $", ConvertionAmount = 100, Currency = currencies.List[0] });

            //Ventas Realizadas PENDIENTE///

            //Ventas brutas totalClass.TaxReport.GrossSales
            // total impuestos totalClass.TaxReport.Taxes
            // 


            totalClass.ComitionsPayments.Add(new PTDocsClasses.SalesReport.ComitionPayment() { Name = "Tarjeta Debito", ComitionAmount = 15 });
            //Comisiones de tarjetas
            //
            double cardsComition = (double)totalClass.ComitionsPayments.Sum(o => o.ComitionAmount);



            double ventasNetas = (double)(totalClass.TaxReport.GrossSales - totalClass.TaxReport.Taxes);
            double ventasNetasTotal = (double)(totalClass.TaxReport.GrossSales - totalClass.TaxReport.Taxes) - cardsComition;

            totalClass.AddDocAmount(new PTDocsClasses.DocAmount(PTDocsClasses.Enums.RoundingType.Simple, 2, currencies) { Base = 1000, Tax = 16, TaxCode = "01", TaxName = "General", Total = 2000, TaxPerceived = false });
            totalClass.AddDocAmount(new PTDocsClasses.DocAmount(PTDocsClasses.Enums.RoundingType.Simple, 2, currencies) { Base = 250, Tax = 8, TaxCode = "02", TaxName = "Reducido", Total = 2000, TaxPerceived = false });
            totalClass.AddDocAmount(new PTDocsClasses.DocAmount(PTDocsClasses.Enums.RoundingType.Simple, 2, currencies) { Base = 700, Tax = 0, TaxCode = "03", TaxName = "Exento", Total = 700, TaxPerceived = false });

            totalClass.AddTotalItem(new PTDocsClasses.SalesReport.TotalItem() { GroupCode = "1" ,GroupName = "POLLOS",Amount=10,Code="1",Module= PTDocsClasses.Enums.AccountType.Table,Description="POLLO ASADO",Quantity=1,UserName="JOSE",DptoName="Dep 1",Doc=1});
            totalClass.AddTotalItem(new PTDocsClasses.SalesReport.TotalItem() { GroupCode = "2", GroupName = "HAMBURGUESAS", Amount = 12, Code = "2", Module = PTDocsClasses.Enums.AccountType.Table, Description = "HAMBURGUESA DE CARNE", Quantity = 1, UserName = "JOSE", DptoName = "Dep 2", Doc = 1 });
            totalClass.AddTotalItem(new PTDocsClasses.SalesReport.TotalItem() { GroupCode="1", GroupName = "POLLOS", Amount = 13, Code = "3", Module = PTDocsClasses.Enums.AccountType.Table, Description = "POLLO FRITO", Quantity = 1, UserName = "JOSE", DptoName = "Dep 3", Doc = 1 });

            //Items eliminados
            PTDocsClasses.SalesReport.AvoidItems avoidItemsp = new PTDocsClasses.SalesReport.AvoidItems() { Type = PTDocsClasses.Enums.TypeAvoidItem.Proccesed };
            avoidItemsp.Items = new List<PTDocsClasses.SalesReport.TotalItem>();
            avoidItemsp.Items.Add(new PTDocsClasses.SalesReport.TotalItem() { Amount = 2, Description = "POLLO ASADO", Quantity = 1, Doc = 55 });

            PTDocsClasses.SalesReport.AvoidItems avoidItemsn = new PTDocsClasses.SalesReport.AvoidItems() { Type = PTDocsClasses.Enums.TypeAvoidItem.Invoiced };
            avoidItemsn.Items = new List<PTDocsClasses.SalesReport.TotalItem>();
            avoidItemsn.Items.Add(new PTDocsClasses.SalesReport.TotalItem() { Amount = 2, Description = "POLLO ASADO", Quantity = 1, Doc = 55 });

            PTDocsClasses.SalesReport.AvoidItems avoidItemsf = new PTDocsClasses.SalesReport.AvoidItems() { Type = PTDocsClasses.Enums.TypeAvoidItem.NotProccesed };
            avoidItemsf.Items = new List<PTDocsClasses.SalesReport.TotalItem>();
            avoidItemsf.Items.Add(new PTDocsClasses.SalesReport.TotalItem() { Amount = 2, Description = "POLLO ASADO", Quantity = 1, Doc = 55 });

            totalClass.AvoidItems.Add(avoidItemsp);
            totalClass.AvoidItems.Add(avoidItemsn);
            totalClass.AvoidItems.Add(avoidItemsf);


            //Ventas gravables totalClass.TaxReport.TaxableSales; 
            //Ventas Exentas totalClass.TaxReport.NoTaxSales;
            //Ventas sin impuesto totalClass.TaxReport.SalesWithOutTaxes;

            // 
            //Vendedores
            totalClass.VendorsComitions.Add(new PTDocsClasses.SalesReport.VendorComitions() { Name = "Mortimer", Amount = 700, Comition = 15 });
            totalClass.VendorsComitions.Add(new PTDocsClasses.SalesReport.VendorComitions() { Name = "Jose", Amount = 800, Comition = 20 });
            decimal totalVentasporVendedor = totalClass.VendorsComitions.Sum(o => o.Amount);
            decimal totalComisionesporVendedor = totalClass.VendorsComitions.Sum(o => o.Comition);
            //Egresos
            totalClass.Fundraisings.Add(new PTDocsClasses.Fundraising() { Amount = 30, Type = PTDocsClasses.Enums.FundraisingType.BankDeposit, Description = "Depositos", Currency = currencies.List[0] });
            totalClass.Fundraisings.Add(new PTDocsClasses.Fundraising() { Amount = 30, Type = PTDocsClasses.Enums.FundraisingType.BankDeposit, Description = "Gastos", Currency = currencies.List[0] });
            totalClass.Fundraisings.Add(new PTDocsClasses.Fundraising() { Amount = 35, Type = PTDocsClasses.Enums.FundraisingType.BankDeposit, Description = "Depositos", Currency = currencies.List[1] });
            double totalEgresos = (double)totalClass.Fundraisings.Sum(o => o.Amount);

            totalClass.FromInvoce = 1;
            totalClass.ToInvoce = 100;

            totalClass.BaseIGTF = 10.80m;
            totalClass.AmountIGTF = 0.32m;

            
            
            totalClass.TotalCash = new PTDocsClasses.BaseList<PTDocsClasses.SalesReport.Cash>() { new PTDocsClasses.SalesReport.Cash() { Currency = currencies.List[2], Counted = 0, FoundRaising = 5, Initial = 10 } };


        }

    }



}

