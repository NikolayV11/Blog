using User = Blog.Core.Models.User;
using UserEntity = Blog.DataAccess.Models.User.Entity.User;

namespace Blog.Application.Mapping {
    public static class UserMapping {
        public static User ToDomain (this UserEntity entity ) {
            var(user, error) = User.Create(
                entity.Id,
                entity.LastName,
                entity.FirstName,
                entity.Surname,
                entity.Birthday,
                entity.Email,
                entity.Phone,
                entity.AvatarId ?? 0,
                null
                ); // Здесь можно добавить маппинг Image, если он подгружен

            return user!;
        }
    }
}
