using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apetito.Composition.ViewModels
{
    public interface IViewModelAppender
    {

        bool WillAppendTo(Type typeOfViewModel);
        Task<dynamic> AppendTo(dynamic viewModel, IViewModelCompositionContext context);
        Task<IEnumerable<dynamic>> AppendTo(IEnumerable<dynamic> list, IViewModelCompositionContext context);
        

    }

    public interface IViewModelAppender<TViewModel> : IViewModelAppender
    {

        Task<TViewModel> AppendTo(TViewModel viewModel, IViewModelCompositionContext context);
        Task<IEnumerable<TViewModel>> AppendTo(IEnumerable<TViewModel> viewModel, IViewModelCompositionContext context);

    }

}
