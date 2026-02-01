namespace Blog.Core.Models {
    public class ImagesPost {
        public int Id { get;  }
        public string StoredName { get; }
        public string Type { get;  }

        private ImagesPost(int id, string stredName, string type ) {
            Id = id;
            StoredName = stredName;
            Type = type;
        }

        public static ImagesPost Create(int  id, string stredName, string type ) {
            return new ImagesPost( id, stredName, type);
        }
    }
}
