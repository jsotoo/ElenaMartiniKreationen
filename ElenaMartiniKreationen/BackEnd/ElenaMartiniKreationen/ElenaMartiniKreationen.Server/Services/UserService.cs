using ElenaMartiniKreationen.DataAccess;
using ElenaMartiniKreationen.Server.Request;
using ElenaMartiniKreationen.Server.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security;
using System.Text;
using ElenaMartiniKreationen.Server.Configuration;
using ElenaMartiniKreationen.Repositories.Interfaces;

namespace ElenaMartiniKreationen.Server.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUserElenaMartiniKreationen> _userManager;
        private readonly ILogger<UserService> _logger;
        private readonly IOptions<AppSettings> _options;
        private readonly IUserProfileRepository _repositoryUserProfile;

        public UserService(UserManager<IdentityUserElenaMartiniKreationen> userManager, ILogger<UserService> logger, IOptions<AppSettings> options, IUserProfileRepository clientRepository)
        {
            _userManager = userManager;
            _logger = logger;
            _options = options;
            _repositoryUserProfile = clientRepository;
        }


        public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
        {
            var response = new LoginDtoResponse();

            try
            {
                var identity = await _userManager.FindByNameAsync(request.User);

                if (identity is null)
                    throw new SecurityException("Usuario no existe");

                if (!await _userManager.CheckPasswordAsync(identity, request.Password))
                {
                    throw new SecurityException($"Clave incorrecta para el usuario {identity.UserName}");
                }

                var roles = await _userManager.GetRolesAsync(identity);

                var fechaExpiracion = DateTime.Now.AddDays(1);

                // Vamos a crear los claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, identity.FullName),
                    new Claim(ClaimTypes.Email, identity.Email!),
                    new Claim(ClaimTypes.Expiration, fechaExpiracion.ToLongDateString()),
                };

                claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
                response.Roles = roles;

                // Creamos el JWT
                var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.SecretKey));
                var credenciales = new SigningCredentials(llaveSimetrica, SecurityAlgorithms.HmacSha256);

                var header = new JwtHeader(credenciales);

                var payload = new JwtPayload(
                    issuer: _options.Value.Jwt.Transmitter,
                    audience: _options.Value.Jwt.Audience,
                    notBefore: DateTime.Now,
                    claims: claims,
                    expires: fechaExpiracion);

                var token = new JwtSecurityToken(header, payload);
                response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                response.FullName = identity.FullName;
                response.Success = true;


                _logger.LogInformation("Se creó el JWT de forma satisfactoria");
            }
            catch (SecurityException ex)
            {
                response.ErrorMessage = ex.Message;
                _logger.LogError(ex, "Error de seguridad {Message}", ex.Message);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error de autenticacion";
                _logger.LogCritical(ex, "Error al autenticar {Message}", ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse> RegisterAsync(RegisterUserDto request)
        {
            var response = new BaseResponse();
            try
            {
                var user = new IdentityUserElenaMartiniKreationen()
                {
                    FullName = request.FullName,
                    Address = request.Address,
                    PhoneNumber = request.Phone,
                    UserName = request.UserName,
                    Email = request.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Constants.RolClient);

                    var cli = new Entities.UserProfile
                    {
                        FirstName = request.FullName.Split(" ", StringSplitOptions.RemoveEmptyEntries).First(),
                        LastName = request.FullName.Split(" ", StringSplitOptions.RemoveEmptyEntries).Last(),
                        Address = request.Address,
                        Phone = request.Phone,
                        RegistrationDate = DateTime.Now,
                        Email = request.Email,
                        UserTypeId = 1
                    };


                    await _repositoryUserProfile.AddAsync(cli);
                }
                else
                {
                    var sb = new StringBuilder();
                    foreach (var identityError in result.Errors)
                    {
                        sb.AppendFormat("{0} ", identityError.Description);
                    }

                    response.ErrorMessage = sb.ToString();
                    sb.Clear();
                }

                response.Success = result.Succeeded;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al registrar";
                _logger.LogWarning(ex, "{MensajeError} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;

        }
    }
 }

