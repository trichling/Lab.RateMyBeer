using System.Diagnostics.CodeAnalysis;

namespace Lab.RateMyBeer.Framework.Composition.ViewModels
{
    public class ViewModelCompositionContext : IViewModelCompositionContext
    {
        private readonly Dictionary<string, object> _values;

        private readonly List<IViewModelAppender> _appenders;

        public ViewModelCompositionContext()
            : this(Enumerable.Empty<IViewModelAppender>())
        {
        }

        public ViewModelCompositionContext(IEnumerable<IViewModelAppender> appenders)
        {   _values = new Dictionary<string, object>();
            _appenders = appenders.ToList();
        }

        public IViewModelCompositionContext AddAppender<TViewModel>(IViewModelAppender appender)
        {
            if (appender == null)
                throw new ArgumentNullException(nameof(appender));

            _appenders.Add(appender);
            return this;
        }
        
        public bool TryGetValue<T>(string key, [MaybeNullWhen(false)] out T value)
        {
            if(_values.TryGetValue(key, out var obj))
            {
                value = (T) obj;
                return true;
            }
            value = default;
            return false;
        }

        public IViewModelCompositionContext SetValue<T>(string key, T value)
        {
            if(!_values.ContainsKey(key))
                _values.Add(key, value);
            _values[key] = value;
            return this;
        }

        public async Task<TViewModel> Compose<TViewModel>(dynamic viewModel)
        {
            var appenders = AppendersFor(typeof(TViewModel));
            foreach (var appender in appenders)
            {
                viewModel = await appender.AppendTo(viewModel, this);
            }
            return viewModel;
        }

       

        public async Task<IEnumerable<TViewModel>> ComposeList<TViewModel>(IEnumerable<dynamic> viewModels)
        {
            var appenders = AppendersFor(typeof(TViewModel));
            foreach (var appender in appenders)
            {
                viewModels = await appender.AppendTo(viewModels, this);
            }

            return viewModels.Cast<TViewModel>();
        }

        public IEnumerable<IViewModelAppender> AppendersFor(Type typeofViewModel)
        {
            Console.WriteLine($"2. key={typeofViewModel}");
            return _appenders.Where(a => a.WillAppendTo(typeofViewModel));
        }
    }

}
