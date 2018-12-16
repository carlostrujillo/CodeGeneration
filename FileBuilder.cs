namespace Project.CodeGeneration
{
    using System.Collections.Generic;
    using Project.CodeGeneration.Contracts.Repository;
    using Project.CodeGeneration.Contracts.Services;
    using Project.CodeGeneration.Repository;
    using Project.CodeGeneration.Service;
    using Project.CodeGeneration.Service.Loader;
    using Project.CodeGeneration.Validation;
    using Project.CodeGeneration.Web.Builder.All;
    using Project.CodeGeneration.Web.Builder.Delete;
    using Project.CodeGeneration.Web.Builder.GetById;
    using Project.CodeGeneration.Web.Builder.PostUpdate;
    using Project.CodeGeneration.Web.ClientApp.Components.Edit;
    using Project.CodeGeneration.Web.ClientApp.Components.List;
    using Project.CodeGeneration.Web.ClientApp.Components.ListItem;
    using Project.CodeGeneration.Web.ClientApp.Effects;
    using Project.CodeGeneration.Web.ClientApp.Model;
    using Project.CodeGeneration.Web.ClientApp.Service;
    using Project.CodeGeneration.Web.ClientApp.Store.Actions;
    using Project.CodeGeneration.Web.ClientApp.Store.Reducer;
    using Project.CodeGeneration.Web.ClientApp.Store.Selector;
    using Project.CodeGeneration.Web.ClientApp.Store.State;
    using Project.CodeGeneration.Web.Controller;
    using Project.CodeGeneration.Web.Converter.DtoToModel;
    using Project.CodeGeneration.Web.Converter.ModelToDto;
    using Project.CodeGeneration.Web.DTO;
    using Project.CodeGeneration.Web.Loader;

    public class FileBuilder
    {
        public FileBuilder(string modelName)
        {
            ModelName = modelName;
        }

        public static Dictionary<string, List<IncludeProj>> IncludeProj { get; set; }

        public string ModelName { get; set; }

        public FileBuilder CreateWebClientAppComponents()
        {
            new ComponentEditHTML().Execute(ModelName);
            new ComponentEditLESS().Execute(ModelName);
            new ComponentEditTS().Execute(ModelName);

            new ComponentListHTML().Execute(ModelName);
            new ComponentListLESS().Execute(ModelName);
            new ComponentListTS().Execute(ModelName);

            new ComponentListItemHTML().Execute(ModelName);
            new ComponentListItemLESS().Execute(ModelName);
            new ComponentListItemTS().Execute(ModelName);

            return this;
        }
        public FileBuilder CreateWebClientApp()
        {
            new ClientAppService().Execute(ModelName);
            new ClientAppActions().Execute(ModelName);
            new ClientAppReducerEdit().Execute(ModelName);
            new ClientAppReducerList().Execute(ModelName);
            new ClientAppSelector().Execute(ModelName);
            new ClientAppStateEdit().Execute(ModelName);
            new ClientAppStateList().Execute(ModelName);
            new ClientAppEffects().Execute(ModelName);
            new ClientAppModel().Execute(ModelName);
            return this;
        }

        public FileBuilder CreateContractService()
        {
            new ContractService().Execute(ModelName);
            return this;
        }

        public FileBuilder CreateContractRepository()
        {
            new ContractRepository().Execute(ModelName);
            return this;
        }

        public FileBuilder CreateModelValidation()
        {
            new ModelValidation().Execute(ModelName);
            return this;
        }

        public FileBuilder CreateModelRepository()
        {
            new ModelRepository().Execute(ModelName);
            return this;
        }

        public FileBuilder UpdateEntityFrameworkContext()
        {
            new EntityFrameworkContext().Execute(ModelName);
            return this;
        }

        public FileBuilder CreateModelService()
        {
            new ModelService().Execute(ModelName);
            return this;
        }

        public FileBuilder CreateModelServiceLoader()
        {
            new ModelServiceLoader().Execute();
            return this;
        }

        public FileBuilder CreateModelWebLoader()
        {
            new ModelWebLoader().Execute();
            return this;
        }

        public FileBuilder UpdateModelValidator()
        {
            new IncludeModelValidator().Execute(ModelName);
            return this;
        }

        public FileBuilder CreateDTO()
        {
            new ModelDTO().Execute(ModelName);
            return this;
        }

        public FileBuilder CreateModelToDto()
        {
            new ModelToDto().Execute(ModelName);
            return this;
        }

        public FileBuilder CreateDtoToModel()
        {
            new DtoToModel().Execute(ModelName);
            return this;
        }

        public FileBuilder CreateController()
        {
            new Controller().Execute(ModelName);
            return this;
        }

        public FileBuilder CreateModelBuilderCRUD()
        {
            new ModelGetAll().Execute(ModelName);
            new ModelGetById().Execute(ModelName);
            new ModelDeleteById().Execute(ModelName);
            new ModelPostUpdate().Execute(ModelName);
            return this;
        }

        public FileBuilder IncludeFilesInProject()
        {
            //foreach (var item in IncludeProj)
            //{
            //    var p = new Microsoft.Build.Evaluation.Project(item.Key);

            //    foreach (var info in item.Value)
            //    {
            //        p.AddItem(info.Key, info.Value);
            //    }

            //    p.Save();
            //}

            return this;
        }
    }
}
