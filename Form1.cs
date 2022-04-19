namespace Progress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Task ProcessData(List<string> list,IProgress<ProgressReport> progress)
        {
            int index = 1;
            int totalProcess = list.Count;
            var progressReport = new ProgressReport();
            return Task.Run(() =>
            {
                for (int i = 0; i < totalProcess; i++)
                {
                    progressReport.PercentComplete = index++ * 100 / totalProcess;
                    progress.Report(progressReport);
                    Thread.Sleep(50);
                }
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(i.ToString()); 
            }
            label1.Text = "Working...";
            var progress = new Progress<ProgressReport>();
            progress.ProgressChanged += (o, report) =>
            {
                    label1.Text = string.Format("Processing....{0}%", report.PercentComplete);
                    progressBar1.Value = report.PercentComplete;
                    progressBar1.Update();
            };
            await ProcessData(list, progress);
            label1.Text = "Done !";
        }
    }
}