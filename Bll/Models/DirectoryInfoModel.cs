using System.Collections.Generic;

namespace Bll.Models
{
	public class DirectoryInfoModel
	{
        public string CurrentDirectory { get; set; }
        public string ParentDirectory { get; set; }
		public IEnumerable<string> Directories { get; set; }
		public IEnumerable<string> Files { get; set; }
	}
}
