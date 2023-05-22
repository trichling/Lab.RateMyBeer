namespace Lab.RateMyBeer.Framework.Composition.ViewModels
{
    public abstract class ViewModelAppenderBase<TViewModel> : IViewModelAppender<TViewModel>
    {
        public abstract Task<TViewModel> AppendTo(TViewModel viewModel, IViewModelCompositionContext context);
        public virtual async Task<IEnumerable<TViewModel>> AppendTo(IEnumerable<TViewModel> viewModel, IViewModelCompositionContext context)
        {
            // Dumb
            foreach (var vm in viewModel)
            {
                await AppendTo(vm, context);
            }

            return viewModel;
        }

        public bool WillAppendTo(Type typeOfViewModel)
        {
            return typeof(TViewModel).IsAssignableFrom(typeOfViewModel) ||
                   typeof(IEnumerable<TViewModel>).IsAssignableFrom(typeOfViewModel);
        }

        public async Task<dynamic> AppendTo(dynamic viewModel, IViewModelCompositionContext context)
        {
            return await AppendTo((TViewModel)viewModel, context);
        }

        public async Task<IEnumerable<dynamic>> AppendTo(IEnumerable<dynamic> list, IViewModelCompositionContext context)
        {
            var result = await AppendTo(list.Cast<TViewModel>(), context);
            return result.Cast<dynamic>();
        }
    }

  
}
