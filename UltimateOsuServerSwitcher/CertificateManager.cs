using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class CertificateManager
  {
    public static Task<bool> GetStatusAsync(Server server)
    {
      return Task.Run(() =>
      {
        X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
        x509Store.Open(OpenFlags.ReadOnly);
        bool found = x509Store.Certificates.Find(X509FindType.FindByThumbprint, server.CertificateThumbprint, true).Count >= 1;
        x509Store.Close();
        return found;
      });
    }

    public static void InstallCertificate(Server server)
    {
      X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
      x509Store.Open(OpenFlags.ReadWrite);
      X509Certificate2 certificate = new X509Certificate2(server.ServerCertificate);
      x509Store.Add(certificate);
      x509Store.Close();
    }

    public static void UninstallAllCertificates(List<Server> servers)
    {
      X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
      x509Store.Open(OpenFlags.ReadWrite);
      foreach (Server server in servers.Where(x => x.CertificateUrl != null))
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
