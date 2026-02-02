using FluentValidation;
using Blog.Contracts.DTO.Post;

namespace Blog.Application.Validators {
    public class CreatePostValidator :
        AbstractValidator<CreatePostRequest>
        {
        public CreatePostValidator ( ) {
            RuleFor(p => p.Content)
                // временная валидность на текст пока не добавим изображения
                .NotEmpty().WithMessage("Пост не может быть пустым")
                .MaximumLength(5000)
                .WithMessage("Слишком длинный текст MAX: 5000.");
        }

    }
}
