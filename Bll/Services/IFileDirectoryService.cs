using System.Collections.Generic;
using System.Threading.Tasks;
using Bll.Models;

namespace Bll.Services
{
    public interface IFileDirectoryService
    {
        DirectoryInfoModel GetDirectoryInfo(string path);

        Task<FileCountInfoModel> GetCountFiles(string path);

        IEnumerable<string> GetDrivesNameWithoutCurrent(string currentDirectory);

        DirectoryInfoModel GetDriveDirectoryInfo(string driveName);
    }
}
