using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class CertificateManager
  {
    /// <summary>
    /// Determines whether the certificate of the specified server is installed, or not.
    /// </summary>
    /// <param name="server">The server with the certificate to check</param>
    /// <returns>bool, if the certificate is installed</returns>
    public static Task<bool> CheckCertificateInstalled(Server server)
    {
      // Check if the certificate of the specified server awas found in the x509 store
      return Task.Run(() =>
      {
        X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
        x509Store.Open(OpenFlags.ReadOnly);
        bool found = x509Store.Certificates.Find(X509FindType.FindByThumbprint, server.CertificateThumbprint, true).Count >= 1;
        x509Store.Close();
        return found;
      });
    }

    /// <summary>
    /// Installs the certificate of the specified server
    /// </summary>
    /// <param name="server">The server which certificate will be installed</param>
    public static void InstallCertificate(Server server)
    {
      // Install the certificate of the specified server
      X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
      x509Store.Open(OpenFlags.ReadWrite);
      X509Certificate2 certificate = new X509Certificate2(server.Certificate);
      x509Store.Add(certificate);
      x509Store.Close();
    }

    /// <summary>
    /// Uninstalls the certificates of all specified servers
    /// </summary>
    /// <param name="servers">The servers which certificates will be uninstalled</param>
    public static void UninstallAllCertificates(List<Server> servers)
    {
      // Uninstall the certificates of all specified servers that has a certificate
      X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
      x509Store.Open(OpenFlags.ReadWrite);
      foreach (Server server in servers.Where(x => x.HasCertificate))
        foreach (X509Certificate2 certificate in x509Store.Certificates.Find(X509FindType.FindByThumbprint, server.CertificateThumbprint, true))
        {
          try
          {
            x509Store.Remove(certificate);
          }
          catch (Exception ex)
          {
            throw ex;
          }
        }

      x509Store?.Close();
    }
  }
}
