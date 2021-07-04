using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using MaterialDesignThemes.Wpf;
using System.IO;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;

namespace ON3A_Restaurant
{
    /// <summary>
    /// Interaction logic for form1.xaml
    /// </summary>
    public partial class MainFRM : Window
    {

        DataTable test =new DataTable();
        public MainFRM()
        {
            InitializeComponent();
            clearGrids();


            fillgroub();

            fillgroub1();
            checkouthotry();
        }

        private void checkouthotry() {

            if(!( User.main_form == "Y" || User.main_form == "y" ))
                btn_main_form.Visibility = Visibility.Collapsed;
            if(!( User.casher_form == "Y" || User.casher_form == "y" ))
                btn_casher_form.Visibility = Visibility.Collapsed;
            if(!( User.kitchen_form == "Y" || User.kitchen_form == "y" ))
                btn_kitchen_form.Visibility = Visibility.Collapsed;
            if(!( User.report_form == "Y" || User.report_form == "y" ))
                btn_report_form.Visibility = Visibility.Collapsed;
            if(!( User.view_form == "Y" || User.view_form == "y" ))
                btn_view_form.Visibility = Visibility.Collapsed;


        }
        private void btn_main_form_Click(object sender, RoutedEventArgs e) {
            new BasicFRM().Show();
            this.Close();
        }

        private void btn_casher_form_Click(object sender, RoutedEventArgs e) {
            new MainFRM().Show();
            this.Close();
        }

        private void btn_kitchen_form_Click(object sender, RoutedEventArgs e) {
            new Orders_DONE().Show();
            this.Close();
        }

        private void btn_view_form_Click(object sender, RoutedEventArgs e) {
            new Orders().Show();
            this.Close();
        }

        private void btn_report_form_Click(object sender, RoutedEventArgs e) {
            new reportFRM().Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void btnMax_Click(object sender, RoutedEventArgs e) {

            this.WindowState = WindowState.Maximized;
            btnMax.Visibility = Visibility.Collapsed;
            btnRestore.Visibility = Visibility.Visible;

        }

        private void Restore_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Normal;
            btnMax.Visibility = Visibility.Visible;
            btnRestore.Visibility = Visibility.Collapsed;
        }

        private void btnMin_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            var move = sender as System.Windows.Controls.Grid;
            var win = Window.GetWindow(move);
            win.DragMove();

        }


