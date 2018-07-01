using Callisto.Module.Tags.Interfaces;
using Callisto.SharedKernel;
using Callisto.SharedModels.Tag;
using Callisto.SharedModels.Tag.ViewModels;
using Callisto.SharedModels.Session;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Callisto.Base.Module;

namespace Callisto.Module.Tags
{
    /// <summary>
    /// Defines the <see cref="TagModule" />
    /// </summary>
    public class TagModule : BaseModule, ITagModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagModule"/> class.
        /// </summary>
        /// <param name="session">The <see cref="ICallistoSession"/></param>
        /// <param name="logger">The <see cref="ILogger{TagModule}"/></param>
        /// <param name="TagRepo">The <see cref="ITagRepository"/></param>
        public TagModule(
            ICallistoSession session,
            ILogger<TagModule> logger,
            ITagRepository tagRepo) : base(tagRepo)
        {
            Session = session;
            Logger = logger;
            TagRepo = tagRepo;
        }

        /// <summary>
        /// Gets the Session
        /// </summary>
        private ICallistoSession Session { get; }

        /// <summary>
        /// Gets the Logger
        /// </summary>
        private ILogger<TagModule> Logger { get; }

        /// <summary>
        /// Gets the TagRepo
        /// </summary>
        private ITagRepository TagRepo { get; }

        /// <summary>
        /// The UpsertCompanyTag
        /// </summary>
        /// <param name="viewModel">The <see cref="TagViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult<long>> UpsertTag(TagViewModel viewModel)
        {

            var Tag = await TagRepo.GetTagByName(viewModel.Name, this.Session.CurrentCompanyRef);

            if (Tag == null)
            {
                Tag = ModelFactory.CreateTag(viewModel, this.Session.CurrentCompanyRef);
                await TagRepo.AddTag(Tag);
            }
            else
            {
                ModelFactory.UpdateTag(Tag, viewModel);
                await TagRepo.SaveTag(Tag);
            }

            return RequestResult<long>.Success(Tag.RefId);
        }

        /// <summary>
        /// The GetTag
        /// </summary>
        /// <param name="TagRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{RequestResult{TagViewModel}}"/></returns>
        public async Task<RequestResult<TagViewModel>> GetTag(long TagRefId)
        {
            var Tag = await TagRepo.GetTagById(TagRefId);
            if (Tag == null)
            {
                return RequestResult<TagViewModel>.Validation("Failed to find Tag", null);
            }

            return RequestResult<TagViewModel>.Success(ModelFactory.CreateTag(Tag));
        }

        /// <summary>
        /// The GetTags
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{TagViewModel}}}"/></returns>
        public async Task<RequestResult<IEnumerable<TagViewModel>>> GetTags()
        {

            var list = new List<TagViewModel>();
            var Tags = await TagRepo.GetTagsByCompany(Session.CurrentCompanyRef);
            foreach (var item in Tags)
            {
                list.Add(ModelFactory.CreateTag(item));
            }

            return RequestResult<IEnumerable<TagViewModel>>.Success(list);
        }

        /// <summary>
        /// The RemoveTag
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> RemoveTag(Guid id)
        {
            var Tag = await TagRepo.GetTagById(id);
            if (Tag != null)
            {
                await TagRepo.RemoveTag(Tag);
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The SaveTag
        /// </summary>
        /// <param name="model">The <see cref="TagViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> SaveTag(TagViewModel model)
        {
            var Tag = await TagRepo.GetTagById(model.Id);
            if (Tag == null)
            {
                throw new InvalidOperationException($"Failed cannot be found");
            }

            ModelFactory.UpdateTag(Tag, model);

            await TagRepo.SaveTag(Tag);

            return RequestResult.Success();
        }
    }
}
