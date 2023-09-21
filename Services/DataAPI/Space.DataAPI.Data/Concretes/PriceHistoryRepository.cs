using Space.Data.Concretes;
using Space.DataAPI.Data.Entities;
using Space.DataAPI.Data.Interfaces;

namespace Space.DataAPI.Data.Concretes
{
    public class PriceHistoryRepository : EntityFrameworkRepository<PriceHistory, DataContext>, IPriceHistoryRepository
    {

    }
}