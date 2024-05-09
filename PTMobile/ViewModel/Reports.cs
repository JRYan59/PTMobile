using PTDocsClasses.SalesReport;
using PTMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.ViewModel
{
    public partial class Reports
    {
        DateTime Mindate = DateTime.Now;
        DateTime MaxDate = DateTime.Now.AddDays(1);
        List<string> Cashier = new List<string>();
        List<ReportZ> DataReport = new List<ReportZ>();
        public List<ReportZ> ReportList { get; set; } = new List<ReportZ>();





        public Reports(DateTime mindate, DateTime maxDate, List<string> cashier)
        {
            Mindate = mindate;
            MaxDate = maxDate;

            Cashier = cashier;

            GetDataReport();
            FilterReport();

            //TotalClass tc = new TotalClass(new PTDocsClasses.Currencies(), PTDocsClasses.Enums.RoundingType.None, 2);
            //tc = (TotalClass)tc.LoadFromJson(jsonTotalClass);



            //ReportList.Add(new ReportZ()
            //{
            //   CashierId="1",
            //   Date = totalClass.Date, 
            //   Number=2,
            //   TotalClass=jsonTotalClass 
            //}) ;


        }
        private void FilterReport()
        {
            ReportList.Clear();
            var filterRp = DataReport.Where(r => r.Date > Mindate && r.Date < MaxDate).ToList();

            if(Cashier.Count > 0)
            {
                foreach (var item in Cashier)
                {
                    foreach (var item1 in filterRp)
                    {
                        if (Convert.ToInt32(item1.CashierId) == Convert.ToInt32(item))
                        {
                            ReportZ rp = new ReportZ()
                            {
                                
                                Date = item1.Date,
                                CashierId = item1.CashierId,
                                TotalClass = item1.TotalClass,
                                Id = item1.Id,
                                DisplayDate = item1.Date.ToString("dd/MM/yyyy") + " Caja: "+item1.CashierId,
                                Number = item1.Number

                            };
                            ReportList.Add(rp);
                        }
                    }
                }
            }
            else
            {
                foreach (var item in filterRp)
                {
                    ReportZ rp = new ReportZ()
                    {

                        Date = item.Date,
                        CashierId = item.CashierId,
                        TotalClass = item.TotalClass,
                        Id = item.Id,
                        DisplayDate = item.Date.ToString("dd/MM/yyyy") + " Caja: " + item.CashierId,
                        Number = item.Number

                    };
                    ReportList.Add(rp);
                }
            }
        }

        private void GetDataReport()
        {
            
            //FillReport();
            //string repjson = totalClass.GetJsonObject();
            //App.CashierData.AddNewReport(new ReportZ() { Id = "01", CashierId = "01", Date = totalClass.Date, TotalClass = repjson }); ;
            //App.CashierData.AddNewReport(new ReportZ() { Id = "02", CashierId = "02", Date = totalClass.Date.AddDays(1), TotalClass = repjson });
            //App.CashierData.AddNewReport(new ReportZ() { Id = "03", CashierId = "03", Date = totalClass.Date.AddDays(2), TotalClass = repjson });

            DataReport = App.CashierData.GetAllReports();
        }

        //void FillReport()
        //{
        //    string prueba = string.Empty;
        //    PTDocsClasses.Currencies currencies = new();

        //    currencies.List.Add(new PTDocsClasses.Currency()
        //    {
        //        CurrencyCode = "01",
        //        CurrencyId = 1,
        //        CurrencyName = "Dollar",
        //        CurrencySymbol = "$",
        //        Factor = (decimal)11.7,
        //        FactorType = PTDocsClasses.Enums.FactorTypeCalculation.Divide,
        //        RoundingType = PTDocsClasses.Enums.RoundingType.Simple,
        //        Decimals = 2
        //    });
        //    currencies.List.Add(
        //        new PTDocsClasses.Currency()
        //        {
        //            CurrencyCode = "02",
        //            CurrencyId = 2,
        //            CurrencyName = "Euro",
        //            CurrencySymbol = "E",
        //            Factor = (decimal)11.7,
        //            FactorType = PTDocsClasses.Enums.FactorTypeCalculation.Divide,
        //            RoundingType = PTDocsClasses.Enums.RoundingType.Simple,
        //            Decimals = 2
        //        });
        //    currencies.List.Add(
        //        new PTDocsClasses.Currency()
        //        {
        //            CurrencyCode = "03",
        //            CurrencyId = 3,
        //            CurrencyName = "Bolivares",
        //            CurrencySymbol = "Bs.",
        //            Factor = (decimal)0,
        //            FactorType = PTDocsClasses.Enums.FactorTypeCalculation.Divide,
        //            IsLocal = true,
        //            RoundingType = PTDocsClasses.Enums.RoundingType.Simple,
        //            Decimals = 2
        //        });

        //    this.totalClass = new PTDocsClasses.SalesReport.TotalClass(currencies, PTDocsClasses.Enums.RoundingType.Simple, 2);
        //    totalClass.IsZ = false;
        //    totalClass.Number = 111;
        //    totalClass.Cashier = "01";
        //    totalClass.User = "Jose";
        //    totalClass.Date = DateTime.Now;

        //    totalClass.TotalPayments = new List<PTDocsClasses.SalesReport.TotalPayment>();

        //    //Ingresos
        //    totalClass.TotalPayments.Add(new PTDocsClasses.SalesReport.TotalPayment() { Amount = 1500, Type = PTDocsClasses.Enums.PaymentType.Cash, Code = "01", Name = "Efectivo Bs", Currency = currencies.List[2] });
        //    totalClass.TotalPayments.Add(new PTDocsClasses.SalesReport.TotalPayment() { Amount = 300, Type = PTDocsClasses.Enums.PaymentType.Cash, Code = "02", Name = "Efectivo $", ConvertionAmount = 100, Currency = currencies.List[0] });

        //    totalClass.TotalModules.Add(new PTDocsClasses.SalesReport.TotalModule(currencies.List) { Module = PTDocsClasses.Enums.AccountType.Direct, Amount = 10 });
        //    totalClass.TotalModules.Add(new PTDocsClasses.SalesReport.TotalModule(currencies.List) { Module = PTDocsClasses.Enums.AccountType.Delivery, Amount = 15 });
        //    totalClass.TotalModules.Add(new PTDocsClasses.SalesReport.TotalModule(currencies.List) { Module = PTDocsClasses.Enums.AccountType.Tab, Amount = 20 });
        //    totalClass.TotalModules.Add(new PTDocsClasses.SalesReport.TotalModule(currencies.List) { Module = PTDocsClasses.Enums.AccountType.Table, Amount = 5 });

        //    //Ventas Realizadas PENDIENTE///

        //    //Ventas brutas totalClass.TaxReport.GrossSales
        //    // total impuestos totalClass.TaxReport.Taxes
        //    // 


        //    totalClass.ComitionsPayments = new List<PTDocsClasses.SalesReport.ComitionPayment>();
        //    totalClass.ComitionsPayments.Add(new PTDocsClasses.SalesReport.ComitionPayment() { Name = "Tarjeta Debito", ComitionAmount = 15 });
        //    //Comisiones de tarjetas
        //    //
        //    double cardsComition = (double)totalClass.ComitionsPayments.Sum(o => o.ComitionAmount);



        //    double ventasNetas = (double)(totalClass.TaxReport.GrossSales - totalClass.TaxReport.Taxes);

        //    double ventasNetasTotal = (double)(totalClass.TaxReport.GrossSales - totalClass.TaxReport.Taxes) - cardsComition;

        //    totalClass.AddDocAmount(new PTDocsClasses.DocAmount(PTDocsClasses.Enums.RoundingType.Simple, 2, currencies) { Base = 1000, Tax = 16, TaxCode = "01", TaxName = "General", Total = 2000, TaxPerceived = false });
        //    totalClass.AddDocAmount(new PTDocsClasses.DocAmount(PTDocsClasses.Enums.RoundingType.Simple, 2, currencies) { Base = 250, Tax = 8, TaxCode = "02", TaxName = "Reducido", Total = 2000, TaxPerceived = false });
        //    totalClass.AddDocAmount(new PTDocsClasses.DocAmount(PTDocsClasses.Enums.RoundingType.Simple, 2, currencies) { Base = 700, Tax = 0, TaxCode = "03", TaxName = "Exento", Total = 700, TaxPerceived = false });

        //    totalClass.TotalGroups.Add(new PTDocsClasses.SalesReport.TotalGroup() { Amount = 1000, Description = "POLLOS", Quantity = 2 });

        //    totalClass.TotalGroups[0].Items = new List<PTDocsClasses.SalesReport.TotalItem>();
        //    totalClass.TotalGroups[0].Items.Add(new PTDocsClasses.SalesReport.TotalItem() { Amount = 500, Description = "POLLO ASADO", Quantity = 10 });
        //    totalClass.TotalGroups[0].Items.Add(new PTDocsClasses.SalesReport.TotalItem() { Amount = 100, Description = "POLLO A LA BROASTER", Quantity = 3 });

        //    totalClass.TotalGroups.Add(new PTDocsClasses.SalesReport.TotalGroup() { Amount = 800, Description = "HAMBURGUESAS", Quantity = 3 });



        //    totalClass.TotalDepartments.Add(new PTDocsClasses.SalesReport.TotalGroup() { Amount = 2000, Description = "CARNICERIA", Quantity = 3 });
        //    totalClass.TotalDepartments.Add(new PTDocsClasses.SalesReport.TotalGroup() { Amount = 1500, Description = "CHARCUTERIA", Quantity = 5 });

        //    //Items eliminados
        //    totalClass.AvoidItems.Add(new PTDocsClasses.SalesReport.TotalItem() { Amount = 2, Description = "POLLO ASADO", Quantity = 1, Doc = 55 });


        //    //Ventas gravables totalClass.TaxReport.TaxableSales; 
        //    //Ventas Exentas totalClass.TaxReport.NoTaxSales;
        //    //Ventas sin impuesto totalClass.TaxReport.SalesWithOutTaxes;

        //    // 
        //    //Vendedores
        //    totalClass.VendorsComitions.Add(new PTDocsClasses.SalesReport.VendorComitions() { Name = "Mortimer", Amount = 700, Comition = 15 });
        //    totalClass.VendorsComitions.Add(new PTDocsClasses.SalesReport.VendorComitions() { Name = "Jose", Amount = 800, Comition = 20 });
        //    decimal totalVentasporVendedor = totalClass.VendorsComitions.Sum(o => o.Amount);
        //    decimal totalComisionesporVendedor = totalClass.VendorsComitions.Sum(o => o.Comition);
        //    //Egresos
        //    totalClass.Fundraisings.Add(new PTDocsClasses.Fundraising() { Amount = 30, Type = PTDocsClasses.Enums.FundraisingType.BankDeposit, Description = "Depositos", Currency = currencies.List[0] });
        //    totalClass.Fundraisings.Add(new PTDocsClasses.Fundraising() { Amount = 30, Type = PTDocsClasses.Enums.FundraisingType.BankDeposit, Description = "Gastos", Currency = currencies.List[0] });
        //    totalClass.Fundraisings.Add(new PTDocsClasses.Fundraising() { Amount = 35, Type = PTDocsClasses.Enums.FundraisingType.BankDeposit, Description = "Depositos", Currency = currencies.List[1] });
        //    double totalEgresos = (double)totalClass.Fundraisings.Sum(o => o.Amount);

        //    totalClass.FromInvoce = 1;
        //    totalClass.ToInvoce = 100;

        //    totalClass.BaseIGTF = 10.80m;
        //    totalClass.AmountIGTF = 0.32m;

        //    totalClass.Invoices = new List<PTDocsClasses.InvoiceClass>();
        //    totalClass.Invoices.Add(new PTDocsClasses.InvoiceClass() { NameUser = "Jose" });

        //    totalClass.TotalCash = new List<PTDocsClasses.SalesReport.Cash>() { new PTDocsClasses.SalesReport.Cash() { Currency = currencies.List[2], Counted = 0, FoundRaising = 5, Initial = 10 } };

        //}
    }
}
