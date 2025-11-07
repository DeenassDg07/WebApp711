using Microsoft.Win32;
using MyMediator.Interfaces;
using MyMediator.Types;
using WebApp711.DB;
using WebApp711.DTO;

namespace WebApp711.sqrs
{
    public class AuthCommand : IRequest<>
    {
        public AuthDTO DTO { get; set; }
        public class AuthCommandHandler :
            IRequestHandler<AuthCommand, IEnumerable<AuthDTO>>
        {

            private readonly ItCompany1135Context db;
            public AuthCommandHandler(ItCompany1135Context db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<AuthDTO>> HandleAsync(AuthCommand request,
                CancellationToken ct = default)
            {

              
            }

        }
    }
}