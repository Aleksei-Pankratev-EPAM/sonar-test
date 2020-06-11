using System;

namespace SonarTest
{
    static class Program
    {
        static void Main(string[] args)
        {
            SampleService service = null;

            Console.WriteLine("Press the key");
            var key = Console.ReadKey();

            if (key.KeyChar == '1')
                try
                {
                    service = Activator.CreateInstance(typeof(SampleService)) as SampleService;
                }
                catch
                {
                    //do nothing
                }

            if (service?.IsFeatureAvailable(FeatureFlag.Feature1) ?? false)
            {
                Console.WriteLine("I'm not dead");
            }

            Console.ReadKey();
        }
    }

    public class SampleService
    {
        public bool IsFeatureAvailable(FeatureFlag feature)
        {
            return feature == FeatureFlag.Feature1;
        }
    }

    public enum FeatureFlag
    {
        Feature1,
        Feature2
    }
}
