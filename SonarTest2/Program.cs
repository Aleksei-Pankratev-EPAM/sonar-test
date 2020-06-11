using System;
using System.Collections.Generic;

namespace SonarTest
{
    static class Program
    {
        static void Main(string[] args)
        {
            Test();

            Console.ReadKey();
        }

        public static List<string> Test()
        {
            List<string> warnings = new List<string>();

            IRepository repository = null;
            IFeatureConfiguration featureConfig = null;
            if (ServiceLocator.IsLocationProviderSet)
                try
                {
                    repository = ServiceLocator.Current.GetInstance(typeof(IRepository)) as IRepository;
                    featureConfig = ServiceLocator.Current.GetInstance(typeof(IFeatureConfiguration)) as IFeatureConfiguration;
                }
                catch (ActivationException)
                {
                    //do nothing
                }

            try
            {
                if (featureConfig?.IsAvailable(FeaturesEnum.AddInflationRatesToPricingProfile) ?? false)
                {
                    Console.WriteLine("I'm alive");
                }

                return warnings;

            }
            finally
            {
                repository?.Dispose();
            }
        }
    }

    public class ServiceLocator
    {
        public static ServiceLocator Current { get; } = new ServiceLocator();

        public static bool IsLocationProviderSet => DateTime.Now.Year == 2020;

        private ServiceLocator()
        {

        }

        public object GetInstance(Type instanceType)
        {
            if (instanceType == typeof(IRepository))
                return new Repository();
            else if (instanceType == typeof(IFeatureConfiguration))
                return new FeatureConfiguration();
            return null;
        }

    }

    public class Repository : IRepository
    {
        public void Dispose()
        {
            //Do nothing
        }
    }

    public interface IRepository : IDisposable
    {

    }

    public class FeatureConfiguration : IFeatureConfiguration
    {
        public bool IsAvailable(string feature)
        {
            return feature.Length > 0 && DateTime.Now.Year > 2010;
        }
    }

    public interface IFeatureConfiguration
    {
        bool IsAvailable(string feature);
    }



    public static class FeaturesEnum
    {
        public const string AddInflationRatesToPricingProfile = "Test";
    }

    public class ActivationException : Exception
    { }



}
