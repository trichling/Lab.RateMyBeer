using System.Reflection;
using System.Runtime.Loader;
using Lab.RateMyBeer.Framework.Composition.Command;
using Lab.RateMyBeer.Framework.Composition.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Lab.RateMyBeer.Framework.Composition
{
    public static class CompositionServiceCollectionExtensions
    {
        public static void AddViewModelAppenders(this IServiceCollection serviceCollection)
        {

            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var allAssemblies = AssembliesFromDirectory(directory).Union(AssembliesFromLoadedAssemblies());

            AddViewModelAppenders(serviceCollection,  allAssemblies);
        }

        public static void AddViewModelAppenders(this IServiceCollection serviceCollection, Assembly assemblyToScan)
        {
            AddViewModelAppenders(serviceCollection, new List<Assembly>() { assemblyToScan });
        }

        public static void AddViewModelAppenders(this IServiceCollection serviceCollection, IEnumerable<Assembly> assembliesToScan)
        {
            serviceCollection.AddTransient<IViewModelCompositionContext, ViewModelCompositionContext>();

            var viewModelAppenderTypes = DiscoverIViewModelAppenderTypesFromAsseblies(assembliesToScan);

            foreach (var viewModelAppenderType in viewModelAppenderTypes)
            {
                serviceCollection.AddTransient(typeof(IViewModelAppender), viewModelAppenderType);
            }
        }

      

        public static void AddCommandAttachers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICommandOutbox, CommandOutbox>();
            serviceCollection.AddTransient<ICommandExecutionContext, CommandExecutionContext>();
        }
        
        public static void AddComposition<T>(this IServiceCollection serviceCollection) where T : class, ICommandOutbox
        {
            serviceCollection.AddTransient<ICommandOutbox, T>();
            AddViewModelAppenders(serviceCollection);
        }
        
        private static IEnumerable<Type> DiscoverIViewModelAppenderTypesFromAsseblies(IEnumerable<Assembly> assemblies)
        {
            var viewModelAppenderTypes = assemblies.SelectMany(a => a.DefinedTypes)
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .Where(t => typeof(IViewModelAppender).IsAssignableFrom(t))
                .Select(t => t.AsType());

            return viewModelAppenderTypes;
        }
        
        private static IEnumerable<Assembly> AssembliesFromDirectory(string directory)
        {
            var assemblies = Directory.GetFiles(directory, "*.dll")
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);

            return assemblies;
        }

        private static IEnumerable<Assembly> AssembliesFromLoadedAssemblies()
        {
            var referncedAssemblies = Assembly.GetEntryAssembly()
                .GetReferencedAssemblies()
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyName)
                .ToList();

            var assemblies = new List<Assembly>(referncedAssemblies);
            assemblies.Add(Assembly.GetEntryAssembly());
            assemblies.Add(Assembly.GetCallingAssembly());
            assemblies.Add(Assembly.GetExecutingAssembly());

            return assemblies;
        }

        
    }
}
