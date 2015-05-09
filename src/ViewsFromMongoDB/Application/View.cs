using MongoRepository;

namespace ViewsFromMongoDB
{
    [CollectionName("views")]
    public class View : Entity
    {   
        public string Name { get; set; }

        public string Path { get; set; }

        public string Content { get; set; }
    }
}
