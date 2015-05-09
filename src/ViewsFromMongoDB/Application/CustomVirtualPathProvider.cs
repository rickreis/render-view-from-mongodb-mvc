using MongoRepository;
using System;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Web.Hosting;

namespace ViewsFromMongoDB
{
    public class CustomVirtualPathProvider : VirtualPathProvider
    {
        public override bool FileExists(string virtualPath)
        {
            View view = GetFromDatabase(virtualPath);

            if (view == null)
            {
                return base.FileExists(virtualPath);
            }

            return true;
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            View view = GetFromDatabase(virtualPath);

            if (view == null)
            {
                return base.GetFile(virtualPath);
            }
            else
            {
                byte[] content = ASCIIEncoding.Default.GetBytes(view.Content);

                return new CustomVirtualFile(virtualPath, content);
            }
        }

        private View GetFromDatabase(string virtualPath)
        {
            if (!virtualPath.Contains("Views") ||
                virtualPath.Contains("_ViewStart") ||
                virtualPath.Contains(".Mobile.cshtml") ||
                !virtualPath.Contains(".cshtml"))
            {
                return null;
            }

            virtualPath = virtualPath.Replace("~", "");

            MemoryCache cache = MemoryCache.Default;

            View view = cache.Get(virtualPath) as View;

            if (view != null)
            {
                return view;
            }

            MongoRepository<View> mongo = new MongoRepository<View>();

            view = mongo.FirstOrDefault(x => x.Path == virtualPath);

            if (view == null)
            {
                return null;
            }

            CacheItemPolicy policy = new CacheItemPolicy();

            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(5);

            cache.Add(virtualPath, view, policy);

            return view;
        }
    }
}