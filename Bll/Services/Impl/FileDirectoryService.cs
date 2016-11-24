using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Bll.Models;

namespace Bll.Services.Impl
{
    public class FileDirectoryService
    {
        public DirectoryInfoModel GetDirectoryInfo(string path)
        {
            var currentPath = path;

            if (path.Length - 1 != path.LastIndexOf("\\", StringComparison.CurrentCultureIgnoreCase))
            {
                currentPath = $"{path}\\";
            }

            return new DirectoryInfoModel()
            {
                CurrentDirectory = path,
                ParentDirectory = Directory.GetParent(path)?.FullName,
                Directories = Directory.EnumerateDirectories(path).Select(x => x.Replace(currentPath, "")),
                Files = Directory.EnumerateFiles(path).Select(x => x.Replace(currentPath, ""))
            };
        }

        public async Task<FileCountInfoModel> GetCountFiles(string path)
        {
            FileCountInfoModel fileCountInfoModel = new FileCountInfoModel();

            await GetFiles(path, fileCountInfoModel);

            return fileCountInfoModel;
        }

        public IEnumerable<string> GetDrivesNameWithoutCurrent(string currentDirectory)
        {
            return DriveInfo.GetDrives()
                .Where(x => x.DriveType == DriveType.Fixed && !string.Equals(x.Name, currentDirectory, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Name)
                .ToList();
        }

        public DirectoryInfoModel GetDriveDirectoryInfo(string driveName)
        {
            DriveInfo driveInfo = new DriveInfo(driveName);
            return new DirectoryInfoModel()
            {
                CurrentDirectory = driveName,
                ParentDirectory = null,
                Directories = driveInfo.RootDirectory.EnumerateDirectories().Select(x => x.Name),
                Files = driveInfo.RootDirectory.EnumerateFiles().Select(x => x.Name)
            };
        }

        public async Task GetFiles(string root, FileCountInfoModel fileCountInfoModel)
        {
            Stack<string> pending = new Stack<string>();
            pending.Push(root);

            await Task.Run(() =>
            {
                while (pending.Count != 0)
                {
                    var path = pending.Pop();
                    string[] next = null;
                    try
                    {
                        next = Directory.GetFiles(path);
                    }
                    catch
                    {
                        // ignored
                    }

                    if (next != null && next.Length != 0)
                    {
                        Parallel.ForEach(next, file =>
                        {
                            var size = new FileInfo(file).Length / 1048576;
                            if (size <= 10)
                            {
                                fileCountInfoModel.LessOrEqualTen++;
                                return;

                            }
                            if (size > 10 && size <= 50)
                            {
                                fileCountInfoModel.LessOrEqualFifty++;
                                return;
                            }

                            if (size >= 100)
                            {
                                fileCountInfoModel.GreaterOrEqualHundred++;
                            }
                        });

                    }

                    try
                    {
                        next = Directory.GetDirectories(path);
                        foreach (var subdir in next)
                        {
                            pending.Push(subdir);
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }
            });
            
        }
    }
}
