using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;
using Squirrel;
using System.Net;
using System.Linq;
using System.IO;

namespace SWeight
{
    static class Program
    {
        async static Task GetUpdate()
        {
            try
            {
                var  restart  = false;
                var latestExe = "";

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var manager = await UpdateManager.GitHubUpdateManager("https://github.com/regata-jinr/SWeight"))
                {
                    var upd = await manager.CheckForUpdate();

                    if (upd.ReleasesToApply.Any())
                    {
                        System.Diagnostics.Process.Start("https://github.com/regata-jinr/SWeight/releases");

                        var LatestVersion = upd.ReleasesToApply.OrderBy(x => x.Version).Last();
                        await manager.DownloadReleases(upd.ReleasesToApply);
                        await manager.ApplyReleases(upd);
                        await manager.UpdateApp();

                        latestExe = Path.Combine(manager.RootAppDirectory, String.Concat("app-", LatestVersion.Version.Version.Major, ".", LatestVersion.Version.Version.Minor, ".", LatestVersion.Version.Version.Build, "."), "SWeight.exe");
                        restart = true;
                    }
                }

                if (restart)
                    UpdateManager.RestartApp(latestExe);
            }
            catch (InvalidOperationException)
            {
                // in case of updates files don't exist
                MessageBox.Show("Обновление не доступно. Обратитесь к администратору.", "Ошибка в процессе запуска приложения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка в процессе запуска приложения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GetUpdate().Wait();
            Application.Run(new FaceForm());
        }
    }
}
