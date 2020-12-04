using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace solid53
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker;

        public MainWindow()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //רק הפונקציות שנרשמו לאירוע ProgressChanged ולאירוע RunWorkerCompleted  יכולות לבצע שינויים בממשק הגרפי בבטחה 

            worker.WorkerReportsProgress = true;//Set the WorkerReportsProgress property to true if you want the BackgroundWorker to support progress updates. When this property is true, user code can call the ReportProgress method to raise the ProgressChanged event.
            worker.WorkerSupportsCancellation = true;//Set the WorkerSupportsCancellation property to true if you want the BackgroundWorker to support cancellation. When this property is true, you can call the CancelAsync method to interrupt a background operation.

            // The default of both property is false
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // BackgroundWorker worker = sender as BackgroundWorker;

            int length = (int)e.Argument;

            for (int i = 1; i <= length; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    e.Result = stopwatch.ElapsedMilliseconds; // Unnecessary
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(500);
                    worker.ReportProgress(i * 100 / length); //מפעיל את ProgressChanged event.

                }
            }

            e.Result = stopwatch.ElapsedMilliseconds;
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            resultLabel.Content = (progress + "%");
            resultProgressBar.Value = progress;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                // e.Result throw System.InvalidOperationException
                resultLabel.Content = "Canceled!";
            }
            else if (e.Error != null)
            {
                // e.Result throw System.Reflection.TargetInvocationException
                resultLabel.Content = "Error: " + e.Error.Message; // Exception Message
            }
            else
            {
                long result = (long)e.Result;
                if (result < 1000)
                    resultLabel.Content = "Done after " + result + " ms.";
                else
                    resultLabel.Content = "Done after " + result / 1000 + " sec.";
            }
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy != true)
                worker.RunWorkerAsync(12); // Start the asynchronous operation.
        }

        //CancelAsync submits a request to terminate the pending background operation and sets the CancellationPending property to true.
        //When you call CancelAsync, your worker method has an opportunity to stop its execution and exit.The worker code should periodically check the CancellationPending property to see if it has been set to true
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
                worker.CancelAsync(); // Cancel the asynchronous operation.
        }
    }
}