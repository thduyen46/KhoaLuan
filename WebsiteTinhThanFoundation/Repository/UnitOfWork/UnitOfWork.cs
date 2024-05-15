using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Repository.Interface;

namespace WebsiteTinhThanFoundation.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IUserRoleRepository _userRoleRepository;
        private IBlogArticleTagRepository _blogArticleTagRepository;
        private IBlogArticleRepository _blogArticleRepository;
        private IBlogArticleCommentRepository _blogArticleCommentRepostiroy;
        private ITagRepository _tagRepository;
        private IRegisteredVolunteerRepository _registeredVolunteerRepository;
#pragma warning disable CS8618
        public UnitOfWork(ApplicationDbContext context)
#pragma warning restore CS8618
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository(_context);
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                return _roleRepository ??= new RoleRepository(_context);
            }
        }

        public IUserRoleRepository UserRoleRepository
        {
            get
            {
                return _userRoleRepository ??= new UserRoleRepository(_context);
            }
        }

        public IBlogArticleCommentRepository BlogArticleCommentRepository
        {
            get
            {
                return _blogArticleCommentRepostiroy ??= new BlogArticleCommentRepository(_context);
            }
        }

        public IBlogArticleRepository BlogArticleRepository
        {
            get
            {
                return _blogArticleRepository ??= new BlogArticleRepository(_context);
            }
        }

        public IBlogArticleTagRepository BlogArticleTagRepository
        {
            get
            {
                return _blogArticleTagRepository ??= new BlogArticleTagRepository(_context);
            }
        }

        public ITagRepository TagRepository
        {
            get
            {
                return _tagRepository ??= new TagRepository(_context);
            }
        }

        public IRegisteredVolunteerRepository RegisteredVolunteerRepository
        {
            get
            {
                return _registeredVolunteerRepository ??= new RegisteredVolunteerRepository(_context);
            }
        }
        public void Commit()
            => _context.SaveChanges();
        public async Task CommitAsync()
            => await _context.SaveChangesAsync();
        public void Rollback()
            => _context.Dispose();

        public async Task RollbackAsync()
            => await _context.DisposeAsync();
    }
}
