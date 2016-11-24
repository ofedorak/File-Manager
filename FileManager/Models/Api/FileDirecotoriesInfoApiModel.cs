using System.Collections.Generic;
using Bll.Models;

namespace FileManager.Models.Api
{
	public class FileDirecotoriesInfoApiModel
	{
        public string CurrentDirectory { get; set; }
        public string ParentDirectory { get; set; }

		public FileCountInfoModel FileCountInfoModel { get; set; }
		public IEnumerable<string> Directories { get; set; } 
		public IEnumerable<string> Files { get; set; } 
	}
}