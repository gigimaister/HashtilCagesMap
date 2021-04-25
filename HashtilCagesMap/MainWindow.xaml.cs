using HashtilCagesMap.DataBase;
using HashtilCagesMap.Entities;
using HashtilCagesMap.ViewModel;
using Microsoft.Maps.MapControl.WPF;
using Syncfusion.Data;
using Syncfusion.SfSkinManager;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HashtilCagesMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        TotalCageCx total = new TotalCageCx();
        ObservableCollection<CageAudit> cageAudits = new ObservableCollection<CageAudit>();
        public ObservableCollection<CageAudit> cageAuditClasses;
        public ObservableCollection<TotalCageCx> totalCageCxes;
        DBAsync dbA = new DBAsync();
        DBManagerOld dbManagerOld = new DBManagerOld();
        int status;
        private static System.Timers.Timer timer;
        public MainWindow()
        {

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("he-IL");
            InitializeComponent();
            UserLogin ul = new UserLogin();


           
            Dispatcher.Invoke(()=> PopulateMainGrid());
            


            timer = new System.Timers.Timer();
            timer.Interval = 120000;

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

           
            

            sfdg.FilterChanged += DataGrid_FilterChanged;
        }

       
        public void  StartAllBusyIndicatord()
        {
            Dispatcher.InvokeAsync(() => { busyrefresh.IsBusy = true;
                busyrefreshcx.IsBusy = true;
                busyrefreshcagecost.IsBusy = true;
                }
            );
           
           
        }
        public void StopAllBusyIndicatord()
        {
            Dispatcher.InvokeAsync(() => {
                busyrefresh.IsBusy = false;
                busyrefreshcx.IsBusy = false;
                busyrefreshcagecost.IsBusy = false;
            }
             );

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            PopulateMainGrid();
        }

        private void drivers_only(object sender, RoutedEventArgs e)
        {

            Mainwindow();
        }
        public void SetPinsOnMap(ObservableCollection<CageAudit> cageAudit)
        {

            this.Dispatcher.BeginInvoke(new Action(() =>
            {

                foreach (var cage in cageAudit)
                {


                    Location location = new Location(cage.Lan,cage.Lon);

                    Pushpin pin = new Pushpin
                    {
                        Location = (location)



                    };
                    ToolTipService.SetToolTip(pin, "שם לקוח: " + cage.Cx + "\n" + "נהג: " + cage.User + "\n" + "מצב: " + cage.Status + "\n" + "כמות כלובים: " + cage.NumOfCage+"\n"
                        +"תאריך: "+cage.Date.ToString("dd/MM/yyyy") + "\n"+"שעה: "+cage.Time);

                    myMap.Children.Add(pin);

                }
            }));
        }

        public async void PopulateMainGrid()
        {
            try
            {

                await Dispatcher.InvokeAsync(async () => 
                {
                    busyrefresh.IsBusy = true;
                    busyrefreshcx.IsBusy = true;
                    busyrefreshcagecost.IsBusy = true;
                    var MainCageTable = await CageAuditTableJson.CageAuditFetchJson(1);
                    sfdg.ItemsSource = MainCageTable;
                    SfSkinManager.SetVisualStyle(sfdg, VisualStyles.MaterialLightBlue);


                    myMap.Children.Clear();

                    var CageAuditCalcTable = await CageAuditTableJson.CageAuditFetchJson(2);
                    sfcagecalc.ItemsSource = CageAuditCalcTable;
                    SfSkinManager.SetVisualStyle(sfcagecalc, VisualStyles.Metro);

                    var CxListTable = await CxListJson.FetchCxListPhp(1);
                    sfdg_cx.ItemsSource = CxListTable;
                    SfSkinManager.SetVisualStyle(sfdg_cx, VisualStyles.Lime);

                    if (cageAudits.Count > 0)
                    {
                        SetPinsOnMap(cageAudits);
                    }
                    else
                    {

                        SetPinsOnMap(CageAuditCalcTable);

                    }
                    sfcb.ItemsSource = CxListTable;
                    this.sfcb.DisplayMemberPath = "CxName";

                    busyrefresh.IsBusy = false;
                    busyrefreshcx.IsBusy = false;
                    busyrefreshcagecost.IsBusy = false;

                }
           );
                
               
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public  void Mainwindow()
        {
            try
            {
                PopulateMainGrid();                             
            }
            catch (Exception l)
            {
                MessageBox.Show(l.ToString());
            }
        }

        private void DataGrid_FilterChanged(object sender, GridFilterEventArgs e)
        {
            myMap.Children.Clear();
            cageAudits.Clear();
            RecordsList records = new RecordsList();
            ///records.Clear();

            // Get filtered records
            records = (sender as SfDataGrid).View.Records;

            foreach (RecordEntry record in records)
                cageAudits.Add(record.Data as CageAudit);
          
            SetPinsOnMap(cageAudits);
            
        }



        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            
            {
                
                PopulateMainGrid();
                

            }

            
        }

       

        private void btnprint_Click(object sender, RoutedEventArgs e)
        {
            sfdg.PrintSettings = new PrintSettings();
            sfdg.PrintSettings.PrintFlowDirection = (FlowDirection)1;
            //sfdg.FlowDirection= (FlowDirection)1;
            
            sfdg.ShowPrintPreview();
        }

        private void btnprintcx_Click(object sender, RoutedEventArgs e)
        {
            sfdg_cx.PrintSettings = new PrintSettings();
            sfdg_cx.PrintSettings.PrintFlowDirection = (FlowDirection)1;
            //sfdg.FlowDirection= (FlowDirection)1;
            
            sfdg_cx.ShowPrintPreview();
        }
        private void btnprintcagecost_Click(object sender, RoutedEventArgs e)
        {
            sfcagecalc.PrintSettings = new PrintSettings();
            sfcagecalc.PrintSettings.PrintFlowDirection = (FlowDirection)1;
            //sfdg.FlowDirection= (FlowDirection)1;
            
            sfcagecalc.ShowPrintPreview();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void sfcb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
           

                Object selectedItem = sfcb.SelectedItem;
                List<Object> lists = new List<Object>();

            if (selectedItem == null)
            {
                return;
            }
           
            try
            {
                Type t = selectedItem.GetType() ;
                PropertyInfo[] props = t.GetProperties();

                foreach (var i in props)
                {
                    var a = i.GetValue(selectedItem);
                    lists.Add(a);
                }
                total.CxId = Convert.ToInt32(lists[0]);
                total.CxName = Convert.ToString(lists[1]);
                total.TotalCages = Convert.ToInt32(lists[2]);
            }
            catch (NullReferenceException)
            {

            }

        }
        
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (userlevel.Text == "0" || userlevel.Text=="1")
            {
                Object selectedItem = sfcb.SelectedItem;
                if (selectedItem == null)
                {

                    AlertBox alertBox = new AlertBox();
                    alertBox.tb.Text = "לא נבחר לקוח!";
                    alertBox.Show();
                    return;
                }
                if (tbunocage.Text != "")
                {
                    SelectBox selectBox = new SelectBox();
                    selectBox.tb.Text = $"  האם לעדכן לקוח \n {total.CxName} \n עם {tbunocage.Text}  כלובים? ";

                    selectBox.ShowDialog();


                    if (selectBox.DialogResult == true)
                    {
                        busyrefreshcx.IsBusy = true;

                        this.Dispatcher.Invoke(() =>
                        {

                            dbManagerOld.Crud($"UPDATE fixed_cx_list SET total_cage={tbunocage.Text} WHERE id={total.CxId}");

                        });


                        await Task.Run(() =>
                        {

                            Mainwindow();

                        });
                        sfcb.SelectedItem = 0;
                        tbunocage.Clear();


                        busyrefreshcx.IsBusy = false;
                    }
                    else
                    {
                        sfcb.SelectedItem = -1;
                        tbunocage.Clear();
                        return;
                    }


                }
                else
                {
                    AlertBox alertBox = new AlertBox();
                    alertBox.tb.Text = "יש להזין כמות כלובים!";
                    alertBox.Show();
                }
            }
            else
            {
                AlertBox alertBox = new AlertBox();
                alertBox.tb.Text = "משתמש לא מורשה לביצוע פעולה זו!";
                alertBox.Show();
                sfcb.SelectedItem = -1;
                tbunocage.Clear();
            }
        }

       
       private async void btn_update_cx(object sender, RoutedEventArgs e)
        {
            if (userlevel.Text == "0" || userlevel.Text == "1")
            {
                if (tbcxupdate.Text != "")
                {
                    SelectBox selectBox = new SelectBox();
                    selectBox.tb.Text = $"האם לעדכן לקוח חדש בשם:\n{tbcxupdate.Text}?";
                    selectBox.ShowDialog();

                    if (selectBox.DialogResult == true)
                    {
                        busyrefreshcx.IsBusy = true;

                        this.Dispatcher.Invoke(() =>
                        {

                            dbManagerOld.Crud($"INSERT INTO fixed_cx_list(cx) VALUES('{tbcxupdate.Text}');");

                        });


                        await Task.Run(() =>
                        {

                            Mainwindow();

                        });
                        sfcb.SelectedItem = -1;
                        tbunocage.Clear();
                        tbcxupdate.Text = "";


                        busyrefreshcx.IsBusy = false;
                    }
                    else
                    {
                        
                        return;
                    }


                }
                else
                {
                    AlertBox alertbox = new AlertBox();
                    alertbox.tb.Text = "לא הוזן ערך!";
                    alertbox.ShowDialog();
                }
            }
            else
            {
                AlertBox alertBox = new AlertBox();
                alertBox.tb.Text = "משתמש לא מורשה!";
                alertBox.ShowDialog();
            }
            
        }

        private void GridTextColumn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void TboxCX_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.sfdg_cx.SearchHelper.AllowFiltering = true;
            this.sfdg_cx.SearchHelper.Search(TboxCX.Text);
        }

        private void TBoxCageCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.sfcagecalc.SearchHelper.AllowFiltering = true;
            this.sfcagecalc.SearchHelper.Search(TBoxCageCost.Text);
        }
        private void TBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.sfdg.SearchHelper.AllowFiltering = true;
            this.sfdg.SearchHelper.Search(TBox.Text);
        }

        private void btnchange_map_mode(object sender, RoutedEventArgs e)
        {
            if (myMap.Mode is RoadMode)
            {
                //Set the map mode to Aerial with labels
                myMap.Mode = new AerialMode(true);
            }
            else
            {
                myMap.Mode = new RoadMode();
            }
        }
    }
}
