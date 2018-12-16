using Project.CodeGeneration;
using System;

namespace Project.CodeGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            FileBuilder.IncludeProj = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<IncludeProj>>();

            Console.WriteLine("Type the model name to generate the files and press enter");

            var model = Console.ReadLine();

            if (Helper.CheckModelName(model))
            {
                Console.WriteLine("Wourl you like to generate the server side files?  (y/n)");
                var key = Console.ReadKey();
                if (key.Key.ToString() == "Y")
                {
                    new FileBuilder(model)
                        .CreateContractService()
                        .CreateContractRepository()
                        .CreateModelValidation()
                        .CreateModelRepository()
                        .CreateModelService()
                        .CreateDTO()
                        .CreateModelToDto()
                        .CreateDtoToModel()
                        .CreateModelBuilderCRUD()
                        .CreateController()
                        .UpdateModelValidator()
                        .CreateModelServiceLoader()
                        .CreateModelWebLoader()
                        .CreateWebClientApp();
                    //.IncludeFilesInProject();
                }

                Console.WriteLine("Would you like to generate the WEB - ClientApp components?  (y/n)");
                var key1 = Console.ReadKey();
                if (key1.Key.ToString() == "Y")
                {
                    new FileBuilder(model)
                            .CreateWebClientAppComponents();
                }
                Console.WriteLine("Done...");
            }
            else
            {
                Console.WriteLine("Model not found");
            }

            Console.ReadLine();
        }
    }
}
