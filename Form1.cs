namespace Notepad
{
    public partial class Form1 : Form
    {
        private float initialFontSize;
        public Form1()
        {
            InitializeComponent();
            textBox1.MouseWheel += textBox1_MouseWheel;
            initialFontSize = textBox1.Font.Size;
            this.Text = "Untitled";
        }

        public enum Theme
        {
            Light,
            Dark
        }

        private readonly Color lightBackColor = Color.White;
        private readonly Color lightForeColor = Color.Black;
        private readonly Color darkBackColor = Color.FromArgb(45, 45, 48);
        private readonly Color darkForeColor = Color.White;

        private void ApplyTheme(Theme theme)
        {
            if (theme == Theme.Light)
            {
                this.BackColor = lightBackColor;
                textBox1.BackColor = lightBackColor;
                textBox1.ForeColor = lightForeColor;
                zoomLevelLabel.BackColor = lightBackColor;
                zoomLevelLabel.ForeColor = lightForeColor;
            }
            else if (theme == Theme.Dark)
            {
                this.BackColor = darkBackColor;
                textBox1.BackColor = darkBackColor;
                textBox1.ForeColor = darkForeColor;
                zoomLevelLabel.BackColor = darkBackColor;
                zoomLevelLabel.ForeColor = darkForeColor;
            }
        }
        private float currentZoomLevel = 100;

        private void ZoomIn()
        {
            if (textBox1.Font.Size < 100)
            {
                textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size + 2);
                currentZoomLevel += 10;
                zoomLevelLabel.Text = $"{currentZoomLevel}%";
            }
        }

        private void ZoomOut()
        {
            if (textBox1.Font.Size > 2)
            {
                textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size - 2);
                currentZoomLevel -= 10;
                zoomLevelLabel.Text = $"{currentZoomLevel}%";
            }
        }

        private Theme currentTheme = Theme.Light;

        private void ToggleTheme()
        {
            currentTheme = (currentTheme == Theme.Light) ? Theme.Dark : Theme.Light;
            ApplyTheme(currentTheme);
        }

        private string currentFilePath = null;

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lightThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleTheme();
        }

        private void darkThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleTheme();
        }

        private void textBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                if (e.Delta > 0)
                {
                    ZoomIn();
                }
                else
                {
                    ZoomOut();
                }
            }
        }

        private void openToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog.FileName);
                textBox1.Text = sr.ReadToEnd();
                sr.Close();

                currentFilePath = openFileDialog.FileName;
                this.Text = System.IO.Path.GetFileNameWithoutExtension(currentFilePath);
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentFilePath != null)
            {
                System.IO.File.WriteAllText(currentFilePath, textBox1.Text);
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(saveFileDialog.FileName, textBox1.Text);
                    currentFilePath = saveFileDialog.FileName;
                }
            }
        }

        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    sw.Write(textBox1.Text);
                }
            }
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            currentFilePath = null;
            this.Text = "Untitled";
        }
    }
}