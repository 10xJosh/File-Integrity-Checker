using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileIntegrityChecker
{
    public partial class FileIntegrityChecker : Form
    {
        private string path { get; set; }

        public FileIntegrityChecker()
        {
            InitializeComponent();
            cmbHashList.Items.Add("MD5");
            cmbHashList.Items.Add("SHA-1");
            cmbHashList.Items.Add("SHA-256");
            cmbHashList.Items.Add("SHA-516");
            cmbHashList.Items.Add("CRC32");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select a file";
                openFileDialog.ShowDialog();
                
                if(openFileDialog.FileName != "")
                {
                    txtBrowse.Text = openFileDialog.FileName;
                    path = openFileDialog.FileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong when trying to open the File. Please try again.",
                    "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtResult.Text != "")
            {
                Clipboard.SetText(txtResult.Text);
                btnCopy.Enabled = false;
            }
            else
            {
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtResult.Text = string.Empty;
            btnClear.Enabled = false;
        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            if(txtResult.Text == string.Empty)
            {
                btnClear.Enabled = false;
                btnCopy.Enabled= false;
                txtResult.Enabled = false;
            }
            else
            {
                btnClear.Enabled = true;
                btnCopy.Enabled = true;
                txtResult.Enabled = true;
            }
        }

        private async void btnVerifyFile_Click(object sender, EventArgs e)
        {
            SetProgressBarProgress(0);

            if (txtBrowse.Text == string.Empty)
            {
                MessageBox.Show("Please select a file to validate.", "File Not Selected",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cmbHashList.Text == "MD5")
            {
                SetProgressBarProgress(75);
                DisableTopButtons();
                await Task.Run(() => HashingAlgorithms.GetMD5Hash(path));

                
                SetProgressBarProgress(100);
                lblStatus.Text = "Done!"; 

                txtResult.Text = HashingAlgorithms.GetHashResult();
                EnableAllButtonsAndTextBox();
            }
            else if (cmbHashList.Text == "SHA-1")
            {
                SetProgressBarProgress(75);
                DisableTopButtons();
                await Task.Run(() => HashingAlgorithms.GetSHA1Hash(path));

                
                SetProgressBarProgress(100);
                lblStatus.Text = "Done!";

                txtResult.Text = HashingAlgorithms.GetHashResult();
                EnableAllButtonsAndTextBox();
            }
            else if (cmbHashList.Text == "SHA-256")
            {
                SetProgressBarProgress(75);
                DisableTopButtons();
                await Task.Run(() => HashingAlgorithms.GetSHA256Hash(path));

                
                SetProgressBarProgress(100);
                lblStatus.Text = "Done!";

                txtResult.Text = HashingAlgorithms.GetHashResult();
                EnableAllButtonsAndTextBox();
            }
            else if (cmbHashList.Text == "SHA-516")
            {
                SetProgressBarProgress(75);
                DisableTopButtons();
                await Task.Run(() => HashingAlgorithms.GetSHA512Hash(path));

                
                SetProgressBarProgress(100);
                lblStatus.Text = "Done!";

                txtResult.Text = HashingAlgorithms.GetHashResult();
                EnableAllButtonsAndTextBox();
            }
            else if (cmbHashList.Text == "CRC32")
            {
                SetProgressBarProgress(75);
                DisableTopButtons();
                await Task.Run(() => HashingAlgorithms.GetCRC32Hash(path));

                
                SetProgressBarProgress(100);
                lblStatus.Text = "Done!";

                txtResult.Text = HashingAlgorithms.GetHashResult();
                EnableAllButtonsAndTextBox();
            }
            else
            {
                MessageBox.Show("Please select a hashing algorithm.","Hashing Agorithm Not Selected!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DisableTopButtons()
        {
            btnVerifyFile.Enabled = false;
            btnBrowse.Enabled = false;
        }

        private void EnableAllButtonsAndTextBox()
        {
            btnBrowse.Enabled = true;
            txtResult.Enabled= true;
            btnVerifyFile.Enabled = true;
            btnClear.Enabled = true;
            btnCopy.Enabled = true;
        }

        private void SetProgressBarProgress(int precentageCompleted)
        {
            lblStatus.Text = "Working...";

            if (precentageCompleted > 100)
            {
                precentageCompleted = 100;
            }
            else
            {
                progressBar.Value = precentageCompleted;
                progressBar.Update();
            }
        }

    }
}
