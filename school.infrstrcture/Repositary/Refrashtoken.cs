using Microsoft.EntityFrameworkCore;
using school.Data.Entites.identity;
using school.infrstrcture.Abstract;
using school.infrstrcture.Baseinfrstcture;
using school.infrstrcture.Data;

namespace school.infrstrcture.Repositary
{

    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IrefrashToken
    {
        #region Fields
        private DbSet<UserRefreshToken> userRefreshToken;
        #endregion

        #region Constructors
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            userRefreshToken = dbContext.Set<UserRefreshToken>();
        }
        #endregion

        #region Handle Functions

        #endregion
    }

}
