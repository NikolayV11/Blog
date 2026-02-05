using FluentValidation;
using Blog.Contracts.DTO.Post;

namespace Blog.Application.Validators {
    public class CreatePostValidator :
        AbstractValidator<CreatePostRequest>
        {
        public CreatePostValidator ( ) {
            // Правило: Если картинок нет,
            // то то контент не должен быть пустым
            RuleFor(x => x.Content)
                .NotEmpty()
                .When(
                x => x.Images == null || x.Images.Count == 0)
                .WithMessage("Пост должен содержать либо текст, либо изображение(я).");

            RuleFor(p => p.Content)
                // временная валидность на текст пока не добавим изображения
                .MaximumLength(5000)
                .WithMessage("Слишком длинный текст (MAX: 5000 сим.)");
        }

    }
}
