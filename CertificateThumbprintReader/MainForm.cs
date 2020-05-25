using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertificateThumbprintReader
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    private void btnGetThumbprint_Click(object sender, EventArgs e)
    {
      try
      {
        X509Certificate2 certificate = new X509Certificate2(Encoding.UTF8.GetBytes(txtCertificate.Text));
        txtThumbprint.Text = certificate.Thumbprint;
      }
      catch (Exception ex)
      {
        txtThumbprint.Text = ex.Message;
      }
    }
  }
}
