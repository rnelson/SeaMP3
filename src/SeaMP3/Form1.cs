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
                MessageBox.Show("URL required.", "SeaMP3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var url = urlTextBox.Text;
            var result = Task.Run(async () => await Yarr.Download(url)).ConfigureAwait(true).GetAwaiter().GetResult();

            if (!result.Success)
            {
                MessageBox.Show(string.Join("\n", result.ErrorOutput), "SeaMP3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
