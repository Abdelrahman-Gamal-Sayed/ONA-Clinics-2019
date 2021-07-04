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

namespace ON3A_Restaurant
{
    /// <summary>
    /// Interaction logic for form1.xaml
    /// </summary>
    public partial class reportFRM : Window
    {

        DataTable test =new DataTable();
        public reportFRM()
        {
            InitializeComponent();
            clearGrids();

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

        void clearGrids()
        {
            g_mior_date_all.Visibility = Visibility.Collapsed;
            g_mior_date.Visibility = Visibility.Collapsed;
            g_mor_date_main.Visibility = Visibility.Collapsed;
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
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearGrids();
            switch (mainList.SelectedIndex)
            {
                case 0:
                    break;
                case 1:

                    g_mior_date.Visibility = Visibility.Visible;
                    g_mor_date_main.Visibility = Visibility.Visible;
                    break;
                case 2:

                    g_mior_date_all.Visibility = Visibility.Visible;
                    break;
                case 3:
                    // g_Specialty.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
        }


        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            g_mior_date.CleanAll();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            g_mor_date_main.CleanAll();
        }
        DB db = new DB();
        private void cbx_osserty_user_DropDownOpened(object sender, EventArgs e)
        {
            try {
                cbx_osserty_user.ItemsSource = db.RunReader("select USER_ID,USER_NAME       from USER  ").DefaultView;

            }
            catch { }
               }

        private void cbx_osserty_user2_DropDownOpened(object sender, EventArgs e)
        {
            cbx_osserty_user2.ItemsSource = db.RunReader("select PRODUCT_CATEGORY_ID,NAME from PRODUCT_CATEGORY  ").DefaultView;
        }

        private void cbx_osserty_user1_DropDownOpened(object sender, EventArgs e)
        {
            cbx_osserty_user1.ItemsSource = db.RunReader("select PRODUCT_ID ,NAME from PRODUCT  where  PRODUCT_CATEGORY_ID='"+ cbx_osserty_user2 .Text+ "'   ").DefaultView;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            String wherequn = "";
            //string datatfrom = (txt_parcedate_emp2.Text.Trim() == string.Empty) ? "" : Convert.ToDateTime( txt_parcedate_emp2.Text.Trim()).ToString("yyyy-MM-dd HH:mm:ss");
            //string datatto = (txt_parcedate_emp2.Text.Trim() == string.Empty) ? "" : Convert.ToDateTime(txt_parcedate_emp2.Text.Trim()).ToString("yyyy-MM-dd HH:mm:ss");
            ////db.RunNonQuery("CREATE VIEW POS_RECIEPT_V AS SELECT INV.INVOICE_ID,      INV.DATEINVOICED,      inv.TIMEINVOICED,     inv.PAID,      INV.total,      inv.charge,      inv.USER_ID,      inv.INVOICE_STATUS,      INVOICE_TYPE,      u.user_name cashir,      inv.CLIENT_ID,      cl.name,       cl.NAME,      cl.MOBILE,      cl.PHONE,      cl.ADDRESS CLIENTaddress,      invl.INVOICE_LINE_ID,      invl.PRODUCT_ID,      invl.QTY,      invl.TOTALLINE,      p.name productname,       p.PRODUCT_CATEGORY_ID,      p.SALES_UNIT_PRICE,      p.PURCHASE_UNIT_PRICE,      invl.QTY * p.SALES_UNIT_PRICE linetotalamt,      pc.name categoryname FROM invoice inv      inner join INVOICE_LINE invl on invl.invoice_id = inv.INVOICE_id      inner join PRODUCT p on p.PRODUCT_id = invl.PRODUCT_id      inner join PRODUCT_CATEGORY pc on pc.PRODUCT_CATEGORY_id = p.PRODUCT_CATEGORY_id      LEFT JOIN USER u ON u.user_id = inv.user_id      LEFT JOIN CLIENT cl ON cl.client_id = inv.CLIENT_id       ");

            if (txt_parcedate_emp2.Text.Trim() != string.Empty)
                wherequn = wherequn + " and DATE( DATEINVOICED)  >=coalesce( DATE(  '" + Convert.ToDateTime(txt_parcedate_emp2.Text).ToString("yyyy-MM-dd HH:mm:ss") + "'),DATEINVOICED)";
            if (txt_parcedate_e2mp2.Text.Trim() != string.Empty)
                wherequn = wherequn + " and DATE( DATEINVOICED)  <=  DATE('" + Convert.ToDateTime(txt_parcedate_e2mp2.Text).ToString("yyyy-MM-dd HH:mm:ss") + "')";
            if (cbx_osserty_user1.Text.Trim() != string.Empty)
                wherequn = wherequn + " and  product_id  ='" + cbx_osserty_user1.Text + "'";
            if (cbx_osserty_user2.Text.Trim() != string.Empty)
                wherequn = wherequn + " and  PRODUCT_CATEGORY_id  ='" + cbx_osserty_user2.Text + "'";


            dg_user_main1.ItemsSource= db.RunReader(@"select sum(qty) qty , sum(totalline) totaline , product_id , productname  ,DATEINVOICED
from POS_RECIEPT_V
where ''=''      " + wherequn + " group by product_id, productname ,DATEINVOICED").DefaultView;

            // and issotrx = coalesce('', issotrx) and product_id = coalesce('" + cbx_osserty_user1.Text + "', product_id)   and invoice_type = coalesce('', invoice_type) and PRODUCT_CATEGORY_id = coalesce('" + cbx_osserty_user2.Text + "', PRODUCT_CATEGORY_id)
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string wherequn = " " ;
            if (cbx_osserty_user.Text.Trim() != string.Empty)
                wherequn= wherequn + " and  USER_ID  ='" + cbx_osserty_user.Text + "'";

            if (txt_parcedate_emp.Text.Trim() != string.Empty)
                wherequn = wherequn + " and DATE( DATEINVOICED)  >=coalesce( DATE(  '" +Convert.ToDateTime( txt_parcedate_emp.Text).ToString("yyyy-MM-dd HH:mm:ss") + "'),DATEINVOICED)";
            if (txt_parcedate_emp1.Text.Trim() != string.Empty)
                wherequn = wherequn + " and DATE( DATEINVOICED)  <=  DATE('" + Convert.ToDateTime(txt_parcedate_emp1.Text).ToString("yyyy-MM-dd HH:mm:ss") + "')";


            dg_user.ItemsSource = db.RunReader("SELECT INVOICE_ID as 'رقم الفاتورة' ,        DATEINVOICED as 'تاريخ الفاتورة',      TIMEINVOICED as 'وقت الفاتورة',      PAID as 'المدفوع',  charge as 'الباقي',   total as 'اجمالي الفاتورة',      cashir as 'الكاشير' ,name as 'اسم العميل' from POS_RECIEPT_V where ''=''  " + wherequn).DefaultView;
        }

      
        private void dg_user_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (dg_user.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_user.SelectedItem;
                    dg_user_Copy.ItemsSource = db.RunReader("SELECT        PRODUCT_ID  as 'كود الصنف',      QTY as 'الكمية',       TOTALLINE as 'اجمالي الصنف',       productname as 'اسم الصنف',        SALES_UNIT_PRICE  as 'سعر الوحدة' from POS_RECIEPT_V where INVOICE_ID='" + row[0].ToString() + "'  ").DefaultView;
                }
            }
            catch { }
        }

        private void dg_user_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            g_mior_date_all.CleanAll();
        }
    }
}
