using System.Diagnostics.CodeAnalysis;

namespace Lab.RateMyBeer.Framework.Composition.ViewModels
{
    public interface IViewModelCompositionContext
    {
        IViewModelCompositionContext AddAppender<TViewModel>(IViewModelAppender appender);
        IEnumerable<IViewModelAppender> AppendersFor(Type typeOfViewModel);


        Task<TViewModel> Compose<TViewModel>(dynamic viewModel);
        Task<IEnumerable<TViewModel>> ComposeList<TViewModel>(IEnumerable<dynamic> viewModels);

        IViewModelCompositionContext SetValue<T>(string key, T value);
        bool TryGetValue<T>(string key, [MaybeNullWhen(false)] out T value);

    }
    
}
