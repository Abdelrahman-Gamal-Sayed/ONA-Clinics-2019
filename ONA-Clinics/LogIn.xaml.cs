using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ON3A_Restaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        
           

            validTest();
        }

        private void validTest() {

            string path = @"impor_×64.txt";
       
             
            if(File.Exists(path)) {
                using(StreamReader sr = File.OpenText(path)) {
                    string s;
                    while(( s = sr.ReadLine() ) != null ) {
                        if(s == "abdo") {

                            sr.Close();

                            using(StreamWriter sw = File.CreateText(path)) {
                                sw.WriteLine(Environment.MachineName);
                                return;
                            }
                        }
                        if(s != Environment.MachineName) {
                            MessageBox.Show("غير مسموح بتشغيل هذا البرنامج على هذا الجهاز"+Environment.NewLine+"للمزيد من المعلومات برجاء الاتصال ب 01211771638");
                
                            Application.Current.Shutdown();
                        }

                        return;
                    }
                }

            }

              

                if(!File.Exists(path)) {
                    MessageBox.Show("بعض الملفات محذوفة برجاء التأكد من الملفات كاملة");
                }
        

         
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            var move = sender as System.Windows.Controls.Grid;
            var win = Window.GetWindow(move);
            win.DragMove();

        }

        DB db = new DB();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            validationFun();


        }
        void validationFun() {

            try {


                DataTable s = db.RunReader("select USER_ID,EMPLOYEE_ID,USER_PASSWORD,USER_NAME,ACCESS_ID from USER where USER_NAME = '" + txtEName.Text + "'");
                if(s.Rows.Count <= 0) {
                    MessageBox.Show("Please check your Name");
                    return;
                }

                if(s.Rows[0][2].ToString() != PasswordBox.Password.ToString()) {
                    MessageBox.Show("Please check your Password");
                    return;
                }

                User.Code = s.Rows[0][0].ToString();
                User.Employee_ID = s.Rows[0][1].ToString();
                User.Name = s.Rows[0][3].ToString();
                User.ACCESS_ID = s.Rows[0][4].ToString();

                DataTable ss = db.RunReader("SELECT ACCESS_TYPE,main_form,casher_form,report_form,view_form,kitchen_form from USER_ACCESS WHERE USER_ACCESS_ID ='" + User.ACCESS_ID + "'");

                User.ACCESS_TYPE = ss.Rows[0][0].ToString();
                User.main_form = ss.Rows[0][1].ToString();
                User.casher_form = ss.Rows[0][2].ToString();
                User.report_form = ss.Rows[0][3].ToString();
                User.view_form = ss.Rows[0][4].ToString();
                User.kitchen_form = ss.Rows[0][5].ToString();



                if(User.main_form == "Y" || User.main_form == "y")
                    new BasicFRM().Show();
                else if(User.casher_form == "Y" || User.casher_form == "y")
                    new MainFRM().Show();

                else if(User.kitchen_form == "Y" || User.kitchen_form == "y")
                    new Orders_DONE().Show();
                else if(User.report_form == "Y" || User.report_form == "y")
                    new reportFRM().Show();
                else if(User.view_form == "Y" || User.view_form == "y")
                    new Orders().Show();

                else MessageBox.Show("برجاء المحاولة مرة اخرى");

                this.Close();
            } catch(Exception ex) { MessageBox.Show(ex.Message); }


        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key==Key.Enter)
            validationFun();
        }
    }
}
