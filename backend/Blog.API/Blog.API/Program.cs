using Blog.Application.Auth;
using Blog.Application.Services.Comments;
using Blog.Application.Services.Posts;
using Blog.Application.Services.Subscriptions;
using Blog.Application.Services.User;
using Blog.Core.Abstractions.Auth;
using Blog.Core.Abstractions.Repository;
using Blog.Core.Abstractions.Service;
using Blog.Core.Abstractions.Service.Comments;
using Blog.Core.Abstractions.Service.LikesComments;
using Blog.Core.Abstractions.Service.LikesPosts;
using Blog.Core.Abstractions.Service.Posts;
using Blog.Core.Abstractions.Service.Subscriptions;
using Blog.Core.Abstractions.Service.User;
using Blog.DataAccess;
using Blog.DataAccess.Repositories.LikeComment;
using Blog.DataAccess.Repositories.LikePost;
using Blog.DataAccess.Repositories.Posts;
using Blog.DataAccess.Repositories.Subscriptions;
using Blog.DataAccess.Repositories.Users;
using Microsoft.EntityFrameworkCore;
// Для токена
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Алиасы для устранения конфликтов имен
using CommentEntity = Blog.DataAccess.Models.Post.Entity.Commentes;
using LikeCommentEntity = Blog.DataAccess.Models.Post.Entity.LikeComment;
using LikePostEntity = Blog.DataAccess.Models.Post.Entity.LikePost;
using PostEntity = Blog.DataAccess.Models.Post.Entity.Post;
using SubscriptionEntity = Blog.DataAccess.Models.User.Entity.Subscription;
using UserEntity = Blog.DataAccess.Models.User.Entity.User;
using Blog.Application.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Настройка БД
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlogDbContext>(options => {
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// 2. Стандартные сервисы API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 3. РЕГИСТРАЦИЯ РЕПОЗИТОРИЕВ (DataAccess) ---

// UserRepository
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ICreateRepository<UserEntity>>(x => x.GetRequiredService<UserRepository>());
builder.Services.AddScoped<IGetByEmailRepository<UserEntity>>(x => x.GetRequiredService<UserRepository>());
builder.Services.AddScoped<IGetByIdRepository<UserEntity>>(x => x.GetRequiredService<UserRepository>());
builder.Services.AddScoped<IGetRepository<UserEntity>>(x => x.GetRequiredService<UserRepository>());

// SubscriptionRepository
builder.Services.AddScoped<SubscriptionRepository>();
builder.Services.AddScoped<ICreateRepository<SubscriptionEntity>>(x => x.GetRequiredService<SubscriptionRepository>());
builder.Services.AddScoped<IDeleteRepository<SubscriptionEntity>>(x => x.GetRequiredService<SubscriptionRepository>());
builder.Services.AddScoped<IGetRepository<SubscriptionEntity>>(x => x.GetRequiredService<SubscriptionRepository>());

// PostRepository
builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<ICreateRepository<PostEntity>>(x => x.GetRequiredService<PostRepository>());
builder.Services.AddScoped<IDeleteRepository<PostEntity>>(x => x.GetRequiredService<PostRepository>());
builder.Services.AddScoped<IGetRepository<PostEntity>>(x => x.GetRequiredService<PostRepository>());
builder.Services.AddScoped<IGetByIdRepository<PostEntity>>(x => x.GetRequiredService<PostRepository>());

// LikePostRepository
builder.Services.AddScoped<LikePostRepository>();
builder.Services.AddScoped<ICreateRepository<LikePostEntity>>(x => x.GetRequiredService<LikePostRepository>());
builder.Services.AddScoped<IDeleteRepository<LikePostEntity>>(x => x.GetRequiredService<LikePostRepository>());
builder.Services.AddScoped<IGetRepository<LikePostEntity>>(x => x.GetRequiredService<LikePostRepository>());
builder.Services.AddScoped<IGetByIdRepository<LikePostEntity>>(x => x.GetRequiredService<LikePostRepository>());

// LikeCommentRepository
builder.Services.AddScoped<LikeCommentRepository>();
builder.Services.AddScoped<ICreateRepository<LikeCommentEntity>>(x => x.GetRequiredService<LikeCommentRepository>());
builder.Services.AddScoped<IDeleteRepository<LikeCommentEntity>>(x => x.GetRequiredService<LikeCommentRepository>());
builder.Services.AddScoped<IGetRepository<LikeCommentEntity>>(x => x.GetRequiredService<LikeCommentRepository>());
builder.Services.AddScoped<IGetByIdRepository<LikeCommentEntity>>(x => x.GetRequiredService<LikeCommentRepository>());


// --- 4. РЕГИСТРАЦИЯ СЕРВИСОВ (Application) ---

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostQueryService, PostQueryService>();

builder.Services.AddScoped<IPostLikeService, PostLikeService>();
builder.Services.AddScoped<ILikePostQueryService, PostLikeQueryService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<ICommentQueryService, CommentQueryService>();

// Репозиторий комментариев
builder.Services.AddScoped<Blog.DataAccess.Repositories.Comments.CommentRepository>();
builder.Services.AddScoped<ICreateRepository<CommentEntity>>(x => x.GetRequiredService<Blog.DataAccess.Repositories.Comments.CommentRepository>());
builder.Services.AddScoped<IDeleteRepository<CommentEntity>>(x => x.GetRequiredService<Blog.DataAccess.Repositories.Comments.CommentRepository>());
builder.Services.AddScoped<IGetRepository<CommentEntity>>(x => x.GetRequiredService<Blog.DataAccess.Repositories.Comments.CommentRepository>());
builder.Services.AddScoped<IGetByIdRepository<CommentEntity>>(x => x.GetRequiredService<Blog.DataAccess.Repositories.Comments.CommentRepository>());
builder.Services.AddScoped<ICommentService, CommentService>();

// ДОБАВЛЕНО: Сервисы лайков комментариев
builder.Services.AddScoped<ICommentLikeQueryService, CommentLikeQueryService>();
builder.Services.AddScoped<ILikeServices, CommentLikeService>();

// Регистрация генератора токена
// 1
builder.Services.AddScoped<IJwtProvaider, JwtProvider>();
// 2. настройка проверки токена
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
        };
    });

//builder.Services.AddSwaggerGen(c => {
//    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
//        Description = "Bearer {eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIxIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoic29ueUBzZXJ2ZXIuY29tIiwiZXhwIjoxNzcwMzM0Mjg0fQ.Y_o8vv7ePwnJrnBhhvIzzmmEq-NuYbDA6b8ln5m9Yro}",
//        Name = "Authorization",
//        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });
//    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
//        {
//            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
//                Reference = new Microsoft.OpenApi.Models.OpenApiReference {
//                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});


var app = builder.Build();

// 5. КОНВЕЙЕР (Middleware)
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();   // Сначала аутентификация
app.UseAuthorization();    // Потом авторизация
app.UseStaticFiles(); // для файлов
app.MapControllers();

app.Run(); 

           