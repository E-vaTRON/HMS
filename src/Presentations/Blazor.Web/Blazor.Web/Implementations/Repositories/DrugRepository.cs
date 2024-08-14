namespace Blazor.Web;

public class DrugRepository : ApplicationBaseRepository<Drug>, IDrugRepository
{
    #region [ Fields ]

    #endregion

    #region [ CTors ]

    public DrugRepository(ApplicationDbContext context) : base(context) { }
    #endregion

    #region [ Methods ]
    #endregion

}