        DB db = new DB();


//        CREATE TABLE PRODUCT (PRODUCT_ID INT,
//  NAME                 TEXT,
//  CODE                 TEXT,
//  PRODUCT_CATEGORY_ID  INT,
//  QTY                  REAL,
//  SALES_UNIT_PRICE     REal,
//  PURCHASE_UNIT_PRICE  REAL ,
//  CREATEDBY            TEXT ,
//  CREATEDDATE          TEXT,
//PRIMARY KEY("PRODUCT_ID")
//)
        void fillitems1(string groub_code)
        {
            DataTable dt = db.RunReader("select    PRODUCT_ID, NAME      from  PRODUCT where PRODUCT_CATEGORY_ID ='" + groub_code + "'");
            subgroup1.Children.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Group temp = new Group();

                temp.Margin = new Thickness(2);
                //temp.Background = Brushes.Purple;




                temp.Code.Content = dt.Rows[i][0].ToString();
                temp.Name.Content = dt.Rows[i][1].ToString();
                string filePath = Environment.CurrentDirectory + "\\items" + "\\" + dt.Rows[i][0].ToString() + ".jpg";
                if (File.Exists(filePath))
                    temp.pic.Source = new BitmapImage(new Uri(filePath));

                temp.btn.Click += (s, e) =>
                {

                    fillorders1(temp.Code.Content.ToString(), temp.pic.Source);
                };
                subgroup1.Children.Add(temp);
            }




        }

       void fillitems (string groub_code)
        {
            DataTable dt = db.RunReader("select    PRODUCT_ID, NAME      from  PRODUCT where PRODUCT_CATEGORY_ID ='"+groub_code+"'");
            subgroup.Children.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               
                Group temp = new Group();

                temp.Margin = new Thickness(2);
                //temp.Background = Brushes.Purple;

        


                temp.Code.Content = dt.Rows[i][0].ToString();
                temp.Name.Content = dt.Rows[i][1].ToString();
                string filePath = Environment.CurrentDirectory + "\\items" + "\\" + dt.Rows[i][0].ToString() + ".jpg";
                if (File.Exists(filePath))
                    temp.pic.Source = new BitmapImage(new Uri(filePath));

                temp.btn.Click += (s, e) =>
                {

                    fillorders(temp.Code.Content.ToString(), temp.pic.Source);
                };
   
                subgroup.Children.Add(temp);
            }




        }
       void totalprices1()
       {
           double totalsss = 0;
           foreach (ListBoxItem item in mainstackss1.Children)
           {

               order temp = item.Content as order;

               totalsss += Convert.ToDouble(temp.totalprice.Text.ToString());

           }
           txtAName3.Text = totalsss.ToString();
       }
        void totalprices()
       { double totalsss=0;
           foreach (ListBoxItem item in mainstackss.Children)
           {

               order temp = item.Content as order;

               totalsss += Convert.ToDouble(temp.totalprice.Text.ToString());
              
           }
           txtAName6.Text = totalsss.ToString();
       }
        private void fillorders1(string p, ImageSource picsource)
        {

            foreach (ListBoxItem item in mainstackss1.Children)
            {

                order temp = item.Content as order;
                if (temp.code.Content.ToString() == p)
                {
                    temp.txtQTY.Text = (Convert.ToInt32(temp.txtQTY.Text) + 1).ToString();
                    return;
                }
            }

            DataTable dt = db.RunReader("select    PRODUCT_ID, NAME,SALES_UNIT_PRICE      from  PRODUCT  where PRODUCT_ID ='" + p + "'");



            for (int i = 0; i < dt.Rows.Count; i++)
            {

                order aa = new order();
                aa.Name.Content = dt.Rows[i][1].ToString();
                aa.code.Content = dt.Rows[i][0].ToString();
                aa.price.Content = dt.Rows[i][2].ToString();
                aa.pic.Source = picsource;
                aa.txtQTY.Text = "2";
                aa.txtQTY.Text = "1";
                aa.totalprice.TextChanged += (s, e) =>
                {

                    totalprices1();
                };

                ListBoxItem cc = new ListBoxItem();
                cc.Margin = new Thickness(2);
               // cc.Background = Brushes.Yellow;
                cc.Content = aa;
                mainstackss1.Children.Add(cc);
                totalprices1();
            }

        }
     
       private void fillorders(string p,ImageSource picsource)
       {

           foreach (ListBoxItem item in mainstackss.Children)
           {

               order temp = item.Content as order;
               if (temp.code.Content.ToString() == p)
               {
                   temp.txtQTY.Text = (Convert.ToInt32(temp.txtQTY.Text) + 1).ToString();
                   return;
               }
           }
           
           DataTable dt = db.RunReader("select    PRODUCT_ID, NAME,SALES_UNIT_PRICE      from  PRODUCT  where PRODUCT_ID ='" + p + "'");

         
           
           for (int i = 0; i < dt.Rows.Count; i++)
           {

               order aa = new order();
               aa.Name.Content = dt.Rows[i][1].ToString();
               aa.code.Content = dt.Rows[i][0].ToString();
               aa.price.Content = dt.Rows[i][2].ToString();
               aa.pic.Source = picsource;
               aa.txtQTY.Text = "2";
               aa.txtQTY.Text = "1";
               aa.totalprice.TextChanged += (s, e) =>
               {

                   totalprices();
               };

               ListBoxItem cc = new ListBoxItem();
               cc.Margin = new Thickness(2);
              // cc.Background = Brushes.Yellow;
               cc.Content = aa;
             
               mainstackss.Children.Add(cc);
               totalprices();
           }

       }
       void fillgroub1()
       {

           DataTable dt = db.RunReader("select    PRODUCT_CATEGORY_ID, NAME      from  PRODUCT_CATEGORY");

           for (int i = 0; i < dt.Rows.Count; i++)
           {
               Group temp = new Group();

               temp.Margin = new Thickness(2);
               //temp.Background = Brushes.Purple;

               temp.btn.Click += (s, e) =>
               {

                   fillitems1(temp.Code.Content.ToString());
               };


               temp.Code.Content = dt.Rows[i][0].ToString();
               temp.Name.Content = dt.Rows[i][1].ToString();
               string filePath = Environment.CurrentDirectory + "\\gropitems" + "\\" + dt.Rows[i][0].ToString() + ".jpg";
               if (File.Exists(filePath))
                   temp.pic.Source = new BitmapImage(new Uri(filePath));


               maingroup1.Children.Add(temp);
           }

       }
        void fillgroub()
        {
            
            DataTable dt = db.RunReader("select    PRODUCT_CATEGORY_ID, NAME      from  PRODUCT_CATEGORY");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Group temp = new Group();

                temp.Margin = new Thickness(2);
                //temp.Background = Brushes.Purple;
             
                temp.btn.Click += (s,e) => {

                    fillitems(temp.Code.Content.ToString());
                };
            
      
                temp.Code.Content = dt.Rows[i][0].ToString();
                temp.Name.Content = dt.Rows[i][1].ToString();
                string filePath = Environment.CurrentDirectory + "\\gropitems" + "\\" + dt.Rows[i][0].ToString() + ".jpg";
                if (File.Exists(filePath))
                    temp.pic.Source = new BitmapImage(new Uri(filePath));

               
                maingroup.Children.Add(temp);
            }
           
        }

        //abdo
        void clearGrids()
        {
            g_cash.Visibility = Visibility.Collapsed;
            g_cash1.Visibility = Visibility.Collapsed;

            g_delevry.Visibility = Visibility.Collapsed;
            g_delevry1.Visibility = Visibility.Collapsed;



            g_TakeIn.Visibility = Visibility.Collapsed;
            g_cash_TakeIn.Visibility = Visibility.Collapsed;
        }
      
        private void LogOutBTN_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenMenueBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenMenueBTN.Visibility = Visibility.Collapsed;
            CloseMenueBTN.Visibility = Visibility.Visible;
        }

        private void CloseMenueBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenMenueBTN.Visibility = Visibility.Visible;
            CloseMenueBTN.Visibility = Visibility.Collapsed;

        }

      

        private void SwitchBTN_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        //abdo
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearGrids();
            switch (mainList.SelectedIndex)
            {
                case 0:

                       g_cash.Visibility = Visibility.Visible;
            g_cash1.Visibility = Visibility.Visible;
                    break;
                case 1:
                 //   g_Users.Visibility = Visibility.Visible;

                    g_delevry.Visibility = Visibility.Visible;
                                   g_delevry1.Visibility = Visibility.Visible;

                    txtAName9.Text = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT ").Rows[0][0].ToString();
                    break;
                case 2:

                    g_TakeIn.Visibility = Visibility.Visible;
                    g_cash_TakeIn.Visibility = Visibility.Visible;
                    break;
                case 3:
                    //g_Specialty.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
        }

    
        //private void btnPlus_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        int oldqty = 0;
        //        if (int.TryParse(txtQTY.Text, out oldqty))
        //        {
        //            txtQTY.Text = (oldqty + 1).ToString();
        //            lblTotalItemPrice.Content = (Convert.ToDouble(lblItemPrice.Content.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

        //        }
        //        else
        //        {
        //            txtQTY.Text = "1";
        //            lblTotalItemPrice.Content = lblItemPrice.Content;
        //            lblTotalItemPrice.Content = (Convert.ToDouble(lblItemPrice.Content.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

        //        }
        //    }
        //    catch { }
           
        //}

        //private void btnMinus_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        int oldqty = 0;
        //        if (int.TryParse(txtQTY.Text, out oldqty))
        //        {
        //            if (oldqty - 1 < 0)
        //                oldqty = 1;

        //            txtQTY.Text = (oldqty - 1).ToString();
        //            lblTotalItemPrice.Content = (Convert.ToDouble(lblItemPrice.Content.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

        //        }
        //        else
        //        {
        //            txtQTY.Text = "1";
        //            lblTotalItemPrice.Content = lblItemPrice.Content;
        //        }


            
           
                   
            
            
        //    }
        //    catch { }
        //}


        //private void txtQTY_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (lblTotalItemPrice == null)
        //            return;
        //        double price = 0;
        //        if (double.TryParse(lblItemPrice.Content.ToString(), out price))
        //        {


        //            lblTotalItemPrice.Content = (price * Convert.ToUInt32(txtQTY.Text)).ToString();



        //        }
        //        else
        //        {

        //            lblTotalItemPrice.Content = price.ToString();
        //        }
        //    }
        //    catch { }
        //}
        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9.]+");
            e.Handled = reg.IsMatch(e.Text);

        }
        private void NumberOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            e.Handled = reg.IsMatch(e.Text);

        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{


        //    order aa = new order("كشرى");
        //    ListBoxItem cc = new ListBoxItem();
        //    cc.Margin = new Thickness(2);
        //    cc.Background = Brushes.Yellow;
        //    cc.Content = aa;
        // aa.lblTotalItemPrice.Content.ToString();
        //    mainstackss.Children.Add(cc);

        //    foreach (ListBoxItem item in mainstackss.Children)
        //    {

        //        order temp = (order)item.Content;
        //        temp.lblTotalItemPrice.Content.ToString();
        //    }



        //}


        //private void btnMinus_Cliaack(object sender, RoutedEventArgs e,Label test)
        //{
        //    try
        //    {
        //        int oldqty = 0;
        //        if (int.TryParse(test.Content.ToString(), out oldqty))
        //        {
        //            if (oldqty - 1 < 0)
        //                oldqty = 1;

        //            test.Content = (oldqty - 1).ToString();
        //            test.Content = (Convert.ToDouble(test.Content.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

        //        }
        //        else
        //        {
        //            test.Content = "1";
        //            lblTotalItemPrice.Content = lblItemPrice.Content;
        //        }







        //    }
        //    catch { }
        //}

        //private void Button_Click_22(object sender, RoutedEventArgs e)
        //{

        //    foreach (ListBoxItem item in mainstackss.Children)
        //    {

        //        order temp = (order)item.Content;
        //     MessageBox.Show(   temp.lblTotalItemPrice.Content.ToString());
        //    }

        //}

        private void maingroup_TouchUp(object sender, TouchEventArgs e)
        {
            foreach (ListBoxItem item in maingroup.Children)
            {

                if (item.IsSelected == true)
                    MessageBox.Show("ss");
            }
        }

        private void maingroup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void maingroup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainstackss.Children.Clear();
            subgroup.Children.Clear();
            txtAName1.Text = "0";
            txtAName6.Text = "0";
            txtAName2.Text = "0";
        }

        private void txtAName1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtAName1.Text.Trim() == "")
                {
                    txtAName1.Text = "0";
                }
                txtAName2.Text = (Convert.ToDouble(txtAName1.Text) - Convert.ToDouble(txtAName15.Text)).ToString();
            }catch
            { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            try
            {

                string INVOICE_ID = db.RunReader("select coalesce(max(INVOICE_ID), '0')+1 from INVOICE").Rows[0][0].ToString();
                db.RunNonQuery(@"insert into INVOICE (INVOICE_ID,DATEINVOICED,TIMEINVOICED,TOTAL,PAID,INVOICE_TYPE,USER_ID   ,CREATEDBY, CREATEDDATE,INVOICE_STATUS,charge,discountpct,totalafterdisount)
values ('" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "','" + txtAName6.Text + "','" + txtAName1.Text + "','Cash','" + User.Code + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','inprogress','"+ txtAName2.Text+ "','"+ txtAName14 .Text+ "','"+ txtAName15 .Text+ "')");


                foreach (ListBoxItem item in mainstackss.Children)
                {

                    order temp = item.Content as order;

                    db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + temp.txtQTY.Text + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");

                }


                try
                {
                    DataTable dt = db.RunReader("select * from POS_RECIEPT_V  where INVOICE_ID='"+ INVOICE_ID + "'");
                    View_Report showreport = new View_Report();
                    crystalrep rpon = new crystalrep();
                    rpon.Database.Tables["POS_RECIEPT_V"].SetDataSource(dt);

                    //rpon.Parameter_discountpct.CurrentValues=
                    rpon.SetParameterValue("discountpct", txtAName14.Text.Trim());
                    rpon.SetParameterValue("totalafterdiscount ", txtAName15.Text.Trim());
                 //   rpon.Parameter_discountpct
                    showreport.crystalReportViewer1.ReportSource = rpon;
                    rpon.PrintToPrinter(2, true, 1, 1);
                      showreport.ShowDialog();
                    MessageBox.Show("تم عملية ");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

             
                mainstackss.Children.Clear();
                subgroup.Children.Clear();
                txtAName1.Text = "0";
                txtAName6.Text = "0";
                txtAName2.Text = "0";
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //public static void SetPrinterPort(string printerName, string portName)
        //{
        //    var oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
        //    oManagementScope.Connect();

        //    SelectQuery oSelectQuery = new SelectQuery();
        //    oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer 
        //    WHERE Name = '" + printerName.Replace("\\", "\\\\") + "'";

        //    ManagementObjectSearcher oObjectSearcher =
        //       new ManagementObjectSearcher(oManagementScope, @oSelectQuery);
        //    ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

        //    foreach (ManagementObject oItem in oObjectCollection)
        //    {
        //        oItem.Properties["PortName"].Value = portName;
        //        oItem.Put();
        //    }
        //}

        //abdo
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DataTable dt = db.RunReader("SELECT sum(totalafterdisount) from INVOICE where shiftdelivery='T' ");

            if(MessageBox.Show("اجمالى المبلغ فى الدرج"+Environment.NewLine+dt.Rows[0][0].ToString()+ Environment.NewLine + Environment.NewLine+"هل تريد تصفير الدرج ؟", "اجمالى المبلغ فى الدرج", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                db.RunNonQuery("UPDATE INVOICE set shiftdelivery='N'","تم تصفير الدرج بنجاح");
            }
               
            //try
                //{
                //    DataTable dt = db.RunReader("select * from POS_RECIEPT_V ");
                //    View_Report showreport = new View_Report();
                //    crystalrep rpon = new crystalrep();
                //    rpon.Database.Tables["POS_RECIEPT_V"].SetDataSource(dt);
                //    showreport.crystalReportViewer1.ReportSource = rpon;
                //    showreport.ShowDialog();
                //}
                //catch { }

            }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            mainstackss1.Children.Clear();
            subgroup1.Children.Clear();
            txtAName3.Text = "0";
            txtAName11.Text = "10";
            txtAName10.Text = "0";
       

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

            DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where (MOBILE='" + txtAName5.Text + "' or PHONE='" + txtAName5.Text + "') and CLIENT_ID='" + txtAName9.Text + "'");
            if(dt.Rows.Count <= 0) {

                MessageBox.Show("برجاء اختيار العميل");
                return;
            }

            try {

                string INVOICE_ID = db.RunReader("select coalesce(max(INVOICE_ID), '0')+1 from INVOICE").Rows[0][0].ToString();
                db.RunNonQuery(@"insert into INVOICE (INVOICE_ID,DATEINVOICED,TIMEINVOICED,TOTAL,PAID,INVOICE_TYPE,USER_ID   ,CREATEDBY, CREATEDDATE,INVOICE_STATUS,CLIENT_ID,discountpct,totalafterdisount,deliveryfee)
                values ('" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "','" + txtAName10.Text + "','0','Delivry','" + User.Code + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','inprogress','"+ txtAName9 .Text+ "','"+ txtAName12 .Text+ "','"+ txtAName3.Text+ "','"+ txtAName11 .Text+ "')");


                foreach(ListBoxItem item in mainstackss1.Children) {

                    order temp = item.Content as order;

                    db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE) 
                values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + temp.txtQTY.Text + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");

                }

                try
                {
                    DataTable dtdel = db.RunReader("select * from POS_RECIEPT_V  where INVOICE_ID='" + INVOICE_ID + "'");
                    View_Report showreport = new View_Report();
                    crystalrepdel rpon = new crystalrepdel();
                    rpon.Database.Tables["POS_RECIEPT_V"].SetDataSource(dtdel);

                    //rpon.Parameter_discountpct.CurrentValues=deliveryfee
                    rpon.SetParameterValue("deliveryfee", txtAName11.Text.Trim());
                    rpon.SetParameterValue("discountpct", txtAName12.Text.Trim());
                    rpon.SetParameterValue("totalafterdiscount ", txtAName10.Text.Trim());
                  
                    showreport.crystalReportViewer1.ReportSource = rpon;
                    rpon.PrintToPrinter(2, true, 1, 1);
                   showreport.ShowDialog();
                    MessageBox.Show("تم عملية ");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

             //   MessageBox.Show("تم عملية ");
                mainstackss1.Children.Clear();
                subgroup1.Children.Clear();
                txtAName3.Text = "0";
                txtAName10.Text = "0";




            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void savenewpaitient_Click(object sender, RoutedEventArgs e) {
    
        
       


            try {


                DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where (MOBILE='" + txtAName5.Text + "' or PHONE='" + txtAName5.Text + "') and CLIENT_ID='" + txtAName9.Text + "'");

                if(dt.Rows.Count > 0) {

                    db.RunNonQuery("update CLIENT set MOBILE='" + txtAName5.Text + "' , PHONE='" + txtAName8.Text + "' ,ADDRESS='" + txtAName7.Text + "' where CLIENT_ID='" + txtAName9.Text + "'","تم تحديث البيانات");

                    return;
                }
                string CLIENT_ID = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT ").Rows[0][0].ToString();
                if(txtAName5.Text.Trim() == "" && txtAName8.Text.Trim() == "") {

                    MessageBox.Show(" برجاء ادخال رقم الهاتف");
                    return;
                }

                if(txtAName7.Text.Trim() == "") {

                    MessageBox.Show(" برجاء ادخال العنوان");
                    return;
                }



                db.RunNonQuery("insert into CLIENT (CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE) values ('" + CLIENT_ID + "','" + txtAName4.Text.Trim() + "','"+ txtAName5 .Text.Trim()+ "','" + txtAName8.Text.Trim() + "','" + txtAName7.Text.Trim() + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')","تم حفظ البيانات");

                txtAName9.Text = CLIENT_ID;
            } catch { }

        }

      

        private void Button_Click_6(object sender, RoutedEventArgs e) {
            txtAName5.Text = "";
          
    
            txtAName9.Text = "";
            txtAName4.Text = "";
            txtAName7.Text = "";
            txtAName8.Text = "";
            txtAName9.Text = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT ").Rows[0][0].ToString();
        }

        private void txtAName5_TextChanged(object sender, TextChangedEventArgs e) {
            try {
                DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where MOBILE='" + txtAName5.Text + "' or PHONE='" + txtAName5.Text + "'");

                if(dt.Rows.Count > 0) {
                    txtAName7.Text = dt.Rows[0][4].ToString();
                    txtAName4.Text = dt.Rows[0][1].ToString();
                    txtAName8.Text = dt.Rows[0][3].ToString();
                    txtAName9.Text = dt.Rows[0][0].ToString();
                    // txtAName5.Text = dt.Rows[0][2].ToString();


                }
            } catch { }
        }

        private void txtAName11_TextChanged(object sender, TextChangedEventArgs e) {

            try {
                if(txtAName11.Text.Trim() == "")
                    txtAName11.Text = "0";

                if(txtAName12.Text.Trim() == "")
                    txtAName12.Text = "0";
                txtAName10.Text = ( ( Convert.ToDouble(txtAName3.Text) * ( ( 100 - Convert.ToDouble(txtAName12.Text) ) / 100 ) ) + Convert.ToDouble(txtAName11.Text) ).ToString();
            } catch { }
        }

        private void txtAName12_TextChanged(object sender, TextChangedEventArgs e) {
           

            try {
                if(txtAName11.Text.Trim() == "")
                    txtAName11.Text = "0";

                if(txtAName12.Text.Trim() == "")
                    txtAName12.Text = "0";
                txtAName10.Text = ( ( Convert.ToDouble(txtAName3.Text) * ( ( 100 - Convert.ToDouble(txtAName12.Text) ) / 100 ) ) + Convert.ToDouble(txtAName11.Text) ).ToString();
            } catch { }
        }

        private void txtAName3_TextChanged(object sender, TextChangedEventArgs e) {

            try {
                if(txtAName11.Text.Trim() == "")
                    txtAName11.Text = "0";

                if(txtAName12.Text.Trim() == "")
                    txtAName12.Text = "0";
                txtAName10.Text = ( ( Convert.ToDouble(txtAName3.Text) * ( ( 100 - Convert.ToDouble(txtAName12.Text) ) / 100 ) ) + Convert.ToDouble(txtAName11.Text) ).ToString();
            } catch { }
        }

        private void txtAName6_TextChanged(object sender, TextChangedEventArgs e) {
            try {
                if(txtAName6.Text.Trim() == "")
                    txtAName6.Text = "0";

                if(txtAName14.Text.Trim() == "")
                    txtAName14.Text = "0";
                txtAName15.Text = (  Convert.ToDouble(txtAName6.Text) * ( ( 100 - Convert.ToDouble(txtAName14.Text) ) / 100 )  ).ToString();
            } catch { }
        }

        private void txtAName14_TextChanged(object sender, TextChangedEventArgs e) {
            try {
                if(txtAName6.Text.Trim() == "")
                    txtAName6.Text = "0";

                if(txtAName14.Text.Trim() == "")
                    txtAName14.Text = "0";
                txtAName15.Text = ( Convert.ToDouble(txtAName6.Text) * ( ( 100 - Convert.ToDouble(txtAName14.Text) ) / 100 ) ).ToString();
            } catch { }
        }
    }
}
