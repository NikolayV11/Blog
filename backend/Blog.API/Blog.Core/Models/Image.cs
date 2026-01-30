namespace Blog.Core.Models {
    public class Image {
        public Image ( int id, string storedName, string originalName, string contentType ) {
            Id = id;
            StoredName = storedName;
            OriginalName = originalName;
            ContentType = contentType;
        }

        public int Id { get; set; }
        public string StoredName { get; set; }
        public string OriginalName { get; set; }
        public string ContentType { get; set; }


    }
}
