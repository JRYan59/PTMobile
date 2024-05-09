using PTDocsClasses.SalesReport;
using PTMobile.Functions;
using PTMobile.Models;
using System.Text.Json;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using TextAlignment = Microsoft.Maui.TextAlignment;
using iText.IO.Image;

namespace PTMobile;

public partial class Report : ContentPage
{
    PTDocsClasses.SalesReport.TotalClass totalClass;

    public string JsonString;

    public int rows = 0;
    public Report()
    {


        InitializeComponent();

        FillReport();

        //FillDatabase();
        AddIn.AddLabel(prueba, 0, 0, "Fecha: "+totalClass.Date.ToString("dd/MM/yyyy"), TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, 0, "Caja: "+totalClass.Cashier, TextAlignment.End, FontAttributes.None);
        rows++;
        #region Ingresos
        AddIn.AddLabel(prueba, 0, rows, "INGRESOS", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        rows++;
        for (int i = 0; i < totalClass.TotalPayments.Count; i++)
        {

            AddIn.AddLabel(prueba, 0, i + 2, totalClass.TotalPayments[i].Name, TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, i + 2, string.Format("{0:###,###,###,##0.00}", totalClass.TotalPayments[i].Amount).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;
            if (totalClass.TotalPayments[i].ConvertionAmount != 0)
            {
                AddIn.AddLabel(prueba, 0, rows, "    Conversion :", TextAlignment.Start, FontAttributes.Italic);
                AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", totalClass.TotalPayments[i].ConvertionAmount).PadLeft(18, ' '), TextAlignment.Start, FontAttributes.Italic);
                rows++;
            }
        }

        AddIn.AddLabel(prueba, 0, rows, "Sub-Total: ", TextAlignment.Start, FontAttributes.Bold);
        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", ((double)totalClass.TotalPayments.Sum(o => o.Amount))).PadLeft(18, ' '), TextAlignment.End, FontAttributes.Bold);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
        rows++;
        #endregion

        #region Egresos

        AddIn.AddLabel(prueba, 0, rows, "EGRESOS", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        rows++;

        foreach (var item2 in totalClass.Currencies.List)
        {
            var listFundRaisingsCurrency = totalClass.Fundraisings.FindAll(o => o.Currency == item2);
            if (listFundRaisingsCurrency.Count > 0)
            {
                AddIn.AddLabel(prueba, 0, rows, item2.CurrencyName, TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
                AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
                rows++;

                foreach (var item in listFundRaisingsCurrency)
                {
                    AddIn.AddLabel(prueba, 0, rows, item.Description + ":", TextAlignment.Start, FontAttributes.None);
                    AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", item.Amount).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
                    rows++;

                    AddIn.AddLabel(prueba, 0, rows, "   Conversion: ", TextAlignment.Start, FontAttributes.Italic);
                    AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", (item.Currency.CalculateConvert(item.Amount))).PadLeft(18, ' '), TextAlignment.Start, FontAttributes.Italic);
                    rows++;
                }

                AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
                rows++;

                AddIn.AddLabel(prueba, 0, rows, "EGRESOS " + item2.CurrencyName + ": ", TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", ((double)listFundRaisingsCurrency.Sum(o => o.Amount))).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
                rows++;

                AddIn.AddLabel(prueba, 0, rows, "   Conversion: ", TextAlignment.Start, FontAttributes.Italic);
                AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", (double)item2.CalculateConvert(listFundRaisingsCurrency.Sum(o => o.Amount))).PadLeft(18, ' '), TextAlignment.Start, FontAttributes.Italic);
                rows++;

            }


        }
        AddIn.AddLabel(prueba, 0, rows, "TOTAL EGRESOS: ", TextAlignment.Start, FontAttributes.Bold);
        AddIn.AddLabel(prueba, 1, rows, (string.Format("{0:###,###,###,##0.00}", ((double)totalClass.Fundraisings.Sum(o => o.Amount))).PadLeft(18, ' ')), TextAlignment.End, FontAttributes.Bold);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "TOTAL INGRESOS: ", TextAlignment.Start, FontAttributes.Bold);
        AddIn.AddLabel(prueba, 1, rows, (string.Format("{0:###,###,###,##0.00}", ((double)totalClass.TotalPayments.Sum(o => o.Amount)) - ((double)totalClass.Fundraisings.Sum(o => o.Amount)))).PadLeft(18, ' '), TextAlignment.End, FontAttributes.Bold);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
        rows++;
        #endregion;

        #region Informe General


        AddIn.AddLabel(prueba, 0, rows, "INFORME GENERAL", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        rows++;

        //report.AddLine("=", false, false, false, false);

        if ((totalClass.ToInvoce - totalClass.FromInvoce) > 0)
        {
            AddIn.AddLabel(prueba, 0, rows, "MEDIA POR FACTURA: ", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, (string.Format("{0:###,###,###,##0.00}", ((totalClass.TaxReport.GrossSales / (totalClass.ToInvoce - totalClass.FromInvoce)))).PadLeft(18, ' ')), TextAlignment.End, FontAttributes.None);
            rows++;

        }
        AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
        rows++;


        //AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
        //AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
        //rows++;

        AddIn.AddLabel(prueba, 0, rows, "VENTAS BRUTAS: ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", totalClass.TaxReport.GrossSales).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "(-) IMPUESTO: ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", totalClass.TaxReport.Taxes).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "Monto IGTF: ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", totalClass.AmountIGTF).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
        rows++;


        double cardsComition = (double)totalClass.ComitionsPayments.Sum(o => o.ComitionAmount);
        double ventasNetasTotal = (double)(totalClass.TaxReport.GrossSales - totalClass.TaxReport.Taxes) - cardsComition;

        AddIn.AddLabel(prueba, 0, rows, "VENTAS NETAS: ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", ventasNetasTotal).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "(-) COMISIONES TARJETAS:", TextAlignment.Start, FontAttributes.None);
        rows++;

        foreach (var item in totalClass.ComitionsPayments)
        {
            AddIn.AddLabel(prueba, 0, rows, item.Name + ": ", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", item.ComitionAmount).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;
        }

        AddIn.AddLabel(prueba, 0, rows, "TOTAL COMI.TARJETAS: ", TextAlignment.Start, FontAttributes.Bold);
        AddIn.AddLabel(prueba, 1, rows, (string.Format("{0:###,###,###,##0.00}", cardsComition).PadLeft(18, ' ')), TextAlignment.End, FontAttributes.Bold);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "TOTAL: ", TextAlignment.Start, FontAttributes.Bold);
        AddIn.AddLabel(prueba, 1, rows, (string.Format("{0:###,###,###,##0.00}", ventasNetasTotal - cardsComition).PadLeft(18, ' ')), TextAlignment.End, FontAttributes.Bold);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
        rows++;
        #endregion

        #region Ordenes Anuladas

        AddIn.AddLabel(prueba, 0, rows, "ORDENES ANULADAS", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        rows++;

        if(totalClass.CreditsNotes.Count() > 0)
        {
            AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
            AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
            rows++;

            foreach (var item in totalClass.CreditsNotes)
            {
                AddIn.AddLabel(prueba, 0, rows, "EMPLEADO: " + item.User, TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, "HORA: " + item.Date.Hour, TextAlignment.Start, FontAttributes.None);

            }
        }
        AddIn.AddLabel(prueba, 0, rows, totalClass.CreditsNotes.Count + " MONTO: ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, (string.Format("{0:###,###,###,##0.00}", totalClass.CreditsNotes.Sum(o => o.Total)).PadLeft(18, ' ')), TextAlignment.End, FontAttributes.None);
        rows++;

        if (totalClass.CreditsNotes.Count > 0)
        {
            AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
            AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
            rows++;

            foreach (var item in totalClass.CreditsNotes)
            {
                AddIn.AddLabel(prueba, 0, rows, "EMPLEADO: " +item.User, TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
                rows++;

                AddIn.AddLabel(prueba, 0, rows, "HORA: "+ item.Date.Hour, TextAlignment.End, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, "MONTO: "+ string.Format("{0:###,###,###,##0.00}", item.Total).PadLeft(18, ' '), TextAlignment.Start, FontAttributes.None);
                rows++;
            }
        }
        AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
        rows++;
        #endregion

        #region Ventas Por Usuario


        AddIn.AddLabel(prueba, 0, rows, "VENTAS POR USUARIO", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });

        rows++;

        foreach (var item in totalClass.GetSalesEmployees())
        {
            AddIn.AddLabel(prueba, 0, rows, item.User + ": ", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", item.Amount).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;
        }
        AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
        rows++;
        #endregion

        #region Informe Fiscal

        AddIn.AddLabel(prueba, 0, rows, "INFORME FISCAL", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "VENTAS BRUTAS: ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", totalClass.TaxReport.GrossSales).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "Base IGTF: ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", totalClass.BaseIGTF).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "Monto IGTF: ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", totalClass.AmountIGTF).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "VENTAS GRAVABLES: ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", totalClass.TaxReport.TaxableSales).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "Exento: ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", totalClass.TaxReport.NoTaxSales).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
        rows++;

        foreach (var item in totalClass.TaxReport.TotalByTaxes)
        {
            if (item.TaxAmount > 0)
            {

                AddIn.AddLabel(prueba, 0, rows, "Base " + item.TaxName + ": ", TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", item.Base).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
                rows++;

                AddIn.AddLabel(prueba, 0, rows, item.TaxName + ": ", TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", item.TaxAmount).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
                rows++;
            }

        }

        AddIn.AddLabel(prueba, 0, rows, "Vta. Sin IMP: ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", (totalClass.TaxReport.TotalByTaxes.Sum(o => o.Base))).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
        rows++;

        foreach (var item in totalClass.Currencies.List)
        {
            if (item.IsLocal)
            {
                AddIn.AddLabel(prueba, 0, rows, "Total " + item.CurrencySymbol + ":", TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", totalClass.TaxReport.GrossSales - totalClass.AmountIGTF).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
                rows++;
                break;
            }
        }

        AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 221, 221, 221 });
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 221, 221, 221 });
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "Desde Factura: " + totalClass.FromInvoce, TextAlignment.Start, FontAttributes.None);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, "Hasta Factura: " + totalClass.ToInvoce, TextAlignment.Start, FontAttributes.None);
        rows++;

        AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
        rows++;

        #endregion

        #region Cuadre de Caja

        AddIn.AddLabel(prueba, 0, rows, "CUADRE DE CAJA", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
        rows++;

        var cash = from items in totalClass.TotalPayments
                   where items.Type == PTDocsClasses.Enums.PaymentType.Cash
                   group items by items.Currency.CurrencyName into g
                   select new
                   { Name = g.Key, Amount = g.Sum(z => z.Amount) };

        foreach (var item in totalClass.TotalCash)
        {
            decimal total = 0;

            AddIn.AddLabel(prueba, 0, rows, item.Currency.CurrencyName, TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
            AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
            rows++;

            AddIn.AddLabel(prueba, 0, rows, "MONTO INICIAL: ", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", item.Initial).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;

            foreach (var item1 in cash)
            {

                if (item.Currency.CurrencyName == item1.Name)
                {
                    total = item1.Amount;
                    break;
                }

            }

            AddIn.AddLabel(prueba, 0, rows, "TOTAL EFECTIVO: ", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", total).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;

            AddIn.AddLabel(prueba, 0, rows, "TOTAL EGRESOS: ", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", item.FoundRaising).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;

            AddIn.AddLabel(prueba, 0, rows, "DEBE HABER: ", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", (total - item.FoundRaising)).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;

            AddIn.AddLabel(prueba, 0, rows, "EFECTIVO CONTADO: ", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", item.Counted).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;

            //new int[] { 170, 230, 115 }

            if ((item.Counted - (total - item.FoundRaising)) < 0)
            {
                AddIn.AddLabel(prueba, 0, rows, "FALTANTE EN CAJA: ", TextAlignment.Start, FontAttributes.None, new int[] { 245, 80, 80 });
                AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", (item.Counted - (total - item.FoundRaising))).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None, new int[] { 245, 80, 80 });
                rows++;
            }
            else
            {
                if ((item.Counted - (total - item.FoundRaising)) == 0)
                {
                    AddIn.AddLabel(prueba, 0, rows, "CUADRE EXACTO: ", TextAlignment.Start, FontAttributes.None, new int[] { 170, 230, 115 });
                    AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", (item.Counted - (total - item.FoundRaising))).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None, new int[] { 170, 230, 115 });
                    rows++;
                }
                if ((item.Counted - (total - item.FoundRaising)) > 0)
                {
                    AddIn.AddLabel(prueba, 0, rows, "SOBRANTE EN CAJA: ", TextAlignment.Start, FontAttributes.None, new int[] { 255, 219, 137 });
                    AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", (item.Counted - (total - item.FoundRaising))).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None, new int[] { 255, 219, 137 });
                    rows++;
                }
            }



            if (item != totalClass.TotalCash[totalClass.TotalCash.Count - 1])
            {
                AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
                AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
                rows++;
            }
        }
        AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
        rows++;
        #endregion

        #region Ventas Por Grupo

        
        try
        {
            var groupsByModules = totalClass.GetTotalGroupsByModules();

            if (groupsByModules != null && groupsByModules.Count() > 0)
            {
                AddIn.AddLabel(prueba, 0, rows, "VENTAS POR GRUPO", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
                AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
                rows++;

                AddIn.AddLabel(prueba, 0, rows, "DESCR", TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, "CANT.".PadLeft(8, ' ') + "MONTO".PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
                rows++;

                string moduleCurrent = "";
                decimal ModuleQty = 0;
                decimal ModuleAmount = 0;

                foreach (var group in groupsByModules)
                {
                    if (group.Module.ToString() != moduleCurrent)
                    {
                        if (ModuleQty != 0)
                        {
                            AddIn.AddLabel(prueba, 0, rows, "TOTAL MODULO: ", TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
                            AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###0.###}", ModuleQty).PadLeft(8, ' ') + " " + string.Format("{0:###,###,###,##0.00}", ModuleAmount).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None, new int[] { 125, 157, 156 });
                            rows++;

                            AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
                            AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
                            rows++;
                        }
                        AddIn.AddLabel(prueba, 0, rows, group.Module.ToString(), TextAlignment.Start, FontAttributes.None);
                        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
                        rows++;

                        //AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
                        //AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
                        //rows++;

                        moduleCurrent = group.Module.ToString();
                        ModuleQty+= group.Quantity;
                        ModuleAmount += group.Amount;

                    }
                    AddIn.AddLabel(prueba, 0, rows, group.Description, TextAlignment.Start, FontAttributes.None, new int[] { 125, 157, 156 });
                    AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###0.###}", group.Quantity).PadLeft(8, ' ') + " " + string.Format("{0:###,###,###,##0.00}", group.Amount).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None, new int[] { 125, 157, 156 });
                    rows++;

                    ModuleQty += group.Quantity;
                    ModuleAmount += group.Amount;

                }
                AddIn.AddLabel(prueba, 0, rows, "TOTAL GENERAL: ", TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", groupsByModules.Sum(o => o.Amount)).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
                rows++;
            }
        }
        catch (Exception e)
        {

            throw;
        }

        var groups = totalClass.GetTotalGroups();

        if (groups != null && groups.Count() > 0)
        {
            AddIn.AddLabel(prueba, 0, rows, "VENTAS POR GRUPO", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
            AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
            rows++;

            AddIn.AddLabel(prueba, 0, rows, "DESCR", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, "CANT.".PadLeft(8, ' ') + "MONTO".PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;

            foreach (var group in groups)
            {
                AddIn.AddLabel(prueba, 0, rows, group.Description, TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###0.###}", group.Quantity).PadLeft(8, ' ') + " " + string.Format("{0:###,###,###,##0.00}", group.Amount).PadLeft(18, ' '), TextAlignment.Start, FontAttributes.None);
                rows++;
            }
            AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
            rows++;

            AddIn.AddLabel(prueba, 0, rows, "TOTAL GENERAL: ", TextAlignment.Start, FontAttributes.Bold);
            AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", groups.Sum(o => o.Amount)).PadLeft(18, ' '), TextAlignment.Start, FontAttributes.None);
            rows++;


        }
        #endregion

        #region Ventas Por Items

        var TotalItems = totalClass.GetTotalItems();

        if(TotalItems != null && TotalItems.Count() > 0)
        {
            AddIn.AddLabel(prueba, 0, rows, "VENTAS POR ITEMS", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
            AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
            rows++;

            AddIn.AddLabel(prueba, 0, rows, "DESCR", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, "CANT.".PadLeft(8, ' ') + "MONTO".PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;

            var totalgroups = totalClass.GetTotalGroups();
            if (totalgroups.Count() > 0)
            {
                foreach (var item in totalClass.GetTotalGroups())
                {
                    AddIn.AddLabel(prueba, 0, rows, "***", TextAlignment.Start, FontAttributes.None);
                    AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
                    rows++;

                    AddIn.AddLabel(prueba, 0, rows, item.Description.Trim() + ":", TextAlignment.Start, FontAttributes.None);
                    AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
                    rows++;
                    foreach (var item1 in item.Items)
                    {
                        AddIn.AddLabel(prueba, 0, rows, item.Description, TextAlignment.Start, FontAttributes.None);
                        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###0.###}", item1.Quantity).PadLeft(8, ' ') + " " + string.Format("{0:###,###,###,##0.00}", item1.Amount).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
                        rows++;
                    }
                    AddIn.AddLabel(prueba, 0, rows, "***", TextAlignment.Start, FontAttributes.None);
                    AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
                    rows++;
                }

            }
            else
            {
                foreach (var item in TotalItems)
                {
                    AddIn.AddLabel(prueba, 0, rows, item.Description, TextAlignment.Start, FontAttributes.None);
                    AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###0.###}", item.Quantity).PadLeft(8, ' ') + " " + string.Format("{0:###,###,###,##0.00}", item.Amount).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
                    rows++;
                }
                
            }
            
            
        }
        #endregion

        #region Cuentas Abiertas

        if(totalClass.OpenAccounts !=  null && totalClass.OpenAccounts.Sum(o=>o.Amount) != 0)
        {
            AddIn.AddLabel(prueba, 0, rows, "CUENTAS ABIERTAS", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
            AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
            rows++;
            AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
            rows++;
            AddIn.AddLabel(prueba, 0, rows, "CUENTA", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, "MONTO + "  + "HORA" + "       EMPLEADO".PadLeft(8, ' ') + "MONTO".PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;

            var sections = from items in totalClass.GetTotalOpenAccount()
                           group items by items.Section into g
                           select new
                           {
                               Section = g.Key
                           };

            foreach (var item in sections)
            {
                AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
                rows++;
                AddIn.AddLabel(prueba, 0, rows, item.Section, TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
                rows++;
                foreach (var item1 in totalClass.GetTotalOpenAccount().Where(o=>o.Section == item.Section))
                {
                    AddIn.AddLabel(prueba, 0, rows, item1.AccountName, TextAlignment.Start, FontAttributes.None);
                    AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", item1.Amount).PadLeft(18, ' ') + " " + item1.Time + " " + item1.User, TextAlignment.Start, FontAttributes.None);
                    rows++;
                }
            }

        }

        #endregion

        #region Ventas Por Departamento
        var totalDptos = totalClass.GetTotalDepartments();

        if (totalDptos != null && totalDptos.Count()>0)
        {
            AddIn.AddLabel(prueba, 0, rows, "VENTAS POR DEP.", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
            AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
            rows++;

            foreach (var item in totalDptos)
            {
                AddIn.AddLabel(prueba, 0, rows, item.Description + ": ", TextAlignment.Start, FontAttributes.None);
                AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", item.Amount).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
                rows++;
            }
            AddIn.AddLabel(prueba, 0, rows, "TOTAL DEPARTAMENTOS: ", TextAlignment.Start, FontAttributes.Bold);
            AddIn.AddLabel(prueba, 1, rows, string.Format("{0:###,###,###,##0.00}", (totalDptos.Sum(o => o.Amount))).PadLeft(18, ' '), TextAlignment.End, FontAttributes.None);
            rows++;
        }
        

        #endregion

        #region Items Eliminados


        if (totalClass.AvoidItems != null && totalClass.AvoidItems.Exists(o => o.Items.Count() > 0))
        {
            AddIn.AddLabel(prueba, 0, rows, "ITEMS ELIMINADOS", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
            AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None, new int[] { 141, 203, 230 });
            rows++;

            AddIn.AddLabel(prueba, 0, rows, "FACTURA " + "DESCR", TextAlignment.Start, FontAttributes.None);
            AddIn.AddLabel(prueba, 1, rows, "CANT.".PadLeft(8, ' '), TextAlignment.End, FontAttributes.None);
            rows++;

            foreach (var avoidItems in totalClass.AvoidItems)
            {
                if (avoidItems.Items != null && avoidItems.Items.Count() > 0)
                {
                    string header = "Items Eliminados " + avoidItems.Type.ToString();

                    AddIn.AddLabel(prueba, 0, rows, header, TextAlignment.Start, FontAttributes.None);
                    rows++;
                    foreach (var item in avoidItems.Items)
                    {
                        AddIn.AddLabel(prueba, 0, rows, item.Doc.ToString().PadRight(7, ' ') + item.Description, TextAlignment.Start, FontAttributes.None);
                        AddIn.AddLabel(prueba, 1, rows, string.Format("{0:####.000}", item.Quantity).PadLeft(8, ' '), TextAlignment.End, FontAttributes.None);
                        rows++;
                    }

                }
                AddIn.AddLabel(prueba, 0, rows, "TOTAL ITEMS DEVUELTOS: ", TextAlignment.Start, FontAttributes.Bold);
                AddIn.AddLabel(prueba, 1, rows, string.Format("{0:####.000}", avoidItems.Items.Count()).PadLeft(8, ' '), TextAlignment.End, FontAttributes.None);
                rows++;
            }
        } 

        AddIn.AddLabel(prueba, 0, rows, " ", TextAlignment.Start, FontAttributes.None);
        AddIn.AddLabel(prueba, 1, rows, " ", TextAlignment.Start, FontAttributes.None);
        rows++;
        #endregion

    }


    void FillReport()
    {
        try
        {
            TotalClass tc = new TotalClass(/*new PTDocsClasses.Currencies(), PTDocsClasses.Enums.RoundingType.None, 2*/);
            tc = (TotalClass)tc.LoadFromJson(MainPage.FinalReport);
            totalClass = tc;

        }
        catch (Exception e)
        {

        }
    }

    void ImgToPdf(string strimage, string path)
    {
        double width = this.prueba.Width;
        double height = this.prueba.Height;
        var pgSize = new PageSize((float)width, (float)height);

        PdfWriter writer = new PdfWriter(path + @"\Reporte.pdf");
        PdfDocument pdf = new PdfDocument(writer);
        Document document = new Document(pdf, pgSize);


        ImageData data = ImageDataFactory.Create(strimage);
        iText.Layout.Element.Image image = new iText.Layout.Element.Image(data);
        // This adds the image to the page
        document.Add(image);

        document.Close();

    }

    async Task<string> TakeScreenAsync(View contenedor)
    {
        if (Screenshot.Default.IsCaptureSupported)
        {
            string path = "";
#if ANDROID
            path = FileSystem.Current.AppDataDirectory;
#endif
#if WINDOWS

                path= System.IO.Directory.GetCurrentDirectory();
                
#endif
            try
            {

                var screen = await contenedor.CaptureAsync();
                
                Stream stream = await screen.OpenReadAsync(ScreenshotFormat.Png, 100);
                string fileDestiny = System.IO.Path.Combine(path, "Capture.png");
                using (var fileStream = File.Create(fileDestiny))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
#if WINDOWS
ImgToPdf(fileDestiny,path);
System.IO.Directory.Delete(fileDestiny);
#endif
                return fileDestiny;

            }
            catch (Exception e)
            {

                string error = e.Message;
            }
        }
        
        return "";
    }


    async void ShareBtn_Clicked(object sender, EventArgs e)
    {
        var file = await TakeScreenAsync(this.prueba);

        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = "Share Screenshot",
            File = new ShareFile(file)
        });
    }
}

