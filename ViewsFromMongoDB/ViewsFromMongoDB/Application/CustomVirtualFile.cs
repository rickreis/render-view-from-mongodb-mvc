using System.IO;
using System.Web.Hosting;

namespace ViewsFromMongoDB
{
    public class CustomVirtualFile : VirtualFile
    {
        private readonly byte[] _viewContent;

        public CustomVirtualFile(string virtualPath, byte[] viewContent) : base(virtualPath)
        {
            _viewContent = viewContent;
        }

        public override Stream Open()
        {
            return new MemoryStream(_viewContent);
        }
    }
}