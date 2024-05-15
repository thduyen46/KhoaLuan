using WebsiteTinhThanFoundation.Repository.Interface;

namespace WebsiteTinhThanFoundation.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        ITagRepository TagRepository { get; }
        IBlogArticleCommentRepository BlogArticleCommentRepository { get; }
        IBlogArticleRepository BlogArticleRepository { get; }
        IBlogArticleTagRepository BlogArticleTagRepository { get; }
        IRegisteredVolunteerRepository RegisteredVolunteerRepository { get; }
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
