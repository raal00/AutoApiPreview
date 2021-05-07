using BLL.ApiModels.Request.Auth;
using BLL.ApiModels.Response.Auth;
using BLL.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository.Abstraction;
using BLL.DomainModels;
using System.Linq;
using DAL.Converters.Abstraction;
using DAL.PersistModels;
using System.Security.Cryptography;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAssociateRepository<AssociateDomain> associateRepository;
        private readonly IRoleRepository<RoleDomain> roleRepository;
        private readonly IAssociateLoginRepository<AssociateLoginDomain> associateLoginRepository;
        private readonly IAssociateRoleRepository<AssociateRoleDomain> associateRoleRepository;

        private readonly IPersistToDomainConverter<AssociatePersist, AssociateDomain> associateConverter;
        private readonly IPersistToDomainConverter<RolePersist, RoleDomain> roleConverter;
        private readonly IPersistToDomainConverter<AssociateRolePersist, AssociateRoleDomain> assoRoleConverter;
        private readonly IPersistToDomainConverter<AssociateLoginPersist, AssociateLoginDomain> assoLoginConverter;

        public AuthService(IAssociateRepository<AssociateDomain> _associateRepository,
                           IAssociateLoginRepository<AssociateLoginDomain> _associateLoginRepository,
                           IAssociateRoleRepository<AssociateRoleDomain> _associateRoleRepository,
                           IRoleRepository<RoleDomain> _roleRepository,

                           IPersistToDomainConverter<AssociatePersist, AssociateDomain> _associateConverter,
                           IPersistToDomainConverter<RolePersist, RoleDomain> _roleConverter,
                           IPersistToDomainConverter<AssociateRolePersist, AssociateRoleDomain> _assoRoleConverter,
                           IPersistToDomainConverter<AssociateLoginPersist, AssociateLoginDomain> _assoLoginConverter)
        {
            this.associateRoleRepository = _associateRoleRepository;
            this.associateRepository = _associateRepository;
            this.associateLoginRepository = _associateLoginRepository;
            this.roleRepository = _roleRepository;

            this.associateConverter = _associateConverter;
            this.roleConverter = _roleConverter;
            this.assoRoleConverter = _assoRoleConverter;
            this.assoLoginConverter = _assoLoginConverter;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Request error");
            }
            var response = new LoginResponseModel();

            string passwordHash = getPasswordHash(request.Password);
            var associate = (from login in associateLoginRepository.Get().ToList()
                            join asso in associateRepository.Get() on login.AssociateId equals asso.Id
                            where 
                            login.Login == request.Login && 
                            login.PasswordHash == passwordHash
                            select asso).FirstOrDefault();
            if (associate == null)
            {
                response.IsSuccess = false;
                response.ResponseMessage = "Логин не найден";
                return response;
            }
            var assoRole = (from role in roleRepository.Get().ToList()
                            join arole in associateRoleRepository.Get() on role.Id equals arole.RoleId
                            where arole.AssociateId == associate.Id
                            select role).FirstOrDefault();
            if (assoRole == null)
            {
                response.IsSuccess = false;
                response.ResponseMessage = "У пользователя не назначена роль";
                return response;
            }
            response.Associate = associateConverter.ConvertToDomain(associate);
            response.Role = roleConverter.ConvertToDomain(assoRole);
            response.IsSuccess = true;
            response.ResponseMessage = $"Добро пожаловать {associate.FirstName}";
            return response;
        }

        public async Task<LogOutResponseModel> LogOut(LogOutRequestModel request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Request error");
            }
            var response = new LogOutResponseModel();

            response.IsSuccess = true;
            response.ResponseMessage = "Вы вышли из системы";
            return response;
        }

        public async Task<SignInResponseModel> SignIn(SignInRequestModel request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Request error");
            }
            var response = new SignInResponseModel();

            if (associateLoginRepository.Get(x => x.Login == request.Login).Any())
            {
                response.IsSuccess = false;
                response.ResponseMessage = "Логин занят";
                return response;
            }

            bool isAdmin = request.AuthToken == "admin123" ? true : false;
            var associate = new AssociateDomain()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName
            };
            string roleSystemName = isAdmin ? "admin" : "user";
            var role = roleRepository.Get().Where(x => x.RoleSystemName == roleSystemName).FirstOrDefault();
            if (role == null)
            {
                throw new NullReferenceException("missing role");
            }
            associate = associateConverter.ConvertToDomain(associateRepository.Create(associate));
            if (associate == null)
            {
                throw new NullReferenceException("associate is null");
            }
            var associateRole = new AssociateRoleDomain()
            {
                AssociateId = associate.Id,
                RoleId = role.Id
            };
            var login = new AssociateLoginDomain()
            {
                AssociateId = associate.Id,
                Login = request.Login,
                PasswordHash = getPasswordHash(request.Password)
            };
            associateRole = assoRoleConverter.ConvertToDomain(associateRoleRepository.Create(associateRole));
            if (associateRole == null)
            {
                throw new NullReferenceException("associate-role is null");
            }
            login = assoLoginConverter.ConvertToDomain(associateLoginRepository.Create(login));
            if (login == null)
            {
                throw new NullReferenceException("login is null");
            }

            response.Associate = associate;
            response.Role = roleConverter.ConvertToDomain(role);
            response.IsSuccess = true;
            response.ResponseMessage = "Вы успешно зарегистрированы";
            return response;
        }
        
        private string getPasswordHash(string pass)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(pass))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        private byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}
