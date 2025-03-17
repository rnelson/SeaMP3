namespace SeaMP3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(async () => await Yarr.Setup()).ConfigureAwait(true).GetAwaiter().GetResult();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(urlTextBox.Text))
            {
                MessageBox.Show("URL(s) required.", "SeaMP3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var urlList = urlTextBox.Text;
            Download(urlList.Split("\r\n"));
        }

        private void Download(string[] urls)
        {
            var failures = new Dictionary<string, string>();

            foreach (var url in urls)
            {
                var result = Task.Run(async () => await Yarr.Download(url))
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();

                if (!result.Success)
                {
                    failures.Add(url, string.Join("\n", result.ErrorOutput));
                }
            }

            if (failures.Count != 0)
            {
                foreach (var failure in failures)
                {
                    MessageBox.Show($"Failed to download {failure.Key}: \n\n{failure.Value}",
                        "SeaMP3",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
