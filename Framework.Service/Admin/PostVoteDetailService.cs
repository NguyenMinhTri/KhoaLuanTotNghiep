using Framework.Model;
using Framework.Repository.RepositorySpace;
using Framework.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Framework.Service.Admin
{
    public interface IPostVoteDetailService : IQlService<PostVoteDetail>
    {
    }
    public class PostVoteDetailService : QlService<PostVoteDetail>, IPostVoteDetailService
    {
        IPostVoteDetailRepository _postVoteDetailRepository;
        public PostVoteDetailService(IPostVoteDetailRepository postVoteDetailRepository, IUnitOfWork unitOfWork) : base(postVoteDetailRepository, unitOfWork) 
        {
            this._repository = postVoteDetailRepository;
        }
    }
}
