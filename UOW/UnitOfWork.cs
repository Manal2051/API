using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Runtime;
using TestToken.Data;
using TestToken.Models;
using TestToken.Repositories.GenericRepository;
using TestToken.Repositories.Interfaces;
using TestToken.Repositories.Services;

namespace TestToken.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailSettings _emailSettings;
        private readonly IEmailService _emailService;
   
        private readonly EmailTemplateService _emailTemplateService;
        private readonly ILogger<AccountRepository> _logger;


        public UnitOfWork
            (ApplicationDbContext context,UserManager<ApplicationUser> userManager,
            ITokenService tokenService, IMapper mapper,
            RoleManager<IdentityRole> roleManager,IEmailService emailService,
            IHttpContextAccessor httpContextAccessor,IOptions<EmailSettings> options,
   
            EmailTemplateService emailTemplateService,
            ILogger<AccountRepository> logger

            )
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _emailSettings = options.Value;
            _emailService = emailService;
            
            _emailTemplateService = emailTemplateService;
            _logger = logger;

            Customers = new AccountRepository(_context, _userManager, _tokenService, _mapper, _emailService,_emailTemplateService, _roleManager,_logger);
    
            Employees = new EmployeeRepository(_context, _mapper);
            //Emails = new EmailService(_emailSettings,_userManager,_context);
           
            Users = new UserRepository(_context);
        }
        public IAccountRepository Customers { get; private set; }
        public IUserRepository Users { get; private set; }

        public IEmployeeRepository Employees { get; private set; }


        public ITokenService TokenService { get; private set; }
        public IEmailService Emails { get; private set; }

        public async Task<int> SaveCompleted()
        {
         return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
           _context.Dispose();
        }
    }
}
