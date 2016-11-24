using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Antlr.Runtime.Misc;
using Bll.Models;
using Bll.Services.Impl;
using FileManager.Models.Api;

namespace FileManager.Controllers
{
    public class ExploreApiController : ApiController
    {
        public FileDirectoryService FileDirectoryService = new FileDirectoryService();


        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            string path = Directory.GetCurrentDirectory();

            var response = await GetFileDirecotoriesInfo(FileDirectoryService.GetDirectoryInfo, path);

            return this.Json(response);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetDirectoryInfo([FromUri]string path)
        {
            var response = await GetFileDirecotoriesInfo(FileDirectoryService.GetDirectoryInfo, path);

            return this.Json(response);
        }

        [HttpGet]
        public IHttpActionResult GetDrivesName(string currentDirectory)
        {
            return this.Json(FileDirectoryService.GetDrivesNameWithoutCurrent(currentDirectory));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetDrivesDirectory([FromUri]string driveName)
        {
           var response = await GetFileDirecotoriesInfo(FileDirectoryService.GetDriveDirectoryInfo, driveName);

            return this.Json(response);
        }

        private async Task<FileDirecotoriesInfoApiModel> GetFileDirecotoriesInfo(Func<string, DirectoryInfoModel> getDirectoryInfo, string path)
        {
            var fileCountInfoModel = await FileDirectoryService.GetCountFiles(path);
            var directoryInfo = getDirectoryInfo(path);

            return new FileDirecotoriesInfoApiModel
            {
                FileCountInfoModel = fileCountInfoModel,
                CurrentDirectory = directoryInfo.CurrentDirectory,
                ParentDirectory = directoryInfo.ParentDirectory,
                Directories = directoryInfo.Directories,
                Files = directoryInfo.Files
            };
        }
    }
}
